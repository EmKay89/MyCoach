using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel
{
    public class MonthCategoryDetailViewModel : BaseViewModel
    {
        private Category category;
        private Month month;

        public MonthCategoryDetailViewModel(Category category, Month month)
        {
            this.category = category;
            this.month = month;
            this.month.PropertyChanged += this.OnMonthChanged;
        }

        public string Name => this.category.Name;

        public ushort Scores
        {
            get => this.GetScores();

            set => this.SetScores(value);
        }

        public string AppendedGoalTag => this.GetAppendedGoalTag();

        public int Percentage
        {
            get
            {
                return this.GetPercentage();
            }
        }

        public bool EditScoresDisabled { get; set; }

        private void OnMonthChanged(object sender, EventArgs e)
        {
            this.InvokePropertiesChanged(
                nameof(this.Scores),
                nameof(this.AppendedGoalTag),
                nameof(this.Percentage));
        }

        private ushort GetScores()
        {
            switch (this.category.ID)
            {
                case ExerciseCategory.Category1:
                    return this.month.Category1Scores;
                case ExerciseCategory.Category2:
                    return this.month.Category2Scores;
                case ExerciseCategory.Category3:
                    return this.month.Category3Scores;
                case ExerciseCategory.Category4:
                    return this.month.Category4Scores;
                case ExerciseCategory.Category5:
                    return this.month.Category5Scores;
                case ExerciseCategory.Category6:
                    return this.month.Category6Scores;
                case ExerciseCategory.Category7:
                    return this.month.Category7Scores;
                case ExerciseCategory.Category8:
                    return this.month.Category8Scores;
                default:
                    return 0;
            }
        }

        private void SetScores(ushort value)
        {
            switch (this.category.ID)
            {
                case ExerciseCategory.Category1:
                    this.month.Category1Scores = value;
                    break;
                case ExerciseCategory.Category2:
                    this.month.Category2Scores = value;
                    break;
                case ExerciseCategory.Category3:
                    this.month.Category3Scores = value;
                    break;
                case ExerciseCategory.Category4:
                    this.month.Category4Scores = value;
                    break;
                case ExerciseCategory.Category5:
                    this.month.Category5Scores = value;
                    break;
                case ExerciseCategory.Category6:
                    this.month.Category6Scores = value;
                    break;
                case ExerciseCategory.Category7:
                    this.month.Category7Scores = value;
                    break;
                case ExerciseCategory.Category8:
                    this.month.Category8Scores = value;
                    break;
            }
        }

        private string GetAppendedGoalTag()
        {
            switch (this.category.ID)
            {
                case ExerciseCategory.Category1:
                    return this.month.Category1Goal == 0 ? string.Empty : $" von {this.month.Category1Goal}";
                case ExerciseCategory.Category2:
                    return this.month.Category2Goal == 0 ? string.Empty : $" von {this.month.Category2Goal}";
                case ExerciseCategory.Category3:
                    return this.month.Category3Goal == 0 ? string.Empty : $" von {this.month.Category3Goal}";
                case ExerciseCategory.Category4:
                    return this.month.Category4Goal == 0 ? string.Empty : $" von {this.month.Category4Goal}";
                case ExerciseCategory.Category5:
                    return this.month.Category5Goal == 0 ? string.Empty : $" von {this.month.Category5Goal}";
                case ExerciseCategory.Category6:
                    return this.month.Category6Goal == 0 ? string.Empty : $" von {this.month.Category6Goal}";
                case ExerciseCategory.Category7:
                    return this.month.Category7Goal == 0 ? string.Empty : $" von {this.month.Category7Goal}";
                case ExerciseCategory.Category8:
                    return this.month.Category8Goal == 0 ? string.Empty : $" von {this.month.Category8Goal}";
                default:
                    return string.Empty;
            }
        }

        private int GetPercentage()
        {
            switch (this.category.ID)
            {
                case ExerciseCategory.Category1:
                    return this.GetPercentageFromGoalAndScores(this.month.Category1Goal, this.month.Category1Scores);
                case ExerciseCategory.Category2:
                    return this.GetPercentageFromGoalAndScores(this.month.Category2Goal, this.month.Category2Scores);
                case ExerciseCategory.Category3:
                    return this.GetPercentageFromGoalAndScores(this.month.Category3Goal, this.month.Category3Scores);
                case ExerciseCategory.Category4:
                    return this.GetPercentageFromGoalAndScores(this.month.Category4Goal, this.month.Category4Scores);
                case ExerciseCategory.Category5:
                    return this.GetPercentageFromGoalAndScores(this.month.Category5Goal, this.month.Category5Scores);
                case ExerciseCategory.Category6:
                    return this.GetPercentageFromGoalAndScores(this.month.Category6Goal, this.month.Category6Scores);
                case ExerciseCategory.Category7:
                    return this.GetPercentageFromGoalAndScores(this.month.Category7Goal, this.month.Category7Scores);
                case ExerciseCategory.Category8:
                    return this.GetPercentageFromGoalAndScores(this.month.Category8Goal, this.month.Category8Scores);
                default:
                    return 0;
            }
        }

        private int GetPercentageFromGoalAndScores(ushort goal, ushort scores) => goal == 0 ? 0 : scores < goal ? scores * 100 / goal : 100;
    }
}
