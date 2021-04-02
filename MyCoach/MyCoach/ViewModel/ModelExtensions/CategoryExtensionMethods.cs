using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.ModelExtensions
{
    public static class CategoryExtensionMethods
    {
        public static ushort GetCount(this ObservableCollection<Category> categories, ExerciseCategory category)
            => categories?.Where(c => c.ID == category).FirstOrDefault()?.Count ?? 1;

        public static string GetName(this ObservableCollection<Category> categories, ExerciseCategory category)
            => categories?.Where(c => c.ID == category).FirstOrDefault()?.Name ?? string.Empty;

        public static bool IsActive(this ObservableCollection<Category> categories, ExerciseCategory category) 
            => categories?.Where(c => c.ID == category).FirstOrDefault()?.Active ?? false;

        public static void SetActive(this ObservableCollection<Category> categories, ExerciseCategory category, bool value)
        {
            var selectedCategory = categories.Where(c => c.ID == category).FirstOrDefault();
            selectedCategory.Active = value;
        }

        public static void SetCount(this ObservableCollection<Category> categories, ExerciseCategory category, ushort value)
        {
            var selectedCategory = categories.Where(c => c.ID == category).FirstOrDefault();
            selectedCategory.Count = value;
        }

        public static void SetName(this ObservableCollection<Category> categories, ExerciseCategory category, string value)
        {
            var selectedCategory = categories.Where(c => c.ID == category).FirstOrDefault();
            selectedCategory.Name = value;
        }
    }
}
