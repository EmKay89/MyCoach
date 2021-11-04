using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.TrainingGenerationAndEvaluation
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

        public string Text
        {
            get => GetText();
        }

        public bool Completed
        {
            get => completed;

            set
            {
                if (value == completed)
                {
                    return;
                }

                completed = value;
                InvokePropertyChanged();
            }
        }

        public string Info
        {
            get => this.Exercise.Info;
        }

        public double RepeatsMultiplier { get; set; } = 1.0;

        public double ScoresMultiplier { get; set; } = 1.0;

        public Exercise Exercise => exercise;

        public uint GetScores()
        {
            return completed ? (uint)Math.Round(Exercise.Scores * ScoresMultiplier) : 0;
        }

        private string GetText()
        {
            var text = ((uint)(this.Exercise.Count * RepeatsMultiplier)).ToString();

            if (this.Exercise.Unit != null && this.Exercise.Unit != string.Empty)
            {
                text = string.Concat(text, " ", this.Exercise.Unit);
            }

            text = this.Exercise.Name != null && this.Exercise.Name != string.Empty
                ? string.Concat(text, " ", this.Exercise.Name)
                : string.Concat(text, " ", UNKNOWN_EXERCISE_NAME);

            return text;
        }
    }
}
