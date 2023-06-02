using MyCoach.DataHandling;
using MyCoach.Model.Defines;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.Services;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
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
            this.ExportTrainingCommand = new RelayCommand(this.ExportTraining, this.CanExportTraining);
            this.ImportTrainingCommand = new RelayCommand(this.ImportTraining, this.CanImportTraining);
        }

        public const string IMPORT_ERROR_TEXT = "Importieren fehlgeschlagen";
        public const string EXPORT_ERROR_TEXT = "Exportieren fehlgeschlagen";

        public override bool CanStartTraining => this.training.Any();

        public override TrainingSettings TrainingSettings => new TrainingSettings(TrainingMode.UserDefinedTraining);

        public RelayCommand ExportTrainingCommand { get; }

        public RelayCommand ImportTrainingCommand { get; }

        private bool CanExportTraining()
        {
            return this.TrainingActive == false
                && this.training.Any(e => e != null && e.Type == TrainingElementType.Exercise);
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

            var exercises = this.training.Where(e => e.Type == TrainingElementType.Exercise && e.Exercise != null)
                .Select(e => e.Exercise).ToList();

            if (DataInterface.GetInstance().ExportTraining(path, exercises) == false)
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

            if (DataInterface.GetInstance().ImportTraining(path, out var exercises))
            {
                this.training.Clear();
                exercises.ForEach(e => this.training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, e)));
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
