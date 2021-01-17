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
        public static ObservableCollection<Category> Categories
        {
            get
            {
                var categories = new ObservableCollection<Category>();
                categories.Add(new Category { ID = ExerciseCategory.WarmUp, Name = "Erwärmung", Count = 3, Active = true, Type = ExerciseType.WarmUp });
                categories.Add(new Category { ID = ExerciseCategory.Category1, Name = "Arme", Active = true, Type = ExerciseType.Training });
                categories.Add(new Category { ID = ExerciseCategory.CoolDown, Name = "Dehnung", Count = 0, Active = false, Type = ExerciseType.CoolDown });
                return categories;
            }
        }

        public static ObservableCollection<Exercise> Exercises
        {
            get
            {
                var exercices = new ObservableCollection<Exercise>();
                exercices.Add(new Exercise
                {
                    Category = ExerciseCategory.WarmUp,
                    Count = 10,
                    Name = "Armkreisen",
                    RelatedCategory = ExerciseCategory.Category1,
                    Scores = 0,
                    Info = "https://www.youtube.com/watch?v=Gl6g9CZTZL0",
                    Active = true
                });

                exercices.Add(new Exercise
                {
                    Category = ExerciseCategory.Category1,
                    Count = 10,
                    Name = "Hantelheben 10kg",
                    RelatedCategory = ExerciseCategory.Category1,
                    Scores = 10,
                    Info = "Hier könnte Ihre Testbeschreibung stehen ... ",
                    Active = true
                });

                exercices.Add(new Exercise
                {
                    Category = ExerciseCategory.CoolDown,
                    Count = 10,
                    Name = "Arme strecken",
                    RelatedCategory = ExerciseCategory.Category1,
                    Scores = 0,
                    Info = "Die Arme in die Höhe!",
                    Active = false
                });

                return exercices;
            }
        }

        public static ObservableCollection<Settings> Settings
        {
            get
            {
                var settings = new ObservableCollection<Settings>();
                settings.Add(new Settings
                {
                    Permission = ExerciseSchedulingRepetitionPermission.NotPreferred,
                    RepeatsRound1 = 100,
                    ScoresRound1 = 100,
                    RepeatsRound2 = 70,
                    ScoresRound2 = 100,
                    RepeatsRound3 = 50,
                    ScoresRound3 = 100,
                    RepeatsRound4 = 30,
                    ScoresRound4 = 100
                });

                return settings;
            }
        }

        public static ObservableCollection<TrainingSchedule> TrainingSchedules
        {
            get
            {
                var schedules = new ObservableCollection<TrainingSchedule>();
                schedules.Add(new TrainingSchedule
                {
                    StartMonth = Month.April,
                    StartYear = 2020,
                    Duration = 5
                });

                return schedules;
            }
        }

        public static ObservableCollection<TrainingScore> TrainingScores
        {
            get
            {
                var scores = new ObservableCollection<TrainingScore>();
                scores.Add(new TrainingScore
                {
                    Month = Month.März,
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
                });

                scores.Add(new TrainingScore
                {
                    Month = Month.April,
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
                });

                return scores;
            }
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
