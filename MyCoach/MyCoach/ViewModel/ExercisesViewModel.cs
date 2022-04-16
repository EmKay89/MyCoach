using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.DataTransferObjects.CollectionExtensions;
using MyCoach.Model.Defines;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.DataBaseValidation;
using MyCoach.ViewModel.Events;
using MyCoach.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace MyCoach.ViewModel
{
    public class ExercisesViewModel : BaseViewModel
    {
        private Category selectedCategory;
        private readonly IFileDialogService fileDialogService;
        private readonly IMessageBoxService messageBoxService;

        public ExercisesViewModel(IMessageBoxService messageBoxService = null, IFileDialogService fileDialogService = null)
        {
            this.Categories.CollectionChanged += this.OnCategoriesChanged;
            this.LoadCategoryBuffer();
            this.LoadExerciseBuffer();
            this.fileDialogService = fileDialogService ?? new FileDialogService();
            this.messageBoxService = messageBoxService ?? new MessageBoxService();

            this.AddExerciseCommand = new RelayCommand(this.AddExercise, () => this.SelectedCategory != null);
            this.ExportExercisesCommand = new RelayCommand(this.ExportExercises);
            this.ImportExercisesCommand = new RelayCommand(this.ImportExercises);
            this.ResetCategoriesCommand = new RelayCommand(this.LoadCategoryBuffer, () => this.HasUnsavedCategories);
            this.ResetExercisesCommand = new RelayCommand(this.LoadExerciseBuffer, () => this.HasUnsavedExercises);
            this.SaveCategoriesCommand = new RelayCommand(this.SaveCategories, () => this.HasUnsavedCategories);
            this.SaveExercisesCommand = new RelayCommand(this.SaveExercises, () => this.HasUnsavedExercises);
            this.SetDefaultsCommand = new RelayCommand(this.SetDefaults);
        }

        public const string NEW_EXERCISE_NAME = "Neue Übung";
        public const string IMPORT_ERROR_TEXT = "Importieren fehlgeschlagen";
        public const string EXPORT_ERROR_TEXT = "Exportieren fehlgeschlagen";
        public const string RESET_TEXT = "Achtung, hierdurch gehen Ihre gespeicherten Übungen verlohren. Möchten Sie fortfahren?";
        public const string RESET_CAPTION = "Zurücksetzen";

        public event ExerciseEventHandler AddExerciseToTrainingExecuted;

        public List<Category> ActiveCategories => this.Categories.Where(c => c.Active).ToList();
        
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        public ObservableCollection<Exercise> Exercises { get; set; } = new ObservableCollection<Exercise>();

        public ObservableCollection<ExerciseViewModel> ExercisesFilteredByCategory { get; set; } = new ObservableCollection<ExerciseViewModel>();

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
                if (value == this.selectedCategory)
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

            foreach (var vm in ExercisesFilteredByCategory)
            {
                vm.AddExerciseToTrainingExecuted -= this.OnExerciseAddExerciseToTrainingExecuted;
                vm.DeleteExerciseExecuted -= this.OnExerciseDeleteExerciseToTrainingExecuted;
                vm.ExerciseChanged -= this.OnExerciseChanged;
            }

            this.ExercisesFilteredByCategory.Clear();
            
            var exercises = this.Exercises.Where(e => e.Category == this.SelectedCategory?.ID);

            foreach (var exercise in exercises)
            {
                var vm = new ExerciseViewModel(exercise);
                vm.AddExerciseToTrainingExecuted += this.OnExerciseAddExerciseToTrainingExecuted;
                vm.DeleteExerciseExecuted += this.OnExerciseDeleteExerciseToTrainingExecuted;
                vm.ExerciseChanged += this.OnExerciseChanged;
                this.ExercisesFilteredByCategory.Add(vm);
            }
        }

        private void AddExercise()
        {
            var id = GetLowestEmptyExerciseId();
            this.Exercises.Add(
                new Exercise { ID = id, Name = NEW_EXERCISE_NAME, Active = true, Scores = 10, Category = this.SelectedCategory.ID });
            this.RefreshExercisesFilteredByCategory();
            this.HasUnsavedExercises = true;
        }

        private uint GetLowestEmptyExerciseId()
        {
            for (uint i = 0; i <= uint.MaxValue; i++)
            {
                if (this.Exercises.Any(e => e.ID == i) == false
                    && DataInterface.GetInstance().GetData<Exercise>().Any(e => e.ID == i) == false)
                {
                    return i;
                }
            }

            throw new OverflowException("No more Exercise ID's available.");
        }

        private void ExportExercises()
        {
            var path = this.fileDialogService.SaveFile(
                System.AppDomain.CurrentDomain.BaseDirectory,
                "XML files (*.xml)|*.xml",
                1,
                out bool okClicked);

            if (okClicked == false)
            {
                return;
            }

            if (path == null)
            {
                this.messageBoxService.ShowMessage(EXPORT_ERROR_TEXT, EXPORT_ERROR_TEXT, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.LoadCategoryBuffer();
            this.LoadExerciseBuffer();

            if (DataInterface.GetInstance().ExportExerciseSet(path) == false)
            {
                this.messageBoxService.ShowMessage(EXPORT_ERROR_TEXT, EXPORT_ERROR_TEXT, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ImportExercises()
        {
            var path = this.fileDialogService.OpenFile(
                System.AppDomain.CurrentDomain.BaseDirectory, 
                "XML files (*.xml)|*.xml", 
                1,
                out bool okClicked);

            if (okClicked == false)
            {
                return;
            }

            if (path == null)
            {
                this.messageBoxService.ShowMessage(IMPORT_ERROR_TEXT, IMPORT_ERROR_TEXT, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DataInterface.GetInstance().ImportExerciseSet(path))
            {
                CategoriesValidator.Validate();
                ExercisesValidator.Validate();
                this.LoadCategoryBuffer();
                this.LoadExerciseBuffer();
                return;
            }

            this.messageBoxService.ShowMessage(IMPORT_ERROR_TEXT, IMPORT_ERROR_TEXT, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void LoadCategoryBuffer()
        {
            var savedSelectedCategoryId = this.SelectedCategory?.ID;
            var savedCategories = DataInterface.GetInstance().GetData<Category>();
            this.Categories.ResetSubscriptions(); // fraglich
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
            var savedExercises = DataInterface.GetInstance().GetData<Exercise>();
            this.Exercises.ResetSubscriptions();
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
                nameof(this.ActiveCategories),
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

        private void OnExerciseAddExerciseToTrainingExecuted(object sender, ExerciseEventArgs e)
        {
            this.InvokeAddExerciseToTrainingExecuted(e.Exercise);
        }

        private void OnExerciseDeleteExerciseToTrainingExecuted(object sender, ExerciseEventArgs e)
        {
            this.Exercises.Remove(e.Exercise);
            this.RefreshExercisesFilteredByCategory();
            this.HasUnsavedExercises = true;
        }

        private void OnExerciseChanged(object sender, ExerciseEventArgs e)
        {
            this.HasUnsavedExercises = true;
        }

        public void InvokeAddExerciseToTrainingExecuted(Exercise exercise)
        {
            this.AddExerciseToTrainingExecuted.Invoke(this, new ExerciseEventArgs(exercise));
        }

        private void SaveCategories()
        {
            var savedCategories = DataInterface.GetInstance().GetData<Category>();

            foreach (var category in this.Categories)
            {
                var savedCategory = savedCategories.Where(c => c.ID == category.ID).FirstOrDefault();
                if (savedCategory != null)
                {
                    category.CopyValuesTo(savedCategory);
                }                
            }

            var result = DataInterface.GetInstance().SaveData<Category>();
            if (result == false)
            {
                this.messageBoxService.ShowMessage(SAVING_ERROR_TEXT, SAVING_ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.InvokePropertiesChanged(nameof(this.SelectedCategory), nameof(this.ActiveCategories));
            this.HasUnsavedCategories = false;
        }

        private void SaveExercises()
        {
            UpdateSavedExercises();

            var result = DataInterface.GetInstance().SaveData<Exercise>();
            if (result == false)
            {
                this.messageBoxService.ShowMessage(SAVING_ERROR_TEXT, SAVING_ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.HasUnsavedExercises = false;
        }

        private void UpdateSavedExercises()
        {
            var savedExercises = DataInterface.GetInstance().GetData<Exercise>();

            foreach (var exercise in this.Exercises)
            {
                var savedExercise = savedExercises.Where(e => e.ID == exercise.ID).FirstOrDefault();
                if (savedExercise != null)
                {
                    exercise.CopyValuesTo(savedExercise);
                }

                if (savedExercises.Any(e => e.ID == exercise.ID) == false)
                {
                    savedExercises.Add(exercise);
                }
            }

            var savedExercisesToRemove = new List<Exercise>();

            foreach (var savedExercise in savedExercises)
            {
                if (this.Exercises.Any(e => e.ID == savedExercise.ID) == false
                    && savedExercisesToRemove.Contains(savedExercise) == false)
                {
                    savedExercisesToRemove.Add(savedExercise);
                }
            }

            foreach (var savedExercise in savedExercisesToRemove)
            {
                savedExercises.Remove(savedExercise);
            }
        }

        private void SetDefaults()
        {
            var result = this.messageBoxService.ShowMessage(RESET_TEXT, RESET_CAPTION, MessageBoxButton.YesNo, MessageBoxImage.Warning);

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
