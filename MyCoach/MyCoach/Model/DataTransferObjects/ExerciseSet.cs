using System.Collections.ObjectModel;

namespace MyCoach.Model.DataTransferObjects
{
    /// <summary>
    ///     Übungssatz, fasst alle Übungen und Übungskategorien zusammen.
    /// </summary>
    public class ExerciseSet
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Exercise> Exercises { get; set; }
    }
}
