using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
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
            foreach (var category in categories)
            {
                category.PropertyChanged += this.OnCategoryChanged;
            }

            var schedule = DataInterface.GetInstance().GetData<TrainingSchedule>().FirstOrDefault();
            this.startDate = this.month.StartDate;
            this.UpdateMonthCategoryDetailViewModels();
        }

        public string Description => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.startDate.Month) + " " + startDate.Year.ToString();

        public ObservableCollection<MonthCategoryDetailViewModel> MonthCategoryDetailViewModels { get; } 
            = new ObservableCollection<MonthCategoryDetailViewModel>();

        public string TotalScores
        {
            get
            {
                return this.Month.TotalGoal > 0
                    ? $"{this.ScoresOfVisibleProperties} von {this.Month.TotalGoal}"
                    : this.ScoresOfVisibleProperties.ToString();
            }
        }

        public uint TotalPercentage
        {
            get
            {
                if (this.Month.TotalGoal == 0)
                {
                    return 0;
                }

                if (this.ScoresOfVisibleProperties < this.Month.TotalGoal)
                {
                    return (uint)(this.ScoresOfVisibleProperties * 100 / this.Month.TotalGoal);
                }

                return 100;
            }
        }

        public Month Month => this.month;

        private int ScoresOfVisibleProperties => this.MonthCategoryDetailViewModels.Sum(d => d.Scores);

        private void OnMonthChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Month.TotalScores) ||
                e.PropertyName == nameof(Month.TotalGoal))
            {
                this.InvokePropertiesChanged(
                    nameof(this.TotalPercentage),
                    nameof(this.TotalScores));
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

            this.UpdateMonthCategoryDetailViewModels();
        }

        private void OnCategoryChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Category.Active))
            {
                this.UpdateMonthCategoryDetailViewModels();
            }
        }

        private void UpdateMonthCategoryDetailViewModels()
        {
            this.MonthCategoryDetailViewModels.Clear();
            foreach (var category in this.categories)
            {
                if (category.Active && category.Type == ExerciseType.Training)
                {
                    this.MonthCategoryDetailViewModels.Add(new MonthCategoryDetailViewModel(category, this.Month));
                }
            }
        }
    }
}
