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
        private uint maxScoreOrGoal;
        private ObservableCollection<Month> monthsInSchedule;

        public TrainingScheduleOverviewViewModel()
        {
            this.Elements = new ObservableCollection<OverviewElementViewModel>();
            DataInterface.GetInstance().GetData<TrainingSchedule>().FirstOrDefault().PropertyChanged += this.OnScheduleChanged;
            DataInterface.GetInstance().GetData<Category>().CollectionChanged += this.OnCategoriesChanged;
            this.UpdateAvailableCategories();
        }

        public ObservableCollection<OverviewElementViewModel> Elements { get; }

        public ObservableCollection<Category> AvailableCategories { get; }

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
                this.selectedCategory.PropertyChanged += this.OnSelectedCategoryChanged;
                this.InvokePropertyChanged();
                this.UpdateElements();
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
                this.InvokePropertyChanged();
                this.UpdateElementsMaxScores();
            }
        }

        private ObservableCollection<Month> MonthsInSchedule 
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
            var categories = DataInterface.GetInstance().GetData<Category>();

            foreach (var category in categories)
            {
                if (category.Active)
                {
                    this.AvailableCategories.Add(category);
                }
            }

            this.SelectedCategory = this.AvailableCategories.FirstOrDefault();
        }

        private void UpdateElements()
        {
            this.Elements.Clear();
            var schedule = DataInterface.GetInstance().GetData<TrainingSchedule>().FirstOrDefault();
            this.MonthsInSchedule = (ObservableCollection<Month>)DataInterface.GetInstance().GetData<Month>().Where(
                m => (int)m.Number < schedule.Duration && m.Number != Defines.MonthNumber.Current);

            foreach (var month in this.MonthsInSchedule)
            {
                this.Elements.Add(new OverviewElementViewModel(month, this.SelectedCategory));
            }

            this.MaxScoreOrGoal = this.MonthsInSchedule.MaxScoreOrGoal(this.SelectedCategory.ID);
        }

        private void UpdateElementsMaxScores()
        {
            foreach (var element in this.Elements)
            {
                element.MaxScoreOrGoal = this.MaxScoreOrGoal;
            }
        }

        private void OnCategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateAvailableCategories();
        }

        private void OnScheduleChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateElements();
        }

        private void OnMonthInScheduleChanged(object sender, PropertyChangedEventArgs e)
        {
            this.MaxScoreOrGoal = this.MonthsInSchedule.MaxScoreOrGoal(this.SelectedCategory.ID);
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
