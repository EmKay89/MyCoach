using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.ViewModel;
using MyCoach.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class SettingsViewModelTests : ViewModelTestBase
    {
        IMessageBoxService messageBoxService;
        SettingsViewModel sut;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.messageBoxService = Mock.Of<IMessageBoxService>(service =>
                service.ShowMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>()) == MessageBoxResult.Yes);
            DataInterface.SetDataManager(DataManager);
            this.sut = new SettingsViewModel(this.messageBoxService);
            this.sut.PropertyChanged += 
                (object sender, PropertyChangedEventArgs e) => { this.PropertyChangedEvents.Add(e.PropertyName); };
        }

        [TestMethod]
        public void Construction_HappyPath_LoadsBufferAndHasNoUnsavedChanges()
        {
            Assert.IsNotNull(this.sut.Settings);
            Assert.IsTrue(DtoUtilities.AreEqual(sut.Settings, TestDtos.Settings.FirstOrDefault()));
            Assert.IsFalse(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void Construction_DataInterfaceSettingsIsNull_SetsBufferToDefaultSettings()
        {
            Mock.Get(this.DataManager).Setup(dm => dm.GetData<Settings>()).Returns((ObservableCollection<Settings>)null);

            this.sut = new SettingsViewModel(this.messageBoxService);

            Assert.IsNotNull(this.sut.Settings);
            Assert.IsTrue(DtoUtilities.AreEqual(sut.Settings, DefaultDtos.Settings.FirstOrDefault()));
        }

        [TestMethod]
        public void Construction_DataInterfaceSettingsIsEmpty_SetsBufferToDefaultSettings()
        {
            Mock.Get(this.DataManager).Setup(dm => dm.GetData<Settings>()).Returns(new ObservableCollection<Settings>());

            this.sut = new SettingsViewModel(this.messageBoxService);

            Assert.IsNotNull(this.sut.Settings);
            Assert.IsTrue(DtoUtilities.AreEqual(sut.Settings, DefaultDtos.Settings.FirstOrDefault()));
        }

        [TestMethod]
        public void Permission_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {            
            this.sut.Permission = MyCoach.Defines.ExerciseSchedulingRepetitionPermission.Yes;

            Assert.AreEqual(2, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], "Permission");
            Assert.AreEqual(this.PropertyChangedEvents[1], "PermissionText");
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void RepeatsRound1_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.RepeatsRound1 = ++this.sut.RepeatsRound1;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.RepeatsRound1));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void RepeatsRound2_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.RepeatsRound2 = ++this.sut.RepeatsRound2;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.RepeatsRound2));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void RepeatsRound3_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.RepeatsRound3 = ++this.sut.RepeatsRound3;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.RepeatsRound3));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void RepeatsRound4_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.RepeatsRound4 = ++this.sut.RepeatsRound4;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.RepeatsRound4));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void ScoresRound1_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.ScoresRound1 = ++this.sut.ScoresRound1;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.ScoresRound1));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void ScoresRound2_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.ScoresRound2 = ++this.sut.ScoresRound2;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.ScoresRound2));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void ScoresRound3_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.ScoresRound3 = ++this.sut.ScoresRound3;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.ScoresRound3));
            Assert.IsTrue(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void ScoresRound4_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue()
        {
            this.sut.ScoresRound4 = ++this.sut.ScoresRound4;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.ScoresRound4));
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
        public void SaveSettingsCommandExecute_HappyPath_CallsSetDataTransferObjectsOfDataManagerAndHasUnsavedChangesIsFalse()
        {
            this.sut.Permission = MyCoach.Defines.ExerciseSchedulingRepetitionPermission.Yes;

            this.sut.SaveSettingsCommand.Execute(null);

            Mock.Get(this.DataManager).Verify(dm => dm.SaveData<Settings>(), Times.Once);
            Assert.IsFalse(this.sut.HasUnsavedChanges);
        }

        [TestMethod]
        public void SetDefaultsCommandCanExecute_ReturnsTrue()
        {
            Assert.IsTrue(this.sut.SetDefaultsCommand.CanExecute(null));
        }

        [TestMethod]
        public void SetDefaultsCommandExecute_HappyPath_CallsSetDefaultsOfDataManagerAndLoadsBufferAndHasUnsavedChangesIsFalse()
        {
            Mock.Get(this.DataManager).Setup(dm => dm.GetData<Settings>()).Returns(DefaultDtos.Settings);
            Mock.Get(this.DataManager).Verify(dm => dm.SetDefaults<Settings>(), Times.Never);
            Assert.IsFalse(DtoUtilities.AreEqual(this.sut.Settings, DefaultDtos.Settings.FirstOrDefault()));
            this.sut.ScoresRound1 = ++this.sut.ScoresRound1;
            Assert.IsTrue(this.sut.HasUnsavedChanges);

            this.sut.SetDefaultsCommand.Execute(null);

            Mock.Get(this.DataManager).Verify(dm => dm.SetDefaults<Settings>(), Times.Once);
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.Settings, DefaultDtos.Settings.FirstOrDefault()));
            Assert.IsFalse(this.sut.HasUnsavedChanges);
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
        public void ResetSettingsCommandExecute_HappyPath_DiscardsChangesAndHasUnsavedChangesIsFalse()
        {
            this.sut.RepeatsRound1 = ++this.sut.RepeatsRound1;
            this.sut.ScoresRound1 = ++this.sut.ScoresRound1;
            this.sut.Permission = MyCoach.Defines.ExerciseSchedulingRepetitionPermission.Yes;
            Assert.IsFalse(DtoUtilities.AreEqual(this.sut.Settings, TestDtos.Settings.FirstOrDefault()));
            Assert.IsTrue(this.sut.HasUnsavedChanges);

            this.sut.ResetSettingsCommand.Execute(null);

            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.Settings, TestDtos.Settings.FirstOrDefault()));
            Assert.IsFalse(this.sut.HasUnsavedChanges);
        }
    }
}
