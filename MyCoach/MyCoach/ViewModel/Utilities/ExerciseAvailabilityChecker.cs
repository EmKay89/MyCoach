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
                int compare = (category == ExerciseCategory.WarmUp || category == ExerciseCategory.CoolDown) && permission == ExerciseSchedulingRepetitionPermission.No
                    ? DataInterface.GetInstance().GetData<Category>().Single(c => c.ID == category).Count
                    : permission == ExerciseSchedulingRepetitionPermission.No ? settings.LapCount : 1;

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
            var warmUpCategoryCount = DataInterface.GetInstance().GetData<Category>().Single(c => c.ID == ExerciseCategory.WarmUp).Count;
            var coolDownCategoryCount = DataInterface.GetInstance().GetData<Category>().Single(c => c.ID == ExerciseCategory.CoolDown).Count;

            if (permission == ExerciseSchedulingRepetitionPermission.No
                && activeCategories.Contains(ExerciseCategory.WarmUp)
                && exercises.Count(e => e.Category == ExerciseCategory.WarmUp) < warmUpCategoryCount
                || (activeCategories.Contains(ExerciseCategory.CoolDown) 
                && exercises.Count(e => e.Category == ExerciseCategory.CoolDown) < coolDownCategoryCount))
            {
                return false;
            }

            if ((activeCategories.Contains(ExerciseCategory.WarmUp) && exercises.Any(e => e.Category == ExerciseCategory.WarmUp) == false)
                || (activeCategories.Contains(ExerciseCategory.CoolDown) && exercises.Any(e => e.Category == ExerciseCategory.CoolDown) == false))
            {
                return false;
            }
            
            activeCategories.Remove(ExerciseCategory.WarmUp);
            activeCategories.Remove(ExerciseCategory.CoolDown);

            if (activeCategories.Count == 0)
            {
                return true;
            }
            
            var trainingExercisesOfActiveCategories = exercises.Where(e => activeCategories.Any(c => c == e.Category)).ToList();

            return permission == ExerciseSchedulingRepetitionPermission.No
                ? trainingExercisesOfActiveCategories.Count >= settings.LapCount * settings.ExercisesPerLap
                : trainingExercisesOfActiveCategories.Any();
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
