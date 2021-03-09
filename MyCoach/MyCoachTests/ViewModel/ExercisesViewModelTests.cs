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
using System.Windows;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class ExercisesViewModelTests
    {
        private const string validExportPath = "validExportPath";
        private const string validImportPath = "validImportPath";
        IDataManager dataManager;
        IMessageBoxService messageBoxService;
        IFileDialogService fileDialogService;
        ExercisesViewModel sut;
        List<string> propertyChangedEvents;

        [TestInitialize]
        public void Init()
        {
            this.SetupDataManager();
            this.SetupServices();
            this.sut = new ExercisesViewModel(this.messageBoxService, this.fileDialogService);
            this.propertyChangedEvents = new List<string>();
            this.sut.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { this.propertyChangedEvents.Add(e.PropertyName); };
        }

        [TestMethod]
        public void Construction_HappyPath_LoadsBuffersAndHasNoUnsavedChanges()
        {
            Assert.IsNotNull(this.sut.Categories);
            Assert.IsTrue(DtoUtilities.AreEqual(sut.Categories, TestDtos.Categories));
            Assert.IsFalse(this.sut.HasUnsavedCategories);

            Assert.IsNotNull(this.sut.Exercises);
            Assert.IsTrue(DtoUtilities.AreEqual(sut.Exercises, TestDtos.Exercises));
            Assert.IsFalse(this.sut.HasUnsavedExercises);
        }

        [TestMethod]
        [DataRow("CategoryWarmUpActive")]
        [DataRow("CategoryWarmUpName")]
        [DataRow("Category1Active")]
        [DataRow("Category1Name")]
        [DataRow("Category2Active")]
        [DataRow("Category2Name")]
        [DataRow("Category3Active")]
        [DataRow("Category3Name")]
        [DataRow("Category4Active")]
        [DataRow("Category4Name")]
        [DataRow("Category5Active")]
        [DataRow("Category5Name")]
        [DataRow("Category6Active")]
        [DataRow("Category6Name")]
        [DataRow("Category7Active")]
        [DataRow("Category7Name")]
        [DataRow("Category8Active")]
        [DataRow("Category8Name")]
        [DataRow("CategoryCoolDownActive")]
        [DataRow("CategoryCoolDownName")]
        public void Property_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue(string propertyName)
        {
            switch (this.sut.GetType().GetProperty(propertyName).GetValue(this.sut, null))
            {
                case bool boolProperty:
                    this.sut.GetType().GetProperty(propertyName).SetValue(sut, !boolProperty);
                    break;
                case string stringProperty:
                    this.sut.GetType().GetProperty(propertyName).SetValue(sut, string.Concat(stringProperty, "SomeAddition"));
                    break;
                default:
                    throw new NotImplementedException();
            }

            Assert.AreEqual(1, this.propertyChangedEvents.Count);
            Assert.AreEqual(this.propertyChangedEvents[0], propertyName);
            Assert.IsTrue(this.sut.HasUnsavedCategories);
        }

        [TestMethod]
        public void AddExerciseCommandCanExecute_SelectedCategoryIsNull_ReturnsFalse()
        {
            this.sut.SelectedCategory = null;

            Assert.IsFalse(this.sut.AddExerciseCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddExerciseCommandCanExecute_SelectedCategoryIsNotNull_ReturnsTrue()
        {
            this.sut.SelectedCategory = this.sut.Categories.FirstOrDefault();

            Assert.IsTrue(this.sut.AddExerciseCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddExerciseCommandExecute_HappyPath_AddsNewExerciseOfSelectedCategory()
        {
            var oldExercisesCount = this.sut.Exercises.Count;

            this.sut.AddExerciseCommand.Execute(null);

            Assert.AreEqual(this.sut.Exercises.Count, oldExercisesCount + 1);
            var newExercise = this.sut.Exercises.Where(e => e.Name == ExercisesViewModel.NEW_EXERCISE_NAME).FirstOrDefault();
            newExercise.Category = this.sut.SelectedCategory.ID;
        }

        [TestMethod]
        public void ExportExercisesCommandCanExecute_ReturnsTrue()
        {
            Assert.IsTrue(this.sut.ExportExercisesCommand.CanExecute(null));
        }

        [TestMethod]
        public void ExportExerciseCommandExecute_HappyPath_TriggersExportExerciseSetOfDataManagerWithPathRetrievedFromDialogAndLoadsBuffer()
        {
            this.sut.Categories = DefaultDtos.Categories;
            this.sut.Exercises = DefaultDtos.Exercises;
            this.sut.HasUnsavedCategories = true;
            this.sut.HasUnsavedExercises = true;

            this.sut.ExportExercisesCommand.Execute(null);

            Mock.Get(this.dataManager).Verify(dataManager => dataManager.TryExportExerciseSet(validExportPath), Times.Once);
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.Categories, TestDtos.Categories));
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.Exercises, TestDtos.Exercises));
            Assert.IsFalse(this.sut.HasUnsavedCategories);
            Assert.IsFalse(this.sut.HasUnsavedExercises);
        }

        [TestMethod]
        public void ExportExerciseCommandExecute_DataManagerReturnsFalse_ShowsSavingErrorMessage()
        {
            Mock.Get(this.dataManager).Setup(dm => dm.TryExportExerciseSet(It.IsAny<string>())).Returns(false);

            this.sut.ExportExercisesCommand.Execute(null);

            Mock.Get(this.messageBoxService).Verify(service => service.ShowMessage(
                ExercisesViewModel.SAVING_ERROR_TEXT,
                ExercisesViewModel.SAVING_ERROR_TEXT,
                It.IsAny<MessageBoxButton>(),
                It.IsAny<MessageBoxImage>()), Times.Once());
        }

        private void SetupServices()
        {
            this.messageBoxService = Mock.Of<IMessageBoxService>(service =>
                service.ShowMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>()) == MessageBoxResult.Yes);
            this.fileDialogService = Mock.Of<IFileDialogService>(service =>
                service.OpenFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()) == validImportPath &&
                service.SaveFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()) == validExportPath);
        }

        private void SetupDataManager()
        {
            this.dataManager = Mock.Of<IDataManager>(manager =>
                manager.GetDataTransferObjects<Category>() == TestDtos.Categories &&
                manager.SetDataTransferObjects<Category>(It.IsAny<ObservableCollection<Category>>()) == true &&
                manager.GetDataTransferObjects<Exercise>() == TestDtos.Exercises &&
                manager.SetDataTransferObjects<Exercise>(It.IsAny<ObservableCollection<Exercise>>()) == true &&
                manager.TryExportExerciseSet(validExportPath) == true &&
                manager.TryExportExerciseSet(It.IsAny<string>()) == false &&
                manager.TryImportExerciseSet(validImportPath) == true &&
                manager.TryImportExerciseSet(It.IsAny<string>()) == false);
            DataInterface.SetDataManager(dataManager);
        }
    }
}
