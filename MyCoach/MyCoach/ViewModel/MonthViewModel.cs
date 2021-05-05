using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel
{
    public class MonthViewModel : BaseViewModel
    {
        private readonly Month month;
        private readonly DateTime startDate;
        private readonly ObservableCollection<Category> categories;

        public MonthViewModel(Month month)
        {            
            this.month = month;
            this.month.PropertyChanged += this.OnMonthChanged;
            this.categories = DataInterface.GetInstance().GetData<Category>();
            this.categories.CollectionChanged += this.OnCategoriesChanged;
            var schedule = DataInterface.GetInstance().GetData<TrainingSchedule>().FirstOrDefault();
            this.startDate = this.month.GetStartDateFromSchedule(schedule);
            this.MonthCategoryDetailViewModels = new ObservableCollection<MonthCategoryDetailViewModel>();
            this.UpdateMonthCategoryDetailViewModels();
        }

        public string Description => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.startDate.Month) + " " + startDate.Year.ToString();

        public ObservableCollection<MonthCategoryDetailViewModel> MonthCategoryDetailViewModels { get; }

        public string TotalScores
        {
            get
            {
                return this.month.TotalGoal > 0
                    ? $"{this.ScoresOfVisibleProperties} von {this.month.TotalGoal}"
                    : this.ScoresOfVisibleProperties.ToString();
            }
        }

        public uint TotalPercentage
        {
            get
            {
                if (this.month.TotalGoal == 0)
                {
                    return 0;
                }

                if (this.ScoresOfVisibleProperties < this.month.TotalGoal)
                {
                    return (uint)(this.ScoresOfVisibleProperties * 100 / this.month.TotalGoal);
                }

                return 100;
            }
        }

        private int ScoresOfVisibleProperties => this.MonthCategoryDetailViewModels.Sum(d => d.Scores);

        private void OnMonthChanged(object sender, PropertyChangedEventArgs e)
        {
            this.InvokePropertiesChanged(
                nameof(this.TotalPercentage),
                nameof(this.TotalScores));
        }

        private void OnCategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateMonthCategoryDetailViewModels();
        }

        private void UpdateMonthCategoryDetailViewModels()
        {
            this.MonthCategoryDetailViewModels.Clear();
            foreach (var category in this.categories)
            {
                if (category.Active && category.Type == ExerciseType.Training)
                {
                    this.MonthCategoryDetailViewModels.Add(new MonthCategoryDetailViewModel(category, this.month));
                }
            }
        }
    }
}
