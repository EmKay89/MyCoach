using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling;
using MyCoach.Helpers.Mvvm.Services;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class SettingsViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        private IMessageBoxService messageBoxService;
        private SettingsViewModel sut;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.messageBoxService = Mock.Of<IMessageBoxService>(service =>
                service.ShowMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>()) == MessageBoxResult.Yes);
            DataInterface.SetDataManager(this.DataManager);
            this.sut = new SettingsViewModel(this.messageBoxService);
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
        public void Construction_HappyPath_LoadsBufferAndHasNoUnsavedChanges()
        {
            Assert.IsNotNull(this.sut.Settings);
            Assert.IsTrue(Utilities.AreEqual(sut.Settings, TestDtos.Settings.FirstOrDefault()));
        }

        [TestMethod]
        public void Permission_Changes_RaisesPropertyChangedAndSavesData()
        {            
            this.sut.Permission = ExerciseSchedulingRepetitionPermission.Yes;

            Assert.AreEqual(2, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], "Permission");
            Assert.AreEqual(this.PropertyChangedEvents[1], "PermissionText");
            this.AssertSave();
        }

        [TestMethod]
        public void RepeatsRound1_Changes_RaisesPropertyChangedAndSavesData()
        {
            this.sut.RepeatsRound1 = ++this.sut.RepeatsRound1;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.RepeatsRound1));
            this.AssertSave();
        }

        [TestMethod]
        public void RepeatsRound2_Changes_RaisesPropertyChangedAndSavesData()
        {
            this.sut.RepeatsRound2 = ++this.sut.RepeatsRound2;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.RepeatsRound2));
            this.AssertSave();
        }

        [TestMethod]
        public void RepeatsRound3_Changes_RaisesPropertyChangedAndSavesData()
        {
            this.sut.RepeatsRound3 = ++this.sut.RepeatsRound3;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.RepeatsRound3));
            this.AssertSave();
        }

        [TestMethod]
        public void RepeatsRound4_Changes_RaisesPropertyChangedAndSavesData()
        {
            this.sut.RepeatsRound4 = ++this.sut.RepeatsRound4;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.RepeatsRound4));
            this.AssertSave();
        }

        [TestMethod]
        public void ScoresRound1_Changes_RaisesPropertyChangedAndSavesData()
        {
            this.sut.ScoresRound1 = ++this.sut.ScoresRound1;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.ScoresRound1));
            this.AssertSave();
        }

        [TestMethod]
        public void ScoresRound2_Changes_RaisesPropertyChangedAndSavesData()
        {
            this.sut.ScoresRound2 = ++this.sut.ScoresRound2;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.ScoresRound2));
            this.AssertSave();
        }

        [TestMethod]
        public void ScoresRound3_Changes_RaisesPropertyChangedAndSavesData()
        {
            this.sut.ScoresRound3 = ++this.sut.ScoresRound3;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.ScoresRound3));
            this.AssertSave();
        }

        [TestMethod]
        public void ScoresRound4_Changes_RaisesPropertyChangedAndSavesData()
        {
            this.sut.ScoresRound4 = ++this.sut.ScoresRound4;

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.ScoresRound4));
            this.AssertSave();
        }

        #endregion

        #region Command Tests

        [TestMethod]
        public void AddUnitCommandCanExecute_NewUnitIsEmpty_ReturnsFalse()
        {
            this.sut.NewUnit = string.Empty;

            Assert.IsFalse(this.sut.AddUnitCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddUnitCommandCanExecute_NewUnitIsNotEmpty_ReturnsTrue()
        {
            this.sut.NewUnit = "AnotherTestUnit";

            Assert.IsTrue(this.sut.AddUnitCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddUnitCommandExecute_HappyPath_AddsNewUnitToExistingUnitsAndClearsNewUnitAfterwards()
        {
            int preexistingUnitsCount = this.sut.Units.Count;
            this.sut.NewUnit = "AnotherTestUnit";

            this.sut.AddUnitCommand.Execute(null);

            Assert.AreEqual(preexistingUnitsCount + 1, this.sut.Units.Count);
            Assert.AreEqual(this.sut.Units.Last(), "AnotherTestUnit");
            Assert.AreEqual(this.sut.NewUnit, string.Empty);
            this.AssertSave();
        }

        [TestMethod]
        public void DeleteUnitCommandCanExecute_SelectedUnitIsNull_ReturnsFalse()
        {
            this.sut.SelectedUnit = null;

            Assert.IsFalse(this.sut.DeleteUnitCommand.CanExecute(null));
        }

        [TestMethod]
        public void DeleteUnitCommandCanExecute_SelectedUnitIsNotNull_ReturnsTrue()
        {
            this.sut.SelectedUnit = this.sut.Units.First();

            Assert.IsTrue(this.sut.DeleteUnitCommand.CanExecute(null));
        }

        [TestMethod]
        public void DeleteUnitCommandExecute_HappyPath_DeletesSelectedUnitFromExistingUnits()
        {
            int preexistingUnitsCount = this.sut.Units.Count;
            string firstUnit = this.sut.Units.First();
            this.sut.SelectedUnit = firstUnit;

            this.sut.DeleteUnitCommand.Execute(null);

            Assert.AreEqual(preexistingUnitsCount - 1, this.sut.Units.Count);
            Assert.IsFalse(this.sut.Units.Contains(firstUnit));
            this.AssertSave();
        }

        [TestMethod]
        public void SetDefaultsCommandCanExecute_ReturnsTrue()
        {
            Assert.IsTrue(this.sut.SetDefaultsCommand.CanExecute(null));
        }

        [TestMethod]
        public void SetDefaultsCommandExecute_HappyPath_CallsSetDefaultsOfDataManagerAndInvokesPropertyChangesAndUpdatedPermissionText()
        {
            Mock.Get(this.DataManager).Verify(dm => dm.SetDefaults<Settings>(), Times.Never);
            this.sut.PermissionText = "AnyChangedText";
            this.PropertyChangedEvents.Clear();

            this.sut.SetDefaultsCommand.Execute(null);

            Mock.Get(this.DataManager).Verify(dm => dm.SetDefaults<Settings>(), Times.Once);
            this.AssertPropertyChangedInvokation(11);
            this.AssertPropertyChangedInvokation(
                nameof(this.sut.Permission),
                nameof(this.sut.PermissionText),
                nameof(this.sut.RepeatsRound1),
                nameof(this.sut.RepeatsRound2),
                nameof(this.sut.RepeatsRound3),
                nameof(this.sut.RepeatsRound4),
                nameof(this.sut.ScoresRound1),
                nameof(this.sut.ScoresRound2),
                nameof(this.sut.ScoresRound3),
                nameof(this.sut.ScoresRound4),
                nameof(this.sut.RepeatsAndScoresMultiplier));
        }

        #endregion

        #region Helper Methods

        private void AssertSave()
        {
            Mock.Get(this.DataManager).Verify(dm => dm.SaveData<Settings>(), Times.Once);
        }

        #endregion
    }
}
