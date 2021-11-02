using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.DataHandling.DataTransferObjects.CollectionExtensions;
using MyCoach.Defines;
using MyCoach.ViewModel;
using MyCoach.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class EditTrainingScheduleViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        private IMessageBoxService messageBoxService;
        private EditTrainingScheduleViewModel sut;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.SetupServices();
            this.sut = new EditTrainingScheduleViewModel(this.messageBoxService);
            this.sut.PropertyChanged += this.OnSutPropertyChanged;
        }

        [TestCleanup]
        public void Cleanup()
        {
            base.CleanupTestBase();
        }

        #endregion

        #region Construction and Properties Tests

        [TestMethod]
        public void Construction_HappyPath_CorrectlyInitialized()
        {
            // Data of data manager and buffer are synchronized
            Assert.IsTrue(Utilities.AreEqual(this.sut.Schedule, this.TrainingSchedule));
            Assert.IsTrue(Utilities.AreEqual(this.sut.Months, this.Months));

            // In buffer, start dates of months correspond to start date of schedule
            this.AssertThatStartDatesAreSynchonizedWithSchedule(this.sut.Months, this.sut.Schedule);

            // CurrentMonth was updated and changes were saved
            Assert.AreEqual((uint)0, this.sut.Months.Where(m => m.Number == MonthNumber.Current).First().TotalScores);
            Assert.AreEqual(
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                this.sut.Months.Where(m => m.Number == MonthNumber.Current).First().StartDate);
            Mock.Get(this.DataManager).Verify(dm => dm.SaveData<Month>(), Times.Once);

            // List of available categories was updated
            this.AssertThatAvailableCategoriesAreUpdatedCorrectly();

            // List of child view models was updated
            this.AssertThatEditMonthViewModelsAreUpdatedCorrectly();

            // No unsaved changes
            Assert.IsFalse(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void Duration_Changes_HasUnsavedChangesAndEditMonthViewModelsUpdated()
        {
            this.sut.Duration = 2;

            Assert.IsTrue(this.sut.HasUnsavedChanges);
            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.HasUnsavedChanges), this.PropertyChangedEvents.First());
            AssertThatEditMonthViewModelsAreUpdatedCorrectly();
        }

        [TestMethod]
        public void StartMonth_Changes_HasUnsavedChangesAndEditMonthViewModelsUpdated()
        {
            this.sut.StartMonth = new DateTime(1989, 11, 14);

            Assert.IsTrue(this.sut.HasUnsavedChanges);
            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.HasUnsavedChanges), this.PropertyChangedEvents.First());
            AssertThatEditMonthViewModelsAreUpdatedCorrectly();
        }

        [TestMethod]
        public void Type_Changes_HasUnsavedChangesAndTimeBasedScheduleElementsVisibleUpdatedAndEditMonthViewModelsUpdated()
        {
            this.sut.Type = ScheduleType.Generic;

            Assert.IsFalse(this.sut.TimeBasedScheduleElementsVisible);
            Assert.IsTrue(this.sut.HasUnsavedChanges);
            Assert.AreEqual(2, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.TimeBasedScheduleElementsVisible), this.PropertyChangedEvents[0]);
            Assert.AreEqual(nameof(this.sut.HasUnsavedChanges), this.PropertyChangedEvents[1]);
            AssertThatEditMonthViewModelsAreUpdatedCorrectly();
        }

        [TestMethod]
        public void EditMonthViewModel_Changes_HasUnsavedChangesUpdated()
        {
            Assert.AreNotEqual(333, this.sut.EditMonthViewModels.First().Category2Goal);

            this.sut.EditMonthViewModels.First().Category2Goal = 333;

            Assert.IsTrue(this.sut.HasUnsavedChanges);
            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.HasUnsavedChanges), this.PropertyChangedEvents.First());
        }

        #endregion

        #region Command Tests

        [TestMethod]
        public void DeleteScheduleCommand_ReturnsTrue()
        {
            Assert.IsTrue(this.sut.DeleteScheduleCommand.CanExecute(null));
        }

        [TestMethod]
        public void DeleteScheduleCommand_HappyPath_SetsDefaultTrainingScheduleAndDeletesGoalsOfAllMonths()
        {
            Mock.Get(this.DataManager).Setup(dm => dm.GetData<TrainingSchedule>()).Returns(DefaultDtos.TrainingSchedules);
            Assert.IsFalse(this.Months.All(
                m => m.Category1Goal == 0
                && m.Category2Goal == 0
                && m.Category3Goal == 0
                && m.Category4Goal == 0
                && m.Category5Goal == 0
                && m.Category6Goal == 0
                && m.Category7Goal == 0
                && m.Category8Goal == 0
                && m.TotalGoal == 0));

            this.sut.DeleteScheduleCommand.Execute(null);

            // TrainingSchedule Buffer is set to default values
            Mock.Get(this.DataManager).Verify(dm => dm.SetDefaults<TrainingSchedule>(), Times.Once);
            Assert.IsTrue(Utilities.AreEqual(this.sut.Schedule, DefaultDtos.TrainingSchedules.First()));

            // Goals of all months must be set to 0 and buffers must have been synchronized
            Assert.IsTrue(this.Months.All(
                m => m.Category1Goal == 0
                && m.Category2Goal == 0
                && m.Category3Goal == 0
                && m.Category4Goal == 0
                && m.Category5Goal == 0
                && m.Category6Goal == 0
                && m.Category7Goal == 0
                && m.Category8Goal == 0
                && m.TotalGoal == 0));
            Assert.IsTrue(Utilities.AreEqual(this.sut.Months, this.Months));

            // Type property changed from TimeBased to Generic, so EditMonthViewModels must have been updated
            this.AssertThatEditMonthViewModelsAreUpdatedCorrectly();
        }

        [TestMethod]
        public void DeleteScoresCommand_ReturnsTrue()
        {
            Assert.IsTrue(this.sut.DeleteScoresCommand.CanExecute(null));
        }

        [TestMethod]
        public void DeleteScoresCommand_HappyPath_DeletesScoresOfAllMonths()
        {
            Assert.IsFalse(this.Months.All(m => m.TotalScores == 0));

            this.sut.DeleteScoresCommand.Execute(null);

            // Goals of all months must be set to 0 and buffers must have been synchronized
            Assert.IsTrue(this.Months.All(m => m.TotalScores == 0));
            Assert.IsTrue(Utilities.AreEqual(this.sut.Months, this.Months));
        }

        [TestMethod]
        public void ResetCommandCanExecute_UnsavedChanges_ReturnsTrue()
        {
            Assert.IsFalse(this.sut.ResetCommand.CanExecute(null));

            this.sut.Duration = 2;

            Assert.IsTrue(this.sut.ResetCommand.CanExecute(null));
        }

        [TestMethod]
        public void ResetCommandExecute_HappyPath_ReloadsDataFromDataManager()
        {
            this.sut.Duration = 2;
            this.sut.StartMonth = new DateTime(1989, 11, 14);
            this.sut.EditMonthViewModels.First().Category2Goal = 333;
            this.sut.Type = ScheduleType.Generic;
            this.sut.EditMonthViewModels.First().Category2Goal = 111;

            this.sut.ResetCommand.Execute(null);

            // Data of data manager and buffer are synchronized
            Assert.IsTrue(Utilities.AreEqual(this.sut.Schedule, this.TrainingSchedule));
            Assert.IsTrue(Utilities.AreEqual(this.sut.Months, this.Months));

            // Changes have not been applied to DataManager
            Assert.AreNotEqual(2, this.sut.Duration);
            Assert.AreNotEqual(new DateTime(1989, 11, 14), this.sut.StartMonth);
            Assert.AreNotEqual(ScheduleType.Generic, this.sut.Type);
            Assert.AreNotEqual(333, this.sut.Months.Where(m => m.Number == MonthNumber.Month1).First().Category2Goal);
            Assert.AreNotEqual(111, this.sut.Months.Where(m => m.Number == MonthNumber.Current).First().Category2Goal);

            // List of child view models was updated
            this.AssertThatEditMonthViewModelsAreUpdatedCorrectly();
        }

        [TestMethod]
        public void SaveCommandCanExecute_UnsavedChanges_ReturnsTrue()
        {
            Assert.IsFalse(this.sut.SaveCommand.CanExecute(null));

            this.sut.Duration = 2;

            Assert.IsTrue(this.sut.SaveCommand.CanExecute(null));
        }

        [TestMethod]
        public void SaveCommandExecute_HappyPath_SavesDataCorrectlyAndSetsUnsavedChangesToFalseAndRaisesPropertyChanged()
        {
            this.sut.Duration = 2;
            this.sut.StartMonth = new DateTime(1989, 11, 14);
            this.sut.EditMonthViewModels.First().Category2Goal = 333;
            this.sut.Type = ScheduleType.Generic;
            this.sut.EditMonthViewModels.First().Category2Goal = 111;

            this.sut.SaveCommand.Execute(null);

            // Data of data manager and buffer are synchronized
            Assert.IsTrue(Utilities.AreEqual(this.sut.Schedule, this.TrainingSchedule));
            Assert.IsTrue(Utilities.AreEqual(this.sut.Months, this.Months));

            // Changes have been applied to DataManager
            Assert.AreEqual(2, this.sut.Duration);
            Assert.AreEqual(new DateTime(1989, 11, 14), this.sut.StartMonth);
            Assert.AreEqual(ScheduleType.Generic, this.sut.Type);
            Assert.AreEqual(333, this.sut.Months.Where(m => m.Number == MonthNumber.Month1).First().Category2Goal);
            Assert.AreEqual(111, this.sut.Months.Where(m => m.Number == MonthNumber.Current).First().Category2Goal);

            // DataManager has been triggered to save data to drive, data for months have been saved twice because of
            // update of current month during initialization
            Mock.Get(this.DataManager).Verify(dm => dm.SaveData<TrainingSchedule>(), Times.Once);
            Mock.Get(this.DataManager).Verify(dm => dm.SaveData<Month>(), Times.Exactly(2));

            // EditMonthViewModels were updated during application of changes
            this.AssertThatEditMonthViewModelsAreUpdatedCorrectly();
        }

        [TestMethod]
        public void SaveCommandExecute_HappyPathWithChangedStartMonth_ScoresDeletedAfterWarningIsShown()
        {
            this.sut.StartMonth = new DateTime(1989, 11, 14);
            Assert.AreNotEqual(0, this.Months.Sum(m => m.TotalScores));

            this.sut.SaveCommand.Execute(null);

            Assert.AreEqual(0, this.Months.Sum(m => m.TotalScores));
            Mock.Get(this.messageBoxService).Verify(
            service => service.ShowMessage(
                EditTrainingScheduleViewModel.CHANGE_SCHEDULE_TEXT,
                EditTrainingScheduleViewModel.RESET_SCORES_CAPTION,
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning), Times.Once);
        }

        [TestMethod]
        public void SaveCommandExecute_HappyPathWithChangedType_ScoresDeletedAfterWarningIsShown()
        {
            this.sut.Type = ScheduleType.Generic;
            Assert.AreNotEqual(0, this.Months.Sum(m => m.TotalScores));

            this.sut.SaveCommand.Execute(null);

            Assert.AreEqual(0, this.Months.Sum(m => m.TotalScores));
            Mock.Get(this.messageBoxService).Verify(
            service => service.ShowMessage(
                EditTrainingScheduleViewModel.CHANGE_SCHEDULE_TEXT,
                EditTrainingScheduleViewModel.RESET_SCORES_CAPTION,
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning), Times.Once);
        }

        [TestMethod]
        public void SaveCommandExecute_HappyPathWithoutChangedStartMonthOrType_ScoresNotDeleted()
        {
            this.sut.Duration = 2;
            this.sut.EditMonthViewModels.First().Category2Goal = 333;

            this.sut.SaveCommand.Execute(null);

            Assert.AreNotEqual(0, this.Months.Sum(m => m.TotalScores));
        }

        [TestMethod]
        public void SaveCommandExecute_HappyPathWithChangedScoresInDataBaseAndChangesWillNotDeleteScores_ChangesInScoreAreNotOverwrittenBySavingBuffer()
        {
            var initialTotalScores = this.Months.Sum(m => m.TotalScores);
            this.sut.EditMonthViewModels.First().Category2Goal = 333;
            this.Months.First().Category1Scores += 50;

            this.sut.SaveCommand.Execute(null);

            Assert.AreEqual(initialTotalScores + 50, this.sut.Months.Sum(m => m.TotalScores));
        }

        [TestMethod]
        public void SaveCommandExecute_HappyPathWithChangedScoresInDataBaseAndChangesWillDeleteScores_ScoresDeleted()
        {
            var initialTotalScores = this.Months.Sum(m => m.TotalScores);
            this.sut.Type = ScheduleType.Generic;
            this.Months.First().Category1Scores += 50;

            this.sut.SaveCommand.Execute(null);

            Assert.AreEqual(0, this.sut.Months.Sum(m => m.TotalScores));
        }

        [TestMethod]
        public void SaveCommandExecute_SavingFails_ShowsErrorMessageAndSetsUnsavedChangesToFalse()
        {
            Mock.Get(this.DataManager).Setup(dm => dm.SaveData<Month>()).Returns(false);
            this.sut.Duration = 2;

            this.sut.SaveCommand.Execute(null);

            Mock.Get(this.messageBoxService).Verify(
                service => service.ShowMessage(
                    BaseViewModel.SAVING_ERROR_TEXT,
                    BaseViewModel.SAVING_ERROR_CAPTION,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error), Times.Once);

            Assert.IsTrue(this.sut.HasUnsavedChanges == false);
        }

        #endregion

        #region Event Reactions

        [TestMethod]
        public void CategoriesChange_UpdatesAvailableCategories()
        {
            // Remove an active Category
            var firstActiveCategory = this.Categories.Where(c => c.Active && c.Type == ExerciseType.Training).First();
            this.Categories.Remove(firstActiveCategory);

            this.AssertThatAvailableCategoriesAreUpdatedCorrectly();

            // Activate an inactive Category
            this.Categories.Where(c => c.Active == false && c.Type == ExerciseType.Training).First().Active = true;

            this.AssertThatAvailableCategoriesAreUpdatedCorrectly();

            // Add an inactive Category (using ID of the formerly removed one)
            this.Categories.Add(
                new Category
                {
                    ID = firstActiveCategory.ID,
                    Active = false,
                    Name = "CategoryChangeTestName",
                    Type = ExerciseType.Training
                });

            this.AssertThatAvailableCategoriesAreUpdatedCorrectly();

            // Activate added Category
            this.Categories.Where(c => c.ID == firstActiveCategory.ID).First().Active = true;

            this.AssertThatAvailableCategoriesAreUpdatedCorrectly();

            // Change name of added Category
            this.Categories.Where(c => c.ID == firstActiveCategory.ID).First().Name = "I am tired of thinking of test strings ... ";

            this.AssertThatAvailableCategoriesAreUpdatedCorrectly();
        }

        #endregion

        #region Helper Methods

        private void SetupServices()
        {
            this.messageBoxService = Mock.Of<IMessageBoxService>(service =>
                service.ShowMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>()) == MessageBoxResult.Yes);
        }

        private void AssertThatAvailableCategoriesAreUpdatedCorrectly()
        {
            var savedCategoryNames = this.DataManager.GetData<Category>()
                .Where(c => c.Active && c.Type == ExerciseType.Training)
                .Select(c => c.Name).ToList();

            Assert.AreEqual(savedCategoryNames.Count + 1, this.sut.AvailableCategories.Count);

            for (int i = 0; i < savedCategoryNames.Count; i++)
            {
                Assert.AreEqual(savedCategoryNames[i], this.sut.AvailableCategories[i]);
            }

            Assert.AreEqual("Gesamt", this.sut.AvailableCategories[savedCategoryNames.Count]);
        }

        private void AssertThatEditMonthViewModelsAreUpdatedCorrectly()
        {
            // If we have a generic schedule, only one EditMonthViewModel without header is required
            if (this.sut.Type == ScheduleType.Generic)
            {
                Assert.AreEqual(1, this.sut.EditMonthViewModels.Count);
                Assert.AreEqual(string.Empty, this.sut.EditMonthViewModels.First().MonthName);
                return;
            }

            // Number of EditMonthViewModel corresponds to duration of schedule
            Assert.AreEqual((int)this.sut.Duration, this.sut.EditMonthViewModels.Count);

            // Start date of first EditMonthViewModel corresponds to start date of schedule
            Assert.AreEqual(
                this.sut.Schedule.StartMonth.ToString("y", CultureInfo.CurrentCulture),
                this.sut.EditMonthViewModels.First().MonthName);

            // Each month within timebased schedule got a EditMonthViewModel with correct heading and position
            Assert.IsTrue(this.sut.Months.All(
                m => m.Number == MonthNumber.Current 
                || (ushort)m.Number > this.sut.Duration
                || this.sut.EditMonthViewModels.Any(
                    vm => vm.MonthName == m.StartDate.ToString("y", CultureInfo.CurrentCulture)
                    && this.sut.EditMonthViewModels.IndexOf(vm) == (int)m.Number - 1)));
        }

        private void AssertThatStartDatesAreSynchonizedWithSchedule(IList<Month> months, TrainingSchedule schedule)
        {
            Assert.IsTrue(months.All(
                m => m.Number == MonthNumber.Current
                || m.StartDate.AddMonths(-(int)m.Number + 1) == schedule.StartMonth));
        }

        #endregion
    }
}
