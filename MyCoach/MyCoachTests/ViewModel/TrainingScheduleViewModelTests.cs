﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCoach.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class TrainingScheduleViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        TrainingScheduleViewModel sut;
        List<string> propertyChangedEvents;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.sut = new TrainingScheduleViewModel();
            this.propertyChangedEvents = new List<string>();
            this.sut.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { this.propertyChangedEvents.Add(e.PropertyName); };
        }

        #endregion

        #region Construction and Properties Tests

        [TestMethod]
        public void Construction_HappyPath_SelectedViewModelIsSetCorrectly()
        {
            Assert.AreEqual(this.sut.ViewViewModel, this.sut.SelectedViewModel);
            Assert.IsFalse(this.sut.EditSelected);
            Assert.IsTrue(this.sut.ViewSelected);
        }

        [TestMethod]
        public void SelectedViewModel_Changes_RaisesPropertyChangedAnd()
        {
            this.sut.SelectedViewModel = this.sut.EditViewModel;

            Assert.AreEqual(3, this.propertyChangedEvents.Count);
            Assert.AreEqual(this.propertyChangedEvents[0], nameof(this.sut.SelectedViewModel));
            Assert.AreEqual(this.propertyChangedEvents[1], nameof(this.sut.EditSelected));
            Assert.AreEqual(this.propertyChangedEvents[2], nameof(this.sut.ViewSelected));
            Assert.IsTrue(this.sut.EditSelected);
            Assert.IsFalse(this.sut.ViewSelected);
        }

        #endregion

        #region Command Tests

        [TestMethod]
        public void UpdateSelectedViewModelCommandCanExecute_ReturnsTrue()
        {
            Assert.IsTrue(this.sut.UpdateSelectedViewModelCommand.CanExecute("Edit"));
            Assert.IsTrue(this.sut.UpdateSelectedViewModelCommand.CanExecute("View"));
        }

        [TestMethod]
        public void UpdateSelectedViewModelCommand_HappyPath_UpdatesSelectedViewModel()
        {
            this.sut.UpdateSelectedViewModelCommand.Execute("Edit");

            Assert.AreEqual(this.sut.SelectedViewModel, this.sut.EditViewModel);

            this.sut.UpdateSelectedViewModelCommand.Execute("View");

            Assert.AreEqual(this.sut.SelectedViewModel, this.sut.ViewViewModel);
        }

        #endregion
    }
}
