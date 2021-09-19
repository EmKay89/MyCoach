using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.TrainingGeneration
{
    public class TrainingExerciseViewModel : BaseViewModel, ITrainingElement
    {        
        private readonly Exercise exercise;
        private bool completed;

        public TrainingExerciseViewModel(Exercise exercise)
        {
            this.exercise = exercise;
        }

        public const string UNKNOWN_EXERCISE_NAME = "unbekannte Übung";

        public event EventHandler TrainingExerciseCompleted;

        public string Text
        {
            get => GetText();
        }

        public bool Completed
        {
            get => this.completed;

            set
            {
                if (value == this.completed)
                {
                    return;
                }

                this.completed = value;
                this.InvokePropertyChanged();
            }
        }

        public string Info
        {
            get => this.exercise.Info;
        }

        public double Multiplier { get; set; } = 1.0;

        public uint GetScores()
        {
            return this.completed ? (uint)Math.Round(this.exercise.Scores * this.Multiplier) : 0;
        }

        private string GetText()
        {
            var text = ((uint)(this.exercise.Count * Multiplier)).ToString();

            if (this.exercise.Unit != null && this.exercise.Unit != string.Empty)
            {
                text = string.Concat(text, " ", this.exercise.Unit);
            }

            text = this.exercise.Name != null && this.exercise.Name != string.Empty
                ? string.Concat(text, " ", this.exercise.Name)
                : string.Concat(text, " ", UNKNOWN_EXERCISE_NAME);            

            return text;
        }
    }
}
