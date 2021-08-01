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
            get => new ObservableCollection<Category>() 
            {
                new Category { ID = ExerciseCategory.WarmUp, Name = "Aufwärmübungen", Count = 3, Active = true, Type = ExerciseType.WarmUp },
                new Category { ID = ExerciseCategory.Category1, Name = "Arme", Count = 0, Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category2, Name = "Bauch", Count = 0, Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category3, Name = "Seiten", Count = 0, Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category4, Name = "Rücken", Count = 0, Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category5, Name = "Beine", Count = 0, Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category6, Name = "", Count = 0, Active = false, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category7, Name = "", Count = 0, Active = false, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category8, Name = "", Count = 0, Active = false, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.CoolDown, Name = "Dehnübungen", Count = 3, Active = true, Type = ExerciseType.CoolDown }
            };
        }

        public static ObservableCollection<Exercise> Exercises
        {
            get => new ObservableCollection<Exercise>()
            {
                new Exercise 
                { 
                    ID = 0, 
                    Category = ExerciseCategory.WarmUp, 
                    Count = 10,
                    Name = "Hampelmann",
                    Unit = "Wiederholungen",
                    RelatedCategory = ExerciseCategory.Category5, 
                    Scores = 0, 
                    Info = "",
                    Active = true
                },

                new Exercise
                {
                    ID = 1,
                    Category = ExerciseCategory.Category1,
                    Count = 20, 
                    Name = "Hantelheben 5 kg",
                    Unit = "Wiederholungen",
                    RelatedCategory = ExerciseCategory.Category1,
                    Scores = 10,
                    Info = "",
                    Active = true
                },

                new Exercise
                {
                    ID = 2,
                    Category = ExerciseCategory.Category2,
                    Count = 30,
                    Name = "Situps",
                    Unit = "Wiederholungen",
                    RelatedCategory = ExerciseCategory.Category2,
                    Scores = 10,
                    Info = "",
                    Active = true
                },

                new Exercise
                {
                    ID = 3,
                    Category = ExerciseCategory.Category3,
                    Count = 15,
                    Name = "Seitstütze",
                    Unit = "Wiederholungen",
                    RelatedCategory = ExerciseCategory.Category3,
                    Scores = 10,
                    Info = "",
                    Active = true
                },
            };
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
            get => new ObservableCollection<TrainingSchedule>() 
            {
                new TrainingSchedule
                {
                    ScheduleType = ScheduleType.Generic,
                    StartMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                    Duration = 1
                }
            };
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
