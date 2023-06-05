using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling.DataManager;
using MyCoach.Model.DataTransferObjects;
using MyCoach.ViewModel;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using MyCoach.ViewModel.TrainingSettingsViewModels;
using MyExtensions.IEnumerable;
using MyMvvm.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MyCoachTests.ViewModel.TrainingSettingsViewModels
{
    [TestClass]
    public class UserDefinedTrainingSettingsViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        private const string validExportPath = "validExportPath";
        private const string validImportPath = "validImportPath";
        private IFileDialogService fileDialogService;
        private IMessageBoxService messageBoxService;
        UserDefinedTrainingSettingsViewModel sut;
        private readonly Training training = new Training();

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.SetupServices();
            this.SetupDataManager();
            this.sut = new UserDefinedTrainingSettingsViewModel(this.training, fileDialogService, messageBoxService);
            this.sut.PropertyChanged += this.OnSutPropertyChanged;
            TrainingEvaluator.MessageBoxService = this.messageBoxService;
        }

        [TestCleanup]
        public void Cleanup()
        {
            base.CleanupTestBase();
        }

        #endregion

        #region Construction and Properties Tests

        [TestMethod]
        public void CanStartTraining_ReturnsTrueOnlyWhenTrainingContainsExercises()
        {
            this.training.Clear();

            Assert.IsFalse(this.sut.CanStartTraining);

            this.training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise()));

            Assert.IsTrue(this.sut.CanStartTraining);
        }

        #endregion

        #region Command Tests

        [TestMethod]
        public void AddExerciseCommandCanExecute_ReturnsTrueOnlyAsLongTrainingIsNotActive()
        {
            Assert.AreEqual(true, this.sut.AddExerciseCommand.CanExecute(null));

            this.sut.TrainingActive = true;

            Assert.AreEqual(false, this.sut.AddExerciseCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddExerciseCommandExecute_HappyPath_AddsNewTrainingElementOfTypeExercise()
        {
            Assert.AreEqual(this.training.Count, 0);

            this.sut.AddExerciseCommand.Execute(null);

            Assert.AreEqual(this.training.Single().Type, TrainingElementType.Exercise);
        }

        [TestMethod]
        public void AddHeadlineCommandCanExecute_ReturnsTrueOnlyAsLongTrainingIsNotActive()
        {
            Assert.AreEqual(true, this.sut.AddHeadlineCommand.CanExecute(null));

            this.sut.TrainingActive = true;

            Assert.AreEqual(false, this.sut.AddHeadlineCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddHeadlineCommandExecute_HappyPath_AddsNewTrainingElementOfTypeHeadline()
        {
            Assert.AreEqual(this.training.Count, 0);

            this.sut.AddHeadlineCommand.Execute(null);

            Assert.AreEqual(this.training.Single().Type, TrainingElementType.Headline);
        }

        [TestMethod]
        public void ImportCommandCanExecute_ReturnsTrueOnlyAsLongTrainingIsNotActive()
        {
            Assert.AreEqual(true, this.sut.ImportTrainingCommand.CanExecute(null));

            this.sut.TrainingActive = true;

            Assert.AreEqual(false, this.sut.ImportTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void ImportTrainingCommandExecute_HappyPath_ImportsExercisesFromFileToTraining()
        {
            Assert.AreEqual(this.training.Count, 0);
            Assert.AreNotEqual(this.Exercises.Count, 0);

            this.sut.ImportTrainingCommand.Execute(null);

            Assert.IsTrue(
                Utilities.AreEqual(
                    this.Exercises,
                    this.training.Where(e => e.Type == TrainingElementType.Exercise).Select(e => e.Exercise).ToList()));
        }

        [TestMethod]
        public void ImportTrainingCommandExecute_WithPreexistingElements_PreexistingElementsAreCleared()
        {
            Assert.AreEqual(this.training.Count, 0);
            Assert.AreNotEqual(this.Exercises.Count, 0);
            var vm = new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise());

            this.sut.ImportTrainingCommand.Execute(null);

            Assert.IsTrue(
                Utilities.AreEqual(
                    this.Exercises,
                    this.training.Where(e => e.Type == TrainingElementType.Exercise).Select(e => e.Exercise).ToList()));
        }

        [TestMethod]
        public void ImportTrainingCommandExecute_AbortedByUser_PreexistingElementsRemain()
        {
            var vm = new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise());
            this.training.Add(vm);
            var okClicked = false;
            Mock.Get(this.fileDialogService).Setup(
                s => s.OpenFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), out okClicked));

            this.sut.ImportTrainingCommand.Execute(null);

            Assert.AreEqual(vm, this.training.Single());
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("someInvalidPath")]
        public void ImportTrainingCommandExecute_LoadingErrors_PreexistingElementsRemainAndErrorMessageShown(string path)
        {
            var vm = new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise());
            this.training.Add(vm);
            var okClicked = true;
            Mock.Get(this.fileDialogService).Setup(
                s => s.OpenFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), out okClicked)).Returns(path);

            this.sut.ImportTrainingCommand.Execute(null);

            Assert.AreEqual(vm, this.training.Single());
            Mock.Get(this.messageBoxService).Verify(service => service.ShowMessage(
                ExercisesViewModel.IMPORT_ERROR_TEXT,
                ExercisesViewModel.IMPORT_ERROR_TEXT,
                It.IsAny<MessageBoxButton>(),
                It.IsAny<MessageBoxImage>()), Times.Once());
        }

        [TestMethod]
        [DataRow(false, true, true)]
        [DataRow(false, false, false)]
        [DataRow(true, false, false)]
        [DataRow(true, true, false)]
        public void ExportCommandCanExecute_VariousConditions_ReturnsCorrectValue(bool isActive, bool hasElements, bool result)
        {
            if (hasElements)
            {
                this.training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise()));
            }

            if (isActive)
            {
                this.sut.TrainingActive = true;
            }

            Assert.AreEqual(result, this.sut.ExportTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void ExportTrainingCommandExecute_HappyPath_CallsDataManagerToExportListOfExercise()
        {
            var vm = new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise());
            this.training.Add(vm);

            this.sut.ExportTrainingCommand.Execute(null);

            Assert.AreEqual(vm, this.training.Single());
            Mock.Get(this.DataManager).Verify(
                dm => dm.TryExportTraining(validExportPath, It.IsAny<List<TrainingElement>>()), Times.Once());
        }

        [TestMethod]
        public void ExportTrainingCommandExecute_AbortedByUser_DoesNotCallDataManagerToExportListOfExercise()
        {
            var vm = new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise());
            this.training.Add(vm);
            var okClicked = false;
            Mock.Get(this.fileDialogService).Setup(
                s => s.SaveFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), out okClicked));

            this.sut.ExportTrainingCommand.Execute(null);

            Assert.AreEqual(vm, this.training.Single());
            Mock.Get(this.DataManager).Verify(
                dm => dm.TryExportTraining(validExportPath, It.IsAny<List<TrainingElement>>()), Times.Never());
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("someInvalidPath")]
        public void ExportTrainingCommandExecute_LoadingErrors_DoesNotCallDataManagerToExportListOfExerciseAndErrorMessageShown(string path)
        {
            var vm = new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise());
            this.training.Add(vm);
            var okClicked = true;
            Mock.Get(this.fileDialogService).Setup(
                s => s.SaveFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), out okClicked)).Returns(path);

            this.sut.ExportTrainingCommand.Execute(null);

            Assert.AreEqual(vm, this.training.Single());
            Mock.Get(this.messageBoxService).Verify(service => service.ShowMessage(
                ExercisesViewModel.EXPORT_ERROR_TEXT,
                ExercisesViewModel.EXPORT_ERROR_TEXT,
                It.IsAny<MessageBoxButton>(),
                It.IsAny<MessageBoxImage>()), Times.Once());
            Mock.Get(this.DataManager).Verify(
                dm => dm.TryExportTraining(validExportPath, It.IsAny<List<TrainingElement>>()), Times.Never());
        }

        #endregion

        #region Helper Methods

        private void SetupServices()
        {
            var okClicked = true;
            this.messageBoxService = Mock.Of<IMessageBoxService>(service =>
                service.ShowMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>()) == MessageBoxResult.Yes);
            this.fileDialogService = Mock.Of<IFileDialogService>(service =>
                service.OpenFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), out okClicked) == validImportPath &&
                service.SaveFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), out okClicked) == validExportPath);
        }

        private void SetupDataManager()
        {
            Assert.IsTrue(this.Exercises.Any());
            List<TrainingElement> expectedTrainingElements = new List<TrainingElement>();
            this.Exercises.ForEach(e => expectedTrainingElements.Add(
                new TrainingElement()
                {
                    Type = TrainingElementType.Exercise,
                    Exercise = (Exercise)e.Clone() 
                }));
            Mock.Get(this.DataManager).Setup(dm => dm.TryImportTraining(validImportPath, out expectedTrainingElements)).Returns(true);
            Mock.Get(this.DataManager).Setup(dm => dm.TryExportTraining(validExportPath, It.IsAny<List<TrainingElement>>())).Returns(true);
        }

        #endregion
    }
}
