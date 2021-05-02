using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.DataBaseValidation
{
    public static class DtoCollectionsValidator
    {
        public static void ValidateAll()
        {
            CategoriesValidator.Validate();
            ExercisesValidator.Validate();
            SettingsValidator.Validate();
            MonthsValidator.Validate();
            TrainingScheduleValidator.Validate();
        }
    }
}
