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
        private Category selectedCategoryForExerciseDisplay;

        public ExercisesViewModel()
        {
            this.Categories = new ObservableCollection<Category>();
            this.Exercises = new ObservableCollection<Exercise>();
            this.LoadCategoryBuffer();
            this.LoadExerciseBuffer();

            this.AddExerciseCommand = new AddExerciseCommand(this);
            this.ResetExercisesCommand = new ResetExercisesCommand(this);
            this.SaveCategoriesCommand = new SaveCategoriesCommand(this);
            this.SaveExercisesCommand = new SaveExercisesCommand(this);
            
            this.Categories.CollectionChanged += this.OnCategoriesChanged;
            this.Exercises.CollectionChanged += this.OnExercisesChanges;

            this.ExercisesFilteredByCategory = new ObservableCollection<ExerciseViewModel>();
            this.SelectedCategoryForExerciseDisplay = this.Categories.FirstOrDefault();
        }

        public ObservableCollection<Category> Categories { get; }

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
            }
        }

        public bool HasUnsavedExercises { get; set; }

        public ICommand AddExerciseCommand { get; }

        public ICommand ResetExercisesCommand { get; }

        public ICommand SaveCategoriesCommand { get; }

        public ICommand SaveExercisesCommand { get; }

        public Category SelectedCategoryForExerciseDisplay
        {
            get => this.selectedCategoryForExerciseDisplay;

            set
            {
                if (this.selectedCategoryForExerciseDisplay == value)
                {
                    return;
                }

                this.selectedCategoryForExerciseDisplay = value;
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
            var exercises = this.Exercises.Where(e => e.Category == this.SelectedCategoryForExerciseDisplay.ID);

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
        }

        private void LoadCategoryBuffer()
        {
            var savedCategories = DataInterface.GetInstance().GetDataTransferObjects<Category>();
            this.Categories.Clear();

            foreach (var category in savedCategories)
            {
                this.Categories.Add((Category)category.Clone());
            }
        }

        private void OnCategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.InvokePropertyChanged("Categories");
        }

        private void OnExercisesChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.InvokePropertyChanged("Exercises");
        }

        private void FilterExercises(object sender, FilterEventArgs e)
        {
            var exercise = e.Item as Exercise;

            e.Accepted = exercise != null && exercise.Category == this.SelectedCategoryForExerciseDisplay.ID ? true : false;
        }
    }
}
