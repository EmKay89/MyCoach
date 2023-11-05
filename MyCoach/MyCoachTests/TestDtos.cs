using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;

namespace MyCoachTests
{
    /// <summary>
    ///     Statische Klasse, die vorgefüllte Listen einzelner DataTransferObjects für Unittests zur Verfügung stellt.
    /// </summary>
    public static class TestDtos
    {
        private static ObservableCollection<Category> categories = CreateCategories();

        private static ObservableCollection<Exercise> exercices = CreateExercises();

        private static ObservableCollection<Settings> settings = CreateSettings();

        private static ObservableCollection<TrainingSchedule> schedules = CreateSchedules();

        private static ObservableCollection<Month> scores = CreateScores();

        private static readonly DtoCollection collection = new DtoCollection
        {
            Categories = Categories,
            Exercises = Exercises,
            Settings = Settings,
            TrainingSchedules = TrainingSchedules,
            TrainingScores = TrainingScores
        };

        public static ObservableCollection<Category> Categories => categories;

        public static ObservableCollection<Exercise> Exercises => exercices;

        public static ObservableCollection<Settings> Settings => settings;

        public static ObservableCollection<TrainingSchedule> TrainingSchedules => schedules;

        public static ObservableCollection<Month> TrainingScores => scores;

        public static DtoCollection Collection => collection;

        public static void Reset()
        {
            categories = CreateCategories();
            exercices = CreateExercises();
            settings = CreateSettings();
            schedules = CreateSchedules();
            scores = CreateScores();
        }

        private static ObservableCollection<Category> CreateCategories()
        {
            return new ObservableCollection<Category>
            {
                new Category { ID = ExerciseCategory.WarmUp, Name = "Erwärmung", Count = 4, Active = true, Type = ExerciseType.WarmUp },
                new Category { ID = ExerciseCategory.Category1, Name = "Bizeps", Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category2, Name = "Bauch", Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category3, Name = "Seiten", Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category4, Name = "Rücken", Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category5, Name = "", Active = false, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category6, Name = "Po", Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category7, Name = "Waden", Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category8, Name = "", Active = false, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.CoolDown, Name = "Dehnung", Count = 4, Active = false, Type = ExerciseType.CoolDown }
            };
        }

        private static ObservableCollection<Exercise> CreateExercises()
        {
            return new ObservableCollection<Exercise>
            {
                new Exercise
                {
                    ID = 0,
                    Category = ExerciseCategory.WarmUp,
                    Count = 10,
                    Name = "Armkreisen",
                    Unit = "Stück",
                    Scores = 0,
                    Info = "https://www.youtube.com/watch?v=Gl6g9CZTZL0",
                    Active = true
                },

                new Exercise
                {
                    ID = 1,
                    Category = ExerciseCategory.Category1,
                    Count = 10,
                    Name = "Hantelheben 10kg",
                    Unit = "Stück",
                    Scores = 10,
                    Info = "Hier könnte Ihre Testbeschreibung stehen ... ",
                    Active = true
                },

                new Exercise
                {
                    ID = 3,
                    Category = ExerciseCategory.Category1,
                    Count = 10,
                    Name = "Hantelheben 20kg",
                    Unit = "Stück",
                    Scores = 10,
                    Info = "Hier könnte Ihre Testbeschreibung stehen ... ",
                    Active = false
                },

                new Exercise
                {
                    ID = 4,
                    Category = ExerciseCategory.Category2,
                    Count = 10,
                    Name = "Situps",
                    Unit = "Stück",
                    Scores = 10,
                    Info = "Schwabbel, Schwabbel und Schwabbel!",
                    Active = true
                },

                new Exercise
                {
                    ID = 5,
                    Category = ExerciseCategory.CoolDown,
                    Count = 10,
                    Name = "Arme strecken",
                    Unit = "Stück",
                    Scores = 0,
                    Info = "Die Arme in die Höhe!",
                    Active = false
                }
            };
        }

        private static ObservableCollection<Settings> CreateSettings()
        {
            var settings = new ObservableCollection<Settings>
            {
                new Settings
                {
                    Permission = ExerciseSchedulingRepetitionPermission.No,
                    RepeatsRound1 = 101,
                    ScoresRound1 = 101,
                    RepeatsRound2 = 71,
                    ScoresRound2 = 101,
                    RepeatsRound3 = 51,
                    ScoresRound3 = 101,
                    RepeatsRound4 = 31,
                    ScoresRound4 = 101,
                    RepeatsAndScoresMultiplier = 100
                }
            };

            settings.Single().Units.Add("TestUnit");
            return settings;
        }

        private static ObservableCollection<TrainingSchedule> CreateSchedules()
        {
            return new ObservableCollection<TrainingSchedule>
            {
                new TrainingSchedule
                {
                    StartMonth = DateTime.MinValue.AddYears(1),
                    ScheduleType = ScheduleType.TimeBased,
                    Duration = 4
                }
            };
        }

        private static ObservableCollection<Month> CreateScores()
        {
            return new ObservableCollection<Month>
            {
                new Month
                {
                    Number = MonthNumber.Current,
                    StartDate = DateTime.MinValue.AddYears(1),
                    Category1Goal = 100,
                    Category1Scores = 50,
                    Category2Goal = 100,
                    Category2Scores = 50,
                    Category3Goal = 100,
                    Category3Scores = 50,
                    Category4Goal = 100,
                    Category4Scores = 50,
                    Category5Goal = 100,
                    Category5Scores = 50,
                    Category6Goal = 100,
                    Category6Scores = 50,
                    Category7Goal = 100,
                    Category7Scores = 50,
                    Category8Goal = 100,
                    Category8Scores = 50
                },

                new Month
                {
                    Number = MonthNumber.Month1,
                    StartDate = DateTime.MinValue.AddYears(1),
                    Category1Goal = 110,
                    Category1Scores = 70,
                    Category2Goal = 110,
                    Category2Scores = 70,
                    Category3Goal = 110,
                    Category3Scores = 70,
                    Category4Goal = 110,
                    Category4Scores = 70,
                    Category5Goal = 110,
                    Category5Scores = 70,
                    Category6Goal = 110,
                    Category6Scores = 70,
                    Category7Goal = 110,
                    Category7Scores = 70,
                    Category8Goal = 110,
                    Category8Scores = 70
                },

                new Month
                {
                    Number = MonthNumber.Month2,
                    StartDate = DateTime.MinValue.AddYears(1).AddMonths(1),
                    Category1Goal = 120,
                    Category1Scores = 0,
                    Category2Goal = 120,
                    Category2Scores = 0,
                    Category3Goal = 120,
                    Category3Scores = 0,
                    Category4Goal = 120,
                    Category4Scores = 0,
                    Category5Goal = 120,
                    Category5Scores = 0,
                    Category6Goal = 120,
                    Category6Scores = 0,
                    Category7Goal = 120,
                    Category7Scores = 0,
                    Category8Goal = 120,
                    Category8Scores = 0
                },

                new Month
                {
                    Number = MonthNumber.Month3,
                    StartDate = DateTime.MinValue.AddYears(1).AddMonths(2),
                    Category1Goal = 130,
                    Category1Scores = 0,
                    Category2Goal = 130,
                    Category2Scores = 0,
                    Category3Goal = 130,
                    Category3Scores = 0,
                    Category4Goal = 130,
                    Category4Scores = 0,
                    Category5Goal = 130,
                    Category5Scores = 0,
                    Category6Goal = 130,
                    Category6Scores = 0,
                    Category7Goal = 130,
                    Category7Scores = 0,
                    Category8Goal = 130,
                    Category8Scores = 0
                },

                new Month
                {
                    Number = MonthNumber.Month4,
                    StartDate = DateTime.MinValue.AddYears(1).AddMonths(3),
                    Category1Goal = 140,
                    Category1Scores = 0,
                    Category2Goal = 140,
                    Category2Scores = 0,
                    Category3Goal = 140,
                    Category3Scores = 0,
                    Category4Goal = 140,
                    Category4Scores = 0,
                    Category5Goal = 140,
                    Category5Scores = 0,
                    Category6Goal = 140,
                    Category6Scores = 0,
                    Category7Goal = 140,
                    Category7Scores = 0,
                    Category8Goal = 140,
                    Category8Scores = 0
                },

                new Month
                {
                    Number = MonthNumber.Month5,
                    StartDate = DateTime.MinValue.AddYears(1).AddMonths(4),
                    Category1Goal = 150,
                    Category1Scores = 0,
                    Category2Goal = 150,
                    Category2Scores = 0,
                    Category3Goal = 150,
                    Category3Scores = 0,
                    Category4Goal = 150,
                    Category4Scores = 0,
                    Category5Goal = 150,
                    Category5Scores = 0,
                    Category6Goal = 150,
                    Category6Scores = 0,
                    Category7Goal = 150,
                    Category7Scores = 0,
                    Category8Goal = 150,
                    Category8Scores = 0
                }
            };
        }
    }
}
