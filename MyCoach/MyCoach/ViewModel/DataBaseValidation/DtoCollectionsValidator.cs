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
