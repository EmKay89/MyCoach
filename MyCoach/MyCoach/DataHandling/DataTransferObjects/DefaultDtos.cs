using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static ObservableCollection<Category> Categories
        {
            get => new ObservableCollection<Category>() { new Category { ID = 1, Name = "Arme", Count = 0, Active = true } };
        }

        public static ObservableCollection<Exercise> Exercises
        {
            get => new ObservableCollection<Exercise>();
        }

        public static ObservableCollection<Settings> Settings
        {
            get => new ObservableCollection<Settings>();
        }

        public static ObservableCollection<TrainingSchedule> TrainingSchedules
        {
            get => new ObservableCollection<TrainingSchedule>();
        }

        public static ObservableCollection<TrainingScore> TrainingScores
        {
            get => new ObservableCollection<TrainingScore>();
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
