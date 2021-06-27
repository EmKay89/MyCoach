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
    public class ExercisesViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        private const string validExportPath = "validExportPath";
        private const string validImportPath = "validImportPath";
        private IMessageBoxService messageBoxService;
        private IFileDialogService fileDialogService;
        private ExercisesViewModel sut;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.SetupDataManager();
            this.SetupServices();
            this.sut = new ExercisesViewModel(this.messageBoxService, this.fileDialogService);
            this.sut.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { this.PropertyChangedEvents.Add(e.PropertyName); };
        }

        #endregion

        #region Construction and Properties Tests

        [TestMethod]
        public void Construction_HappyPath_LoadsBuffersAndHasNoUnsavedChanges()
        {
            Assert.IsNotNull(this.sut.Categories);
            Assert.IsTrue(Utilities.AreEqual(sut.Categories, TestDtos.Categories));
            Assert.IsFalse(this.sut.HasUnsavedCategories);

            Assert.IsNotNull(this.sut.Exercises);
            Assert.IsTrue(Utilities.AreEqual(sut.Exercises, TestDtos.Exercises));
            Assert.IsFalse(this.sut.HasUnsavedExercises);
        }

        [TestMethod]
        [DataRow(nameof(ExercisesViewModel.CategoryWarmUpActive))]
        [DataRow(nameof(ExercisesViewModel.CategoryWarmUpName))]
        [DataRow(nameof(ExercisesViewModel.Category1Active))]
        [DataRow(nameof(ExercisesViewModel.Category1Name))]
        [DataRow(nameof(ExercisesViewModel.Category2Active))]
        [DataRow(nameof(ExercisesViewModel.Category2Name))]
        [DataRow(nameof(ExercisesViewModel.Category3Active))]
        [DataRow(nameof(ExercisesViewModel.Category3Name))]
        [DataRow(nameof(ExercisesViewModel.Category4Active))]
        [DataRow(nameof(ExercisesViewModel.Category4Name))]
        [DataRow(nameof(ExercisesViewModel.Category5Active))]
        [DataRow(nameof(ExercisesViewModel.Category5Name))]
        [DataRow(nameof(ExercisesViewModel.Category6Active))]
        [DataRow(nameof(ExercisesViewModel.Category6Name))]
        [DataRow(nameof(ExercisesViewModel.Category7Active))]
        [DataRow(nameof(ExercisesViewModel.Category7Name))]
        [DataRow(nameof(ExercisesViewModel.Category8Active))]
        [DataRow(nameof(ExercisesViewModel.Category8Name))]
        [DataRow(nameof(ExercisesViewModel.CategoryCoolDownActive))]
        [DataRow(nameof(ExercisesViewModel.CategoryCoolDownName))]
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

            Assert.AreEqual(1, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], propertyName);
            Assert.IsTrue(this.sut.HasUnsavedCategories);
        }

        [TestMethod]
        public void SelectedCategory_Changes_ExercisesFilteredByCategoryUpdated()
        {
            foreach (Category category in this.sut.Categories)
            {
                this.sut.SelectedCategory = category;

                Assert.IsTrue(this.sut.ExercisesFilteredByCategory.All(e => e.Category == this.sut.SelectedCategory.ID));
            }
        }

        #endregion

        #region Command Tests

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
            var oldExercisesFilteredByCategoryCount = this.sut.ExercisesFilteredByCategory.Count;

            this.sut.AddExerciseCommand.Execute(null);

            Assert.AreEqual(this.sut.Exercises.Count, oldExercisesCount + 1);
            Assert.AreEqual(this.sut.ExercisesFilteredByCategory.Count, oldExercisesFilteredByCategoryCount + 1);
            var newExercise = this.sut.Exercises.Where(e => e.Name == ExercisesViewModel.NEW_EXERCISE_NAME).FirstOrDefault();
            newExercise.Category = this.sut.SelectedCategory.ID;
        }

        [TestMethod]
        public void AddExerciseCommandExecute_ExerciseID_SetupCorrectly()
        {
            this.sut.Exercises.Clear();
            this.sut.SaveExercisesCommand.Execute(null);
            Assert.AreEqual(0, this.Exercises.Count);

            this.sut.AddExerciseCommand.Execute(null);

            Assert.AreEqual((uint)0, this.sut.Exercises.First().ID);

            this.sut.AddExerciseCommand.Execute(null);

            Assert.AreEqual((uint)1, this.sut.Exercises.Skip(1).First().ID);

            this.sut.SaveExercisesCommand.Execute(null);
            Assert.AreEqual(2, this.Exercises.Count);
            this.sut.Exercises.RemoveAt(1);

            this.sut.AddExerciseCommand.Execute(null);

            Assert.AreEqual((uint)2, this.sut.Exercises.Skip(1).First().ID);

            this.sut.Exercises.RemoveAt(0);
            this.Exercises.RemoveAt(0);

            this.sut.AddExerciseCommand.Execute(null);

            Assert.AreEqual((uint)0, this.sut.Exercises.Skip(1).First().ID);
        }

        [TestMethod]
        public void ExportExercisesCommandCanExecute_ReturnsTrue()
        {
            Assert.IsTrue(this.sut.ExportExercisesCommand.CanExecute(null));
        }

        [TestMethod]
        public void ExportExerciseCommandExecute_HappyPath_CallsExportExerciseSetOfDataManagerWithPathRetrievedFromDialogAndLoadsBuffer()
        {
            this.sut.Categories = DefaultDtos.Categories;
            this.sut.Exercises = DefaultDtos.Exercises;
            this.sut.HasUnsavedCategories = true;
            this.sut.HasUnsavedExercises = true;

            this.sut.ExportExercisesCommand.Execute(null);

            Mock.Get(this.DataManager).Verify(DataManager => DataManager.TryExportExerciseSet(validExportPath), Times.Once);
            Assert.IsTrue(Utilities.AreEqual(this.sut.Categories, TestDtos.Categories));
            Assert.IsTrue(Utilities.AreEqual(this.sut.Exercises, TestDtos.Exercises));
            Assert.IsFalse(this.sut.HasUnsavedCategories);
            Assert.IsFalse(this.sut.HasUnsavedExercises);
        }

        [TestMethod]
        public void ExportExerciseCommandExecute_DataManagerReturnsFalse_ShowsSavingErrorMessage()
        {
            Mock.Get(this.DataManager).Setup(dm => dm.TryExportExerciseSet(It.IsAny<string>())).Returns(false);

            this.sut.ExportExercisesCommand.Execute(null);

            Mock.Get(this.messageBoxService).Verify(service => service.ShowMessage(
                ExercisesViewModel.EXPORT_ERROR_TEXT,
                ExercisesViewModel.EXPORT_ERROR_TEXT,
                It.IsAny<MessageBoxButton>(),
                It.IsAny<MessageBoxImage>()), Times.Once());
        }

        [TestMethod]
        public void ImportExercisesCommandCanExecute_ReturnsTrue()
        {
            Assert.IsTrue(this.sut.ImportExercisesCommand.CanExecute(null));
        }

        [TestMethod]
        public void ImportExerciseCommandExecute_HappyPath_CallsImportExerciseSetOfDataManagerWithPathRetrievedFromDialogAndLoadsBuffer()
        {
            Mock.Get(this.DataManager).Setup(dm => dm.GetData<Category>()).Returns(DefaultDtos.Categories);
            Mock.Get(this.DataManager).Setup(dm => dm.GetData<Exercise>()).Returns(DefaultDtos.Exercises);
            this.sut.HasUnsavedCategories = true;
            this.sut.HasUnsavedExercises = true;

            this.sut.ImportExercisesCommand.Execute(null);

            Mock.Get(this.DataManager).Verify(dm => dm.TryImportExerciseSet(validImportPath), Times.Once);
            Assert.IsTrue(Utilities.AreEqual(this.sut.Categories, DefaultDtos.Categories));
            Assert.IsTrue(Utilities.AreEqual(this.sut.Exercises, DefaultDtos.Exercises));
            Assert.IsFalse(this.sut.HasUnsavedCategories);
            Assert.IsFalse(this.sut.HasUnsavedExercises);
        }

        [TestMethod]
        public void ImportExerciseCommandExecute_DataManagerReturnsFalse_ShowsLoadingErrorMessage()
        {
            Mock.Get(this.DataManager).Setup(dm => dm.TryImportExerciseSet(It.IsAny<string>())).Returns(false);

            this.sut.ImportExercisesCommand.Execute(null);

            Mock.Get(this.messageBoxService).Verify(service => service.ShowMessage(
                ExercisesViewModel.IMPORT_ERROR_TEXT,
                ExercisesViewModel.IMPORT_ERROR_TEXT,
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

            Assert.IsTrue(Utilities.AreEqual(this.sut.Categories, TestDtos.Categories));
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

            Assert.IsTrue(Utilities.AreEqual(this.sut.Exercises, TestDtos.Exercises));
            Assert.IsFalse(this.sut.HasUnsavedExercises);
        }

        [TestMethod]
        public void SaveCategoriesCommandCanExecute_UnsavedCategories_ReturnsTrue()
        {
            Assert.IsFalse(this.sut.SaveCategoriesCommand.CanExecute(null));

            this.sut.HasUnsavedCategories = true;

            Assert.IsTrue(this.sut.SaveCategoriesCommand.CanExecute(null));
        }

        [TestMethod]
        public void SaveCategoriesCommandExecute_HappyPath_SavesDataCorrectlyAndSetsUnsavedCategoriesToFalseAndRaisesPropertyChanged()
        {            
            this.sut.Categories = DefaultDtos.Categories;
            this.sut.HasUnsavedCategories = true;

            this.sut.SaveCategoriesCommand.Execute(null);

            Mock.Get(this.DataManager).Verify(dm => dm.SaveData<Category>(), Times.Once);
            foreach (var category in this.Categories)
            {
                Assert.IsTrue(Utilities.AreEqual(category, DefaultDtos.Categories.Where(c => c.ID == category.ID).First()));
            }

            Assert.IsTrue(this.sut.HasUnsavedCategories == false);
            Assert.AreEqual(2, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.SelectedCategory));
            Assert.AreEqual(this.PropertyChangedEvents[1], nameof(this.sut.ActiveCategories));
        }

        [TestMethod]
        public void SaveCategoriesCommandExecute_SavingFails_ShowsErrorMessageAndSetsUnsavedCategoriesToFalse()
        {
            Mock.Get(this.DataManager).Setup(dm => dm.SaveData<Category>()).Returns(false);
            this.sut.HasUnsavedCategories = true;

            this.sut.SaveCategoriesCommand.Execute(null);

            Mock.Get(this.messageBoxService).Verify(
                service => service.ShowMessage(
                    ExercisesViewModel.SAVING_ERROR_TEXT,
                    ExercisesViewModel.SAVING_ERROR_CAPTION,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error), Times.Once);

            Assert.IsTrue(this.sut.HasUnsavedCategories == false);
        }

        [TestMethod]
        public void SaveExercisesCommandCanExecute_UnsavedExercises_ReturnsTrue()
        {
            Assert.IsFalse(this.sut.SaveExercisesCommand.CanExecute(null));

            this.sut.HasUnsavedExercises = true;

            Assert.IsTrue(this.sut.SaveExercisesCommand.CanExecute(null));
        }

        [TestMethod]
        public void SaveExercisesCommandExecute_HappyPath_CallsSaveDataFromDataManagerAndSetsUnsavedExercisesToFalse()
        {
            this.sut.Exercises = DefaultDtos.Exercises;
            this.sut.HasUnsavedExercises = true;

            this.sut.SaveExercisesCommand.Execute(null);

            foreach (var exercise in this.Exercises)
            {
                Assert.IsTrue(Utilities.AreEqual(exercise, DefaultDtos.Exercises.Where(c => c.ID == exercise.ID).First()));
            }

            Mock.Get(this.DataManager).Verify(dm => dm.SaveData<Exercise>(), Times.Once);
            this.sut.HasUnsavedExercises = false;
        }

        [TestMethod]
        public void SaveExercisesCommandExecute_SavingFails_ShowsErrorMessageAndSetsUnsavedExercisesToFalse()
        {
            Mock.Get(this.DataManager).Setup(dm => dm.SaveData<Exercise>()).Returns(false);
            this.sut.HasUnsavedExercises = true;

            this.sut.SaveExercisesCommand.Execute(null);

            Mock.Get(this.messageBoxService).Verify(
                service => service.ShowMessage(
                    ExercisesViewModel.SAVING_ERROR_TEXT,
                    ExercisesViewModel.SAVING_ERROR_CAPTION,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error), Times.Once);

            Assert.IsTrue(this.sut.HasUnsavedExercises == false);
        }

        [TestMethod]
        public void SetDefaultsCommandCanExecute_ReturnsTrue()
        {
            Assert.IsTrue(this.sut.SetDefaultsCommand.CanExecute(null));
        }

        [TestMethod]
        public void SetDefaultsCommandCommandExecute_HappyPath_CallsSaveDataFromDataManagerAndLoadsExerciseAndCategoryBuffer()
        {
            Mock.Get(this.DataManager).Setup(dm => dm.GetData<Category>()).Returns(DefaultDtos.Categories);
            Mock.Get(this.DataManager).Setup(dm => dm.GetData<Exercise>()).Returns(DefaultDtos.Exercises);

            this.sut.SetDefaultsCommand.Execute(null);

            Mock.Get(this.DataManager).Verify(dm => dm.SetDefaults<Exercise>(), Times.Once);
            Mock.Get(this.DataManager).Verify(dm => dm.SetDefaults<Category>(), Times.Once);
            Assert.IsTrue(Utilities.AreEqual(this.sut.Categories, DefaultDtos.Categories));
            Assert.IsTrue(Utilities.AreEqual(this.sut.Exercises, DefaultDtos.Exercises));
        }

        #endregion

        #region Helper Methods

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
            Mock.Get(this.DataManager).Setup(dm => dm.TryExportExerciseSet(validExportPath)).Returns(true);
            Mock.Get(this.DataManager).Setup(dm => dm.TryImportExerciseSet(validImportPath)).Returns(true);
        }

        #endregion
    }
}
