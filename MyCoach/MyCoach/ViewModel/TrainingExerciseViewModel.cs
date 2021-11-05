using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel
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
            get => Exercise.Info;
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
            var text = ((uint)(Exercise.Count * RepeatsMultiplier)).ToString();

            if (Exercise.Unit != null && Exercise.Unit != string.Empty)
            {
                text = string.Concat(text, " ", Exercise.Unit);
            }

            text = Exercise.Name != null && Exercise.Name != string.Empty
                ? string.Concat(text, " ", Exercise.Name)
                : string.Concat(text, " ", UNKNOWN_EXERCISE_NAME);

            return text;
        }
    }
}
