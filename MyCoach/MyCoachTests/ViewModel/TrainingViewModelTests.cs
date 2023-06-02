﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel;
using MyCoach.ViewModel.Services;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using MyCoach.ViewModel.TrainingSettingsViewModels;
using MyExtensions.Enumeration;
using MyExtensions.IEnumerable;
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
            this.sut = new TrainingViewModel(this.fileDialogService, this.messageBoxService);
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
        public void Construction_TrainingModeIsCircleTraining()
        {
            Assert.AreEqual(TrainingMode.CircleTraining, this.sut.TrainingMode);
        }

        [TestMethod]
        public void Construction_SelectedViewModelIsAutoGeneratedViewModel()
        {
            Assert.IsInstanceOfType(this.sut.SelectedViewModel, typeof(AutoGeneratedTrainingSettingsViewModel));
        }

        [TestMethod]
        [DataRow(TrainingMode.FocusTraining)]
        [DataRow(TrainingMode.RandomTraining)]
        [DataRow(TrainingMode.UserDefinedTraining)]
        [DataRow(TrainingMode.CircleTraining)]
        public void ModeChanges_CorrectTrainingSettingsViewModelIsSelectedAndEventsFired(TrainingMode mode)
        {
            if (this.sut.TrainingMode == mode)
            {
                this.sut.TrainingMode = mode.Next();
            }

            this.PropertyChangedEvents.Clear();

            this.sut.TrainingMode = mode;

            if (mode == TrainingMode.UserDefinedTraining)
            {
                Assert.IsInstanceOfType(this.sut.SelectedViewModel, typeof(UserDefinedTrainingSettingsViewModel));
            }
            else
            {
                Assert.IsInstanceOfType(this.sut.SelectedViewModel, typeof(AutoGeneratedTrainingSettingsViewModel));
            }

            if (mode == TrainingMode.UserDefinedTraining)
            {
                Assert.AreEqual(3, this.PropertyChangedEvents.Count);

                Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.SelectedViewModel));
                Assert.AreEqual(this.PropertyChangedEvents[1], nameof(this.sut.TrainingMode));
                Assert.AreEqual(this.PropertyChangedEvents[2], nameof(this.sut.ModeExplanation));
            }
            else
            {
                Assert.AreEqual(2, this.PropertyChangedEvents.Count);
                Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.TrainingMode));
                Assert.AreEqual(this.PropertyChangedEvents[1], nameof(this.sut.ModeExplanation));
            }
        }

        #endregion

        #region Command Tests

        [TestMethod]
        public void StartCommandCanExecute_ReturnsValueFromTrainingSettingsViewModel()
        {
            this.Categories.ForEach(c => c.Active = false);
            this.sut.TrainingMode = TrainingMode.CircleTraining;

            Assert.IsFalse(this.sut.AutoGeneratedTrainingViewModel.CanStartTraining);
            Assert.IsFalse(this.sut.StartTrainingCommand.CanExecute(null));

            this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First().Active = true;
            this.sut.AutoGeneratedTrainingViewModel.Category1EnabledForTraining = true;

            Assert.IsTrue(this.sut.AutoGeneratedTrainingViewModel.CanStartTraining);
            Assert.IsTrue(this.sut.StartTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartCommandExecute_ActivatesAndDeactivatesTraining()
        {
            var exercise = new Exercise();            
            this.sut.TrainingMode = TrainingMode.UserDefinedTraining;
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise));
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
        public void TrainingElementVmMoveElementUpCommandExecute_HappyPath_MovesExerciseUp()
        {
            var exercise1 = new Exercise();
            var exercise2 = new Exercise();
            var exercise3 = new Exercise();
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise1));
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise2));
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise3));

            this.sut.Training.Last().MoveElementUpCommand.Execute(null);
            Assert.AreEqual(exercise1, this.sut.Training.First().Exercise);
            Assert.AreEqual(exercise2, this.sut.Training.Last().Exercise);

            this.sut.Training[1].MoveElementUpCommand.Execute(null);
            Assert.AreEqual(exercise3, this.sut.Training.First().Exercise);
            Assert.AreEqual(exercise2, this.sut.Training.Last().Exercise);

            this.sut.Training.First().MoveElementUpCommand.Execute(null);
            Assert.AreEqual(exercise3, this.sut.Training.First().Exercise);
            Assert.AreEqual(exercise2, this.sut.Training.Last().Exercise);
        }

        [TestMethod]
        public void TrainingElementVmMoveElementDownCommandExecute_HappyPath_MovesExerciseDown()
        {
            var exercise1 = new Exercise();
            var exercise2 = new Exercise();
            var exercise3 = new Exercise();
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise1));
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise2));
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise3));

            this.sut.Training.First().MoveElementDownCommand.Execute(null);
            Assert.AreEqual(exercise2, this.sut.Training.First().Exercise);
            Assert.AreEqual(exercise3, this.sut.Training.Last().Exercise);

            this.sut.Training[1].MoveElementDownCommand.Execute(null);
            Assert.AreEqual(exercise2, this.sut.Training.First().Exercise);
            Assert.AreEqual(exercise1, this.sut.Training.Last().Exercise);

            this.sut.Training.Last().MoveElementDownCommand.Execute(null);
            Assert.AreEqual(exercise2, this.sut.Training.First().Exercise);
            Assert.AreEqual(exercise1, this.sut.Training.Last().Exercise);
        }

        [TestMethod]
        public void TrainingElementVmRemoveElementCommandExecute_HappyPath_RemovesExerciseFromTraining()
        {
            var exercise = new Exercise();
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise));

            this.sut.Training.Single().RemoveElementCommand.Execute(null);

            Assert.IsFalse(this.sut.Training.Any());
        }

        #endregion

        #region Event Reactions

        [TestMethod]
        public void TrainingExercises_AllCompleted_TrainingDeactivated()
        {
            // Actually belongs to a separate unit test class for Training class ... 
            var exercise1 = new Exercise() { Name = "Test1" };
            var exercise2 = new Exercise() { Name = "Test2" };
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise1));
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.Exercise, exercise2));
            this.sut.Training.Start();

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

        #endregion
    }
}
