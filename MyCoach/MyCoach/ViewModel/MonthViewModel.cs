using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel
{
    public class MonthViewModel : BaseViewModel
    {
        private Month month;
        private DateTime startDate;
        private ObservableCollection<Category> categories;

        public MonthViewModel(DateTime startDate, Month month)
        {            
            this.month = month;
            this.startDate = startDate;
            this.categories = DataInterface.GetInstance().GetDataTransferObjects<Category>();
            this.categories.CollectionChanged += this.OnCategoriesChanged;
            this.MonthCategoryDetailViewModels = new ObservableCollection<MonthCategoryDetailViewModel>();
            this.OnCategoriesChanged(this, new EventArgs());
        }

        public string Description => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.startDate.Month) + " " + startDate.Year.ToString();

        public ObservableCollection<MonthCategoryDetailViewModel> MonthCategoryDetailViewModels { get; }

        public string TotalScores
        {
            get
            {
                return this.month.TotalGoal > 0
                    ? $"{this.scoresOfVisibleProperties} von {this.month.TotalGoal}"
                    : this.scoresOfVisibleProperties.ToString();
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

                if (this.scoresOfVisibleProperties < this.month.TotalGoal)
                {
                    return (uint)(this.scoresOfVisibleProperties / this.month.TotalGoal);
                }

                return 100;
            }
        }

        private int scoresOfVisibleProperties => this.MonthCategoryDetailViewModels.Sum(d => d.Scores);

        private void OnCategoriesChanged(object sender, EventArgs e)
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
