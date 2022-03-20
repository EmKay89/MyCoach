using MyCoach.Model.Defines;
using System.Collections.Generic;
using System.Linq;

namespace MyCoach.Model.DataTransferObjects.CollectionExtensions
{
    public static class CategoryCollectionExtensions
    {
        public static ushort GetCount(this IEnumerable<Category> categories, ExerciseCategory category)
            => categories?.Where(c => c.ID == category).FirstOrDefault()?.Count ?? 1;

        public static string GetName(this IEnumerable<Category> categories, ExerciseCategory category)
            => categories?.Where(c => c.ID == category).FirstOrDefault()?.Name ?? string.Empty;

        public static bool IsActive(this IEnumerable<Category> categories, ExerciseCategory category)
            => categories?.Where(c => c.ID == category).FirstOrDefault()?.Active ?? false;

        public static void SetActive(this IEnumerable<Category> categories, ExerciseCategory category, bool value)
        {
            var selectedCategory = categories.Where(c => c.ID == category).FirstOrDefault();
            selectedCategory.Active = value;
        }

        public static void SetCount(this IEnumerable<Category> categories, ExerciseCategory category, ushort value)
        {
            var selectedCategory = categories.Where(c => c.ID == category).FirstOrDefault();
            selectedCategory.Count = value;
        }

        public static void SetName(this IEnumerable<Category> categories, ExerciseCategory category, string value)
        {
            var selectedCategory = categories.Where(c => c.ID == category).FirstOrDefault();
            selectedCategory.Name = value;
        }
    }
}
