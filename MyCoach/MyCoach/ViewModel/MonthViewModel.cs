using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel
{
    public class MonthViewModel : BaseViewModel
    {
        private Month month;
        private ViewTrainingScheduleViewModel parent;

        public MonthViewModel(ViewTrainingScheduleViewModel parent, Month month)
        {
            this.parent = parent;
            this.month = month;
        }

        //public string Description
        //{
        //    get
        //    {
        //        int monthToInt;
        //        int year;

        //        if (this.Number == MonthNumber.Current)
        //        {
        //            monthToInt = DateTime.Now.Month;
        //            year = DateTime.Now.Year;
        //        }
        //        else
        //        {
        //            var targetDate = this.parent.StartDate.AddMonths((int)this.Number);
        //            monthToInt = targetDate.Month;
        //            year = targetDate.Year;
        //        }

        //        return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthToInt) + " " + year.ToString();
        //    }
        //}

        public MonthNumber Number => this.month.Number;

        public ushort Category1Scores
        {
            get => this.month.Category1Scores;

            set
            {
                if (this.month.Category1Scores == value)
                {
                    return;
                }

                this.month.Category1Scores = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category1Goal
        {
            get => this.month.Category1Goal;

            set
            {
                if (this.month.Category1Goal == value)
                {
                    return;
                }

                this.month.Category1Goal = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category2Scores
        {
            get => this.month.Category2Scores;

            set
            {
                if (this.month.Category2Scores == value)
                {
                    return;
                }

                this.month.Category2Scores = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category2Goal
        {
            get => this.month.Category2Goal;

            set
            {
                if (this.month.Category2Goal == value)
                {
                    return;
                }

                this.month.Category2Goal = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category3Scores
        {
            get => this.month.Category3Scores;

            set
            {
                if (this.month.Category3Scores == value)
                {
                    return;
                }

                this.month.Category3Scores = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category3Goal
        {
            get => this.month.Category3Goal;

            set
            {
                if (this.month.Category3Goal == value)
                {
                    return;
                }

                this.month.Category3Goal = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category4Scores
        {
            get => this.month.Category4Scores;

            set
            {
                if (this.month.Category4Scores == value)
                {
                    return;
                }

                this.month.Category4Scores = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category4Goal
        {
            get => this.month.Category4Goal;

            set
            {
                if (this.month.Category4Goal == value)
                {
                    return;
                }

                this.month.Category4Goal = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category5Scores
        {
            get => this.month.Category5Scores;

            set
            {
                if (this.month.Category5Scores == value)
                {
                    return;
                }

                this.month.Category5Scores = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category5Goal
        {
            get => this.month.Category5Goal;

            set
            {
                if (this.month.Category5Goal == value)
                {
                    return;
                }

                this.month.Category5Goal = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category6Scores
        {
            get => this.month.Category6Scores;

            set
            {
                if (this.month.Category6Scores == value)
                {
                    return;
                }

                this.month.Category6Scores = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category6Goal
        {
            get => this.month.Category6Goal;

            set
            {
                if (this.month.Category6Goal == value)
                {
                    return;
                }

                this.month.Category6Goal = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category7Scores
        {
            get => this.month.Category7Scores;

            set
            {
                if (this.month.Category7Scores == value)
                {
                    return;
                }

                this.month.Category7Scores = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category7Goal
        {
            get => this.month.Category7Goal;

            set
            {
                if (this.month.Category7Goal == value)
                {
                    return;
                }

                this.month.Category7Goal = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category8Scores
        {
            get => this.month.Category8Scores;

            set
            {
                if (this.month.Category8Scores == value)
                {
                    return;
                }

                this.month.Category8Scores = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Category8Goal
        {
            get => this.month.Category8Goal;

            set
            {
                if (this.month.Category8Goal == value)
                {
                    return;
                }

                this.month.Category8Goal = value;
                this.InvokePropertyChanged();
            }
        }

        public uint TotalGoal
        {
            get => this.month.TotalGoal;

            set
            {
                if (this.month.TotalGoal == value)
                {
                    return;
                }

                this.month.TotalGoal = value;
                this.InvokePropertyChanged();
            }
        }
    }
}
