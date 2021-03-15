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
        public void AddExerciseCommandCanExecute_SelectedCategoryIsNotNull_ReturnsTrue()
        {
            this.sut.SelectedCategory = null;

            Assert.IsFalse(this.sut.AddExerciseCommand.CanExecute(null));

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

        [TestMethod]
        public void ImportExercisesCommandCanExecute_ReturnsTrue()
        {
            Assert.IsTrue(this.sut.ImportExercisesCommand.CanExecute(null));
        }

        [TestMethod]
        public void ImportExerciseCommandExecute_HappyPath_TriggersImportExerciseSetOfDataManagerWithPathRetrievedFromDialogAndLoadsBuffer()
        {
            Mock.Get(this.dataManager).Setup(dm => dm.GetDataTransferObjects<Category>()).Returns(DefaultDtos.Categories);
            Mock.Get(this.dataManager).Setup(dm => dm.GetDataTransferObjects<Exercise>()).Returns(DefaultDtos.Exercises);
            this.sut.HasUnsavedCategories = true;
            this.sut.HasUnsavedExercises = true;

            this.sut.ImportExercisesCommand.Execute(null);

            Mock.Get(this.dataManager).Verify(dm => dm.TryImportExerciseSet(validImportPath), Times.Once);
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.Categories, DefaultDtos.Categories));
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.Exercises, DefaultDtos.Exercises));
            Assert.IsFalse(this.sut.HasUnsavedCategories);
            Assert.IsFalse(this.sut.HasUnsavedExercises);
        }

        [TestMethod]
        public void ImportExerciseCommandExecute_DataManagerReturnsFalse_ShowsLoadingErrorMessage()
        {
            Mock.Get(this.dataManager).Setup(dm => dm.TryImportExerciseSet(It.IsAny<string>())).Returns(false);

            this.sut.ImportExercisesCommand.Execute(null);

            Mock.Get(this.messageBoxService).Verify(service => service.ShowMessage(
                ExercisesViewModel.LOADING_ERROR_TEXT,
                ExercisesViewModel.LOADING_ERROR_TEXT,
                It.IsAny<MessageBoxButton>(),
                It.IsAny<MessageBoxImage>()),Times.Once());
        }

        [TestMethod]
        public void ResetCategoriesCommandCanExecute_UnsavedCategories_ReturnsTrue()
        {
            Assert.IsFalse(this.sut.ResetCategoriesCommand.CanExecute(null));

            this.sut.HasUnsavedCategories = true;

            Assert.IsTrue(this.sut.ResetCategoriesCommand.CanExecute(null));
        }

        [TestMethod]
        public void ResetCategoriesCommandExecute_HappyPath_ReloadsCategoriesFromDataManager()
        {
            this.sut.Categories = DefaultDtos.Categories;
            this.sut.HasUnsavedCategories = true;

            this.sut.ResetCategoriesCommand.Execute(null);

            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.Categories, TestDtos.Categories));
            Assert.IsFalse(this.sut.HasUnsavedCategories);
        }

        [TestMethod]
        public void ResetExercisesCommandCanExecute_UnsavedExercises_ReturnsTrue()
        {
            Assert.IsFalse(this.sut.ResetExercisesCommand.CanExecute(null));

            this.sut.HasUnsavedExercises = true;

            Assert.IsTrue(this.sut.ResetExercisesCommand.CanExecute(null));
        }

        [TestMethod]
        public void ResetExercisesCommandExecute_HappyPath_ReloadsExercisesFromDataManager()
        {
            this.sut.Exercises = DefaultDtos.Exercises;
            this.sut.HasUnsavedExercises = true;

            this.sut.ResetExercisesCommand.Execute(null);

            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.Exercises, TestDtos.Exercises));
            Assert.IsFalse(this.sut.HasUnsavedExercises);
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
                manager.TryImportExerciseSet(validImportPath) == true);
            DataInterface.SetDataManager(this.dataManager);
        }
    }
}
