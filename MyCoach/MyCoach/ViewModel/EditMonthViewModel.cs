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
    public class EditMonthViewModel : BaseViewModel
    {
        private Month month;
        private ObservableCollection<Category> categories;

        public EditMonthViewModel(Month month)
        {
            this.month = month;
            this.categories = DataInterface.GetInstance().GetData<Category>();
            foreach (var category in this.categories)
            {
                category.PropertyChanged += this.OnCategoryChanged;
            }
            
            this.categories.CollectionChanged += this.OnCategoriesChanged;
        }

        public bool Category1ItemsVisible
        {
            get => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category1)?.Active == true;
        }

        public ushort Category1Goal
        {
            get => this.month.Category1Goal;

            set => this.month.Category1Goal = value;
        }

        public bool Category2ItemsVisible
        {
            get => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category2)?.Active == true;
        }

        public ushort Category2Goal
        {
            get => this.month.Category2Goal;

            set => this.month.Category2Goal = value;
        }

        public bool Category3ItemsVisible
        {
            get => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category3)?.Active == true;
        }

        public ushort Category3Goal
        {
            get => this.month.Category3Goal;

            set => this.month.Category3Goal = value;
        }

        public bool Category4ItemsVisible
        {
            get => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category4)?.Active == true;
        }

        public ushort Category4Goal
        {
            get => this.month.Category4Goal;

            set => this.month.Category4Goal = value;
        }

        public bool Category5ItemsVisible
        {
            get => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category5)?.Active == true;
        }

        public ushort Category5Goal
        {
            get => this.month.Category5Goal;

            set => this.month.Category5Goal = value;
        }

        public bool Category6ItemsVisible
        {
            get => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category6)?.Active == true;
        }

        public ushort Category6Goal
        {
            get => this.month.Category6Goal;

            set => this.month.Category6Goal = value;
        }

        public bool Category7ItemsVisible
        {
            get => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category7)?.Active == true;
        }

        public ushort Category7Goal
        {
            get => this.month.Category7Goal;

            set => this.month.Category7Goal = value;
        }

        public bool Category8ItemsVisible
        {
            get => categories.FirstOrDefault(c => c.ID == ExerciseCategory.Category8)?.Active == true;
        }

        public ushort Category8Goal
        {
            get => this.month.Category8Goal;

            set => this.month.Category8Goal = value;
        }

        public uint TotalGoal
        {
            get => this.month.TotalGoal;

            set => this.month.TotalGoal = value;
        }

        public string MonthName
        {
            get => this.month.Number == MonthNumber.Current 
                ? string.Empty 
                : this.month.StartDate.ToString("y", CultureInfo.CurrentCulture);
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
