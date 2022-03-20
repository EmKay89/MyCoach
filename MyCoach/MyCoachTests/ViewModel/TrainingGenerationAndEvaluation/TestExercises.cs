using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using System.Collections.Generic;
using System.Linq;

namespace MyCoachTests.ViewModel.TrainingGenerationAndEvaluation
{
    public static class TestExercises
    {
        public static List<Exercise> SixOfEachCategory => new List<Exercise>
        {
            new Exercise
            {
                ID = 0,
                Category = ExerciseCategory.WarmUp,
                Count = 1,
                Name = "WarmUp-1",
                Unit = "Repeats",
                Scores = 1,
                Info = "WarmUp-1",
                Active = true                
            },

            new Exercise
            {
                ID = 1,
                Category = ExerciseCategory.WarmUp,
                Count = 2,
                Name = "WarmUp-2",
                Unit = "Repeats",
                Scores = 2,
                Info = "WarmUp-2",
                Active = true
            },

            new Exercise
            {
                ID = 2,
                Category = ExerciseCategory.WarmUp,
                Count = 3,
                Name = "WarmUp-3",
                Unit = "Repeats",
                Scores = 3,
                Info = "WarmUp-3",
                Active = true
            },

            new Exercise
            {
                ID = 3,
                Category = ExerciseCategory.WarmUp,
                Count = 4,
                Name = "WarmUp-4",
                Unit = "Repeats",
                Scores = 4,
                Info = "WarmUp-4",
                Active = true
            },

            new Exercise
            {
                ID = 4,
                Category = ExerciseCategory.WarmUp,
                Count = 5,
                Name = "WarmUp-5",
                Unit = "Repeats",
                Scores = 5,
                Info = "WarmUp-5",
                Active = true
            },

            new Exercise
            {
                ID = 5,
                Category = ExerciseCategory.WarmUp,
                Count = 6,
                Name = "WarmUp-6",
                Unit = "Repeats",
                Scores = 6,
                Info = "WarmUp-6",
                Active = true
            },

            new Exercise
            {
                ID = 0,
                Category = ExerciseCategory.Category1,
                Count = 11,
                Name = "Category1-1",
                Unit = "Repeats",
                Scores = 11,
                Info = "Category1-1",
                Active = true
            },

            new Exercise
            {
                ID = 1,
                Category = ExerciseCategory.Category1,
                Count = 12,
                Name = "Category1-2",
                Unit = "Repeats",
                Scores = 12,
                Info = "Category1-2",
                Active = true
            },

            new Exercise
            {
                ID = 2,
                Category = ExerciseCategory.Category1,
                Count = 13,
                Name = "Category1-3",
                Unit = "Repeats",
                Scores = 13,
                Info = "Category1-3",
                Active = true
            },

            new Exercise
            {
                ID = 3,
                Category = ExerciseCategory.Category1,
                Count = 14,
                Name = "Category1-4",
                Unit = "Repeats",
                Scores = 14,
                Info = "Category1-4",
                Active = true
            },

            new Exercise
            {
                ID = 4,
                Category = ExerciseCategory.Category1,
                Count = 15,
                Name = "Category1-5",
                Unit = "Repeats",
                Scores = 15,
                Info = "Category1-5",
                Active = true
            },

            new Exercise
            {
                ID = 5,
                Category = ExerciseCategory.Category1,
                Count = 16,
                Name = "Category1-6",
                Unit = "Repeats",
                Scores = 16,
                Info = "Category1-6",
                Active = true
            },

            new Exercise
            {
                ID = 0,
                Category = ExerciseCategory.Category2,
                Count = 21,
                Name = "Category2-1",
                Unit = "Repeats",
                Scores = 21,
                Info = "Category2-1",
                Active = true
            },

            new Exercise
            {
                ID = 1,
                Category = ExerciseCategory.Category2,
                Count = 22,
                Name = "Category2-2",
                Unit = "Repeats",
                Scores = 22,
                Info = "Category2-2",
                Active = true
            },

            new Exercise
            {
                ID = 2,
                Category = ExerciseCategory.Category2,
                Count = 23,
                Name = "Category2-3",
                Unit = "Repeats",
                Scores = 23,
                Info = "Category2-3",
                Active = true
            },

            new Exercise
            {
                ID = 3,
                Category = ExerciseCategory.Category2,
                Count = 24,
                Name = "Category2-4",
                Unit = "Repeats",
                Scores = 24,
                Info = "Category2-4",
                Active = true
            },

            new Exercise
            {
                ID = 4,
                Category = ExerciseCategory.Category2,
                Count = 25,
                Name = "Category2-5",
                Unit = "Repeats",
                Scores = 25,
                Info = "Category2-5",
                Active = true
            },

            new Exercise
            {
                ID = 5,
                Category = ExerciseCategory.Category2,
                Count = 26,
                Name = "Category2-6",
                Unit = "Repeats",
                Scores = 26,
                Info = "Category2-6",
                Active = true
            },

            new Exercise
            {
                ID = 0,
                Category = ExerciseCategory.Category3,
                Count = 31,
                Name = "Category3-1",
                Unit = "Repeats",
                Scores = 31,
                Info = "Category3-1",
                Active = true
            },

            new Exercise
            {
                ID = 1,
                Category = ExerciseCategory.Category3,
                Count = 32,
                Name = "Category3-2",
                Unit = "Repeats",
                Scores = 32,
                Info = "Category3-2",
                Active = true
            },

            new Exercise
            {
                ID = 2,
                Category = ExerciseCategory.Category3,
                Count = 33,
                Name = "Category3-3",
                Unit = "Repeats",
                Scores = 33,
                Info = "Category3-3",
                Active = true
            },

            new Exercise
            {
                ID = 3,
                Category = ExerciseCategory.Category3,
                Count = 34,
                Name = "Category3-4",
                Unit = "Repeats",
                Scores = 34,
                Info = "Category3-4",
                Active = true
            },

            new Exercise
            {
                ID = 4,
                Category = ExerciseCategory.Category3,
                Count = 35,
                Name = "Category3-5",
                Unit = "Repeats",
                Scores = 35,
                Info = "Category3-5",
                Active = true
            },

            new Exercise
            {
                ID = 5,
                Category = ExerciseCategory.Category3,
                Count = 36,
                Name = "Category3-6",
                Unit = "Repeats",
                Scores = 36,
                Info = "Category3-6",
                Active = true
            },

            new Exercise
            {
                ID = 0,
                Category = ExerciseCategory.Category4,
                Count = 41,
                Name = "Category4-1",
                Unit = "Repeats",
                Scores = 41,
                Info = "Category4-1",
                Active = true
            },

            new Exercise
            {
                ID = 1,
                Category = ExerciseCategory.Category4,
                Count = 42,
                Name = "Category4-2",
                Unit = "Repeats",
                Scores = 42,
                Info = "Category4-2",
                Active = true
            },

            new Exercise
            {
                ID = 2,
                Category = ExerciseCategory.Category4,
                Count = 43,
                Name = "Category4-3",
                Unit = "Repeats",
                Scores = 43,
                Info = "Category4-3",
                Active = true
            },

            new Exercise
            {
                ID = 3,
                Category = ExerciseCategory.Category4,
                Count = 44,
                Name = "Category4-4",
                Unit = "Repeats",
                Scores = 44,
                Info = "Category4-4",
                Active = true
            },

            new Exercise
            {
                ID = 4,
                Category = ExerciseCategory.Category4,
                Count = 45,
                Name = "Category4-5",
                Unit = "Repeats",
                Scores = 45,
                Info = "Category4-5",
                Active = true
            },

            new Exercise
            {
                ID = 5,
                Category = ExerciseCategory.Category4,
                Count = 46,
                Name = "Category4-6",
                Unit = "Repeats",
                Scores = 46,
                Info = "Category4-6",
                Active = true
            },

            new Exercise
            {
                ID = 0,
                Category = ExerciseCategory.Category5,
                Count = 51,
                Name = "Category5-1",
                Unit = "Repeats",
                Scores = 51,
                Info = "Category5-1",
                Active = true
            },

            new Exercise
            {
                ID = 1,
                Category = ExerciseCategory.Category5,
                Count = 52,
                Name = "Category5-2",
                Unit = "Repeats",
                Scores = 52,
                Info = "Category5-2",
                Active = true
            },

            new Exercise
            {
                ID = 2,
                Category = ExerciseCategory.Category5,
                Count = 53,
                Name = "Category5-3",
                Unit = "Repeats",
                Scores = 53,
                Info = "Category5-3",
                Active = true
            },

            new Exercise
            {
                ID = 3,
                Category = ExerciseCategory.Category5,
                Count = 54,
                Name = "Category5-4",
                Unit = "Repeats",
                Scores = 54,
                Info = "Category5-4",
                Active = true
            },

            new Exercise
            {
                ID = 4,
                Category = ExerciseCategory.Category5,
                Count = 55,
                Name = "Category5-5",
                Unit = "Repeats",
                Scores = 55,
                Info = "Category5-5",
                Active = true
            },

            new Exercise
            {
                ID = 5,
                Category = ExerciseCategory.Category5,
                Count = 56,
                Name = "Category5-6",
                Unit = "Repeats",
                Scores = 56,
                Info = "Category5-6",
                Active = true
            },

            new Exercise
            {
                ID = 0,
                Category = ExerciseCategory.Category6,
                Count = 61,
                Name = "Category6-1",
                Unit = "Repeats",
                Scores = 61,
                Info = "Category6-1",
                Active = true
            },

            new Exercise
            {
                ID = 1,
                Category = ExerciseCategory.Category6,
                Count = 62,
                Name = "Category6-2",
                Unit = "Repeats",
                Scores = 62,
                Info = "Category6-2",
                Active = true
            },

            new Exercise
            {
                ID = 2,
                Category = ExerciseCategory.Category6,
                Count = 63,
                Name = "Category6-3",
                Unit = "Repeats",
                Scores = 63,
                Info = "Category6-3",
                Active = true
            },

            new Exercise
            {
                ID = 3,
                Category = ExerciseCategory.Category6,
                Count = 64,
                Name = "Category6-4",
                Unit = "Repeats",
                Scores = 64,
                Info = "Category6-4",
                Active = true
            },

            new Exercise
            {
                ID = 4,
                Category = ExerciseCategory.Category6,
                Count = 65,
                Name = "Category6-5",
                Unit = "Repeats",
                Scores = 65,
                Info = "Category6-5",
                Active = true
            },

            new Exercise
            {
                ID = 5,
                Category = ExerciseCategory.Category6,
                Count = 66,
                Name = "Category6-6",
                Unit = "Repeats",
                Scores = 66,
                Info = "Category6-6",
                Active = true
            },

            new Exercise
            {
                ID = 0,
                Category = ExerciseCategory.Category7,
                Count = 71,
                Name = "Category7-1",
                Unit = "Repeats",
                Scores = 71,
                Info = "Category7-1",
                Active = true
            },

            new Exercise
            {
                ID = 1,
                Category = ExerciseCategory.Category7,
                Count = 72,
                Name = "Category7-2",
                Unit = "Repeats",
                Scores = 72,
                Info = "Category7-2",
                Active = true
            },

            new Exercise
            {
                ID = 2,
                Category = ExerciseCategory.Category7,
                Count = 73,
                Name = "Category7-3",
                Unit = "Repeats",
                Scores = 73,
                Info = "Category7-3",
                Active = true
            },

            new Exercise
            {
                ID = 3,
                Category = ExerciseCategory.Category7,
                Count = 74,
                Name = "Category7-4",
                Unit = "Repeats",
                Scores = 74,
                Info = "Category7-4",
                Active = true
            },

            new Exercise
            {
                ID = 4,
                Category = ExerciseCategory.Category7,
                Count = 75,
                Name = "Category7-5",
                Unit = "Repeats",
                Scores = 75,
                Info = "Category7-5",
                Active = true
            },

            new Exercise
            {
                ID = 5,
                Category = ExerciseCategory.Category7,
                Count = 76,
                Name = "Category7-6",
                Unit = "Repeats",
                Scores = 76,
                Info = "Category7-6",
                Active = true
            },

            new Exercise
            {
                ID = 0,
                Category = ExerciseCategory.Category8,
                Count = 81,
                Name = "Category8-1",
                Unit = "Repeats",
                Scores = 81,
                Info = "Category8-1",
                Active = true
            },

            new Exercise
            {
                ID = 1,
                Category = ExerciseCategory.Category8,
                Count = 82,
                Name = "Category8-2",
                Unit = "Repeats",
                Scores = 82,
                Info = "Category8-2",
                Active = true
            },

            new Exercise
            {
                ID = 2,
                Category = ExerciseCategory.Category8,
                Count = 83,
                Name = "Category8-3",
                Unit = "Repeats",
                Scores = 83,
                Info = "Category8-3",
                Active = true
            },

            new Exercise
            {
                ID = 3,
                Category = ExerciseCategory.Category8,
                Count = 84,
                Name = "Category8-4",
                Unit = "Repeats",
                Scores = 84,
                Info = "Category8-4",
                Active = true
            },

            new Exercise
            {
                ID = 4,
                Category = ExerciseCategory.Category8,
                Count = 85,
                Name = "Category8-5",
                Unit = "Repeats",
                Scores = 85,
                Info = "Category8-5",
                Active = true
            },

            new Exercise
            {
                ID = 5,
                Category = ExerciseCategory.Category8,
                Count = 86,
                Name = "Category8-6",
                Unit = "Repeats",
                Scores = 86,
                Info = "Category8-6",
                Active = true
            },

            new Exercise
            {
                ID = 0,
                Category = ExerciseCategory.CoolDown,
                Count = 91,
                Name = "CoolDown-1",
                Unit = "Repeats",
                Scores = 91,
                Info = "CoolDown-1",
                Active = true
            },

            new Exercise
            {
                ID = 1,
                Category = ExerciseCategory.CoolDown,
                Count = 92,
                Name = "CoolDown-2",
                Unit = "Repeats",
                Scores = 92,
                Info = "CoolDown-2",
                Active = true
            },

            new Exercise
            {
                ID = 2,
                Category = ExerciseCategory.CoolDown,
                Count = 93,
                Name = "CoolDown-3",
                Unit = "Repeats",
                Scores = 93,
                Info = "CoolDown-3",
                Active = true
            },

            new Exercise
            {
                ID = 3,
                Category = ExerciseCategory.CoolDown,
                Count = 94,
                Name = "CoolDown-4",
                Unit = "Repeats",
                Scores = 94,
                Info = "CoolDown-4",
                Active = true
            },

            new Exercise
            {
                ID = 4,
                Category = ExerciseCategory.CoolDown,
                Count = 95,
                Name = "CoolDown-5",
                Unit = "Repeats",
                Scores = 95,
                Info = "CoolDown-5",
                Active = true
            },

            new Exercise
            {
                ID = 5,
                Category = ExerciseCategory.CoolDown,
                Count = 96,
                Name = "CoolDown-6",
                Unit = "Repeats",
                Scores = 96,
                Info = "CoolDown-6",
                Active = true
            },
        };

        public static List<Exercise> TwoOfEachCategory => SixOfEachCategory.Where(e => e.ID < 2).ToList();

        public static List<Exercise> SixOfCategory1To4 => SixOfEachCategory.Where(
            e => e.Category == ExerciseCategory.Category1 ||
            e.Category == ExerciseCategory.Category2 ||
            e.Category == ExerciseCategory.Category3 ||
            e.Category == ExerciseCategory.Category4).ToList();

        public static List<Exercise> TwoOfCategory1To4 => SixOfCategory1To4.Where(e => e.ID < 2).ToList();

        public static List<Exercise> SixOfEachCategoryWithCategory2Inactive => Deactivate(SixOfEachCategory, ExerciseCategory.Category2);

        public static List<Exercise> TwoOfEachCategoryWithCategory2Inactive => SixOfEachCategoryWithCategory2Inactive.Where(e => e.ID < 2).ToList();

        private static List<Exercise> Deactivate(List<Exercise> exercises, ExerciseCategory category)
        {
            foreach (var exercise in exercises)
            {
                if (exercise.Category == category)
                {
                    exercise.Active = false;
                }
            }

            return exercises;
        }
    }
}
