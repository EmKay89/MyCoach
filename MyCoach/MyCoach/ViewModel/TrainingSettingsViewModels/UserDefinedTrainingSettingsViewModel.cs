using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.Services;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MyCoach.ViewModel.TrainingSettingsViewModels
{
    public class UserDefinedTrainingSettingsViewModel : TrainingSettingsViewModelBase
    {
        private Training training;
        private readonly IFileDialogService fileDialogService;
        private readonly IMessageBoxService messageBoxService;

        public UserDefinedTrainingSettingsViewModel(Training training, IFileDialogService fileDialogService = null, IMessageBoxService messageBoxService = null)
        {
            this.training = training;
            this.fileDialogService = fileDialogService ?? new FileDialogService();
            this.messageBoxService = messageBoxService ?? new MessageBoxService();
            this.AddExerciseCommand = new RelayCommand(this.AddExercise, this.CanAddExercise);
            this.AddHeadlineCommand = new RelayCommand(this.AddHeadline, this.CanAddHeadline);
            this.ExportTrainingCommand = new RelayCommand(this.ExportTraining, this.CanExportTraining);
            this.ImportTrainingCommand = new RelayCommand(this.ImportTraining, this.CanImportTraining);
        }

        public const string IMPORT_ERROR_TEXT = "Importieren fehlgeschlagen";
        public const string EXPORT_ERROR_TEXT = "Exportieren fehlgeschlagen";

        public override bool CanStartTraining => this.training.Any();

        public override TrainingSettings TrainingSettings => new TrainingSettings(TrainingMode.UserDefinedTraining);

        public RelayCommand AddExerciseCommand { get; }

        public RelayCommand AddHeadlineCommand { get; }

        public RelayCommand ExportTrainingCommand { get; }

        public RelayCommand ImportTrainingCommand { get; }

        private bool CanAddExercise()
        {
            return this.TrainingActive == false;
        }

        private void AddExercise()
        {
            this.training.Add(
                new TrainingElementViewModel(
                    TrainingElementType.Exercise,
                    new Exercise()
                    {
                        Name = "Neue Übung"
                    }));
        }

        private bool CanAddHeadline()
        {
            return this.TrainingActive == false;
        }

        private void AddHeadline()
        {
            this.training.Add(
                new TrainingElementViewModel(TrainingElementType.Headline, null)
                {
                    Headline = "Neue Überschrift"
                });
        }

        private bool CanExportTraining()
        {
            return this.TrainingActive == false
                && this.training.Any();
        }

        private void ExportTraining()
        {
            var path = this.fileDialogService.SaveFile(
                System.AppDomain.CurrentDomain.BaseDirectory,
                "XML files (*.xml)|*.xml",
                1,
                out bool okClicked);

            if (okClicked == false)
            {
                return;
            }

            if (path == null)
            {
                this.messageBoxService.ShowMessage(
                    EXPORT_ERROR_TEXT,
                    EXPORT_ERROR_TEXT,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            var trainingElements = new List<TrainingElement>();
            foreach (var element in training)
            {
                trainingElements.Add(new TrainingElement()
                {
                    Type = element.Type,
                    Headline = element.Headline,
                    Exercise = element.Exercise
                });
            }

            if (DataInterface.GetInstance().ExportTraining(path, trainingElements) == false)
            {
                this.messageBoxService.ShowMessage(
                    EXPORT_ERROR_TEXT,
                    EXPORT_ERROR_TEXT,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private bool CanImportTraining()
        {
            return this.TrainingActive == false;
        }

        private void ImportTraining()
        {
            var path = this.fileDialogService.OpenFile(
                System.AppDomain.CurrentDomain.BaseDirectory,
                "XML files (*.xml)|*.xml",
                1,
                out bool okClicked);

            if (okClicked == false)
            {
                return;
            }

            if (path == null)
            {
                this.messageBoxService.ShowMessage(
                    IMPORT_ERROR_TEXT,
                    IMPORT_ERROR_TEXT,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (DataInterface.GetInstance().ImportTraining(path, out var elements))
            {
                this.training.Clear();
                elements.ForEach(e => this.training.Add(
                    new TrainingElementViewModel(e.Type, e.Exercise)
                    {
                        Headline = e.Headline
                    })) ;
                return;
            }

            this.messageBoxService.ShowMessage(
                IMPORT_ERROR_TEXT,
                IMPORT_ERROR_TEXT,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
