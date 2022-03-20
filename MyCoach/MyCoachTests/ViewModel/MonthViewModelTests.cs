using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class MonthViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        private MonthViewModel sut;
        private readonly Month month = new Month
        {
            Number = MonthNumber.Month1,
            Category1Goal = 30,
            Category1Scores = 10,
            Category2Goal = 60,
            Category2Scores = 20,
            Category3Goal = 120,
            Category3Scores = 40,
            Category4Goal = 240,
            Category4Scores = 80,
            Category5Goal = 480,
            Category5Scores = 160,
            Category6Goal = 960,
            Category6Scores = 320,
            Category7Goal = 1920,
            Category7Scores = 640,
            Category8Goal = 3840,
            Category8Scores = 1280,
            TotalGoal = 3000
        };        

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.sut = new MonthViewModel(this.month);
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
        public void Construction_ChildViewModelsCreated()
        {
            AssertThatMonthCategoryDetailViewModelsAreUpdatedCorrectly();
        }

        [TestMethod]
        public void Construction_PropertiesCorrectlySetUp()
        {
            var activeTrainingCategories = TestDtos.Categories
                .Where(c => c.Type == ExerciseType.Training && c.Active).ToList();

            int sum = 0;

            foreach (var category in activeTrainingCategories)
            {
                sum += this.month.GetScores(category.ID);
            }

            Assert.IsTrue(this.sut.TotalScores == $"{sum} von 3000");
            Assert.IsTrue(this.sut.TotalPercentage == (uint)(sum * 100 / 3000));
            Assert.IsTrue(this.sut.Description == "Januar 1");
        }

        #endregion

        #region Event Reactions

        [TestMethod]
        public void MonthChanges_PropertyChangedEventsRaised()
        {
            this.month.Category1Scores++;

            this.AssertThatTotalPercentageAndTotalScoresPropertyChangedEventsAreRaisedOnce();

            this.PropertyChangedEvents.Clear();

            this.month.TotalGoal++;

            this.AssertThatTotalPercentageAndTotalScoresPropertyChangedEventsAreRaisedOnce();
        }

        [TestMethod]
        public void CategoriesChange_ChildViewModelsUpdated()
        {
            this.Categories.Clear();

            this.AssertThatMonthCategoryDetailViewModelsAreUpdatedCorrectly();

            this.Categories.Add(
                new Category { Name = "Aufwärmen", ID = ExerciseCategory.WarmUp, Active = true, Type = ExerciseType.WarmUp });
            this.Categories.Add(
                new Category { Name = "Übung 1", ID = ExerciseCategory.Category1, Active = true, Type = ExerciseType.Training });
            this.Categories.Add(
                new Category { Name = "Übung 2", ID = ExerciseCategory.Category2, Active = false, Type = ExerciseType.Training });
            this.Categories.Add(
                new Category { Name = "Übung 3", ID = ExerciseCategory.Category3, Active = true, Type = ExerciseType.Training });
            this.Categories.Add(
                new Category { Name = "Abwärmen", ID = ExerciseCategory.CoolDown, Active = true, Type = ExerciseType.CoolDown });

            this.AssertThatMonthCategoryDetailViewModelsAreUpdatedCorrectly();

            this.Categories.Skip(3).First().Active = false;
            this.Categories.Skip(2).First().Active = true;
            this.Categories.Skip(1).First().Name = "neue Übung 1";

            this.AssertThatMonthCategoryDetailViewModelsAreUpdatedCorrectly();
        }

        #endregion

        #region Helper Methods

        private void AssertThatTotalPercentageAndTotalScoresPropertyChangedEventsAreRaisedOnce()
        {
            Assert.IsTrue(this.PropertyChangedEvents.Count == 2);
            Assert.IsTrue(this.PropertyChangedEvents[0] == nameof(this.sut.TotalPercentage));
            Assert.IsTrue(this.PropertyChangedEvents[1] == nameof(this.sut.TotalScores));
        }

        private void AssertThatMonthCategoryDetailViewModelsAreUpdatedCorrectly()
        {
            var activeTrainingCategories = TestDtos.Categories
                .Where(c => c.Type == ExerciseType.Training && c.Active).ToList();

            Assert.AreEqual(activeTrainingCategories.Count(), this.sut.MonthCategoryDetailViewModels.Count());
            for (int i = 0; i < activeTrainingCategories.Count(); i++)
            {
                Assert.AreEqual(activeTrainingCategories[i].Name, this.sut.MonthCategoryDetailViewModels[i].Name);
            }
        }

        #endregion
    }
}
