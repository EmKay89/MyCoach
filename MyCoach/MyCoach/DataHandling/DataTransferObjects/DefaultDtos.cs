using MyCoach.Defines;
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
            get => new ObservableCollection<Category>() { new Category { ID = ExerciseCategory.Category1, Name = "Arme", Count = 0, Active = true, Type = ExerciseType.Training } };
        }

        public static ObservableCollection<Exercise> Exercises
        {
            get => new ObservableCollection<Exercise>();
        }

        public static ObservableCollection<Settings> Settings
        {
            get => new ObservableCollection<Settings>
            {
                new Settings
                {
                    Permission = ExerciseSchedulingRepetitionPermission.NotPreferred,
                    RepeatsRound1 = 100,
                    RepeatsRound2 = 75,
                    RepeatsRound3 = 60,
                    RepeatsRound4 = 50,
                    ScoresRound1 = 100,
                    ScoresRound2 = 100,
                    ScoresRound3 = 100,
                    ScoresRound4 = 100
                }
            };
        }

        public static ObservableCollection<TrainingSchedule> TrainingSchedules
        {
            get => new ObservableCollection<TrainingSchedule>();
        }

        public static ObservableCollection<Month> TrainingScores
        {
            get => new ObservableCollection<Month>();
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
