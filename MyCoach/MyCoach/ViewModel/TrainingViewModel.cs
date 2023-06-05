﻿using MyCoach.DataHandling.DataManager;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using MyCoach.ViewModel.TrainingSettingsViewModels;
using MyCoach.ViewModel.Utilities;
using MyMvvm.Commands;
using MyMvvm.Services;
using System;
using System.Collections.Generic;

namespace MyCoach.ViewModel
{
    public class TrainingViewModel : BaseViewModel
    {
        private TrainingMode trainingMode;
        private Training training;
        private TrainingSettingsViewModelBase selectedViewModel;

        public TrainingViewModel(IFileDialogService fileDialogService = null, IMessageBoxService messageBoxService = null)
        {
            this.Training = new Training();            
            this.AutoGeneratedTrainingViewModel = new AutoGeneratedTrainingSettingsViewModel();
            this.UserDefinedTrainingViewModel = new UserDefinedTrainingSettingsViewModel(this.Training, fileDialogService, messageBoxService);
            this.StartTrainingCommand = new RelayCommand(this.StartTraining, this.CanStartTraining);
            this.TrainingMode = TrainingMode.CircleTraining;
        }

        public Dictionary<TrainingMode, string> ModesWithCaption { get; } = new Dictionary<TrainingMode, string>
        {
            { TrainingMode.CircleTraining, "Zirkeltraining" },
            { TrainingMode.RandomTraining, "Zufallstraining" },
            { TrainingMode.FocusTraining, "Fokustraining" },
            { TrainingMode.UserDefinedTraining, "Benutzerdefiniertes Training" }
        };

        public TrainingSettingsViewModelBase SelectedViewModel
        {
            get => this.selectedViewModel;

            private set
            {
                if (value == this.selectedViewModel)
                {
                    return;
                }

                this.selectedViewModel = value;
                this.InvokePropertyChanged();
            }
        }

        public AutoGeneratedTrainingSettingsViewModel AutoGeneratedTrainingViewModel { get; }

        public UserDefinedTrainingSettingsViewModel UserDefinedTrainingViewModel { get; }

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
                this.AutoGeneratedTrainingViewModel.TrainingMode = value;

                if (value == this.trainingMode)
                {
                    return;
                }

                this.trainingMode = value;                

                if (value != TrainingMode.UserDefinedTraining)
                {
                    this.Training?.Clear();
                    this.SelectedViewModel = this.AutoGeneratedTrainingViewModel;
                }
                else
                {
                    this.SelectedViewModel = this.UserDefinedTrainingViewModel;
                }

                this.InvokePropertiesChanged(
                    nameof(this.TrainingMode),
                    nameof(this.ModeExplanation));
            }
        }

        public string ModeExplanation => TrainingModeDescriptions.GetTrainingModeDescription(this.TrainingMode);

        public bool TrainingActive => this.Training?.IsActive == true;

        public bool TrainingSettingsEnabled => this.TrainingActive == false;

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

            var training = TrainingGenerator.CreateTraining(this.SelectedViewModel.TrainingSettings);
            this.Training = training ?? this.Training;
            this.Training.Start();
        }

        private bool CanStartTraining()
        {
            return this.SelectedViewModel?.CanStartTraining?? false;
        }

        private void OnTrainingActiveChanged(object sender, EventArgs e)
        {
            this.InvokePropertiesChanged(
                nameof(this.TrainingActive),
                nameof(this.TrainingSettingsEnabled));
            this.SelectedViewModel.TrainingActive = this.TrainingActive;
        }
    }
}
