using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel
{
    public class OverviewElementViewModel : BaseViewModel
    {
        private readonly Month month;
        private readonly Category category;
        private uint maxScoreOrGoal;

        public OverviewElementViewModel(Month month, Category category)
        {
            this.month = month;
            this.category = category;
            this.month.PropertyChanged += this.OnMonthChanged;
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
                this.InvokePropertiesChanged(
                    nameof(this.MaxScoreOrGoal),
                    nameof(this.RelativeHeightGoal),
                    nameof(this.RelativeHeightSpaceAboveGoal),
                    nameof(this.RelativeHeightScores),
                    nameof(this.RelativeHeightSpaceAboveScores));
            }
        }

        public uint RelativeHeightGoal => this.GetGoalPercentage();

        public uint RelativeHeightSpaceAboveGoal => (100 - this.GetGoalPercentage());

        public uint RelativeHeightScores => this.GetScoresPercentage();

        public uint RelativeHeightSpaceAboveScores => (100 - this.GetScoresPercentage());

        public string ScoresString => this.GetScoresString();

        public string Month => this.month.GetStartDateFromSchedule(
            DataInterface.GetInstance().GetData<TrainingSchedule>().FirstOrDefault())
                .ToString("y", CultureInfo.CurrentCulture);

        private uint GetGoalPercentage()
        {
            var goal = this.category == null ? this.month.TotalGoal : this.month.GetGoal(this.category.ID);
            if (goal != 0)
            {
                return (uint)((double)goal / (double)this.MaxScoreOrGoal * 100);
            }

            return goal;
        }

        private uint GetScoresPercentage()
        {
            var scores = this.category == null ? this.GetTotalScoresOfAllActiveCategories() : this.month.GetScores(this.category.ID);
            if (scores != 0)
            {
                return (uint)((double)scores / (double)this.MaxScoreOrGoal * 100);
            }

            return scores;
        }

        private string GetScoresString()
        {
            if (this.category == null)
            {
                return this.GetTotalScoresOfAllActiveCategories().ToString()
                        + (this.month.TotalGoal == 0 ? string.Empty : $" von {this.month.TotalGoal}"); ;
            }

            switch (this.category.ID)
            {
                case ExerciseCategory.Category1:
                    return this.month.Category1Scores.ToString() 
                        + (this.month.Category1Goal == 0 ? string.Empty : $" von {this.month.Category1Goal}");
                case ExerciseCategory.Category2:
                    return this.month.Category2Scores.ToString()
                        + (this.month.Category2Goal == 0 ? string.Empty : $" von {this.month.Category2Goal}");
                case ExerciseCategory.Category3:
                    return this.month.Category3Scores.ToString()
                        + (this.month.Category3Goal == 0 ? string.Empty : $" von {this.month.Category3Goal}");
                case ExerciseCategory.Category4:
                    return this.month.Category4Scores.ToString()
                        + (this.month.Category4Goal == 0 ? string.Empty : $" von {this.month.Category4Goal}");
                case ExerciseCategory.Category5:
                    return this.month.Category5Scores.ToString()
                        + (this.month.Category5Goal == 0 ? string.Empty : $" von {this.month.Category5Goal}");
                case ExerciseCategory.Category6:
                    return this.month.Category6Scores.ToString()
                        + (this.month.Category6Goal == 0 ? string.Empty : $" von {this.month.Category6Goal}");
                case ExerciseCategory.Category7:
                    return this.month.Category7Scores.ToString()
                        + (this.month.Category7Goal == 0 ? string.Empty : $" von {this.month.Category7Goal}");
                case ExerciseCategory.Category8:
                    return this.month.Category8Scores.ToString()
                        + (this.month.Category8Goal == 0 ? string.Empty : $" von {this.month.Category8Goal}");
                default:
                    return string.Empty;
            }
        }

        private uint GetTotalScoresOfAllActiveCategories()
        {
            var activeCategories = DataInterface.GetInstance().GetData<Category>()
                .Where(c => c.Active && c.Type == ExerciseType.Training);
            uint scores = 0;

            foreach (var category in activeCategories)
            {
                scores += this.month.GetScores(category.ID);
            }

            return scores;
        }

        private void OnMonthChanged(object sender, PropertyChangedEventArgs e)
        {
            this.InvokePropertiesChanged(
                nameof(this.ScoresString),
                nameof(this.RelativeHeightGoal),
                nameof(this.RelativeHeightSpaceAboveGoal),
                nameof(this.RelativeHeightScores),
                nameof(this.RelativeHeightSpaceAboveScores));
        }
    }
}
