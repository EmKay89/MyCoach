using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.Services;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using MyCoach.ViewModel.TrainingSettingsViewModels;
using MyCoach.ViewModel.Utilities;
using System;
using System.Collections.Generic;

namespace MyCoach.ViewModel
{
    public class TrainingViewModel : BaseViewModel
    {
        private TrainingMode trainingMode;
        private Training training;

        public TrainingViewModel(IFileDialogService fileDialogService = null, IMessageBoxService messageBoxService = null)
        {
            this.Training = new Training();
            this.TrainingMode = TrainingMode.CircleTraining;
            this.CircleRandomAndFocusTrainingViewModel = new CircleRandomAndFocusTrainingViewModel();
            this.UserDefinedTrainingViewModel = new UserDefinedTrainingViewModel(this.Training, fileDialogService, messageBoxService);
            this.StartTrainingCommand = new RelayCommand(this.StartTraining, this.CanStartTraining);
        }

        public Dictionary<TrainingMode, string> ModesWithCaption { get; } = new Dictionary<TrainingMode, string>
        {
            { TrainingMode.CircleTraining, "Zirkeltraining" },
            { TrainingMode.RandomTraining, "Zufallstraining" },
            { TrainingMode.FocusTraining, "Fokustraining" },
            { TrainingMode.UserDefinedTraining, "Benutzerdefiniertes Training" }
        };

        public ITrainingSettingsViewModel SelectedViewModel { get; private set; }

        public CircleRandomAndFocusTrainingViewModel CircleRandomAndFocusTrainingViewModel { get; }

        public UserDefinedTrainingViewModel UserDefinedTrainingViewModel { get; }

        public Training Training
        {
            get => this.training;

            private set
            {
                if (value == this.training)
                {
                    return;
                }

                this.training = value;
                this.training.TrainingActiveChanged += this.OnTrainingActiveChanged;
                this.InvokePropertyChanged();
            }
        }

        public TrainingMode TrainingMode
        {
            get { return this.trainingMode; }
            set
            {
                if (value == this.trainingMode)
                {
                    return;
                }

                this.trainingMode = value;

                if (value != TrainingMode.UserDefinedTraining)
                {
                    this.Training?.Clear();
                }

                this.InvokePropertiesChanged(
                    nameof(this.TrainingMode),
                    nameof(this.ModeExplanation));
            }
        }

        public string ModeExplanation => TrainingModeDescriptions.GetTrainingModeDescription(this.TrainingMode);

        public bool TrainingActive => this.Training?.IsActive == true;

        public bool TrainingSettingsEnabled => !this.TrainingActive;

        public RelayCommand StartTrainingCommand { get; }

        public void AddExerciseToTraining(Exercise exercise)
        {
            if (this.Training.IsActive == false)
            {
                var vm = new TrainingElementViewModel(TrainingElementType.Exercise, exercise);
                this.Training.Add(vm);
                this.TrainingMode = TrainingMode.UserDefinedTraining;
            }
        }

        private void StartTraining()
        {
            if (this.TrainingActive)
            {
                this.Training.Finish();
                return;
            }

            if (this.TrainingMode != TrainingMode.UserDefinedTraining)
            {
                var settings = this.CircleRandomAndFocusTrainingViewModel.GetTrainingSettings();
                this.Training = TrainingGenerator.CreateTraining(settings);
            }

            this.Training.Start();
        }

        private bool CanStartTraining()
        {
            return this.SelectedViewModel?.CanStartTraining() ?? false;
        }

        private void OnTrainingActiveChanged(object sender, EventArgs e)
        {
            this.InvokePropertiesChanged(
                nameof(this.TrainingActive),
                nameof(this.TrainingSettingsEnabled));
        }
    }
}
