using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using MyExtensions.IEnumerable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.TrainingGeneration
{
    public static class TrainingGenerator
    {
        public static Training CreateTraining(TrainingSettings trainingSettings)
        {
            var globalSettings = DataInterface.GetInstance().GetData<Settings>().FirstOrDefault();

            switch (trainingSettings.TrainingMode)
            {
                case Defines.TrainingMode.CircleTraining:
                    return CreateCircleTraining(globalSettings, trainingSettings);
                case Defines.TrainingMode.FocusTraining:
                    return CreateFocusTraining(globalSettings, trainingSettings);
                case Defines.TrainingMode.UserDefinedTraining:
                default:
                    return new Training();
            }
        }

        private static Training CreateCircleTraining(Settings globalSettings, TrainingSettings trainingSettings)
        {
            throw new NotImplementedException();
        }

        private static Training CreateFocusTraining(Settings globalSettings, TrainingSettings trainingSettings)
        {
            throw new NotImplementedException();
        }

        private static List<Exercise> RefreshPool(ExerciseCategory category)
        {
            var list = new List<Exercise>();
            list.AddRange(DataInterface.GetInstance().GetData<Exercise>().Where(e => e.Category == category));
            return list;
        }

        private static Exercise GetExerciseFromPool(
            List<Exercise> pool, 
            ExerciseCategory category, 
            ExerciseSchedulingRepetitionPermission repetitionPermission)
        {
            if (pool.Any() == false 
                && repetitionPermission == ExerciseSchedulingRepetitionPermission.NotPreferred)
            {
                pool = RefreshPool(category);
            }

            if (pool.Any() == false)
            {
                return null;
            }
            
            switch (repetitionPermission)
            {
                case ExerciseSchedulingRepetitionPermission.Yes:
                    return pool.GetRandom();

                case ExerciseSchedulingRepetitionPermission.NotPreferred:
                case ExerciseSchedulingRepetitionPermission.No:
                    var item = pool.GetRandom();
                    pool.Remove(item);
                    return item;

                default:
                    return null;
            }
        }

        public static double DetermineMultiplierForRound(
            int round, 
            Settings globalSettings)
        {
            switch (round)
            {
                case 1:
                    return globalSettings.ScoresRound1 / 100;
                case 2:
                    return globalSettings.ScoresRound2 / 100;
                case 3:
                    return globalSettings.ScoresRound3 / 100;
                case 4:
                    return globalSettings.ScoresRound4 / 100;
                default:
                    return 1.0;                    
            }
        }
    }
}
