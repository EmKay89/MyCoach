using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel;
using MyCoach.ViewModel.Services;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using MyExtensions.IEnumerable;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class TrainingViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        private const string validExportPath = "validExportPath";
        private const string validImportPath = "validImportPath";
        private IFileDialogService fileDialogService;
        private IMessageBoxService messageBoxService;
        TrainingViewModel sut;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.SetupServices();
            this.SetupDataManager();
            this.sut = new TrainingViewModel(fileDialogService, messageBoxService);
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
        public void Construction_TrainingInitialized()
        {
            Assert.IsNotNull(this.sut.Training);
            Assert.AreEqual(0, this.sut.Training.Count);
        }

        [TestMethod]
        public void Construction_ActiveCategoriesSetupCorrectly()
        {
            foreach (var category in this.Categories)
            {
                if (category.Active)
                {
                    this.sut.ActiveCategories.Any(c => c == category);
                }
            }
        }

        [TestMethod]
        public void Construction_CategoriesAndCategoryNamesDisplayedCorrectly()
        {
            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();
        }

        [TestMethod]
        public void ModeChanges_ToCircleTraining_CorrectTrainingSettingsVisibility()
        {
            this.sut.TrainingMode = TrainingMode.UserDefinedTraining;
            this.PropertyChangedEvents.Clear();

            this.sut.TrainingMode = TrainingMode.CircleTraining;

            Assert.IsTrue(this.sut.CircleTrainingElementsVisible);
            Assert.IsFalse(this.sut.FocusTrainingElementsVisible);
            Assert.IsTrue(this.sut.CircleOrFocusTrainingElementsVisible);

            Assert.AreEqual(6, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.TrainingMode), this.PropertyChangedEvents[0]);
            Assert.AreEqual(nameof(this.sut.CircleTrainingElementsVisible), this.PropertyChangedEvents[1]);
            Assert.AreEqual(nameof(this.sut.FocusTrainingElementsVisible), this.PropertyChangedEvents[2]);
            Assert.AreEqual(nameof(this.sut.CircleOrFocusTrainingElementsVisible), this.PropertyChangedEvents[3]);
            Assert.AreEqual(nameof(this.sut.NotEnoughExercisesAvailable), this.PropertyChangedEvents[4]);
            Assert.AreEqual(nameof(this.sut.ModeExplanation), this.PropertyChangedEvents[5]);
        }

        [TestMethod]
        public void ModeChanges_ToFocusTraining_CorrectTrainingSettingsVisibility()
        {
            this.sut.TrainingMode = TrainingMode.CircleTraining;
            this.PropertyChangedEvents.Clear();

            this.sut.TrainingMode = TrainingMode.FocusTraining;

            Assert.IsFalse(this.sut.CircleTrainingElementsVisible);
            Assert.IsTrue(this.sut.FocusTrainingElementsVisible);
            Assert.IsTrue(this.sut.CircleOrFocusTrainingElementsVisible);
            Assert.AreEqual(TrainingViewModel.DESCRIPTION_FOCUSTRAINING, this.sut.ModeExplanation);

            Assert.AreEqual(6, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.TrainingMode), this.PropertyChangedEvents[0]);
            Assert.AreEqual(nameof(this.sut.CircleTrainingElementsVisible), this.PropertyChangedEvents[1]);
            Assert.AreEqual(nameof(this.sut.FocusTrainingElementsVisible), this.PropertyChangedEvents[2]);
            Assert.AreEqual(nameof(this.sut.CircleOrFocusTrainingElementsVisible), this.PropertyChangedEvents[3]);
            Assert.AreEqual(nameof(this.sut.NotEnoughExercisesAvailable), this.PropertyChangedEvents[4]);
            Assert.AreEqual(nameof(this.sut.ModeExplanation), this.PropertyChangedEvents[5]);
        }

        [TestMethod]
        public void ModeChanges_ToUserDefinedTraining_CorrectTrainingSettingsVisibility()
        {
            this.sut.TrainingMode = TrainingMode.FocusTraining;
            this.PropertyChangedEvents.Clear();

            this.sut.TrainingMode = TrainingMode.UserDefinedTraining;

            Assert.IsFalse(this.sut.CircleTrainingElementsVisible);
            Assert.IsFalse(this.sut.FocusTrainingElementsVisible);
            Assert.IsFalse(this.sut.CircleOrFocusTrainingElementsVisible);
            Assert.AreEqual(TrainingViewModel.DESCRIPTION_USERDEFINEDTRAINING, this.sut.ModeExplanation);

            Assert.AreEqual(6, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.TrainingMode), this.PropertyChangedEvents[0]);
            Assert.AreEqual(nameof(this.sut.CircleTrainingElementsVisible), this.PropertyChangedEvents[1]);
            Assert.AreEqual(nameof(this.sut.FocusTrainingElementsVisible), this.PropertyChangedEvents[2]);
            Assert.AreEqual(nameof(this.sut.CircleOrFocusTrainingElementsVisible), this.PropertyChangedEvents[3]);
            Assert.AreEqual(nameof(this.sut.NotEnoughExercisesAvailable), this.PropertyChangedEvents[4]);
            Assert.AreEqual(nameof(this.sut.ModeExplanation), this.PropertyChangedEvents[5]);
        }

        #endregion

        #region Command Tests

        [TestMethod]
        public void StartCommandCanExecute_CircleTraining_True()
        {            
            this.Categories.Where(c => c.ID == ExerciseCategory.Category1).FirstOrDefault().Active = true;
            this.sut.Category1EnabledForTraining = true;
            this.sut.TrainingMode = TrainingMode.CircleTraining;

            Assert.IsTrue(this.sut.StartTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartCommandCanExecute_CircleTrainingWithoutEnabledCategories_False()
        {            
            this.Categories.Foreach(c => c.Active = false);
            this.sut.TrainingMode = TrainingMode.CircleTraining;

            Assert.IsFalse(this.sut.StartTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartCommandCanExecute_FocusTraining_True()
        {
            this.sut.CategoryInFocus = this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First();
            this.sut.TrainingMode = TrainingMode.FocusTraining;

            Assert.IsTrue(this.sut.StartTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartCommandCanExecute_FocusTrainingWihtoutCategoryInFocus_False()
        {
            this.sut.CategoryInFocus = null;
            this.sut.TrainingMode = TrainingMode.FocusTraining;

            Assert.IsFalse(this.sut.StartTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartCommandCanExecute_UserDefinedTrainingWithExercises_True()
        {
            var exercise = new Exercise();
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise));
            this.sut.TrainingMode = TrainingMode.UserDefinedTraining;

            Assert.IsTrue(this.sut.StartTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartCommandCanExecute_UserDefinedTrainingWithoutExercises_False()
        {
            this.sut.Training.Clear();
            this.sut.TrainingMode = TrainingMode.UserDefinedTraining;

            Assert.IsFalse(this.sut.StartTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartCommandExecute_ActivatesAndDeactivatesTraining()
        {
            var exercise = new Exercise();
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise));
            this.sut.TrainingMode = TrainingMode.UserDefinedTraining;
            this.PropertyChangedEvents.Clear();

            Assert.IsFalse(this.sut.TrainingActive);

            this.sut.StartTrainingCommand.Execute(null);

            Assert.AreEqual(2, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.TrainingActive), this.PropertyChangedEvents[0]);
            Assert.AreEqual(nameof(this.sut.TrainingSettingsEnabled), this.PropertyChangedEvents[1]);
            Assert.IsTrue(this.sut.TrainingActive);

            this.sut.StartTrainingCommand.Execute(null);

            Assert.IsFalse(this.sut.TrainingActive);
        }

        [TestMethod]
        public void TrainingElementVmRemoveExerciseCommandExecute_HappyPath_RemovesExerciseFromTraining()
        {
            var exercise = new Exercise();
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise));

            this.sut.Training.Single().RemoveExerciseCommand.Execute(null);

            Assert.IsFalse(this.sut.Training.Any());
        }

        [TestMethod]
        [DataRow(false, TrainingMode.UserDefinedTraining, true)]
        [DataRow(true, TrainingMode.UserDefinedTraining, false)]
        [DataRow(false, TrainingMode.CircleTraining, false)]    
        public void ImportCommandCanExecute_VariousConditions_ReturnsCorrectValue(
            bool trainingActive,
            TrainingMode trainingMode,
            bool canExecute)
        {
            this.sut.TrainingMode = trainingMode;

            if (trainingActive)
            {
                this.sut.Training.Start();
            }

            Assert.AreEqual(canExecute, this.sut.ImportTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void ImportTrainingCommandExecute_HappyPath_ImportsExercisesFromFileToTraining()
        {
            this.sut.ImportTrainingCommand.Execute(null);

            Assert.IsTrue(
                Utilities.AreEqual(
                    this.Exercises, 
                    this.sut.Training.Where(e => e.Type == TrainingElementType.Exercise).Select(e => e.Exercise).ToList()));
        }

        [TestMethod]
        public void ImportTrainingCommandExecute_WithPreexistingElements_PreexistingElementsAreCleared()
        {
            var vm = new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise());

            this.sut.ImportTrainingCommand.Execute(null);

            Assert.IsTrue(
                Utilities.AreEqual(
                    this.Exercises,
                    this.sut.Training.Where(e => e.Type == TrainingElementType.Exercise).Select(e => e.Exercise).ToList()));
        }

        [TestMethod]
        public void ImportTrainingCommandExecute_AbortedByUser_PreexistingElementsRemain()
        {
            var vm = new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise());
            this.sut.Training.Add(vm);
            var okClicked = false;
            Mock.Get(this.fileDialogService).Setup(
                s => s.OpenFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), out okClicked));

            this.sut.ImportTrainingCommand.Execute(null);

            Assert.AreEqual(vm, this.sut.Training.Single());
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("someInvalidPath")]
        public void ImportTrainingCommandExecute_LoadingErrors_PreexistingElementsRemainAndErrorMessageShown(string path)
        {
            var vm = new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise());
            this.sut.Training.Add(vm);
            var okClicked = true;
            Mock.Get(this.fileDialogService).Setup(
                s => s.OpenFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), out okClicked)).Returns(path);

            this.sut.ImportTrainingCommand.Execute(null);

            Assert.AreEqual(vm, this.sut.Training.Single());
            Mock.Get(this.messageBoxService).Verify(service => service.ShowMessage(
                ExercisesViewModel.IMPORT_ERROR_TEXT,
                ExercisesViewModel.IMPORT_ERROR_TEXT,
                It.IsAny<MessageBoxButton>(),
                It.IsAny<MessageBoxImage>()), Times.Once());
        }

        [TestMethod]
        [DataRow(false, TrainingMode.UserDefinedTraining, true, true)]
        [DataRow(true, TrainingMode.UserDefinedTraining, true, false)]
        [DataRow(false, TrainingMode.CircleTraining, true, false)]
        [DataRow(false, TrainingMode.UserDefinedTraining, false, false)]
        public void ExportCommandCanExecute_VariousConditions_ReturnsCorrectValue(
            bool trainingActive,
            TrainingMode trainingMode,
            bool hasElement,
            bool canExecute)
        {
            if (hasElement)
            {
                var exercise = new Exercise();
                this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise));
            }

            this.sut.TrainingMode = trainingMode;

            if (trainingActive)
            {
                this.sut.Training.Start();
            }

            Assert.AreEqual(canExecute, this.sut.ExportTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void ExportTrainingCommandExecute_HappyPath_CallsDataManagerToExportListOfExercise()
        {
            var vm = new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise());
            this.sut.Training.Add(vm);

            this.sut.ExportTrainingCommand.Execute(null);

            Assert.AreEqual(vm, this.sut.Training.Single());
            Mock.Get(this.DataManager).Verify(
                dm => dm.TryExportTraining(validExportPath, It.IsAny<List<Exercise>>()), Times.Once());
        }

        [TestMethod]
        public void ExportTrainingCommandExecute_AbortedByUser_DoesNotCallDataManagerToExportListOfExercise()
        {
            var vm = new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise());
            this.sut.Training.Add(vm);
            var okClicked = false;
            Mock.Get(this.fileDialogService).Setup(
                s => s.SaveFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), out okClicked));

            this.sut.ExportTrainingCommand.Execute(null);

            Assert.AreEqual(vm, this.sut.Training.Single());
            Mock.Get(this.DataManager).Verify(
                dm => dm.TryExportTraining(validExportPath, It.IsAny<List<Exercise>>()), Times.Never());
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("someInvalidPath")]
        public void ExportTrainingCommandExecute_LoadingErrors_DoesNotCallDataManagerToExportListOfExerciseAndErrorMessageShown(string path)
        {
            var vm = new TrainingElementViewModel(TrainingElementType.Exercise, new Exercise());
            this.sut.Training.Add(vm);
            var okClicked = true;
            Mock.Get(this.fileDialogService).Setup(
                s => s.SaveFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), out okClicked)).Returns(path);

            this.sut.ExportTrainingCommand.Execute(null);

            Assert.AreEqual(vm, this.sut.Training.Single());
            Mock.Get(this.messageBoxService).Verify(service => service.ShowMessage(
                ExercisesViewModel.EXPORT_ERROR_TEXT,
                ExercisesViewModel.EXPORT_ERROR_TEXT,
                It.IsAny<MessageBoxButton>(),
                It.IsAny<MessageBoxImage>()), Times.Once());
            Mock.Get(this.DataManager).Verify(
                dm => dm.TryExportTraining(validExportPath, It.IsAny<List<Exercise>>()), Times.Never());
        }

        #endregion

        #region Event Reactions

        [TestMethod]
        public void CategoryChanges_RemovedAndAdded_CategoriesAndCategoryNamesDisplayedCorrectly()
        {
            var cat1 = this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First();
            this.Categories.Remove(cat1);

            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();

            this.PropertyChangedEvents.Clear();
            this.Categories.Add(cat1);

            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();
        }

        [TestMethod]
        public void CategoryChanges_Name_CategoriesAndCategoryNamesDisplayedCorrectly()
        {
            this.Categories.Foreach(c => c.Name = string.Concat(c.Name, "_Test"));
            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();
        }

        [TestMethod]
        public void CategoryChanges_Active_CategoriesAndCategoryNamesDisplayedCorrectly()
        {
            this.Categories.Foreach(c => c.Active = !c.Active);
            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();
        }

        [TestMethod]
        public void TrainingExercises_AllCompleted_TrainingDeactivated()
        {
            // Actually belongs to a separate unit test class for Training class ... 
            var exercise1 = new Exercise() { Name = "Test1" };
            var exercise2 = new Exercise() { Name = "Test2" }; ;
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise1));
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise2));
            this.sut.StartTrainingCommand.Execute(null);

            (this.sut.Training.First() as TrainingElementViewModel).Completed = true;

            Assert.IsTrue(this.sut.TrainingActive);

            (this.sut.Training.Last() as TrainingElementViewModel).Completed = true;

            Assert.IsFalse(this.sut.TrainingActive);
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
            List<Exercise> expectedExercises = new List<Exercise>();
            this.Exercises.Foreach(e => expectedExercises.Add((Exercise)e.Clone()));
            Mock.Get(this.DataManager).Setup(dm => dm.TryImportTraining(validImportPath, out expectedExercises)).Returns(true);
            Mock.Get(this.DataManager).Setup(dm => dm.TryExportTraining(validExportPath, It.IsAny<List<Exercise>>())).Returns(true);
        }

        private void AssertThatCategoryNamesAndActivitiyValuesAreCorrect()
        {
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.WarmUp).FirstOrDefault()?.Active ?? false,
                this.sut.CategoryWarmUpActive);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.WarmUp).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.CategoryWarmUpName);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category1).FirstOrDefault()?.Active ?? false,
                this.sut.Category1Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category1).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category1Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category2).FirstOrDefault()?.Active ?? false,
                this.sut.Category2Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category2).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category2Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category3).FirstOrDefault()?.Active ?? false,
                this.sut.Category3Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category3).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category3Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category4).FirstOrDefault()?.Active ?? false,
                this.sut.Category4Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category4).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category4Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category5).FirstOrDefault()?.Active ?? false,
                this.sut.Category5Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category5).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category5Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category6).FirstOrDefault()?.Active ?? false,
                this.sut.Category6Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category6).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category6Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category7).FirstOrDefault()?.Active ?? false,
                this.sut.Category7Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category7).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category7Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category8).FirstOrDefault()?.Active ?? false,
                this.sut.Category8Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category8).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category8Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.CoolDown).FirstOrDefault()?.Active ?? false,
                this.sut.CategoryCoolDownActive);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.CoolDown).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.CategoryCoolDownName);
        }

        #endregion
    }
}
