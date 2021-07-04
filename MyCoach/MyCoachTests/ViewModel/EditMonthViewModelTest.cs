using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using MyCoach.ViewModel;
using System;
using System.ComponentModel;
using System.Linq;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class EditMonthViewModelTest : ViewModelTestBase
    {
        #region Initialization and Cleanup

        private EditMonthViewModel sut;
        private Month month;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.SetupMonth();
            this.sut = new EditMonthViewModel(month);
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
        public void Construction_PropertiesSetUpCorrectly()
        {
            Assert.AreEqual(100, this.sut.Category1Goal);
            Assert.AreEqual(200, this.sut.Category2Goal);
            Assert.AreEqual(300, this.sut.Category3Goal);
            Assert.AreEqual(400, this.sut.Category4Goal);
            Assert.AreEqual(500, this.sut.Category5Goal);
            Assert.AreEqual(600, this.sut.Category6Goal);
            Assert.AreEqual(700, this.sut.Category7Goal);
            Assert.AreEqual(800, this.sut.Category8Goal);

            Assert.AreEqual(this.Categories.First(c => c.ID == ExerciseCategory.Category1).Active,
                this.sut.Category1ItemsVisible);
            Assert.AreEqual(this.Categories.First(c => c.ID == ExerciseCategory.Category2).Active,
                this.sut.Category2ItemsVisible);
            Assert.AreEqual(this.Categories.First(c => c.ID == ExerciseCategory.Category3).Active,
                this.sut.Category3ItemsVisible);
            Assert.AreEqual(this.Categories.First(c => c.ID == ExerciseCategory.Category4).Active,
                this.sut.Category4ItemsVisible);
            Assert.AreEqual(this.Categories.First(c => c.ID == ExerciseCategory.Category5).Active,
                this.sut.Category5ItemsVisible);
            Assert.AreEqual(this.Categories.First(c => c.ID == ExerciseCategory.Category6).Active,
                this.sut.Category6ItemsVisible);
            Assert.AreEqual(this.Categories.First(c => c.ID == ExerciseCategory.Category7).Active,
                this.sut.Category7ItemsVisible);
            Assert.AreEqual(this.Categories.First(c => c.ID == ExerciseCategory.Category8).Active,
                this.sut.Category8ItemsVisible);

            Assert.AreEqual("November 1989", this.sut.MonthName);

            this.sut = new EditMonthViewModel(new Month(){ Number = MonthNumber.Current });

            Assert.AreEqual(string.Empty, this.sut.MonthName);
        }

        [TestMethod]
        public void SettingGoals_PropertiesOfMonthUpdatedCorrectly()
        {
            this.sut.Category1Goal = (ushort)(this.sut.Category1Goal * 10);
            this.sut.Category2Goal = (ushort)(this.sut.Category2Goal * 10);
            this.sut.Category3Goal = (ushort)(this.sut.Category3Goal * 10);
            this.sut.Category4Goal = (ushort)(this.sut.Category4Goal * 10);
            this.sut.Category5Goal = (ushort)(this.sut.Category5Goal * 10);
            this.sut.Category6Goal = (ushort)(this.sut.Category6Goal * 10);
            this.sut.Category7Goal = (ushort)(this.sut.Category7Goal * 10);
            this.sut.Category8Goal = (ushort)(this.sut.Category8Goal * 10);

            Assert.AreEqual(1000, this.month.Category1Goal);
            Assert.AreEqual(2000, this.month.Category2Goal);
            Assert.AreEqual(3000, this.month.Category3Goal);
            Assert.AreEqual(4000, this.month.Category4Goal);
            Assert.AreEqual(5000, this.month.Category5Goal);
            Assert.AreEqual(6000, this.month.Category6Goal);
            Assert.AreEqual(7000, this.month.Category7Goal);
            Assert.AreEqual(8000, this.month.Category8Goal);
        }

        #endregion

        #region Helper Methods

        private void SetupMonth()
        {
            this.month = new Month()
            {
                Number = MonthNumber.Month1,
                Category1Goal = 100,
                Category2Goal = 200,
                Category3Goal = 300,
                Category4Goal = 400,
                Category5Goal = 500,
                Category6Goal = 600,
                Category7Goal = 700,
                Category8Goal = 800,
                StartDate = new DateTime(1989, 11, 01)
            };
        }

        #endregion
    }
}
