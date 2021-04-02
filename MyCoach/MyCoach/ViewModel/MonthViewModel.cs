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
        }

        private uint scoresOfVisibleProperties
        {
            get
            {
                uint scores = 0;
                scores = this.Category1Visisble ? scores + this.month.Category1Scores : scores;
                scores = this.Category2Visisble ? scores + this.month.Category2Scores : scores;
                scores = this.Category3Visisble ? scores + this.month.Category3Scores : scores;
                scores = this.Category4Visisble ? scores + this.month.Category4Scores : scores;
                scores = this.Category5Visisble ? scores + this.month.Category5Scores : scores;
                scores = this.Category6Visisble ? scores + this.month.Category6Scores : scores;
                scores = this.Category7Visisble ? scores + this.month.Category7Scores : scores;
                scores = this.Category8Visisble ? scores + this.month.Category8Scores : scores;
                return scores;
            }
        }

        public string Description => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.startDate.Month) + " " + startDate.Year.ToString();

        public MonthNumber Number => this.month.Number;

        public string Category1Scores => this.month.Category1Goal > 0
                    ? $"{month.Category1Scores} von {month.Category1Goal}"
                    : month.Category1Scores.ToString();

        public int Category1Percentage
        {
            get
            {
                if (this.month.Category1Goal == 0)
                {
                    return 0;
                }

                if (this.month.Category1Scores < this.month.Category1Goal)
                {
                    return this.month.Category1Scores / this.month.Category1Goal;
                }

                return 100;
            }
        }

        public bool Category1Visisble => this.categories?.Any(c => c.ID == ExerciseCategory.Category1 && c.Active) == true;

        public string Category1Name => this.categories?.Where(c => c.ID == ExerciseCategory.Category1)?.FirstOrDefault()?.Name ?? string.Empty;

        public string Category2Scores => this.month.Category2Goal > 0
            ? $"{month.Category2Scores} von {month.Category2Goal}"
            : month.Category2Scores.ToString();

        public int Category2Percentage
        {
            get
            {
                if (this.month.Category2Goal == 0)
                {
                    return 0;
                }

                if (this.month.Category2Scores < this.month.Category2Goal)
                {
                    return this.month.Category2Scores / this.month.Category2Goal;
                }

                return 100;
            }
        }

        public bool Category2Visisble => this.categories?.Any(c => c.ID == ExerciseCategory.Category2 && c.Active) == true;

        public string Category2Name => this.categories?.Where(c => c.ID == ExerciseCategory.Category2)?.FirstOrDefault()?.Name ?? string.Empty;

        public string Category3Scores => this.month.Category3Goal > 0
            ? $"{month.Category3Scores} von {month.Category3Goal}"
            : month.Category3Scores.ToString();

        public int Category3Percentage
        {
            get
            {
                if (this.month.Category3Goal == 0)
                {
                    return 0;
                }

                if (this.month.Category3Scores < this.month.Category3Goal)
                {
                    return this.month.Category3Scores / this.month.Category3Goal;
                }

                return 100;
            }
        }

        public bool Category3Visisble => this.categories?.Any(c => c.ID == ExerciseCategory.Category3 && c.Active) == true;

        public string Category3Name => this.categories?.Where(c => c.ID == ExerciseCategory.Category3)?.FirstOrDefault()?.Name ?? string.Empty;

        public string Category4Scores => this.month.Category4Goal > 0
            ? $"{month.Category4Scores} von {month.Category4Goal}"
            : month.Category4Scores.ToString();

        public int Category4Percentage
        {
            get
            {
                if (this.month.Category4Goal == 0)
                {
                    return 0;
                }

                if (this.month.Category4Scores < this.month.Category4Goal)
                {
                    return this.month.Category4Scores / this.month.Category4Goal;
                }

                return 100;
            }
        }

        public bool Category4Visisble => this.categories?.Any(c => c.ID == ExerciseCategory.Category4 && c.Active) == true;

        public string Category4Name => this.categories?.Where(c => c.ID == ExerciseCategory.Category4)?.FirstOrDefault()?.Name ?? string.Empty;

        public string Category5Scores => this.month.Category5Goal > 0
            ? $"{month.Category5Scores} von {month.Category5Goal}"
            : month.Category5Scores.ToString();

        public int Category5Percentage
        {
            get
            {
                if (this.month.Category5Goal == 0)
                {
                    return 0;
                }

                if (this.month.Category5Scores < this.month.Category5Goal)
                {
                    return this.month.Category5Scores / this.month.Category5Goal;
                }

                return 100;
            }
        }

        public bool Category5Visisble => this.categories?.Any(c => c.ID == ExerciseCategory.Category5 && c.Active) == true;

        public string Category5Name => this.categories?.Where(c => c.ID == ExerciseCategory.Category5)?.FirstOrDefault()?.Name ?? string.Empty;

        public string Category6Scores => this.month.Category6Goal > 0
            ? $"{month.Category6Scores} von {month.Category6Goal}"
            : month.Category6Scores.ToString();

        public int Category6Percentage
        {
            get
            {
                if (this.month.Category6Goal == 0)
                {
                    return 0;
                }

                if (this.month.Category6Scores < this.month.Category6Goal)
                {
                    return this.month.Category6Scores / this.month.Category6Goal;
                }

                return 100;
            }
        }

        public bool Category6Visisble => this.categories?.Any(c => c.ID == ExerciseCategory.Category6 && c.Active) == true;

        public string Category6Name => this.categories?.Where(c => c.ID == ExerciseCategory.Category6)?.FirstOrDefault()?.Name ?? string.Empty;

        public string Category7Scores => this.month.Category7Goal > 0
            ? $"{month.Category7Scores} von {month.Category7Goal}"
            : month.Category7Scores.ToString();

        public int Category7Percentage
        {
            get
            {
                if (this.month.Category7Goal == 0)
                {
                    return 0;
                }

                if (this.month.Category7Scores < this.month.Category7Goal)
                {
                    return this.month.Category7Scores / this.month.Category7Goal;
                }

                return 100;
            }
        }

        public bool Category7Visisble => this.categories?.Any(c => c.ID == ExerciseCategory.Category7 && c.Active) == true;

        public string Category7Name => this.categories?.Where(c => c.ID == ExerciseCategory.Category7)?.FirstOrDefault()?.Name ?? string.Empty;

        public string Category8Scores => this.month.Category8Goal > 0
            ? $"{month.Category8Scores} von {month.Category8Goal}"
            : month.Category8Scores.ToString();

        public int Category8Percentage
        {
            get
            {
                if (this.month.Category8Goal == 0)
                {
                    return 0;
                }

                if (this.month.Category8Scores < this.month.Category8Goal)
                {
                    return this.month.Category8Scores / this.month.Category8Goal;
                }

                return 100;
            }
        }

        public bool Category8Visisble => this.categories?.Any(c => c.ID == ExerciseCategory.Category8 && c.Active) == true;

        public string Category8Name => this.categories?.Where(c => c.ID == ExerciseCategory.Category8)?.FirstOrDefault()?.Name ?? string.Empty;

        public string TotalScores
        {
            get
            {
                return this.month.TotalGoal > 0
                    ? $"{this.scoresOfVisibleProperties} von {this.month.TotalGoal}"
                    : this.scoresOfVisibleProperties.ToString();
            }
        }

        public uint ToatalPercentage
        {
            get
            {
                if (this.scoresOfVisibleProperties == 0)
                {
                    return 0;
                }

                if (this.scoresOfVisibleProperties < this.month.TotalGoal)
                {
                    return this.scoresOfVisibleProperties / this.month.TotalGoal;
                }

                return 100;
            }
        }
    }
}
