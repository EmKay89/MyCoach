using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class TrainingScheduleOverviewViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        TrainingScheduleOverviewViewModel sut;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.sut = new TrainingScheduleOverviewViewModel();
            this.sut.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { this.PropertyChangedEvents.Add(e.PropertyName); };
        }

        #endregion

        #region Construction and Properties Tests

        [TestMethod]
        public void Construction_HappyPath_SelectedViewModelIsSetCorrectly()
        {

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
