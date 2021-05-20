using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
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
    public class MonthCategoryDetailViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        private MonthCategoryDetailViewModel sut;
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

        private readonly Category category = new Category
        {
            ID = ExerciseCategory.Category1,
            Name = "Test-Kategorie",
            Type = ExerciseType.Training,
            Count = 10,
            Active = true
        };

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.sut = new MonthCategoryDetailViewModel(this.category, this.month);
            this.sut.PropertyChanged += (object sender, PropertyChangedEventArgs e) 
                => { this.PropertyChangedEvents.Add(e.PropertyName); };
        }

        [TestCleanup]
        public void Cleanup()
        {
            base.CleanupTestBase();
        }

        #endregion

        #region Construction and Properties Tests

        [TestMethod]
        public void Construction_PropertiesCorrectlySetUp()
        {
            Assert.AreEqual(this.sut.Name, "Test-Kategorie");
            Assert.AreEqual(this.sut.Scores, 10);
            Assert.AreEqual(this.sut.AppendedGoalTag, " von 30");
            Assert.AreEqual(this.sut.Percentage, 33);
        }

        [TestMethod]
        public void Scores_Set_ScoresAreSetCorrectlyAndCallsSaveDataFromDataManager()
        {
            ushort newScores = 33;

            this.sut.Scores = newScores;

            Assert.AreEqual(newScores, this.month.Category1Scores);
            Mock.Get(this.DataManager).Verify(dm => dm.SaveData<Month>(), Times.Once);
        }

        #endregion

        #region Event Reactions

        [TestMethod]
        public void MonthChanges_PropertiesCorrectlyUpdated()
        {
            this.month.Category1Scores = 20;

            Assert.AreEqual(this.sut.Scores, 20);
            Assert.AreEqual(this.sut.Percentage, 67);

            this.month.Category1Goal = 40;

            Assert.AreEqual(this.sut.AppendedGoalTag, " von 40");

            this.month.Category1Goal = 10;

            Assert.AreEqual(this.sut.Percentage, 100);

            this.month.Category1Goal = 0;

            Assert.AreEqual(this.sut.AppendedGoalTag, string.Empty); 
            Assert.AreEqual(this.sut.Percentage, 0);
        }

        [TestMethod]
        public void MonthChanges_PropertyChangedEventsRaised()
        {
            this.month.Category1Goal = 20;

            Assert.AreEqual(this.PropertyChangedEvents.Count, 3);
            Assert.AreEqual(this.PropertyChangedEvents[0], nameof(this.sut.Scores));
            Assert.AreEqual(this.PropertyChangedEvents[1], nameof(this.sut.AppendedGoalTag));
            Assert.AreEqual(this.PropertyChangedEvents[2], nameof(this.sut.Percentage));
        }

        #endregion
    }
}
