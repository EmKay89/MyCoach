using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.DataHandling.DataTransferObjects.CollectionExtensions;
using MyCoach.Defines;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.TrainingGeneration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCoach.ViewModel
{
    public class TrainingViewModel : BaseViewModel
    {
        private Category categoryInFocus;
        private ushort selectedLapCount = 2;
        private ushort selectedExercisesPerLapCount = 2;
        private bool trainingActive;
        private TrainingMode trainingMode;
        private string modeExplanation;
        
        public TrainingViewModel()
        {
            this.Categories = DataInterface.GetInstance().GetData<Category>();
            this.Categories.CollectionChanged += this.OnCategoriesChanged;
            this.StartTrainingCommand = new RelayCommand(this.StartTraining, this.CanStartTraining);
            this.TrainingMode = TrainingMode.CircleTraining;
        }

        public const string DESCRIPTION_CIRCLETRAINING = "In diesem Modus wird pro Runde je eine Übung der als aktiv markierten Kategorien ins Training eingeplant. " +
            "Vor und nach dem eigentlichen Zirkeltraining folgt je ein Block Auf- und Abwärmübungen.";
        public const string DESCRIPTION_FOCUSTRAINING = "In diesem Modus werden nur Übungen der ausgewählten Kategorie ins Training eingeplant.";
        public const string DESCRIPTION_USERDEFINEDTRAINING = "In diesem Modus kann ein Training selbst aus Übungen aus dem gleichnamigen Menü auf der linken Seite " +
            "zusammengestellt werden.";

        public List<Category> ActiveCategories
        {
            get
            {
                List<Category> categories = new List<Category>();

                categories.AddRange(this.Categories.Where(c => c.Active));

                if (this.CategoryInFocus != null && categories.Where(c => c.ID == CategoryInFocus.ID).Any() == false)
                {
                    this.CategoryInFocus = null;
                }

                return categories;
            }
        }

        public ObservableCollection<Category> Categories { get; }

        public ObservableCollection<ushort> NumbersOneToTen { get; } = new ObservableCollection<ushort> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public ObservableCollection<ushort> NumbersOneToFour { get; } = new ObservableCollection<ushort> { 1, 2, 3, 4 };

        public Dictionary<TrainingMode, string> ModesWithCaption { get; } = new Dictionary<TrainingMode, string>
        {
            { TrainingMode.CircleTraining, "Zirkeltraining" },
            { TrainingMode.FocusTraining, "Fokustraining" },
            { TrainingMode.UserDefinedTraining, "Benutzerdefiniertes Training" }
        };

        public Training Training { get; } = new Training();

        public TrainingMode TrainingMode
        {
            get { return this.trainingMode; }
            set
            {
                if (value == this.trainingMode)
                {
                    return;
                }

                this.trainingMode = value;
                this.InvokePropertiesChanged(
                    nameof(this.TrainingMode),
                    nameof(this.CircleTrainingElementsVisible),
                    nameof(this.FocusTrainingElementsVisible),
                    nameof(this.CircleOrFocusTrainingElementsVisible));
                this.OnTrainingModeChanged();
            }
        }

        public string ModeExplanation
        {
            get { return this.modeExplanation; }
            set
            {
                if (value == this.modeExplanation)
                {
                    return;
                }

                this.modeExplanation = value;
                this.InvokePropertyChanged();
            }
        }

        public Category CategoryInFocus
        {
            get { return this.categoryInFocus; }
            set 
            {
                if (value == this.categoryInFocus)
                {
                    return;
                }

                this.categoryInFocus = value;
                this.InvokePropertiesChanged(
                    "CategoryInFocus");
            }
        }

        public string CategoryWarmUpName => this.Categories.GetName(ExerciseCategory.WarmUp);

        public bool CategoryWarmUpActive => this.Categories.IsActive(ExerciseCategory.WarmUp);

        public bool CategoryWarmUpEnabledForTraining { get; set; } = true;

        public string Category1Name => this.Categories.GetName(ExerciseCategory.Category1);

        public bool Category1Active => this.Categories.IsActive(ExerciseCategory.Category1);

        public bool Category1EnabledForTraining { get; set; } = true;

        public string Category2Name => this.Categories.GetName(ExerciseCategory.Category2);

        public bool Category2Active => this.Categories.IsActive(ExerciseCategory.Category2);

        public bool Category2EnabledForTraining { get; set; } = true;

        public string Category3Name => this.Categories.GetName(ExerciseCategory.Category3);

        public bool Category3Active => this.Categories.IsActive(ExerciseCategory.Category3);

        public bool Category3EnabledForTraining { get; set; } = true;

        public string Category4Name => this.Categories.GetName(ExerciseCategory.Category4);

        public bool Category4Active => this.Categories.IsActive(ExerciseCategory.Category4);

        public bool Category4EnabledForTraining { get; set; } = true;

        public string Category5Name => this.Categories.GetName(ExerciseCategory.Category5);

        public bool Category5Active => this.Categories.IsActive(ExerciseCategory.Category5);

        public bool Category5EnabledForTraining { get; set; } = true;

        public string Category6Name => this.Categories.GetName(ExerciseCategory.Category6);

        public bool Category6Active => this.Categories.IsActive(ExerciseCategory.Category6);

        public bool Category6EnabledForTraining { get; set; } = true;

        public string Category7Name => this.Categories.GetName(ExerciseCategory.Category7);

        public bool Category7Active => this.Categories.IsActive(ExerciseCategory.Category7);

        public bool Category7EnabledForTraining { get; set; } = true;

        public string Category8Name => this.Categories.GetName(ExerciseCategory.Category8);

        public bool Category8Active => this.Categories.IsActive(ExerciseCategory.Category8);

        public bool Category8EnabledForTraining { get; set; } = true;

        public string CategoryCoolDownName => this.Categories.GetName(ExerciseCategory.CoolDown);

        public bool CategoryCoolDownActive => this.Categories.IsActive(ExerciseCategory.CoolDown);

        public bool CategoryCoolDownEnabledForTraining { get; set; } = true;
        
        public ushort SelectedLapCount
        {
            get => this.selectedLapCount;
            set
            {
                if (this.selectedLapCount == value)
                {
                    return;
                }

                this.selectedLapCount = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort SelectedExercisesPerLapCount
        {
            get => this.selectedExercisesPerLapCount;
            set
            {
                if (this.selectedExercisesPerLapCount == value)
                {
                    return;
                }

                this.selectedExercisesPerLapCount = value;
                this.InvokePropertyChanged();
            }
        }

        public RelayCommand StartTrainingCommand { get; }

        public bool TrainingActive
        {
            get => this.trainingActive;

            set
            {
                if (this.trainingActive == value)
                {
                    return;
                }

                this.trainingActive = value;
                this.InvokePropertiesChanged(
                    "TrainingActive",
                    "TrainingSettingsEnabled");
            }
        }

        public bool TrainingSettingsEnabled => !this.TrainingActive;

        public bool CircleTrainingElementsVisible => this.TrainingMode == TrainingMode.CircleTraining;

        public bool FocusTrainingElementsVisible => this.TrainingMode == TrainingMode.FocusTraining;

        public bool CircleOrFocusTrainingElementsVisible => this.TrainingMode != TrainingMode.UserDefinedTraining;

        private void StartTraining()
        {
            this.TrainingActive = !this.TrainingActive;
        }
        
        private bool CanStartTraining()
        {
            if (this.Training.Count > 0)
            {
                return true;
            }

            if (this.TrainingMode == TrainingMode.CircleTraining && this.GetCategoriesEnabledForTraining().Any())
            {
                return true;
            }

            if (this.TrainingMode == TrainingMode.FocusTraining && this.CategoryInFocus != null)
            {
                return true;
            }

            return false;
        }

        private List<Category> GetCategoriesEnabledForTraining()
        {
            var categories = new List<Category>();

            if (this.CategoryWarmUpActive && this.CategoryWarmUpEnabledForTraining)
            {
                categories.Add(this.Categories.Where(c => c.ID == ExerciseCategory.WarmUp).First());
            }

            if (this.Category1Active && this.Category1EnabledForTraining)
            {
                categories.Add(this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First());
            }

            if (this.Category2Active && this.Category2EnabledForTraining)
            {
                categories.Add(this.Categories.Where(c => c.ID == ExerciseCategory.Category2).First());
            }

            if (this.Category3Active && this.Category3EnabledForTraining)
            {
                categories.Add(this.Categories.Where(c => c.ID == ExerciseCategory.Category3).First());
            }

            if (this.Category4Active && this.Category4EnabledForTraining)
            {
                categories.Add(this.Categories.Where(c => c.ID == ExerciseCategory.Category4).First());
            }

            if (this.Category5Active && this.Category5EnabledForTraining)
            {
                categories.Add(this.Categories.Where(c => c.ID == ExerciseCategory.Category5).First());
            }

            if (this.Category6Active && this.Category6EnabledForTraining)
            {
                categories.Add(this.Categories.Where(c => c.ID == ExerciseCategory.Category6).First());
            }

            if (this.Category7Active && this.Category7EnabledForTraining)
            {
                categories.Add(this.Categories.Where(c => c.ID == ExerciseCategory.Category7).First());
            }

            if (this.Category8Active && this.Category8EnabledForTraining)
            {
                categories.Add(this.Categories.Where(c => c.ID == ExerciseCategory.Category8).First());
            }

            if (this.CategoryCoolDownActive && this.CategoryCoolDownEnabledForTraining)
            {
                categories.Add(this.Categories.Where(c => c.ID == ExerciseCategory.CoolDown).First());
            }

            return categories;
        }

        private void OnCategoriesChanged(object sender, EventArgs e)
        {
            this.InvokePropertiesChanged(
                "ActiveCategories",
                "CategoryWarmUpName",
                "CategoryWarmUpActive",
                "Category1Name",
                "Category1Active",
                "Category2Name",
                "Category2Active",
                "Category3Name",
                "Category3Active",
                "Category4Name",
                "Category4Active",
                "Category5Name",
                "Category5Active",
                "Category6Name",
                "Category6Active",
                "Category7Name",
                "Category7Active",
                "Category8Name",
                "Category8Active",
                "CategoryCoolDownName",
                "CategoryCoolDownActive"
                );
        }

        private void OnTrainingModeChanged()
        {
            switch (this.TrainingMode)
            {
                case TrainingMode.CircleTraining:
                    this.ModeExplanation = DESCRIPTION_CIRCLETRAINING;
                    break;
                case TrainingMode.FocusTraining:
                    this.ModeExplanation = DESCRIPTION_FOCUSTRAINING;
                    break;
                case TrainingMode.UserDefinedTraining:
                    this.ModeExplanation = DESCRIPTION_USERDEFINEDTRAINING;
                    break;
            }
        }
    }
}
