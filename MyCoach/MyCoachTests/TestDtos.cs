using MyCoach.Defines;
using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MyCoachTests
{
    /// <summary>
    ///     Statische Klasse, die vorgefüllte Listen einzelner DataTransferObjects für Unittests zur Verfügung stellt.
    /// </summary>
    public static class TestDtos
    {
        private static readonly ObservableCollection<Category> categories = new ObservableCollection<Category>
        {
            new Category { ID = ExerciseCategory.WarmUp, Name = "Erwärmung", Count = 3, Active = true, Type = ExerciseType.WarmUp },
            new Category { ID = ExerciseCategory.Category1, Name = "Arme", Active = true, Type = ExerciseType.Training },
            new Category { ID = ExerciseCategory.Category2, Name = "Bauch", Active = true, Type = ExerciseType.Training },
            new Category { ID = ExerciseCategory.Category3, Name = "Seiten", Active = true, Type = ExerciseType.Training },
            new Category { ID = ExerciseCategory.Category4, Name = "Rücken", Active = true, Type = ExerciseType.Training },
            new Category { ID = ExerciseCategory.Category5, Name = "Beine", Active = true, Type = ExerciseType.Training },
            new Category { ID = ExerciseCategory.Category6, Name = "", Active = false, Type = ExerciseType.Training },
            new Category { ID = ExerciseCategory.Category7, Name = "", Active = false, Type = ExerciseType.Training },
            new Category { ID = ExerciseCategory.Category8, Name = "", Active = false, Type = ExerciseType.Training },
            new Category { ID = ExerciseCategory.CoolDown, Name = "Dehnung", Count = 3, Active = false, Type = ExerciseType.CoolDown }
        };

        private static readonly ObservableCollection<Exercise> exercices = new ObservableCollection<Exercise>
        {
            new Exercise
            {
                Category = ExerciseCategory.WarmUp,
                Count = 10,
                Name = "Armkreisen",
                RelatedCategory = ExerciseCategory.Category1,
                Scores = 0,
                Info = "https://www.youtube.com/watch?v=Gl6g9CZTZL0",
                Active = true
            },

            new Exercise
            {
                Category = ExerciseCategory.Category1,
                Count = 10,
                Name = "Hantelheben 10kg",
                RelatedCategory = ExerciseCategory.Category1,
                Scores = 10,
                Info = "Hier könnte Ihre Testbeschreibung stehen ... ",
                Active = true
            },

            new Exercise
            {
                Category = ExerciseCategory.CoolDown,
                Count = 10,
                Name = "Arme strecken",
                RelatedCategory = ExerciseCategory.Category1,
                Scores = 0,
                Info = "Die Arme in die Höhe!",
                Active = false
            }
        };

        private static readonly ObservableCollection<Settings> settings = new ObservableCollection<Settings>
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
                ScoresRound4 = 101
            }
        };

        private static readonly ObservableCollection<TrainingSchedule> schedules = new ObservableCollection<TrainingSchedule>
        {
            new TrainingSchedule
            {
                StartMonth = DateTime.MinValue,
                ScheduleType = ScheduleType.TimeBased,
                Duration = 5
            }
        };

        private static readonly ObservableCollection<Month> scores = new ObservableCollection<Month>
        {
            new Month
            {
                Number = MonthNumber.Current,
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
                Category1Goal = 120,
                Category1Scores = 70,
                Category2Goal = 120,
                Category2Scores = 70,
                Category3Goal = 120,
                Category3Scores = 70,
                Category4Goal = 120,
                Category4Scores = 70,
                Category5Goal = 120,
                Category5Scores = 70,
                Category6Goal = 120,
                Category6Scores = 70,
                Category7Goal = 120,
                Category7Scores = 70,
                Category8Goal = 120,
                Category8Scores = 70
            }
        };

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
    }
}
