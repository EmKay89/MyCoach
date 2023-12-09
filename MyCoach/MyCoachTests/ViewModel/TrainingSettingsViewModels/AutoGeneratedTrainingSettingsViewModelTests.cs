﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCoach.Helpers.Extensions.IEnumerable;
using MyCoach.Model.Defines;
using MyCoach.ViewModel;
using MyCoach.ViewModel.TrainingSettingsViewModels;
using System;
using System.Linq;

namespace MyCoachTests.ViewModel.TrainingSettingsViewModels
{
    [TestClass]
    public class AutoGeneratedTrainingSettingsViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        AutoGeneratedTrainingSettingsViewModel sut;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.sut = new AutoGeneratedTrainingSettingsViewModel();
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
        public void Construction_ActiveCategoriesSetupCorrectly()
        {
            foreach (var category in this.Categories)
            {
                if (category.Active)
                {
                    Assert.IsTrue(this.sut.ActiveCategories.Any(c => c == category));
                }
            }
        }

        [TestMethod]
        public void Construction_CategoriesAndCategoryNamesDisplayedCorrectly()
        {
            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();
        }

        [TestMethod]
        public void ModeChanges_ToCircleTraining_CorrectTrainingSettingsVisibility()
        {
            this.sut.TrainingMode = TrainingMode.UserDefinedTraining;
            this.PropertyChangedEvents.Clear();

            this.sut.TrainingMode = TrainingMode.CircleTraining;

            Assert.IsFalse(this.sut.SingleCategorySelectionVisible);
            Assert.IsTrue(this.sut.MultipleCategorySelectionVisible);
            Assert.IsTrue(this.sut.LapCountSelectionVisible);
            Assert.IsFalse(this.sut.ExercisesPerLapSelectionVisible);

            Assert.AreEqual(6, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.TrainingMode), this.PropertyChangedEvents[0]);
            Assert.AreEqual(nameof(this.sut.SingleCategorySelectionVisible), this.PropertyChangedEvents[1]);
            Assert.AreEqual(nameof(this.sut.MultipleCategorySelectionVisible), this.PropertyChangedEvents[2]);
            Assert.AreEqual(nameof(this.sut.LapCountSelectionVisible), this.PropertyChangedEvents[3]);
            Assert.AreEqual(nameof(this.sut.ExercisesPerLapSelectionVisible), this.PropertyChangedEvents[4]);
            Assert.AreEqual(nameof(this.sut.NotEnoughExercisesAvailable), this.PropertyChangedEvents[5]);
        }

        [TestMethod]
        public void ModeChanges_ToRandomTraining_CorrectTrainingSettingsVisibility()
        {
            this.sut.TrainingMode = TrainingMode.CircleTraining;
            this.PropertyChangedEvents.Clear();

            this.sut.TrainingMode = TrainingMode.RandomTraining;

            Assert.IsFalse(this.sut.SingleCategorySelectionVisible);
            Assert.IsTrue(this.sut.MultipleCategorySelectionVisible);
            Assert.IsTrue(this.sut.LapCountSelectionVisible);
            Assert.IsTrue(this.sut.ExercisesPerLapSelectionVisible);

            Assert.AreEqual(6, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.TrainingMode), this.PropertyChangedEvents[0]);
            Assert.AreEqual(nameof(this.sut.SingleCategorySelectionVisible), this.PropertyChangedEvents[1]);
            Assert.AreEqual(nameof(this.sut.MultipleCategorySelectionVisible), this.PropertyChangedEvents[2]);
            Assert.AreEqual(nameof(this.sut.LapCountSelectionVisible), this.PropertyChangedEvents[3]);
            Assert.AreEqual(nameof(this.sut.ExercisesPerLapSelectionVisible), this.PropertyChangedEvents[4]);
            Assert.AreEqual(nameof(this.sut.NotEnoughExercisesAvailable), this.PropertyChangedEvents[5]);
        }

        [TestMethod]
        public void ModeChanges_ToFocusTraining_CorrectTrainingSettingsVisibility()
        {
            this.sut.TrainingMode = TrainingMode.RandomTraining;
            this.PropertyChangedEvents.Clear();

            this.sut.TrainingMode = TrainingMode.FocusTraining;

            Assert.IsTrue(this.sut.SingleCategorySelectionVisible);
            Assert.IsFalse(this.sut.MultipleCategorySelectionVisible);
            Assert.IsTrue(this.sut.LapCountSelectionVisible);
            Assert.IsTrue(this.sut.ExercisesPerLapSelectionVisible);

            Assert.AreEqual(6, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.TrainingMode), this.PropertyChangedEvents[0]);
            Assert.AreEqual(nameof(this.sut.SingleCategorySelectionVisible), this.PropertyChangedEvents[1]);
            Assert.AreEqual(nameof(this.sut.MultipleCategorySelectionVisible), this.PropertyChangedEvents[2]);
            Assert.AreEqual(nameof(this.sut.LapCountSelectionVisible), this.PropertyChangedEvents[3]);
            Assert.AreEqual(nameof(this.sut.ExercisesPerLapSelectionVisible), this.PropertyChangedEvents[4]);
            Assert.AreEqual(nameof(this.sut.NotEnoughExercisesAvailable), this.PropertyChangedEvents[5]);
        }

        [TestMethod]
        [DataRow(TrainingMode.CircleTraining)]
        [DataRow(TrainingMode.RandomTraining)]
        public void CanStartTraining_CircleOrRandomTrainingWithAtLeastOneCategoryEnabledForTraining_True(TrainingMode mode)
        {
            this.Categories.Where(c => c.ID == ExerciseCategory.Category1).FirstOrDefault().Active = true;
            this.sut.Category1EnabledForTraining = true;
            this.sut.TrainingMode = mode;

            Assert.IsTrue(this.sut.CanStartTraining);
        }

        [TestMethod]
        [DataRow(TrainingMode.CircleTraining)]
        [DataRow(TrainingMode.RandomTraining)]
        public void CanStartTraining_CircleOrRandomTrainingTrainingWithoutEnabledCategories_False(TrainingMode mode)
        {
            this.Categories.ForEach(c => c.Active = false);
            this.sut.TrainingMode = mode;

            Assert.IsFalse(this.sut.CanStartTraining);
        }

        [TestMethod]
        public void CanStartTraining_FocusTrainingWithCategoryInFocus_True()
        {
            this.sut.CategoryInFocus = this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First();
            this.sut.TrainingMode = TrainingMode.FocusTraining;

            Assert.IsTrue(this.sut.CanStartTraining);
        }

        [TestMethod]
        public void StartCommandCanExecute_FocusTrainingWithoutCategoryInFocus_False()
        {
            this.sut.CategoryInFocus = null;
            this.sut.TrainingMode = TrainingMode.FocusTraining;

            Assert.IsFalse(this.sut.CanStartTraining);
        }

        #endregion

        #region Event Reactions

        [TestMethod]
        public void CategoryChanges_RemovedAndAdded_CategoriesAndCategoryNamesDisplayedCorrectly()
        {
            var cat1 = this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First();
            this.Categories.Remove(cat1);

            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();

            this.PropertyChangedEvents.Clear();
            this.Categories.Add(cat1);

            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();
        }

        [TestMethod]
        public void CategoryChanges_Name_CategoriesAndCategoryNamesDisplayedCorrectly()
        {
            this.Categories.ForEach(c => c.Name = string.Concat(c.Name, "_Test"));
            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();
        }

        [TestMethod]
        public void CategoryChanges_Active_CategoriesAndCategoryNamesDisplayedCorrectly()
        {
            this.Categories.ForEach(c => c.Active = !c.Active);
            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();
        }

        #endregion

        #region Helper Methods

        private void AssertThatCategoryNamesAndActivitiyValuesAreCorrect()
        {
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.WarmUp).FirstOrDefault()?.Active ?? false,
                this.sut.CategoryWarmUpActive);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.WarmUp).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.CategoryWarmUpName);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category1).FirstOrDefault()?.Active ?? false,
                this.sut.Category1Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category1).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category1Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category2).FirstOrDefault()?.Active ?? false,
                this.sut.Category2Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category2).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category2Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category3).FirstOrDefault()?.Active ?? false,
                this.sut.Category3Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category3).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category3Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category4).FirstOrDefault()?.Active ?? false,
                this.sut.Category4Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category4).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category4Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category5).FirstOrDefault()?.Active ?? false,
                this.sut.Category5Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category5).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category5Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category6).FirstOrDefault()?.Active ?? false,
                this.sut.Category6Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category6).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category6Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category7).FirstOrDefault()?.Active ?? false,
                this.sut.Category7Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category7).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category7Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category8).FirstOrDefault()?.Active ?? false,
                this.sut.Category8Active);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.Category8).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.Category8Name);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.CoolDown).FirstOrDefault()?.Active ?? false,
                this.sut.CategoryCoolDownActive);
            Assert.AreEqual(
                this.Categories.Where(c => c.ID == ExerciseCategory.CoolDown).FirstOrDefault()?.Name ?? string.Empty,
                this.sut.CategoryCoolDownName);
        }

        #endregion
    }
}
