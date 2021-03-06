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
using System.Text;
using System.Threading.Tasks;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class SettingsViewModelTests
    {
        IDataManager dataManager;
        SettingsViewModel sut;
        List<string> propertyChangedEvents;

        [TestInitialize]
        public void Init()
        {
            this.dataManager = Mock.Of<IDataManager>(manager =>
                manager.GetDataTransferObjects<Settings>() == TestDtos.Settings);
            DataInterface.SetDataManager(dataManager);
            this.sut = new SettingsViewModel();
            this.propertyChangedEvents = new List<string>();
            this.sut.PropertyChanged += 
                (object sender, PropertyChangedEventArgs e) => { this.propertyChangedEvents.Add(e.PropertyName); };
        }

        [TestMethod]
        public void Construction_HappyPath_LoadsBufferAndHasNoUnsavedChanges()
        {
            Assert.IsNotNull(this.sut.Settings);
            Assert.IsTrue(DtoUtilities.AreEqual(sut.Settings, TestDtos.Settings.FirstOrDefault()));
            Assert.IsFalse(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void Permission_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {            
            this.sut.Permission = MyCoach.Defines.ExerciseSchedulingRepetitionPermission.Yes;

            Assert.AreEqual(2, this.propertyChangedEvents.Count);
            Assert.AreEqual(this.propertyChangedEvents[0], "Permission");
            Assert.AreEqual(this.propertyChangedEvents[1], "PermissionText");
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void RepeatsRound1_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.RepeatsRound1 = ++this.sut.RepeatsRound1;

            Assert.AreEqual(1, this.propertyChangedEvents.Count);
            Assert.AreEqual(this.propertyChangedEvents[0], nameof(this.sut.RepeatsRound1));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void RepeatsRound2_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.RepeatsRound2 = ++this.sut.RepeatsRound2;

            Assert.AreEqual(1, this.propertyChangedEvents.Count);
            Assert.AreEqual(this.propertyChangedEvents[0], nameof(this.sut.RepeatsRound2));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void RepeatsRound3_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.RepeatsRound3 = ++this.sut.RepeatsRound3;

            Assert.AreEqual(1, this.propertyChangedEvents.Count);
            Assert.AreEqual(this.propertyChangedEvents[0], nameof(this.sut.RepeatsRound3));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void RepeatsRound4_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.RepeatsRound4 = ++this.sut.RepeatsRound4;

            Assert.AreEqual(1, this.propertyChangedEvents.Count);
            Assert.AreEqual(this.propertyChangedEvents[0], nameof(this.sut.RepeatsRound4));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void ScoresRound1_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.ScoresRound1 = ++this.sut.ScoresRound1;

            Assert.AreEqual(1, this.propertyChangedEvents.Count);
            Assert.AreEqual(this.propertyChangedEvents[0], nameof(this.sut.ScoresRound1));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void ScoresRound2_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.ScoresRound2 = ++this.sut.ScoresRound2;

            Assert.AreEqual(1, this.propertyChangedEvents.Count);
            Assert.AreEqual(this.propertyChangedEvents[0], nameof(this.sut.ScoresRound2));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void ScoresRound3_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.ScoresRound3 = ++this.sut.ScoresRound3;

            Assert.AreEqual(1, this.propertyChangedEvents.Count);
            Assert.AreEqual(this.propertyChangedEvents[0], nameof(this.sut.ScoresRound3));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void ScoresRound4_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.ScoresRound4 = ++this.sut.ScoresRound4;

            Assert.AreEqual(1, this.propertyChangedEvents.Count);
            Assert.AreEqual(this.propertyChangedEvents[0], nameof(this.sut.ScoresRound4));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void SaveSettingsCommandCanExecute_NoUnsavedChanges_ReturnsFalse()
        {
            Assert.IsFalse(this.sut.SaveSettingsCommand.CanExecute(null));
        }

        [TestMethod]
        public void SaveSettingsCommandCanExecute_HasUnsavedChanges_ReturnsTrue()
        {
            this.sut.Permission = MyCoach.Defines.ExerciseSchedulingRepetitionPermission.Yes;

            Assert.IsTrue(this.sut.SaveSettingsCommand.CanExecute(null));
        }

        [TestMethod]
        public void SaveSettingsCommandExecute_CallsSetDataTransferObjectsOfDataManagerAndHasUnsavedChangesIsFalse()
        {
            this.sut.Permission = MyCoach.Defines.ExerciseSchedulingRepetitionPermission.Yes;

            this.sut.SaveSettingsCommand.Execute(null);

            Mock.Get(this.dataManager).Verify(
                dataManager => dataManager.SetDataTransferObjects<Settings>(It.IsAny<ObservableCollection<Settings>>()), Times.Once);
            Assert.IsFalse(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void SetDefaultsCommandCanExecute_ReturnsTrue()
        {
            Assert.IsTrue(this.sut.SetDefaultsCommand.CanExecute(null));
        }

        [TestMethod]
        public void SetDefaultsCommandExecute_CallsSetDataTransferObjectsOfDataManagerAndHasUnsavedChangesIsFalse()
        {
            // ToDo: Ausfüllen
        }

        [TestMethod]
        public void ResetSettingsCommandCanExecute_NoUnsavedChanges_ReturnsFalse()
        {
            Assert.IsFalse(this.sut.ResetSettingsCommand.CanExecute(null));
        }

        [TestMethod]
        public void ResetSettingsCommandCanExecute_HasUnsavedChanges_ReturnsTrue()
        {
            this.sut.Permission = MyCoach.Defines.ExerciseSchedulingRepetitionPermission.Yes;

            Assert.IsTrue(this.sut.ResetSettingsCommand.CanExecute(null));
        }

        [TestMethod]
        public void ResetSettingsCommandExecute_CallsSetDataTransferObjectsOfDataManagerAndHasUnsavedChangesIsFalse()
        {
            this.sut.RepeatsRound1 = ++this.sut.RepeatsRound1;
            this.sut.ScoresRound1 = ++this.sut.ScoresRound1;
            this.sut.Permission = MyCoach.Defines.ExerciseSchedulingRepetitionPermission.Yes;

            this.sut.ResetSettingsCommand.Execute(null);

            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.Settings, TestDtos.Settings.FirstOrDefault()));
            Assert.IsFalse(this.sut.HasUnsavedChanges);
        }
    }
}
