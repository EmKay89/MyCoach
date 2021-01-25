﻿using MyCoach.DataHandling.DataTransferObjects;
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
        public static string GetName(this ObservableCollection<Category> categories, ExerciseCategory category)
            => categories?.Where(c => c.ID == category).FirstOrDefault()?.Name ?? string.Empty;

        public static bool IsActive(this ObservableCollection<Category> categories, ExerciseCategory category) 
            => categories?.Where(c => c.ID == category).FirstOrDefault()?.Active ?? false;

        public static void SetActive(this ObservableCollection<Category> categories, ExerciseCategory category, bool value)
        {
            if (categories == null)
            {
                return;
            }

            var selectedCategory = categories.Where(c => c.ID == category).FirstOrDefault();

            if (selectedCategory == null)
            {
                categories.Add(new Category { ID = category, Active = value });
                return;
            }

            selectedCategory.Active = value;
        }

        public static void SetName(this ObservableCollection<Category> categories, ExerciseCategory category, string value)
        {
            if (categories == null)
            {
                return;
            }

            var selectedCategory = categories.Where(c => c.ID == category).FirstOrDefault();

            if (selectedCategory == null)
            {
                categories.Add(new Category { ID = category, Name = value });
                return;
            }

            selectedCategory.Name = value;
        }
    }
}
