using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.DataHandling.DataTransferObjects
{
    /// <summary>
    ///     Stellt Listen aller Dtos mit Standardwerten für jede Datanstruktur zur Verfügung.
    /// </summary>
    public static class DefaultDtos
    {
        public static List<Category> Categories
        {
            get => new List<Category>();
        }

        public static List<Exercise> Exercises
        {
            get => new List<Exercise>();
        }

        public static List<Settings> Settings
        {
            get => new List<Settings>();
        }

        public static List<TrainingSchedule> TrainingSchedules
        {
            get => new List<TrainingSchedule>();
        }

        public static List<TrainingScore> TrainingScores
        {
            get => new List<TrainingScore>();
        }

        public static DtoCollection Collection
        {
            get
            {
                return new DtoCollection
                {
                    Categories = Categories,
                    Exercises = Exercises,
                    Settings = Settings,
                    TrainingSchedules = TrainingSchedules,
                    TrainingScores = TrainingScores
                };
            }
        }
    }
}
