using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel
{
    public class MonthCategoryDetailViewModel : BaseViewModel
    {
        private readonly Category category;
        private readonly Month month;

        public MonthCategoryDetailViewModel(Category category, Month month)
        {
            this.category = category;
            this.month = month;
            this.month.PropertyChanged += this.OnMonthChanged;
        }

        public string Name => this.category.Name;

        public ushort Scores
        {
            get => this.month.GetScores(this.category.ID);

            set => this.SetScores(value);
        }

        public string AppendedGoalTag => this.GetAppendedGoalTag();

        public int Percentage
        {
            get
            {
                return this.month.GetPercentage(this.category.ID);
            }
        }

        private void OnMonthChanged(object sender, PropertyChangedEventArgs e)
        {
            this.InvokePropertiesChanged(
                nameof(this.Scores),
                nameof(this.AppendedGoalTag),
                nameof(this.Percentage));
        }

        private void SetScores(ushort value)
        {
            this.month.SetScores(this.category.ID, value);
            DataInterface.GetInstance().SaveData<Month>();
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
    }
}
