using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using System.Collections.Generic;
using System.Linq;

namespace MyCoach.ViewModel.Utilities
{
    /// <summary>
    ///     This static class can be used to check, if there are enought exercises available to create a training
    ///     with given <see cref="TrainingSettings"/>.
    /// </summary>
    public static class ExerciseAvailabilityChecker
    {
        public static bool Check(TrainingSettings settings)
        {
            var permission = DataInterface.GetInstance().GetData<Settings>().Single().Permission;
            var exercises = DataInterface.GetInstance().GetData<Exercise>().Where(e => e.Active).ToList();

            switch (settings.TrainingMode)
            {
                case TrainingMode.CircleTraining:
                    return AreEnoughExercisesAvailableForCircleTraining(permission, exercises, settings);
                case TrainingMode.RandomTraining:
                    return AreEnoughExercisesAvailableForRandomTraining(permission, exercises, settings);
                case TrainingMode.FocusTraining:
                    return AreEnoughExercisesAvailableForFocusTraining(permission, exercises, settings);
                case TrainingMode.UserDefinedTraining:
                default:
                    return true;
            }
        }

        private static bool AreEnoughExercisesAvailableForCircleTraining(
            ExerciseSchedulingRepetitionPermission permission,
            List<Exercise> exercises,
            TrainingSettings settings)
        {
            foreach (var category in settings.CategoriesEnabledForTraining)
            {
                int compare;

                if (category == ExerciseCategory.WarmUp || category == ExerciseCategory.CoolDown)
                {
                    compare = DataInterface.GetInstance().GetData<Category>().Single(c => c.ID == category).Count;
                }
                else if (permission == ExerciseSchedulingRepetitionPermission.No)
                {
                    compare = settings.LapCount;
                }
                else
                {
                    compare = 1;
                }

                if (exercises.Count(e => e.Category == category) < compare)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool AreEnoughExercisesAvailableForRandomTraining(
            ExerciseSchedulingRepetitionPermission permission,
            List<Exercise> exercises,
            TrainingSettings settings)
        {
            var activeCategories = settings.CategoriesEnabledForTraining;
            activeCategories.Remove(ExerciseCategory.WarmUp);
            activeCategories.Remove(ExerciseCategory.CoolDown);
            var exercisesOfActiveCategories = exercises.Where(e => activeCategories.Any(c => c == e.Category)).ToList();

            return permission == ExerciseSchedulingRepetitionPermission.No
                ? exercisesOfActiveCategories.Count >= settings.LapCount * settings.ExercisesPerLap
                : exercisesOfActiveCategories.Any();
        }

        private static bool AreEnoughExercisesAvailableForFocusTraining(
            ExerciseSchedulingRepetitionPermission permission,
            List<Exercise> exercises,
            TrainingSettings settings)
        {
            if (settings.CategoryInFocus == null)
            {
                return true;
            }

            return permission == ExerciseSchedulingRepetitionPermission.No
                ? exercises.Count(e => e.Category == settings.CategoryInFocus) >= settings.LapCount * settings.ExercisesPerLap
                : exercises.Any(e => e.Category == settings.CategoryInFocus);
        }
    }
}
