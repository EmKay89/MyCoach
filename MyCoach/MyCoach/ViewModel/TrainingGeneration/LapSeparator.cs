namespace MyCoach.ViewModel.TrainingGeneration
{
    /// <summary>
    /// This class is used to indicate lap transitions within a <see cref="Training"/>.
    /// </summary>
    public class LapSeparator : ITrainingElement
    {
        public bool Completed => true;
        public string Headline;
    }
}
