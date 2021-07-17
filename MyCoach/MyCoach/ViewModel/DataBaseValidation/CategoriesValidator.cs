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
            var categories = DataInterface.GetInstance().GetData<Category>();

            if (categories == null)
            {
                categories = DefaultDtos.Categories;
            }

            foreach (var category in Enum.GetValues(typeof(ExerciseCategory)).Cast<ExerciseCategory>())
            {
                if (categories.Any(c => c.ID == category) == false)
                {
                    categories.Add(new Category { ID = category, Type = GetTypeFromCategory(category) });
                }

                var dublicates = categories.Where(c => c.ID == category).Skip(1);

                foreach (var dublicate in dublicates)
                {
                    categories.Remove(dublicate);
                }
            }

            DataInterface.GetInstance().SaveData<Category>();
        }

        private static ExerciseType GetTypeFromCategory(ExerciseCategory category)
        {
            switch (category)
            {
                case ExerciseCategory.WarmUp:
                    return ExerciseType.WarmUp;
                case ExerciseCategory.CoolDown:
                    return ExerciseType.CoolDown;
                default:
                    return ExerciseType.Training;
            }
        }
    }
}
