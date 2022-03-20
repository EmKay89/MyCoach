using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyExtensions.IEnumerable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.TrainingGenerationAndEvaluation
{
    public static class TrainingGenerator
    {
        private static readonly Dictionary<ExerciseCategory, List<Exercise>> pool
            = new Dictionary<ExerciseCategory, List<Exercise>>();
        private static TrainingSettings trainingSettings;
        private static Settings globalSettings;

        /// <summary>
        ///     Creates a new <see cref="Training"/> based on a given object of type <see cref="TrainingSettings"/>,
        ///     saved exercises and global settings.
        /// </summary>
        /// <param name="trainingSettings">The given <see cref="TrainingSettings"/>.</param>
        /// <returns>
        ///     A new object of type <see cref="Training"/>, that may be empty if no exercises were found to match 
        ///     the settings for training generation.
        /// </returns>
        public static Training CreateTraining(TrainingSettings trainingSettings)
        {
            // (Re-)initialize settings for training generation
            TrainingGenerator.trainingSettings = trainingSettings;
            globalSettings = DataInterface.GetInstance().GetData<Settings>().FirstOrDefault();
            RefreshPoolList();

            // Actually create the training
            switch (trainingSettings.TrainingMode)
            {
                case TrainingMode.CircleTraining:
                    return CreateCircleTraining();
                case TrainingMode.FocusTraining:
                    return CreateFocusTraining();
                case TrainingMode.UserDefinedTraining:
                default:
                    return null;
            }
        }

        private static Training CreateCircleTraining()
        {
            var training = new Training();

            AddWarmUpOrCoolDown(training, ExerciseCategory.WarmUp);
            for (int i = 0; i < trainingSettings.LapCount; i++)
            {
                AddCircleTrainingLap(training, i + 1);
            }

            AddWarmUpOrCoolDown(training, ExerciseCategory.CoolDown);

            return training;
        }

        private static Training CreateFocusTraining()
        {
            var training = new Training();

            for (int i = 0; i < trainingSettings.LapCount; i++)
            {
                AddFocusTrainingLap(training, i + 1);
            }

            return training;
        }

        private static void AddCircleTrainingLap(Training training, int lap)
        {
            var usedCategories = trainingSettings.CategoriesEnabledForTraining;
            var exercises = new List<Exercise>();

            foreach (var category in usedCategories)
            {
                if (category == ExerciseCategory.WarmUp || category == ExerciseCategory.CoolDown)
                {
                    continue;
                }

                var exercise = GetExerciseFromPool(category);

                if (exercise != null)
                {
                    exercises.Add(exercise);
                }
            }

            ConvertExercisesToTrainingLap(training, lap, exercises);
        }

        private static void AddWarmUpOrCoolDown(Training training, ExerciseCategory category)
        {
            var exercises = new List<Exercise>();
            var savedCategoryDto = DataInterface.GetInstance().GetData<Category>()
                .Where(c => c.ID == category).FirstOrDefault();

            if (savedCategoryDto == null
                || (category != ExerciseCategory.WarmUp && category != ExerciseCategory.CoolDown)
                || trainingSettings.CategoriesEnabledForTraining.Any(ec => ec == category) == false)
            {
                return;
            }

            for (int i = 0; i < savedCategoryDto.Count; i++)
            {
                var exercise = GetExerciseFromPool(category);

                if (exercise != null)
                {
                    exercises.Add(exercise);
                }
            }

            if (exercises.Any() == false)
            {
                return;
            }

            training.Add(new TrainingElementViewModel(TrainingElementType.lapSeparator, null) 
            {
                LapHeadline = savedCategoryDto.Name 
            });

            foreach (var exercise in exercises)
            {
                training.Add(new TrainingElementViewModel(TrainingElementType.exercise, exercise));
            }
        }

        private static void AddFocusTrainingLap(Training training, int lap)
        {
            var exercises = new List<Exercise>();

            for (int i = 0; i < trainingSettings.ExercisesPerLap; i++)
            {
                var exercise = GetExerciseFromPool(trainingSettings.CategoryInFocus);

                if (exercise != null)
                {
                    exercises.Add(exercise);
                }
            }

            ConvertExercisesToTrainingLap(training, lap, exercises);
        }

        private static void ConvertExercisesToTrainingLap(Training training, int lap, List<Exercise> exercises)
        {
            if (exercises.Any() == false)
            {
                return;
            }

            training.Add(new TrainingElementViewModel (TrainingElementType.lapSeparator, null) 
            { 
                LapHeadline = TrainingElementViewModel.LAPDESIGNATION + " " + lap.ToString() 
            });

            foreach (var exercise in exercises)
            {
                var vm = new TrainingElementViewModel(TrainingElementType.exercise, exercise)
                {
                    RepeatsMultiplier = DetermineRepeatsMultiplierForLap(lap),
                    ScoresMultiplier = DetermineScoresMultiplierForLap(lap)                    
                };

                training.Add(vm);
            }
        }

        private static double DetermineRepeatsMultiplierForLap(int lap)
        {
            switch (lap)
            {
                case 1:
                    return (double)globalSettings.RepeatsRound1 / (double)100;
                case 2:
                    return (double)globalSettings.RepeatsRound2 / (double)100;
                case 3:
                    return (double)globalSettings.RepeatsRound3 / (double)100;
                case 4:
                    return (double)globalSettings.RepeatsRound4 / (double)100;
                default:
                    return 1.0;
            }
        }

        private static double DetermineScoresMultiplierForLap(int lap)
        {
            switch (lap)
            {
                case 1:
                    return (double)globalSettings.ScoresRound1 / (double)100;
                case 2:
                    return (double)globalSettings.ScoresRound2 / (double)100;
                case 3:
                    return (double)globalSettings.ScoresRound3 / (double)100;
                case 4:
                    return (double)globalSettings.ScoresRound4 / (double)100;
                default:
                    return 1.0;
            }
        }

        private static void RefreshPoolList()
        {
            pool.Clear();

            foreach (var category in (ExerciseCategory[])Enum.GetValues(typeof(ExerciseCategory)))
            {
                var subPool = RefreshSubPool(category);
                pool.Add(category, subPool);
            }
        }

        private static Exercise GetExerciseFromPool(ExerciseCategory category)
        {
            if (pool.TryGetValue(category, out var subPool) == false)
            {
                return null;
            }

            if (subPool.Any() == false
                && globalSettings.Permission == ExerciseSchedulingRepetitionPermission.NotPreferred)
            {
                subPool = RefreshSubPool(category);
            }

            if (subPool.Any() == false)
            {
                return null;
            }

            switch (globalSettings.Permission)
            {
                case ExerciseSchedulingRepetitionPermission.Yes:
                    return subPool.GetRandom();
                case ExerciseSchedulingRepetitionPermission.NotPreferred:
                case ExerciseSchedulingRepetitionPermission.No:
                    var item = subPool.GetRandom();
                    subPool.Remove(item);
                    return item;
                default:
                    return null;
            }
        }

        private static List<Exercise> RefreshSubPool(ExerciseCategory category)
        {
            return DataInterface.GetInstance().GetData<Exercise>().Where(e => e.Category == category && e.Active).ToList();
        }
    }
}
