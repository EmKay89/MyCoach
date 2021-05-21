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
            var exercises = DataInterface.GetInstance().GetData<Exercise>();

            if (exercises == null)
            {
                exercises = new ObservableCollection<Exercise>();
            }

            RemoveDoublicates(exercises);

            DataInterface.GetInstance().SaveData<Exercise>();
        }

        private static void RemoveDoublicates(ObservableCollection<Exercise> exercises)
        {
            List<Exercise> exercisesWithDoublicateIds = new List<Exercise>();

            foreach (var exercise in exercises)
            {
                if (exercisesWithDoublicateIds.Contains(exercise))
                {
                    continue;
                }

                var newDoublicates = exercises.Where(e => e.ID == exercise.ID 
                    && exercisesWithDoublicateIds.Contains(e) == false
                    && e.GetHashCode() != exercise.GetHashCode());
                exercisesWithDoublicateIds.AddRange(newDoublicates);
            }

            foreach (var exercise in exercisesWithDoublicateIds)
            {
                exercises.Remove(exercise);
            }
        }
    }
}
