using MyCoach.DataHandling.DataTransferObjects;
using System.Collections.Generic;

namespace MyCoach.DataHandling.DataTransferObjects
{
    /// <summary>
    ///     Fasst Listen aller DataTransferObject Typen zu einem Objekt zusammen.
    /// </summary>
    public class DtoCollection
    {
        public List<Category> Categories { get; set; }
        public List<Exercise> Exercises { get; set; }
        public List<Settings> Settings { get; set; }
        public List<TrainingSchedule> TrainingSchedules { get; set; }
        public List<TrainingScore> TrainingScores { get; set; }
    }
}
