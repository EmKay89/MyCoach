using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.DataHandling.DataTransferObjects;
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

        ViewTrainingScheduleViewModel sut;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.sut = new ViewTrainingScheduleViewModel();
            this.sut.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { this.PropertyChangedEvents.Add(e.PropertyName); };
        }

        #endregion

        #region Construction and Properties Tests

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
        public void SelectedViewModel_Changes_RaisesPropertyChangedAnd()
        {

        }

        #endregion

        #region Command Tests

        [TestMethod]
        public void UpdateSelectedViewModelCommandCanExecute_ReturnsTrue()
        {

        }

        [TestMethod]
        public void UpdateSelectedViewModelCommand_HappyPath_UpdatesSelectedViewModel()
        {

        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
