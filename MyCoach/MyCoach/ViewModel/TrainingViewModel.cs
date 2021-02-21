using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.ModelExtensions;
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
        private Category categoryInFocus = new Category { ID = 0, Name = "- keine Auswahl -" };
        private int selectedLapCount = 2;
        private bool trainingActive;

        public TrainingViewModel()
        {
            this.Categories = DataInterface.GetInstance().GetDataTransferObjects<Category>();
            this.Categories.CollectionChanged += this.OnCategoriesChanged;
            this.StartTrainingCommand = new RelayCommand(() => { this.TrainingActive = this.TrainingActive ? false : true; });
        }

        public List<Category> ActiveCategories
        {
            get
            {
                List<Category> categories = new List<Category>
                {
                    new Category { ID = 0, Name = "- keine Auswahl -" }
                };

                categories.AddRange(this.Categories.Where(
                    c => c.Active && c.Type == ExerciseType.Training));

                return categories;
            }
        }

        public ObservableCollection<Category> Categories { get; }

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
                    "CategoryInFocus",
                    "TrainingSettingsFocusEnabled");
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

        public ObservableCollection<int> Laps { get; } = new ObservableCollection<int>() { 1, 2, 3, 4 };

        public int SelectedLapCount
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
                    "TrainingSettingsEnabled",
                    "TrainingSettingsFocusEnabled");
            }
        }

        public bool TrainingSettingsEnabled => !this.TrainingActive;

        public bool TrainingSettingsFocusEnabled => this.TrainingSettingsEnabled && this.CategoryInFocus.ToString() != "- keine Auswahl -";

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
    }
}
