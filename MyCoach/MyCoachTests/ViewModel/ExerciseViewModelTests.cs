using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel;
using MyCoach.ViewModel.Events;
using System.Collections.Generic;
using System.Linq;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class ExerciseViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        private ExerciseViewModel sut;
        private Exercise exercise;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.exercise = new Exercise()
            {
                ID = 1,
                Category = ExerciseCategory.Category1,
                Count = 20,
                Name = "Testübung",
                Unit = "Testeinheit",
                Scores = 10,
                Info = "Testbeschreibung",
                Active = true
            };

            this.sut = new ExerciseViewModel(exercise);
            this.sut.PropertyChanged += this.OnSutPropertyChanged;
        }

        [TestCleanup]
        public new void CleanUp()
        {
            base.CleanUp();
        }

        #endregion

        #region Construction and Properties Tests

        [TestMethod]
        public void Construction_HappyPath_DisplaysCorrectInformationOfExercise()
        {
            Assert.AreEqual(this.exercise.Category, this.sut.Category);
            Assert.AreEqual(this.exercise.Count, this.sut.Count);
            Assert.AreEqual(this.exercise.Name, this.sut.Name);
            Assert.AreEqual(this.exercise.Unit, this.sut.Unit);
            Assert.AreEqual(this.exercise.Scores, this.sut.Scores);
            Assert.AreEqual(this.exercise.Info, this.sut.Info);
            Assert.AreEqual(this.exercise.Active, this.sut.Active);
        }

        [TestMethod]
        public void Construction_HappyPath_LoadsCategoriesFromDataBaseAndFillsActiveCategoryListCorrectly()
        {
            // Check preconditions of test environment
            Assert.IsTrue(this.Categories.Any(c => c.Active == false));

            Assert.AreEqual(this.Categories, this.sut.Categories);
            Assert.AreEqual(this.Categories.Count(c => c.Active), this.sut.ActiveCategories.Count);
            for (int i = 0; i < this.sut.Categories.Count; i++)
            {
                Assert.AreEqual(this.Categories[i].ID, this.sut.Categories[i].ID);
            }
        }

        #endregion

        #region Command Tests

        [TestMethod]
        public void AddExerciseToTrainingCommand_Execute_InvokesAddExerciseToTrainingExecutedWithAnExerciseCopy()
        {
            List<Exercise> addedExercises = new List<Exercise>();
            this.sut.AddExerciseToTrainingExecuted += (object sender, ExerciseEventArgs e) =>
            {
                addedExercises.Add(e.Exercise);
            };

            this.sut.AddExerciseToTrainingCommand.Execute(sut.Exercise);

            Assert.AreNotEqual(this.exercise, addedExercises.Single());
            Assert.IsTrue(this.exercise.ValuesAreEqual(addedExercises.Single()));
        }

        [TestMethod]
        public void RemoveExerciseCommand_Execute_InvokesDeleteExerciseExecuted()
        {
            List<Exercise> deletedExercises = new List<Exercise>();
            this.sut.DeleteExerciseExecuted += (object sender, ExerciseEventArgs e) =>
            {
                deletedExercises.Add(e.Exercise);
            };

            this.sut.RemoveExerciseCommand.Execute(sut.Exercise);

            Assert.AreEqual(this.exercise, deletedExercises.Single());
        }

        #endregion

        #region Event Reactions

        [TestMethod]
        public void CategoriesChange_SelectedCategoryBecomesInactive_SelectedCategoryOfDtoRemainsUnchanged()
        {
            // The view model must not delete or change the category of the exercise DTO!
            var selectedCategory = this.Categories.Single(c => c.ID == this.sut.Category);

            // Check preconditions of test environment
            Assert.IsTrue(selectedCategory.Active);

            selectedCategory.Active = false;

            Assert.AreEqual(selectedCategory.ID, this.sut.Category);
            Assert.AreEqual(selectedCategory.ID, this.exercise.Category);
        }

        [TestMethod]
        public void CategoriesChange_SelectedCategoryIsRemovedFromDatabase_SelectedCategoryRemainsUnchanged()
        {
            // The view model must not delete or change the category of the exercise DTO!
            var selectedCategory = this.Categories.Single(c => c.ID == this.sut.Category);

            this.Categories.Clear();

            Assert.AreEqual(selectedCategory.ID, this.sut.Category);
            Assert.AreEqual(selectedCategory.ID, this.exercise.Category);
        }

        [TestMethod]
        public void Excercise_Changes_InvokesExerciseChanged()
        {
            List<Exercise> exerciseChanges = new List<Exercise>();
            this.sut.ExerciseChanged += (object sender, ExerciseEventArgs e) =>
            {
                exerciseChanges.Add(e.Exercise);
            };

            this.sut.Count++;

            Assert.AreEqual(this.exercise, exerciseChanges.Single());
        }

        #endregion
    }
}
