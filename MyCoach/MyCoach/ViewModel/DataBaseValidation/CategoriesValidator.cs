using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.DataBaseValidation
{
    public static class CategoriesValidator
    {
        public static void Validate()
        {
            var categories = DataInterface.GetInstance().GetDataTransferObjects<Category>();

            if (categories == null)
            {
                categories = new ObservableCollection<Category>();
            }

            foreach (var category in Enum.GetValues(typeof(ExerciseCategory)).Cast<ExerciseCategory>())
            {
                if (categories.Any(c => c.ID == category) == false)
                {
                    categories.Add(new Category { ID = category });
                }

                var dublicates = categories.Where(c => c.ID == category).Skip(1);

                foreach (var dublicate in dublicates)
                {
                    categories.Remove(dublicate);
                }
            }

            DataInterface.GetInstance().SetDataTransferObjects(categories);
        }
    }
}
