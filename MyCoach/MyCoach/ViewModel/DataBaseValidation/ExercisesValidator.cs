using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.DataBaseValidation
{
    public static class ExercisesValidator
    {
        public static void Validate()
        {
            var exercises = DataInterface.GetInstance().GetDataTransferObjects<Exercise>();

            if (exercises == null)
            {
                exercises = new ObservableCollection<Exercise>();
            }

            DataInterface.GetInstance().SetDataTransferObjects(exercises);
        }
    }
}
