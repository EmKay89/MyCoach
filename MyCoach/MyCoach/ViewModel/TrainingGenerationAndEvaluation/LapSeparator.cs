namespace MyCoach.ViewModel.TrainingGenerationAndEvaluation
{
    /// <summary>
    /// This class is used to indicate lap transitions within a <see cref="Training"/>.
    /// </summary>
    public class LapSeparator : ITrainingElement
    {
        public const string LapName = "Runde";
        public bool Completed => true;
        public string Headline;        
    }
}
