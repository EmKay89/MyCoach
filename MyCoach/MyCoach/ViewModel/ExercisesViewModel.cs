using Microsoft.Win32;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.ModelExtensions;
using MyCoach.ViewModel.Services;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MyCoach.ViewModel
{
    public class ExercisesViewModel : BaseViewModel
    {
        private Category selectedCategory;
        private IMessageBoxService messageBoxService;
        private IFileDialogService fileDialogService;

        public ExercisesViewModel(IMessageBoxService messageBoxService = null, IFileDialogService fileDialogService = null)
        {
            this.ExercisesFilteredByCategory = new ObservableCollection<ExerciseViewModel>();
            this.Categories = new ObservableCollection<Category>();
            this.Exercises = new ObservableCollection<Exercise>();
            this.Categories.CollectionChanged += this.OnCategoriesChanged;
            this.Exercises.CollectionChanged += this.OnExercisesChanges;
            this.LoadCategoryBuffer();
            this.LoadExerciseBuffer();
            this.messageBoxService = messageBoxService ?? new MessageBoxService();
            this.fileDialogService = fileDialogService ?? new FileDialogService();

            this.AddExerciseCommand = new RelayCommand(this.AddExercise, () => this.SelectedCategory == null ? false : true);
            this.ExportExercisesCommand = new RelayCommand(this.ExportExercises);
            this.ImportExercisesCommand = new RelayCommand(this.ImportExercises);
            this.ResetCategoriesCommand = new RelayCommand(this.LoadCategoryBuffer, () => this.HasUnsavedCategories);
            this.ResetExercisesCommand = new RelayCommand(this.LoadExerciseBuffer, () => this.HasUnsavedExercises);
            this.SaveCategoriesCommand = new RelayCommand(this.SaveCategories, () => this.HasUnsavedCategories);
            this.SaveExercisesCommand = new RelayCommand(this.SaveExercises, () => this.HasUnsavedExercises);
            this.SetDefaultsCommand = new RelayCommand(this.SetDefaults);
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

        public RelayCommand AddExerciseCommand { get; }

        public RelayCommand ExportExercisesCommand { get; }

        public RelayCommand ImportExercisesCommand { get; }

        public RelayCommand ResetCategoriesCommand { get; }

        public RelayCommand ResetExercisesCommand { get; }

        public RelayCommand SaveCategoriesCommand { get; }

        public RelayCommand SaveExercisesCommand { get; }

        public RelayCommand SetDefaultsCommand { get; }

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

        private void AddExercise()
        {
            this.Exercises.Add(
                new Exercise { Name = "Neue Übung", Active = true, Scores = 10, Category = this.SelectedCategory.ID });
            this.RefreshExercisesFilteredByCategory();
            this.HasUnsavedExercises = true;
        }

        private void ImportExercises()
        {
            var path = this.fileDialogService.OpenFile(
                System.AppDomain.CurrentDomain.BaseDirectory, "XML files (*.xml)|*.xml", 1);

            if (path == null)
            {
                return;
            }

            if (DataInterface.GetInstance().ImportExerciseSet(path))
            {
                this.LoadCategoryBuffer();
                this.LoadExerciseBuffer();
                return;
            }

            this.messageBoxService.ShowMessage("Fehler beim Laden", "Fehler beim Laden", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ExportExercises()
        {
            var path = this.fileDialogService.SaveFile(
                System.AppDomain.CurrentDomain.BaseDirectory, "XML files (*.xml)|*.xml", 1);

            if (path == null)
            {
                return;
            }

            this.LoadCategoryBuffer();
            this.LoadExerciseBuffer();

            if (DataInterface.GetInstance().ExportExerciseSet(path) == false)
            {
                this.messageBoxService.ShowMessage("Fehler beim Speichern", "Fehler beim Speichern", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadCategoryBuffer()
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

        private void LoadExerciseBuffer()
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

        private void OnCategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.InvokePropertiesChanged(
                nameof(this.Categories),
                nameof(this.CategoryWarmUpActive),
                nameof(this.CategoryWarmUpName),
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

        private void SaveCategories()
        {
            var savedCategories = DataInterface.GetInstance().GetDataTransferObjects<Category>();
            savedCategories.Clear();
            foreach (var category in this.Categories)
            {
                savedCategories.Add((Category)category.Clone());
            }

            var result = DataInterface.GetInstance().SetDataTransferObjects<Category>(savedCategories);
            if (result == false)
            {
                this.messageBoxService.ShowMessage("Speichern fehlgeschlagen. Die Änderungen werden beim nächsten Neustart des Programms nicht mehr zur Verfügung stehen.",
                    "Speichern",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            this.InvokePropertyChanged(nameof(SelectedCategory));
            this.HasUnsavedCategories = false;
        }

        private void SaveExercises()
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

        private void SetDefaults()
        {
            var result = this.messageBoxService.ShowMessage("Achtung, hierdurch gehen Ihre gespeicherten Übungen verlohren. Möchten Sie fortfahren?",
                "Zurücksetzen",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                DataInterface.GetInstance().SetDefaults<Exercise>();
                DataInterface.GetInstance().SetDefaults<Category>();
                this.LoadCategoryBuffer();
                this.LoadExerciseBuffer();
            }
        }
    }
}
