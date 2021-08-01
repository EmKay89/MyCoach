using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.DataHandling.DataTransferObjects.CollectionExtensions;
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
            DataInterface.GetInstance().GetData<TrainingSchedule>().First().PropertyChanged += this.OnScheduleChanged;
            var categories = DataInterface.GetInstance().GetData<Category>();
            categories.CollectionChanged += this.OnCategoriesChanged;

            foreach (var category in categories)
            {
                category.PropertyChanged += this.OnCategoryChanged;
            }

            this.UpdateAvailableCategories();
            this.UpdateSelectedCategory();
        }

        public ObservableCollection<OverviewElementViewModel> Elements { get; } = new ObservableCollection<OverviewElementViewModel>();

        public ObservableCollection<Category> AvailableCategories { get; } = new ObservableCollection<Category>();

        public ObservableCollection<string> AvailableCategoryListItems { get; } = new ObservableCollection<string>();

        private Category SelectedCategory
        {
            get => this.selectedCategory;

            set
            {
                if (value == this.selectedCategory)
                {
                    return;
                }

                this.selectedCategory = value;
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

            private set
            {
                // Elements must be updated before equality check, because they may have been
                // cleared and reloaded and this MaxScoreOrGoal may have been unchanged.
                this.UpdateElementsMaxScores();

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

                if (this.monthsInSchedule != null)
                {
                    foreach (var month in monthsInSchedule)
                    {
                        month.PropertyChanged += this.OnMonthInScheduleChanged;
                    }
                }
            }
        }

        private void UpdateAvailableCategories()
        {
            this.AvailableCategories.Clear();
            Utilities.GetActiveTrainingCategories().Foreach(c => this.AvailableCategories.Add(c));
            this.AvailableCategories.Add(null);
            UpdateAvailableCategoryListItems(true);
        }

        private void UpdateAvailableCategoryListItems(bool resetSelectedIndex)
        {
            this.AvailableCategoryListItems.Clear();

            foreach (var category in this.AvailableCategories)
            {
                if (category != null)
                {
                    this.AvailableCategoryListItems.Add(category.Name);
                }
            }

            this.AvailableCategoryListItems.Add("Gesamt");

            if (resetSelectedIndex)
            {
                this.SelectedCategoryListIndex = 0;
            }
        }

        private void UpdateChart()
        {
            this.Elements.Clear();
            var schedule = DataInterface.GetInstance().GetData<TrainingSchedule>().FirstOrDefault();
            this.MonthsInSchedule = DataInterface.GetInstance().GetData<Month>().Where(
                m => (int)m.Number <= schedule.Duration && m.Number != Defines.MonthNumber.Current).ToList();

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
            if (e.OldItems != null)
            {
                foreach (var category in e.OldItems)
                {
                    ((Category)category).PropertyChanged -= this.OnCategoryChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (var category in e.NewItems)
                {
                    ((Category)category).PropertyChanged += this.OnCategoryChanged;
                }
            }

            this.UpdateAvailableCategories();
        }

        private void OnCategoryChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Category.Active))
            {
                this.UpdateAvailableCategories();
            }
            else if (e.PropertyName == nameof(Category.Name))
            {
                this.UpdateAvailableCategoryListItems(false);
            }
        }

        private void OnScheduleChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateChart();
        }

        private void OnMonthInScheduleChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateMaxScoreOrGoal();
        }
    }
}
