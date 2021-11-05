using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System.Collections.Generic;

namespace MyCoachTests.ViewModel.TrainingGenerationAndEvaluation
{
    public static class TestCategories
    {
        public static List<Category> AllCategoriesActive => new List<Category>
        {
            new Category
            {
                ID = ExerciseCategory.WarmUp,
                Name = "WarmUp",
                Active = true,
                Type = ExerciseType.WarmUp,
                Count = 3
            },

            new Category
            {
                ID = ExerciseCategory.Category1,
                Name = "Category1",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },

            new Category
            {
                ID = ExerciseCategory.Category2,
                Name = "Category2",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },


            new Category
            {
                ID = ExerciseCategory.Category3,
                Name = "Category3",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },

            new Category
            {
                ID = ExerciseCategory.Category4,
                Name = "Category4",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },

            new Category
            {
                ID = ExerciseCategory.Category5,
                Name = "Category5",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },

            new Category
            {
                ID = ExerciseCategory.Category6,
                Name = "Category6",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },

            new Category
            {
                ID = ExerciseCategory.Category7,
                Name = "Category7",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },

            new Category
            {
                ID = ExerciseCategory.Category8,
                Name = "Category8",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },

            new Category
            {
                ID = ExerciseCategory.CoolDown,
                Name = "CoolDown",
                Active = true,
                Type = ExerciseType.CoolDown,
                Count = 3
            }
        };

        public static List<Category> CategoryOneToFourActiveAndRestMissing => new List<Category>
        {
            new Category
            {
                ID = ExerciseCategory.Category1,
                Name = "Category1",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },

            new Category
            {
                ID = ExerciseCategory.Category2,
                Name = "Category2",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },


            new Category
            {
                ID = ExerciseCategory.Category3,
                Name = "Category3",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },

            new Category
            {
                ID = ExerciseCategory.Category4,
                Name = "Category4",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            }
        };

        public static List<Category> CategoryOneTwoThreeFiveWarmUpAndCoolDownActive => new List<Category>
        {
            new Category
            {
                ID = ExerciseCategory.WarmUp,
                Name = "WarmUp",
                Active = true,
                Type = ExerciseType.WarmUp,
                Count = 3
            },

            new Category
            {
                ID = ExerciseCategory.Category1,
                Name = "Category1",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },

            new Category
            {
                ID = ExerciseCategory.Category2,
                Name = "Category2",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },


            new Category
            {
                ID = ExerciseCategory.Category3,
                Name = "Category3",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },

            new Category
            {
                ID = ExerciseCategory.Category5,
                Name = "Category5",
                Active = true,
                Type = ExerciseType.Training,
                Count = 0
            },

            new Category
            {
                ID = ExerciseCategory.CoolDown,
                Name = "CoolDown",
                Active = true,
                Type = ExerciseType.CoolDown,
                Count = 3
            }
        };
    }
}
