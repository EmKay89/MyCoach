using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using MyCoach.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class ViewTrainingScheduleViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        private ViewTrainingScheduleViewModel sut;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.sut = new ViewTrainingScheduleViewModel();
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
        public void Construction_ChildViewModelsCreated()
        {
            Assert.IsNotNull(this.sut.CurrentMonthViewModel);
            Assert.IsNotNull(this.sut.TrainingScheduleOverviewViewModel);
            Assert.IsNotNull(this.sut.MonthViewModelsInTimeBasedSchedule);
        }

        [TestMethod]
        public void Construction_CurrentMonthViewModel_CorrectlySetForGenericSchedule()
        {
            this.TrainingSchedule.ScheduleType = ScheduleType.Generic;
            this.sut = new ViewTrainingScheduleViewModel();
            var currentMonth = TestDtos.TrainingScores.Where(m => m.Number == MonthNumber.Current).First();
            Assert.IsTrue(this.sut.CurrentMonthViewModel.Month == currentMonth);
        }

        [TestMethod]
        public void Construction_CurrentMonthViewModel_CorrectlySetForTimeBasedSchedule()
        {
            this.TrainingSchedule.ScheduleType = ScheduleType.TimeBased;
            this.sut = new ViewTrainingScheduleViewModel();
            var currentMonth = TestDtos.TrainingScores.Where(m => m.Number == MonthNumber.Month1).First();
            Assert.IsTrue(this.sut.CurrentMonthViewModel.Month == currentMonth);
        }

        [TestMethod]
        public void Construction_MonthViewModelsInTimeBasedSchedule_CorrectMonthsAdded()
        {
            ushort duration = 3;
            this.TrainingSchedule.Duration = duration;

            this.sut = new ViewTrainingScheduleViewModel();

            Assert.IsTrue(this.sut.MonthViewModelsInTimeBasedSchedule.Count == duration);
            for (int i = 0; i < duration; i++)
            {
                Assert.IsTrue(this.sut.MonthViewModelsInTimeBasedSchedule[i].Month == TestDtos.TrainingScores[i + 1]);
            }
        }

        [TestMethod]
        public void Construction_ScheduleTypeGeneric_TimeBasedElementsNotVisible()
        {
            this.TrainingSchedule.ScheduleType = MyCoach.Defines.ScheduleType.Generic;

            this.sut = new ViewTrainingScheduleViewModel();

            Assert.IsFalse(this.sut.TimeBasedScheduleElementsVisible);
        }

        [TestMethod]
        public void Construction_ScheduleTypeTimeBased_TimeBasedElementsVisible()
        {
            this.TrainingSchedule.ScheduleType = MyCoach.Defines.ScheduleType.TimeBased;

            this.sut = new ViewTrainingScheduleViewModel();

            Assert.IsTrue(this.sut.TimeBasedScheduleElementsVisible);
        }

        [TestMethod]
        public void Construction_ScheduleTypeTimeBased_OverviewElementsVisibleByDefault()
        {
            this.TrainingSchedule.ScheduleType = MyCoach.Defines.ScheduleType.TimeBased;

            this.sut = new ViewTrainingScheduleViewModel();

            Assert.IsTrue(this.sut.OverviewElementsVisible);
        }

        #endregion

        #region Command Tests

        [TestMethod]
        public void DisplayTimeBasedElementsCommandCanExecute_ReturnsTrue()
        {
            Assert.IsTrue(this.sut.DisplayTimeBasedElementsCommand.CanExecute(null));
        }

        [TestMethod]
        public void DisplayTimeBasedElementsCommand_HappyPath_UpdatesVisibilities()
        {
            this.TrainingSchedule.ScheduleType = MyCoach.Defines.ScheduleType.TimeBased;
            this.sut = new ViewTrainingScheduleViewModel();

            this.sut.DisplayTimeBasedElementsCommand.Execute("Details");

            Assert.IsTrue(this.sut.DetailsElementsVisible);
            Assert.IsFalse(this.sut.OverviewElementsVisible);

            this.sut.DisplayTimeBasedElementsCommand.Execute("Overview");

            Assert.IsFalse(this.sut.DetailsElementsVisible);
            Assert.IsTrue(this.sut.OverviewElementsVisible);
        }

        #endregion

        #region Event Reactions

        [TestMethod]
        public void TrainingScheduleChanges_ScheduleType_UpdatesVisibilities()
        {
            this.TrainingSchedule.ScheduleType = ScheduleType.Generic;

            Assert.IsFalse(this.sut.TimeBasedScheduleElementsVisible);
        }

        [TestMethod]
        public void TrainingScheduleChanges_StartMonthToCurrentMonth_CurrentMonthViewModelIsFirstViewModelOfTimeBasedSchedule()
        {
            this.TrainingSchedule.StartMonth = 
                new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    1);

            Assert.AreEqual(
                this.sut.CurrentMonthViewModel.Month, 
                this.sut.MonthViewModelsInTimeBasedSchedule.First().Month);
        }

        [TestMethod]
        public void TrainingScheduleChanges_Duration_UpdatesVisibilities()
        {
            ushort duration = 4;
            this.TrainingSchedule.Duration = duration;

            Assert.IsTrue(this.sut.MonthViewModelsInTimeBasedSchedule.Count == duration);
            for (int i = 0; i < duration; i++)
            {
                Assert.IsTrue(this.sut.MonthViewModelsInTimeBasedSchedule[i].Month == TestDtos.TrainingScores[i + 1]);
            }
        }

        #endregion
    }
}
