using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class TrainingScheduleOverviewViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        private TrainingScheduleOverviewViewModel sut;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.sut = new TrainingScheduleOverviewViewModel();
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
        public void Construction_AvailableCategoriesUpdated()
        {
            var activeTestCategories = this.Categories.Where(c => c.Active && c.Type == ExerciseType.Training).ToList();

            Assert.IsTrue(this.sut.AvailableCategories.Count == activeTestCategories.Count() + 1);

            Assert.IsNull(this.sut.AvailableCategories[0]);

            for (int i = 0; i < activeTestCategories.Count(); i++)
            {
                Assert.AreEqual(activeTestCategories[i], sut.AvailableCategories[i + 1]);
            }            
        }

        [TestMethod]
        public void Construction_ChartElementsUpdated()
        {
            AssertChartElementsCountAndType();
        }

        [TestMethod]
        public void Construction_MaxScoreOrGoalUpdated()
        {
            this.CreateThreeEmptyMonths();
            this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First().Active = true;
            this.Categories.Where(c => c.ID == ExerciseCategory.Category2).First().Active = true;
            this.Months.Where(m => m.Number == MonthNumber.Month1).First().Category1Goal = 50;
            this.Months.Where(m => m.Number == MonthNumber.Month1).First().Category2Goal = 1200;
            this.Months.Where(m => m.Number == MonthNumber.Month1).First().TotalGoal = 1000;

            this.sut = new TrainingScheduleOverviewViewModel();

            Assert.IsTrue(this.sut.MaxScoreOrGoal == 1000);

            this.Months.Where(m => m.Number == MonthNumber.Month2).First().Category1Scores = 1500;

            this.sut = new TrainingScheduleOverviewViewModel();

            Assert.IsTrue(this.sut.MaxScoreOrGoal == 1500);
        }

        [TestMethod]
        public void SelectedCategoryListIndex_Changes_ChartElementsUpdated()
        {
            this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First().Active = true;
            this.Categories.Where(c => c.ID == ExerciseCategory.Category2).First().Active = true;
            this.Months.Where(m => m.Number == MonthNumber.Month1).First().Category2Scores = 1234;
            this.Months.Where(m => m.Number == MonthNumber.Month1).First().Category2Goal = 2345;

            this.sut.SelectedCategoryListIndex = 2;

            Assert.IsTrue(this.sut.Elements.First().ScoresString == "1234 von 2345");
            this.AssertChartElementsCountAndType();
        }

        [TestMethod]
        public void SelectedCategoryListIndex_Changes_MaxScoreOrGoalUpdated()
        {
            var activeTestCategories = this.Categories.Where(c => c.Active && c.Type == ExerciseType.Training).ToList();
            this.CreateThreeEmptyMonths();
            this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First().Active = true;
            this.Categories.Where(c => c.ID == ExerciseCategory.Category2).First().Active = true;
            this.Months.Where(m => m.Number == MonthNumber.Month1).First().Category1Goal = 50;
            this.Months.Where(m => m.Number == MonthNumber.Month1).First().Category2Goal = 100;
            this.Months.Where(m => m.Number == MonthNumber.Month1).First().TotalGoal = 1000;

            this.sut.SelectedCategoryListIndex = 2;

            Assert.IsTrue(this.sut.MaxScoreOrGoal == 100);

            this.sut.SelectedCategoryListIndex = 0;

            Assert.IsTrue(this.sut.MaxScoreOrGoal == 1000);

            this.sut.SelectedCategoryListIndex = 1;

            Assert.IsTrue(this.sut.MaxScoreOrGoal == 50);

            this.Months.Where(m => m.Number == MonthNumber.Month1).First().Category1Scores = 500;
            this.Months.Where(m => m.Number == MonthNumber.Month1).First().Category2Scores = 1000;

            this.sut.SelectedCategoryListIndex = 0;

            Assert.IsTrue(this.sut.MaxScoreOrGoal == 1500);
        }

        #endregion

        #region Event Reactions

        [TestMethod]
        public void CategoriesChange_NotSelectedCategoryRemoved_ChartElementsUpdated()
        {
            var cat1 = this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First();
            var cat2 = this.Categories.Where(c => c.ID == ExerciseCategory.Category2).First();
            Assert.IsTrue(cat1.Active);
            Assert.IsTrue(cat2.Active);
            Assert.IsTrue(cat1.Name != cat2.Name);
            Assert.IsTrue(this.sut.SelectedCategoryListIndex == 0);

            this.Categories.Remove(cat2);

            this.AssertChartElementsCountAndType();
        }

        [TestMethod]
        public void CategoriesChange_SelectedCategoryRemoved_TotalSelected()
        {
            var cat1 = this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First();
            var cat2 = this.Categories.Where(c => c.ID == ExerciseCategory.Category2).First();
            Assert.IsTrue(cat1.Active);
            Assert.IsTrue(cat2.Active);
            Assert.IsTrue(cat1.Name != cat2.Name);
            this.sut.SelectedCategoryListIndex = 1;
            Assert.IsTrue(this.sut.AvailableCategories[this.sut.SelectedCategoryListIndex].Name == cat1.Name);
            Assert.IsTrue(this.sut.AvailableCategoryListItems[this.sut.SelectedCategoryListIndex] == cat1.Name);

            this.Categories.Remove(cat1);

            this.AssertChartElementsCountAndType();
            Assert.IsTrue(this.sut.SelectedCategoryListIndex == 0);
            Assert.IsTrue(this.sut.AvailableCategories[this.sut.SelectedCategoryListIndex] == null);
            Assert.IsTrue(this.sut.AvailableCategoryListItems[this.sut.SelectedCategoryListIndex] == "Gesamt");            
        }

        [TestMethod]
        public void SelectedCategoryChanges_Renamed_NameInListUpdated()
        {
            var newName = "New Category Name";
            var cat1 = this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First();
            this.sut.SelectedCategoryListIndex = 1;
            Assert.IsTrue(cat1.Name != newName);
            Assert.IsTrue(this.sut.AvailableCategoryListItems[this.sut.SelectedCategoryListIndex] == cat1.Name);

            cat1.Name = newName;

            this.AssertChartElementsCountAndType();
            Assert.IsTrue(this.sut.AvailableCategoryListItems[this.sut.SelectedCategoryListIndex] == newName);
        }

        [TestMethod]
        public void SelectedCategoryChanges_Inactivated_TotalSelected()
        {
            var cat1 = this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First();
            var cat2 = this.Categories.Where(c => c.ID == ExerciseCategory.Category2).First();
            Assert.IsTrue(cat1.Active);
            Assert.IsTrue(cat2.Active);
            Assert.IsTrue(cat1.Name != cat2.Name);
            this.sut.SelectedCategoryListIndex = 1;
            Assert.IsTrue(this.sut.AvailableCategories[this.sut.SelectedCategoryListIndex].Name == cat1.Name);
            Assert.IsTrue(this.sut.AvailableCategoryListItems[this.sut.SelectedCategoryListIndex] == cat1.Name);

            cat1.Active = false;

            Assert.IsTrue(this.sut.SelectedCategoryListIndex == 0);
            Assert.IsTrue(this.sut.AvailableCategories[this.sut.SelectedCategoryListIndex] == null);
            Assert.IsTrue(this.sut.AvailableCategoryListItems[this.sut.SelectedCategoryListIndex] == "Gesamt");
        }

        [TestMethod]
        public void ScheduleChanges_Duration_ChartElementsUpdated()
        {
            Assert.IsTrue(this.TrainingSchedule.Duration > 1);

            this.TrainingSchedule.Duration--;

            this.AssertChartElementsCountAndType();
        }

        [TestMethod]
        public void ScheduleChanges_StartMonth_ChartElementsUpdated()
        {
            this.TrainingSchedule.StartMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            this.AssertChartElementsCountAndType();
        }

        [TestMethod]
        public void MonthInScheduleChanges_MaxScoreOrGoalUpdated()
        {
            var cat1 = this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First();
            Assert.IsTrue(cat1.Active);
            Assert.IsTrue(cat1.Name != string.Empty);
            this.sut.SelectedCategoryListIndex = 1;

            this.Months.First(m => m.Number != MonthNumber.Current).Category1Scores = ushort.MaxValue;

            Assert.IsTrue(this.sut.MaxScoreOrGoal == ushort.MaxValue);

            foreach (var element in this.sut.Elements)
            {
                Assert.IsTrue(element.MaxScoreOrGoal == ushort.MaxValue);
            }
        }

        #endregion

        #region Helper Methods

        private void CreateThreeEmptyMonths()
        {
            this.Months.Clear();
            this.Months.Add(
                new Month
                {
                    Number = MonthNumber.Current,
                    Category1Goal = 0,
                    Category1Scores = 0,
                    Category2Goal = 0,
                    Category2Scores = 0,
                    Category3Goal = 0,
                    Category3Scores = 0,
                    Category4Goal = 0,
                    Category4Scores = 0,
                    Category5Goal = 0,
                    Category5Scores = 0,
                    Category6Goal = 0,
                    Category6Scores = 0,
                    Category7Goal = 0,
                    Category7Scores = 0,
                    Category8Goal = 0,
                    Category8Scores = 0
                });

            this.Months.Add(
                new Month
                {
                    Number = MonthNumber.Month1,
                    Category1Goal = 0,
                    Category1Scores = 0,
                    Category2Goal = 0,
                    Category2Scores = 0,
                    Category3Goal = 0,
                    Category3Scores = 0,
                    Category4Goal = 0,
                    Category4Scores = 0,
                    Category5Goal = 0,
                    Category5Scores = 0,
                    Category6Goal = 0,
                    Category6Scores = 0,
                    Category7Goal = 0,
                    Category7Scores = 0,
                    Category8Goal = 0,
                    Category8Scores = 0
                });

            this.Months.Add(
                new Month
                {
                    Number = MonthNumber.Month2,
                    Category1Goal = 0,
                    Category1Scores = 0,
                    Category2Goal = 0,
                    Category2Scores = 0,
                    Category3Goal = 0,
                    Category3Scores = 0,
                    Category4Goal = 0,
                    Category4Scores = 0,
                    Category5Goal = 0,
                    Category5Scores = 0,
                    Category6Goal = 0,
                    Category6Scores = 0,
                    Category7Goal = 0,
                    Category7Scores = 0,
                    Category8Goal = 0,
                    Category8Scores = 0
                });
        }

        private void AssertChartElementsCountAndType()
        {
            var duration = this.TrainingSchedule.Duration;

            Assert.AreEqual(duration, this.sut.Elements.Count());

            for (int i = 0; i < duration; i++)
            {
                Assert.AreEqual(
                    this.sut.Elements[i].Month,
                    this.Months[i + 1].StartDate.ToString("y", CultureInfo.CurrentCulture));
            }
        }

        #endregion
    }
}
