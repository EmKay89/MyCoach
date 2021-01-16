using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using MyCoach.ViewModel.Commands;
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
        private int selectedLapCount = 2;
        private bool trainingActive;

        public TrainingViewModel()
        {
            this.Categories = DataInterface.GetInstance().GetDataTransferObjects<Category>();
            this.Categories.CollectionChanged += this.OnCategoriesChanged;
            this.StartTrainingCommand = new StartTraingingCommand(this);
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
                    c => c.Active && c.ID != (int)ExerciseCategory.WarmUp && c.ID != (int)ExerciseCategory.CoolDown));

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
                this.InvokePropertyChanged();
            }
        }

        public string CategoryWarmUpName => GetCategoryName(ExerciseCategory.WarmUp);

        public bool CategoryWarmUpActive => GetCategoryActive(ExerciseCategory.WarmUp);

        public string Category1Name => GetCategoryName(ExerciseCategory.Category1);

        public bool Category1Active => GetCategoryActive(ExerciseCategory.Category1);

        public string Category2Name => GetCategoryName(ExerciseCategory.Category2);

        public bool Category2Active => GetCategoryActive(ExerciseCategory.Category2);

        public string Category3Name => GetCategoryName(ExerciseCategory.Category3);

        public bool Category3Active => GetCategoryActive(ExerciseCategory.Category3);

        public string Category4Name => GetCategoryName(ExerciseCategory.Category4);

        public bool Category4Active => GetCategoryActive(ExerciseCategory.Category4);

        public string Category5Name => GetCategoryName(ExerciseCategory.Category5);

        public bool Category5Active => GetCategoryActive(ExerciseCategory.Category5);

        public string Category6Name => GetCategoryName(ExerciseCategory.Category6);

        public bool Category6Active => GetCategoryActive(ExerciseCategory.Category6);

        public string Category7Name => GetCategoryName(ExerciseCategory.Category7);

        public bool Category7Active => GetCategoryActive(ExerciseCategory.Category7);

        public string Category8Name => GetCategoryName(ExerciseCategory.Category8);

        public bool Category8Active => GetCategoryActive(ExerciseCategory.Category8);

        public string CategoryCoolDownName => GetCategoryName(ExerciseCategory.CoolDown);

        public bool CategoryCoolDownActive => GetCategoryActive(ExerciseCategory.CoolDown);

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

        public StartTraingingCommand StartTrainingCommand { get; }

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
                this.TrainingActiveChanged?.Invoke(this, new EventArgs());
                this.InvokePropertyChanged();
            }
        }

        public event EventHandler TrainingActiveChanged;

        private bool GetCategoryActive(ExerciseCategory category) => this.Categories?.Where(c => c.ID == (int)category).FirstOrDefault()?.Active ?? false;

        private string GetCategoryName(ExerciseCategory category) => this.Categories?.Where(c => c.ID == (int)category).FirstOrDefault()?.Name;

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
