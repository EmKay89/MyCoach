using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel.Defines;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace MyCoach.ViewModel
{
    public class EditMonthViewModel : BaseViewModel
    {
        private readonly Month month;
        private readonly ObservableCollection<Category> categories;
        private ScheduleEditingType scheduleEditingType;

        public EditMonthViewModel(Month month)
        {
            this.month = month;
            this.month.PropertyChanged += this.OnMonthChanged;

            this.categories = DataInterface.GetInstance().GetData<Category>();
            foreach (var category in this.categories)
            {
                category.PropertyChanged += this.OnCategoryChanged;
            }

            this.categories.CollectionChanged += this.OnCategoriesChanged;
        }

        public bool NotCurrentMonth => this.month.Number != MonthNumber.Current;

        public bool Category1ItemsVisible => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category1)?.Active == true;

        public ushort Category1Goal
        {
            get => this.month.Category1Goal;

            set 
            {
                this.month.Category1Goal = value;
                this.RecalculateScoresBasedOnScheduleEditingType();
            }
        }

        public bool Category2ItemsVisible => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category2)?.Active == true;

        public ushort Category2Goal
        {
            get => this.month.Category2Goal;

            set
            {
                this.month.Category2Goal = value;
                this.RecalculateScoresBasedOnScheduleEditingType();
            }
        }

        public bool Category3ItemsVisible => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category3)?.Active == true;

        public ushort Category3Goal
        {
            get => this.month.Category3Goal;

            set
            {
                this.month.Category3Goal = value;
                this.RecalculateScoresBasedOnScheduleEditingType();
            }
        }

        public bool Category4ItemsVisible => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category4)?.Active == true;

        public ushort Category4Goal
        {
            get => this.month.Category4Goal;

            set
            {
                this.month.Category4Goal = value;
                this.RecalculateScoresBasedOnScheduleEditingType();
            }
        }

        public bool Category5ItemsVisible => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category5)?.Active == true;

        public ushort Category5Goal
        {
            get => this.month.Category5Goal;

            set
            {
                this.month.Category5Goal = value;
                this.RecalculateScoresBasedOnScheduleEditingType();
            }
        }

        public bool Category6ItemsVisible => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category6)?.Active == true;

        public ushort Category6Goal
        {
            get => this.month.Category6Goal;

            set
            {
                this.month.Category6Goal = value;
                this.RecalculateScoresBasedOnScheduleEditingType();
            }
        }

        public bool Category7ItemsVisible => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category7)?.Active == true;

        public ushort Category7Goal
        {
            get => this.month.Category7Goal;

            set
            {
                this.month.Category7Goal = value;
                this.RecalculateScoresBasedOnScheduleEditingType();
            }
        }

        public bool Category8ItemsVisible => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category8)?.Active == true;

        public ushort Category8Goal
        {
            get => this.month.Category8Goal;

            set
            {
                this.month.Category8Goal = value;
                this.RecalculateScoresBasedOnScheduleEditingType();
            }
        }

        public uint TotalGoal
        {
            get => this.month.TotalGoal;

            set
            {
                this.month.TotalGoal = value;
                this.RecalculateScoresBasedOnScheduleEditingType();
            }
        }

        public string MonthName => this.month.Number == MonthNumber.Current
                ? string.Empty
                : this.month.StartDate.ToString("y", CultureInfo.CurrentCulture);

        public ScheduleEditingType ScheduleEditingType
        {
            get => this.scheduleEditingType;
            
            set
            {
                if (value == this.scheduleEditingType)
                {
                    return;
                }

                this.scheduleEditingType = value;
                this.InvokePropertiesChanged(
                    nameof(this.ScheduleEditingType),
                    nameof(this.SingleCategoryGoalsEnabled),
                    nameof(this.TotalCategoryGoalEnabled));
                this.RecalculateScoresBasedOnScheduleEditingType();
            }
        }

        public bool SingleCategoryGoalsEnabled => this.ScheduleEditingType != ScheduleEditingType.DivideTotal;

        public bool TotalCategoryGoalEnabled => this.ScheduleEditingType != ScheduleEditingType.SumUpTotal;

        private void RecalculateScoresBasedOnScheduleEditingType()
        {
            switch (this.ScheduleEditingType)
            {
                case ScheduleEditingType.DivideTotal:
                    this.DistributeTotalGoalToActiveCategories();
                    break;
                case ScheduleEditingType.SumUpTotal:
                    this.month.TotalGoal = this.GetSumOfActiveCategories();
                    break;
                default:
                    break;
            }
        }

        private void DistributeTotalGoalToActiveCategories()
        {
            var activeCategories = this.categories.Where(c => c.Active && c.Type == ExerciseType.Training).ToList();
            var modulo = activeCategories.Count == 0
                ? 0
                : this.month.TotalGoal % activeCategories.Count;

            var distributedGoal = activeCategories.Count == 0
                ? 0
                : this.month.TotalGoal / activeCategories.Count;

            var goalLimitedToUshort = (ushort)(distributedGoal > ushort.MaxValue ? ushort.MaxValue : distributedGoal);

            for (int i = 0; i < activeCategories.Count; i++)
            {
                if (i >= modulo
                    || goalLimitedToUshort == ushort.MaxValue)
                {
                    this.month.SetGoal(activeCategories[i].ID, goalLimitedToUshort);
                }
                else
                {
                    this.month.SetGoal(activeCategories[i].ID, (ushort)(goalLimitedToUshort + 1));
                }
            }
        }

        private uint GetSumOfActiveCategories()
        {
            uint subtotal = 0;

            if (this.categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category1)?.Active == true)
            {
                subtotal += this.month.Category1Goal;
            }

            if (this.categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category2)?.Active == true)
            {
                subtotal += this.month.Category2Goal;
            }

            if (this.categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category3)?.Active == true)
            {
                subtotal += this.month.Category3Goal;
            }

            if (this.categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category4)?.Active == true)
            {
                subtotal += this.month.Category4Goal;
            }

            if (this.categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category5)?.Active == true)
            {
                subtotal += this.month.Category5Goal;
            }

            if (this.categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category6)?.Active == true)
            {
                subtotal += this.month.Category6Goal;
            }

            if (this.categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category7)?.Active == true)
            {
                subtotal += this.month.Category7Goal;
            }

            if (this.categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category8)?.Active == true)
            {
                subtotal += this.month.Category8Goal;
            }

            return subtotal;
        }

        private void OnMonthChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.month.Category1Goal):
                    this.InvokePropertyChanged(nameof(this.Category1Goal));
                    break;
                case nameof(this.month.Category2Goal):
                    this.InvokePropertyChanged(nameof(this.Category2Goal));
                    break;
                case nameof(this.month.Category3Goal):
                    this.InvokePropertyChanged(nameof(this.Category3Goal));
                    break;
                case nameof(this.month.Category4Goal):
                    this.InvokePropertyChanged(nameof(this.Category4Goal));
                    break;
                case nameof(this.month.Category5Goal):
                    this.InvokePropertyChanged(nameof(this.Category5Goal));
                    break;
                case nameof(this.month.Category6Goal):
                    this.InvokePropertyChanged(nameof(this.Category6Goal));
                    break;
                case nameof(this.month.Category7Goal):
                    this.InvokePropertyChanged(nameof(this.Category7Goal));
                    break;
                case nameof(this.month.Category8Goal):
                    this.InvokePropertyChanged(nameof(this.Category8Goal));
                    break;
                case nameof(this.month.TotalGoal):
                    this.InvokePropertyChanged(nameof(this.TotalGoal));
                    break;
                default:
                    break;
            }
        }

        private void OnCategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Category category in e.OldItems)
                {
                    category.PropertyChanged -= this.OnCategoryChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (Category category in e.NewItems)
                {
                    category.PropertyChanged += this.OnCategoryChanged;
                }
            }
        }

        private void OnCategoryChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Category.Active))
            {
                return;
            }

            var category = (Category)sender;
            switch (category.ID)
            {
                case ExerciseCategory.Category1:
                    InvokePropertyChanged(nameof(this.Category1ItemsVisible));
                    break;
                case ExerciseCategory.Category2:
                    InvokePropertyChanged(nameof(this.Category2ItemsVisible));
                    break;
                case ExerciseCategory.Category3:
                    InvokePropertyChanged(nameof(this.Category3ItemsVisible));
                    break;
                case ExerciseCategory.Category4:
                    InvokePropertyChanged(nameof(this.Category4ItemsVisible));
                    break;
                case ExerciseCategory.Category5:
                    InvokePropertyChanged(nameof(this.Category5ItemsVisible));
                    break;
                case ExerciseCategory.Category6:
                    InvokePropertyChanged(nameof(this.Category6ItemsVisible));
                    break;
                case ExerciseCategory.Category7:
                    InvokePropertyChanged(nameof(this.Category7ItemsVisible));
                    break;
                case ExerciseCategory.Category8:
                    InvokePropertyChanged(nameof(this.Category8ItemsVisible));
                    break;
                default:
                    break;
            }
        }
    }
}
