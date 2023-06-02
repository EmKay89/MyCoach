using MyCoach.Model.Defines;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;

namespace MyCoach.ViewModel.TrainingSettingsViewModels
{
    public abstract class TrainingSettingsViewModelBase : BaseViewModel
    {
        private bool trainingActive;

        /// <summary>
        ///     Getter that tells the parent view model, if the training settings are set up in a valid way to start the training.
        /// </summary>
        public abstract bool CanStartTraining { get; }

        /// <summary>
        ///     Setter to tell the view model that the training is active. One reaction of the view model might be to disable its controls as a consequence.
        /// </summary>
        public bool TrainingActive
        {
            protected get => this.trainingActive;

            set
            {
                if (value == this.trainingActive)
                {
                    return;
                }

                this.trainingActive = value;
                this.InvokePropertiesChanged(
                    nameof(this.TrainingActive),
                    nameof(this.TrainingSettingsEnabled));
            }
        }

        /// <summary>
        ///     Getter to notify the View, if the training settings controls should be enabled or not.
        /// </summary>
        public bool TrainingSettingsEnabled => this.TrainingActive == false;

        /// <summary>
        ///     Gets a new TrainingSettings DTO with the current data.
        /// </summary>
        public abstract TrainingSettings TrainingSettings { get; }
    }
}
