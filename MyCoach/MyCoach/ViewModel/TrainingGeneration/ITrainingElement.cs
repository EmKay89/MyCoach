namespace MyCoach.ViewModel.TrainingGeneration
{
    /// <summary>
    /// This interface can be used to mark types as possible items to add to a <see cref="Training"/>.
    /// </summary>
    public interface ITrainingElement
    {
        bool Completed { get; }
    }
}