using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;

namespace MyCoach.ViewModel
{
    /// <summary>
    ///     Holds data to display an exercise or lap separator for a training. Which of the two is displayed is
    ///     determined by the property <see cref="Type"/>.
    /// </summary>
    public class TrainingElementViewModel : BaseViewModel
    {
        private readonly TrainingElementType type;
        private readonly Exercise exercise;
        private bool completed;

        /// <summary>
        ///     Creates a new instance of <see cref="TrainingElementViewModel"/>.
        /// </summary>
        /// <param name="type">
        ///     The type of training element to be represented by this view model.
        /// </param>
        /// <param name="exercise">
        ///     The exercise to be represented by this view model. If this view model is supposed to be a lap 
        ///     separator, pass null.
        /// </param>
        public TrainingElementViewModel(TrainingElementType type, Exercise exercise)
        {
            this.type = type;
            this.exercise = exercise;
        }

        public const string UNKNOWN_EXERCISE_NAME = "unbekannte Übung";
        public const string LAPDESIGNATION = "Runde";

        /// <summary>
        ///     Gets or sets, if this <see cref="TrainingElementViewModel"/> is supposed to represent an
        ///     exercise or lap separator.
        /// </summary>
        public TrainingElementType Type => this.type;

        /// <summary>
        ///     Gets, if lap separator specific view elements are supposed to be shown.
        /// </summary>
        public bool LapSeparatorElementsVisible => this.Type == TrainingElementType.lapSeparator;

        /// <summary>
        ///     Gets, if exercise specific view elements are supposed to be shown.
        /// </summary>
        public bool TrainingExerciseElementsVisible => this.Type == TrainingElementType.exercise;

        /// <summary>
        ///     Text to be displayed, if this object represents an exercise.
        /// </summary>
        public string ExerciseText
        {
            get => GetExerciseText();
        }

        /// <summary>
        ///     Text to be displayed, if this object represents a lap separator.
        /// </summary>
        public string LapHeadline { get; set; }

        /// <summary>
        ///     The exercise DTO represented by this <see cref="TrainingElementViewModel"/>.
        /// </summary>
        public Exercise Exercise => exercise;

        /// <summary>
        ///     Gets or sets, if an exercise was completed during training.
        /// </summary>
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

        /// <summary>
        ///     Gets additional information on how to perform the exercise.
        /// </summary>
        public string Info
        {
            get => Exercise?.Info ?? string.Empty;
        }

        /// <summary>
        ///     The multiplier to calculate the number of exercises to perform in the training.
        /// </summary>
        public double RepeatsMultiplier { get; set; } = 1.0;

        /// <summary>
        ///     The multiplier to for scores calculation after completing this exercise.
        /// </summary>
        public double ScoresMultiplier { get; set; } = 1.0;

        private string GetExerciseText()
        {
            if (this.Exercise == null)
            {
                return string.Empty;
            }

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
