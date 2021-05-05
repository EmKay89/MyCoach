using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.ViewModel.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel
{
    public class TrainingScheduleOverviewViewModel : BaseViewModel
    {
        private Category selectedCategory;
        private int selectedCategoryListIndex;
        private uint maxScoreOrGoal;
        private List<Month> monthsInSchedule;

        public TrainingScheduleOverviewViewModel()
        {
            this.Elements = new ObservableCollection<OverviewElementViewModel>();
            this.AvailableCategories = new ObservableCollection<Category>();
            this.AvailableCategoryListItems = new ObservableCollection<string>();
            DataInterface.GetInstance().GetData<TrainingSchedule>().First().PropertyChanged += this.OnScheduleChanged;
            DataInterface.GetInstance().GetData<Category>().CollectionChanged += this.OnCategoriesChanged;
            this.UpdateAvailableCategories();
        }

        public ObservableCollection<OverviewElementViewModel> Elements { get; }

        public ObservableCollection<Category> AvailableCategories { get; }

        public ObservableCollection<string> AvailableCategoryListItems { get; }

        public Category SelectedCategory
        {
            get => this.selectedCategory;

            set
            {
                if (value == this.selectedCategory)
                {
                    return;
                }

                if (this.selectedCategory != null)
                {
                    this.selectedCategory.PropertyChanged -= this.OnSelectedCategoryChanged;
                }

                this.selectedCategory = value;

                if (this.selectedCategory != null)
                {
                    this.selectedCategory.PropertyChanged += this.OnSelectedCategoryChanged;
                }

                this.InvokePropertyChanged();
                this.UpdateChart();
            }
        }

        public int SelectedCategoryListIndex
        {
            get => this.selectedCategoryListIndex;

            set
            {
                if (value == this.selectedCategoryListIndex)
                {
                    return;
                }

                if (value < 0)
                {
                    this.selectedCategoryListIndex = 0;
                }
                else
                {
                    this.selectedCategoryListIndex = value;
                }

                this.UpdateSelectedCategory();
                this.InvokePropertyChanged();
            }
        }

        public uint MaxScoreOrGoal
        {
            get => this.maxScoreOrGoal;

            set
            {
                if (value == this.maxScoreOrGoal)
                {
                    return;
                }

                this.maxScoreOrGoal = value;
                this.InvokePropertiesChanged(
                    nameof(this.MaxScoreOrGoal), 
                    nameof(this.MaxScoreOrGoal75), 
                    nameof(this.MaxScoreOrGoal50), 
                    nameof(this.MaxScoreOrGoal25));
                this.UpdateElementsMaxScores();
            }
        }

        public uint MaxScoreOrGoal75 => (uint)(this.MaxScoreOrGoal * 0.75);

        public uint MaxScoreOrGoal50 => (uint)(this.MaxScoreOrGoal * 0.5);

        public uint MaxScoreOrGoal25 => (uint)(this.MaxScoreOrGoal * 0.25);

        private List<Month> MonthsInSchedule
        {
            get => this.monthsInSchedule;

            set
            {
                if (this.monthsInSchedule != null)
                {
                    foreach (var month in this.monthsInSchedule)
                    {
                        month.PropertyChanged -= this.OnMonthInScheduleChanged;
                    }
                }

                this.monthsInSchedule = value;

                foreach (var month in monthsInSchedule)
                {
                    month.PropertyChanged += this.OnMonthInScheduleChanged;
                }
            }
        }

        private void UpdateAvailableCategories()
        {
            this.AvailableCategories.Clear();
            this.AvailableCategoryListItems.Clear();
            var allCategories = DataInterface.GetInstance().GetData<Category>();

            foreach (var category in allCategories)
            {
                if (category.Active && category.Type == Defines.ExerciseType.Training)
                {
                    this.AvailableCategories.Add(category);
                }
            }

            foreach (var category in this.AvailableCategories)
            {
                this.AvailableCategoryListItems.Add(category.Name);
            }

            this.AvailableCategoryListItems.Add("Gesamt");
            this.AvailableCategories.Add(null);
            this.SelectedCategoryListIndex = 0;
            this.UpdateSelectedCategory();
        }

        private void UpdateChart()
        {
            this.Elements.Clear();
            var schedule = DataInterface.GetInstance().GetData<TrainingSchedule>().FirstOrDefault();
            this.MonthsInSchedule = DataInterface.GetInstance().GetData<Month>().Where(
                m => (int)m.Number <= schedule.Duration && m.Number != Defines.MonthNumber.Current).ToList(); // asdfasdf

            foreach (var month in this.MonthsInSchedule)
            {
                this.Elements.Add(new OverviewElementViewModel(month, this.SelectedCategory));
            }

            this.UpdateMaxScoreOrGoal();
        }

        private void UpdateMaxScoreOrGoal()
        {
            if (this.SelectedCategory == null)
            {
                this.MaxScoreOrGoal = GetMaxTotalScoreOrTotalGoalValueOfMonthsInTrainingSchedule();
                return;
            }

            this.MaxScoreOrGoal = this.MonthsInSchedule.MaxScoreOrGoal(this.SelectedCategory.ID);
        }

        private uint GetMaxTotalScoreOrTotalGoalValueOfMonthsInTrainingSchedule()
        {
            uint value = 0;

            foreach (var month in this.MonthsInSchedule)
            {
                uint totalScoresForMonth = 0;

                foreach (var category in this.AvailableCategories)
                {
                    if (category == null)
                    {
                        continue;
                    }

                    totalScoresForMonth += month.GetScores(category.ID);
                }

                value = value < totalScoresForMonth ? totalScoresForMonth : value;
                value = value < month.TotalGoal ? month.TotalGoal : value;
            }

            return value;
        }

        private void UpdateElementsMaxScores()
        {
            foreach (var element in this.Elements)
            {
                element.MaxScoreOrGoal = this.MaxScoreOrGoal;
            }
        }

        private void UpdateSelectedCategory()
        {
            if (this.SelectedCategoryListIndex + 1 <= this.AvailableCategories.Count)
            {
                this.SelectedCategory = this.AvailableCategories[this.SelectedCategoryListIndex];
            }
        }

        private void OnCategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateAvailableCategories();
        }

        private void OnScheduleChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateChart();
        }

        private void OnMonthInScheduleChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateMaxScoreOrGoal();
        }

        private void OnSelectedCategoryChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Category.Name):
                    this.InvokePropertyChanged(nameof(this.SelectedCategory));
                    break;
                case nameof(Category.Active):
                    this.UpdateAvailableCategories();
                    break;
            }
        }
    }
}
