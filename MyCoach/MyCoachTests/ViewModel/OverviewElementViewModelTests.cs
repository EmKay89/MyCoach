using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using MyCoach.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class OverviewElementViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        private OverviewElementViewModel sut;
        private Category category;
        private Month month;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.SetupCategoryAndMonth();
            this.sut = new OverviewElementViewModel(month, category);
            this.sut.PropertyChanged += 
                (object sender, PropertyChangedEventArgs e) => { this.PropertyChangedEvents.Add(e.PropertyName); };
        }

        [TestCleanup]
        public void Cleanup()
        {
            base.CleanupTestBase();
        }

        #endregion

        #region Construction and Properties Tests

        [TestMethod]
        public void Construction_WithCategory_PropertiesSetUpCorrectly()
        {
            var monthString = this.month.GetStartDateFromSchedule(
            DataInterface.GetInstance().GetData<TrainingSchedule>().FirstOrDefault())
                .ToString("y", CultureInfo.CurrentCulture);

            Assert.IsTrue(this.sut.MaxScoreOrGoal == 0);
            Assert.IsTrue(this.sut.Month == monthString);
            Assert.IsTrue(this.sut.ScoresString == $"{this.month.Category1Scores} von {this.month.Category1Goal}");
        }

        [TestMethod]
        public void Construction_WithoutCategory_PropertiesSetUpCorrectly()
        {
            var cat1 = this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First();
            var cat2 = this.Categories.Where(c => c.ID == ExerciseCategory.Category2).First();
            var cat3 = this.Categories.Where(c => c.ID == ExerciseCategory.Category3).First();

            foreach (var cat in this.Categories)
            {
                cat.Active = false;
            }

            cat1.Active = true;
            cat2.Active = true;

            this.month.TotalGoal = 333;
            this.month.Category1Scores = 10;
            this.month.Category2Scores = 20;
            this.month.Category3Scores = 40;

            this.sut = new OverviewElementViewModel(month, null);

            Assert.IsTrue(this.sut.ScoresString == $"{this.month.Category1Scores + this.month.Category2Scores}" +
                $" von {this.month.TotalGoal}");
        }

        [TestMethod]
        public void MaxScoreOrGoal_Changes_HeightPropertiesUpdated()
        {
            this.sut.MaxScoreOrGoal = 500;

            Assert.IsTrue(this.sut.RelativeHeightGoal == 40);
            Assert.IsTrue(this.sut.RelativeHeightSpaceAboveGoal == 60);
            Assert.IsTrue(this.sut.RelativeHeightScores == 20);
            Assert.IsTrue(this.sut.RelativeHeightSpaceAboveScores == 80);
        }

        [TestMethod]
        public void MaxScoreOrGoal_Changes_PropertyChangedEventsRaised()
        {
            this.sut.MaxScoreOrGoal = 500;

            Assert.AreEqual(5, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.MaxScoreOrGoal));
            Assert.AreEqual(this.PropertyChangedEvents[1], nameof(this.sut.RelativeHeightGoal));
            Assert.AreEqual(this.PropertyChangedEvents[2], nameof(this.sut.RelativeHeightSpaceAboveGoal));
            Assert.AreEqual(this.PropertyChangedEvents[3], nameof(this.sut.RelativeHeightScores));
            Assert.AreEqual(this.PropertyChangedEvents[4], nameof(this.sut.RelativeHeightSpaceAboveScores));
        }

        #endregion

        #region Event Reactions

        [TestMethod]
        public void MonthChanges_HeightPropertiesUpdated()
        {
            this.sut.MaxScoreOrGoal = 500;

            this.month.Category1Scores = 200;
            this.month.Category1Goal = 400;

            Assert.IsTrue(this.sut.RelativeHeightGoal == 80);
            Assert.IsTrue(this.sut.RelativeHeightSpaceAboveGoal == 20);
            Assert.IsTrue(this.sut.RelativeHeightScores == 40);
            Assert.IsTrue(this.sut.RelativeHeightSpaceAboveScores == 60);
        }

        [TestMethod]
        public void MonthChanges_PropertyChangedEventsRaised()
        {
            this.PropertyChangedEvents.Clear();

            this.month.Category1Goal++;

            Assert.AreEqual(5, this.PropertyChangedEvents.Count);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.ScoresString));
            Assert.AreEqual(this.PropertyChangedEvents[1], nameof(this.sut.RelativeHeightGoal));
            Assert.AreEqual(this.PropertyChangedEvents[2], nameof(this.sut.RelativeHeightSpaceAboveGoal));
            Assert.AreEqual(this.PropertyChangedEvents[3], nameof(this.sut.RelativeHeightScores));
            Assert.AreEqual(this.PropertyChangedEvents[4], nameof(this.sut.RelativeHeightSpaceAboveScores));
        }

        #endregion

        #region Helper Methods

        private void SetupCategoryAndMonth()
        {
            this.category = new Category
            {
                ID = ExerciseCategory.Category1,
                Name = "Arme",
                Active = true,
                Type = ExerciseType.Training
            };

            this.month = new Month()
            {
                Category1Goal = 200,
                Category1Scores = 100
            };
        }

        #endregion
    }
}
