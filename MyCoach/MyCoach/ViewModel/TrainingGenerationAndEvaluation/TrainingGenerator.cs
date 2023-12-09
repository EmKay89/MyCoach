using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.Helpers.Extensions.IEnumerable;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    return CreateTraining(true, AddCircleTrainingLap);
                case TrainingMode.RandomTraining:
                    return CreateTraining(true, AddRandomTrainingLap);
                case TrainingMode.FocusTraining:
                    return CreateTraining(false, AddFocusTrainingLap);
                case TrainingMode.UserDefinedTraining:
                default:
                    return null;
            }
        }

        private static Training CreateTraining(bool addWarmUpAndCoolDown, Action<Training, int> lapCreationAction)
        {
            var training = new Training();

            if (addWarmUpAndCoolDown)
            {
                AddWarmUpOrCoolDown(training, ExerciseCategory.WarmUp);
            }

            for (int i = 0; i < trainingSettings.LapCount; i++)
            {
                lapCreationAction(training, i + 1);
            }

            if (addWarmUpAndCoolDown)
            {
                AddWarmUpOrCoolDown(training, ExerciseCategory.CoolDown);
            }

            return training;
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

            training.Add(new TrainingElementViewModel(TrainingElementType.Headline, null)
            {
                Headline = savedCategoryDto.Name
            });

            foreach (var exercise in exercises)
            {
                training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise));
            }
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

        private static void AddRandomTrainingLap(Training training, int lap)
        {
            var usedCategories = trainingSettings.CategoriesEnabledForTraining;
            var exercises = new List<Exercise>();

            for (int i = 0; i < trainingSettings.ExercisesPerLap; i++)
            {
                var exercise = GetExerciseFromPool(usedCategories, ExerciseCategory.WarmUp, ExerciseCategory.CoolDown);

                if (exercise != null)
                {
                    exercises.Add(exercise);
                }
            }

            ConvertExercisesToTrainingLap(training, lap, exercises);
        }

        private static void AddFocusTrainingLap(Training training, int lap)
        {
            var exercises = new List<Exercise>();

            if (!(trainingSettings.CategoryInFocus is ExerciseCategory categoryInFocus))
            {
                return;
            }

            for (int i = 0; i < trainingSettings.ExercisesPerLap; i++)
            {
                var exercise = GetExerciseFromPool(categoryInFocus);

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

            training.Add(new TrainingElementViewModel (TrainingElementType.Headline, null) 
            {
                Headline = TrainingElementViewModel.LAPDESIGNATION + " " + lap.ToString() 
            });

            foreach (var exercise in exercises)
            {
                var vm = new TrainingElementViewModel(TrainingElementType.Exercise, exercise)
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
                    return (double)globalSettings.RepeatsRound1 * (double)globalSettings.RepeatsAndScoresMultiplier / (double)10000;
                case 2:
                    return (double)globalSettings.RepeatsRound2 * (double)globalSettings.RepeatsAndScoresMultiplier / (double)10000;
                case 3:
                    return (double)globalSettings.RepeatsRound3 * (double)globalSettings.RepeatsAndScoresMultiplier / (double)10000;
                case 4:
                    return (double)globalSettings.RepeatsRound4 * (double)globalSettings.RepeatsAndScoresMultiplier / (double)10000;
                default:
                    return 1.0;
            }
        }

        private static double DetermineScoresMultiplierForLap(int lap)
        {
            switch (lap)
            {
                case 1:
                    return (double)globalSettings.ScoresRound1 * (double)globalSettings.RepeatsAndScoresMultiplier / (double)10000;
                case 2:
                    return (double)globalSettings.ScoresRound2 * (double)globalSettings.RepeatsAndScoresMultiplier / (double)10000;
                case 3:
                    return (double)globalSettings.ScoresRound3 * (double)globalSettings.RepeatsAndScoresMultiplier / (double)10000;
                case 4:
                    return (double)globalSettings.ScoresRound4 * (double)globalSettings.RepeatsAndScoresMultiplier / (double)10000;
                default:
                    return 1.0;
            }
        }

        /// <summary>
        ///     Gets a random exercise of the given <see cref="ExerciseCategory"/> from the pool.
        ///     The exercise will be removed from the pool unless the <see cref="Settings.Permission"/>
        ///     of the <see cref="globalSettings"/> is set to <see cref="ExerciseSchedulingRepetitionPermission.Yes"/>.
        /// </summary>
        /// <param name="category">The given <see cref="ExerciseCategory"/>.</param>
        /// <returns>The randomly choosen exercise or null, if no exercise is available.</returns>
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

        /// <summary>
        ///     Gets a random exercise from the pool whose category is in the list of categories to include,
        ///     but not in the list of categories to exclude.
        ///     The exercise will be removed from the pool unless the <see cref="Settings.Permission"/>
        ///     of the <see cref="globalSettings"/> is set to <see cref="ExerciseSchedulingRepetitionPermission.Yes"/>.
        /// </summary>
        /// <param name="includes">The list of categories to include.</param>
        /// <param name="excludes">The list of categories to exclude.</param>
        /// <returns>The randomly choosen exercise or null, if no exercise is available.</returns>
        private static Exercise GetExerciseFromPool(List<ExerciseCategory> includes, params ExerciseCategory[] excludes)
        {
            List<Exercise> filteredExercises = GetFilteredPool(includes, excludes.ToList());

            if (filteredExercises.Any() == false
                && globalSettings.Permission == ExerciseSchedulingRepetitionPermission.NotPreferred)
            {
                RefreshPoolList();
            }

            filteredExercises = GetFilteredPool(includes, excludes.ToList());

            if (filteredExercises.Any() == false)
            {
                return null;
            }

            switch (globalSettings.Permission)
            {
                case ExerciseSchedulingRepetitionPermission.Yes:
                    return filteredExercises.GetRandom();
                case ExerciseSchedulingRepetitionPermission.NotPreferred:
                case ExerciseSchedulingRepetitionPermission.No:
                    var item = filteredExercises.GetRandom();
                    var parentSubPool = pool.Single(subPool => subPool.Value.Contains(item)).Value;
                    parentSubPool.Remove(item);
                    return item;
                default:
                    return null;
            }
        }

        private static List<Exercise> GetFilteredPool(List<ExerciseCategory> includes, List<ExerciseCategory> excludes)
        {
            var allExercises = pool.SelectMany(p => p.Value).ToList();
            var filteredExercises = allExercises.Where(
                e => includes.Contains((ExerciseCategory)e.Category) && excludes.Contains((ExerciseCategory)e.Category) == false).ToList();
            return filteredExercises;
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

        private static List<Exercise> RefreshSubPool(ExerciseCategory category)
        {
            return DataInterface.GetInstance().GetData<Exercise>().Where(e => e.Category == category && e.Active).ToList();
        }
    }
}
