using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyCoach.Model.DataTransferObjects
{
    /// <summary>
    ///     Fasst Listen aller DataTransferObject Typen zu einem Objekt zusammen.
    /// </summary>
    public class DtoCollection
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Exercise> Exercises { get; set; }
        public ObservableCollection<Settings> Settings { get; set; }
        public ObservableCollection<TrainingSchedule> TrainingSchedules { get; set; }
        public ObservableCollection<Month> TrainingScores { get; set; }
    }
}
