using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.Events;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using System.Linq;
using System.Text;

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
        private bool isActive;

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
            this.MoveElementUpCommand = new RelayCommand(this.InvokeMoveElementUpExecuted);
            this.MoveElementDownCommand = new RelayCommand(this.InvokeMoveElementDownExecuted);
            this.RemoveElementCommand = new RelayCommand(this.InvokeRemoveElementFromTrainingExecuted);
        }

        public const string UNKNOWN_EXERCISE_NAME = "unbekannte Übung";
        public const string LAPDESIGNATION = "Runde";

        public event ExerciseEventHandler MoveElementUpExecuted;

        public event ExerciseEventHandler MoveElementDownExecuted;

        public event ExerciseEventHandler RemoveElementFromTrainingExecuted;

        public RelayCommand MoveElementUpCommand { get; }

        public RelayCommand MoveElementDownCommand { get; }

        public RelayCommand RemoveElementCommand { get; }

        /// <summary>
        ///     Gets or sets, if this <see cref="TrainingElementViewModel"/> is supposed to represent an
        ///     exercise or lap separator.
        /// </summary>
        public TrainingElementType Type => this.type;

        /// <summary>
        ///     Gets, if lap separator specific view elements are supposed to be shown.
        /// </summary>
        public bool LapSeparatorElementsVisible => this.Type == TrainingElementType.LapSeparator;

        /// <summary>
        ///     Gets, if exercise specific view elements are supposed to be shown.
        /// </summary>
        public bool TrainingExerciseElementsVisible => this.Type == TrainingElementType.Exercise;

        /// <summary>
        ///     Gets, if the user controls for completing the exercise are allowed to be used.
        ///     Only allowed, if the training element has been activated.
        /// </summary>
        public bool CompletionAllowed => this.isActive;

        /// <summary>
        ///     Gets, if the user controls for editing the exercise are allowed to be used.
        ///     Only allowed, if the training element has not yet been activated.
        /// </summary>
        public bool EditingAllowed => this.isActive == false;

        /// <summary>
        ///     Gets a string indicating how much repeats of the exercise are supposed to be performed.
        ///     Empty string, if the this object represents a lap separator.
        /// </summary>
        public string NameAndRepeats => GetNameAndRepeats();

        /// <summary>
        ///     Gets a string indicating how much scores will be added to which exercise category after completing the exercise.
        ///     Empty string, if the this object represents a lap separator, the exercise category is not set or the scores for the exercise is 0.
        /// </summary>
        public string ScoresForCategory => GetScoresForCategory();

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

        /// <summary>
        ///     Activates the training element, if it is of type <see cref="TrainingElementType.Exercise"/>.
        /// </summary>
        public void Activate()
        {
            this.isActive = true;
            this.InvokePropertiesChanged(
                nameof(this.CompletionAllowed),
                nameof(this.EditingAllowed));
        }

        private string GetNameAndRepeats()
        {
            if (this.Exercise == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            sb.Append(((uint)(this.Exercise.Count * this.RepeatsMultiplier)).ToString());

            if (this.Exercise.Unit != null && this.Exercise.Unit != string.Empty)
            {
                sb.Append(" ");
                sb.Append(this.Exercise.Unit);
            }

            if (this.Exercise.Name != null && this.Exercise.Name != string.Empty)
            {
                sb.Append(" ");
                sb.Append(this.Exercise.Name);
            }
            else
            {
                sb.Append(" ");
                sb.Append(UNKNOWN_EXERCISE_NAME);
            }

            return sb.ToString();
        }

        private string GetScoresForCategory()
        {
            if (this.Exercise == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            var categoryActive = DataInterface.GetInstance().GetData<Category>().SingleOrDefault(c => c.ID == this.Exercise.Category)?.Active;
            var categoryName = DataInterface.GetInstance().GetData<Category>().SingleOrDefault(c => c.ID == this.Exercise.Category)?.Name;

            if (categoryActive == true && this.Exercise.Scores > 0)
            {
                sb.Append($" --- {(uint)(this.Exercise.Scores * this.ScoresMultiplier)} Punkte für Kategorie '{categoryName ?? string.Empty}'");
            }

            return sb.ToString();
        }

        private void InvokeMoveElementUpExecuted()
        {
            if (this.Type == TrainingElementType.Exercise)
            {
                this.MoveElementUpExecuted?.Invoke(
                    this,
                    new ExerciseEventArgs(this.exercise));
            }
        }

        private void InvokeMoveElementDownExecuted()
        {
            if (this.Type == TrainingElementType.Exercise)
            {
                this.MoveElementDownExecuted?.Invoke(
                    this,
                    new ExerciseEventArgs(this.exercise));
            }
        }

        private void InvokeRemoveElementFromTrainingExecuted()
        {
            if (this.Type == TrainingElementType.Exercise)
            {
                this.RemoveElementFromTrainingExecuted?.Invoke(
                    this,
                    new ExerciseEventArgs(this.exercise));
            }
        }
    }
}
