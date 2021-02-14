﻿using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.ModelExtensions;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace MyCoach.ViewModel
{
    public class ExercisesViewModel : BaseViewModel
    {
        private Category selectedCategory;

        public ExercisesViewModel()
        {
            this.ExercisesFilteredByCategory = new ObservableCollection<ExerciseViewModel>();
            this.Categories = new ObservableCollection<Category>();
            this.Exercises = new ObservableCollection<Exercise>();
            this.Categories.CollectionChanged += this.OnCategoriesChanged;
            this.Exercises.CollectionChanged += this.OnExercisesChanges;
            this.LoadCategoryBuffer();
            this.LoadExerciseBuffer();

            this.AddExerciseCommand = new AddExerciseCommand(this);
            this.ResetCategoriesCommand = new ResetCategoriesCommand(this);
            this.ResetExercisesCommand = new ResetExercisesCommand(this);
            this.SaveCategoriesCommand = new SaveCategoriesCommand(this);
            this.SaveExercisesCommand = new SaveExercisesCommand(this);
        }

        public ObservableCollection<Category> Categories { get; set; }

        public ObservableCollection<Exercise> Exercises { get; }

        public ObservableCollection<ExerciseViewModel> ExercisesFilteredByCategory { get; set; }

        public ObservableCollection<ushort> NumbersOneToTen { get; } = new ObservableCollection<ushort> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public bool CategoryWarmUpActive
        {
            get => this.Categories.IsActive(ExerciseCategory.WarmUp);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.WarmUp) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.WarmUp, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public string CategoryWarmUpName
        {
            get => this.Categories.GetName(ExerciseCategory.WarmUp);

            set 
            {
                if (this.Categories.GetName(ExerciseCategory.WarmUp) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.WarmUp, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public bool Category1Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category1);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category1) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category1, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public string Category1Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category1);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category1) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category1, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public bool Category2Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category2);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category2) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category2, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public string Category2Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category2);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category2) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category2, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public bool Category3Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category3);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category3) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category3, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public string Category3Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category3);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category3) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category3, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public bool Category4Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category4);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category4) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category4, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public string Category4Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category4);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category4) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category4, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public bool Category5Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category5);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category5) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category5, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public string Category5Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category5);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category5) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category5, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public bool Category6Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category6);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category6) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category6, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public string Category6Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category6);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category6) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category6, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public bool Category7Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category7);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category7) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category7, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public string Category7Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category7);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category7) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category7, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public bool Category8Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category8);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category8) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category8, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public string Category8Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category8);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category8) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category8, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public bool CategoryCoolDownActive
        {
            get => this.Categories.IsActive(ExerciseCategory.CoolDown);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.CoolDown) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.CoolDown, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public string CategoryCoolDownName
        {
            get => this.Categories.GetName(ExerciseCategory.CoolDown);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.CoolDown) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.CoolDown, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public ushort CategoryWarmUpCount
        {
            get => this.Categories.GetCount(ExerciseCategory.WarmUp);

            set
            {
                if (this.Categories.GetCount(ExerciseCategory.WarmUp) == value)
                {
                    return;
                }

                this.Categories.SetCount(ExerciseCategory.WarmUp, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public ushort CategoryCoolDownCount
        {
            get => this.Categories.GetCount(ExerciseCategory.CoolDown);

            set
            {
                if (this.Categories.GetCount(ExerciseCategory.CoolDown) == value)
                {
                    return;
                }

                this.Categories.SetCount(ExerciseCategory.CoolDown, value);
                this.InvokePropertyChanged();
                this.HasUnsavedCategories = true;
            }
        }

        public bool HasUnsavedCategories { get; set; }

        public bool HasUnsavedExercises { get; set; }

        public ICommand AddExerciseCommand { get; }

        public ICommand ResetCategoriesCommand { get; }

        public ICommand ResetExercisesCommand { get; }

        public ICommand SaveCategoriesCommand { get; }

        public ICommand SaveExercisesCommand { get; }

        public Category SelectedCategory
        {
            get => this.selectedCategory;

            set
            {
                if (value == this.selectedCategory || value == null)
                {
                    return;
                }

                this.selectedCategory = value;
                this.InvokePropertyChanged();
                this.RefreshExercisesFilteredByCategory();
            }
        }

        public void RefreshExercisesFilteredByCategory()
        {
            if (this.ExercisesFilteredByCategory == null)
            {
                return;
            }

            this.ExercisesFilteredByCategory.Clear();
            var exercises = this.Exercises.Where(e => e.Category == this.SelectedCategory?.ID);

            foreach (var exercise in exercises)
            {
                this.ExercisesFilteredByCategory.Add(
                    new ExerciseViewModel
                    {
                        Exercise = exercise,
                        Parent = this
                    });
            }
        }

        public void LoadExerciseBuffer()
        {
            var savedExercises = DataInterface.GetInstance().GetDataTransferObjects<Exercise>();
            this.Exercises.Clear();

            foreach (var exercise in savedExercises)
            {
                this.Exercises.Add((Exercise)exercise.Clone());
            }

            this.RefreshExercisesFilteredByCategory();
            this.HasUnsavedExercises = false;
        }

        public void LoadCategoryBuffer()
        {
            var savedSelectedCategoryId = this.SelectedCategory?.ID;
            var savedCategories = DataInterface.GetInstance().GetDataTransferObjects<Category>();
            this.Categories.Clear();

            foreach (var category in savedCategories)
            {
                this.Categories.Add((Category)category.Clone());
            }

            this.HasUnsavedCategories = false;

            if (savedSelectedCategoryId != null
                && this.Categories.FirstOrDefault(c => c.ID == savedSelectedCategoryId) is var foundCategory)
            {
                this.SelectedCategory = foundCategory;
                return;
            }

            this.SelectedCategory = this.Categories.FirstOrDefault();
        }

        public void SaveCategories()
        {
            var savedCategories = DataInterface.GetInstance().GetDataTransferObjects<Category>();
            savedCategories.Clear();
            foreach (var category in this.Categories)
            {
                savedCategories.Add((Category)category.Clone());
            }

            DataInterface.GetInstance().SetDataTransferObjects<Category>(savedCategories);
            this.InvokePropertyChanged(nameof(SelectedCategory));
            this.HasUnsavedCategories = false;
        }

        public void SaveExercises()
        {
            var savedExercises = DataInterface.GetInstance().GetDataTransferObjects<Exercise>();
            savedExercises.Clear();
            foreach (var exercise in this.Exercises)
            {
                savedExercises.Add((Exercise)exercise.Clone());
            }

            DataInterface.GetInstance().SetDataTransferObjects<Exercise>(savedExercises);
            this.HasUnsavedExercises = false;
        }

        private void OnCategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.InvokePropertiesChanged(
                nameof(this.Categories),
                nameof(this.CategoryWarmUpActive),
                nameof(this.CategoryWarmUpCount),
                nameof(this.Category1Active),
                nameof(this.Category1Name),
                nameof(this.Category2Active),
                nameof(this.Category2Name),
                nameof(this.Category3Active),
                nameof(this.Category3Name),
                nameof(this.Category4Active),
                nameof(this.Category4Name),
                nameof(this.Category5Active),
                nameof(this.Category5Name),
                nameof(this.Category6Active),
                nameof(this.Category6Name),
                nameof(this.Category7Active),
                nameof(this.Category7Name),
                nameof(this.Category8Active),
                nameof(this.Category8Name),
                nameof(this.CategoryCoolDownActive),
                nameof(this.CategoryCoolDownName),
                nameof(this.CategoryWarmUpCount),
                nameof(this.CategoryCoolDownCount),
                nameof(this.SelectedCategory));
        }

        private void OnExercisesChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.InvokePropertyChanged("Exercises");
        }
    }
}