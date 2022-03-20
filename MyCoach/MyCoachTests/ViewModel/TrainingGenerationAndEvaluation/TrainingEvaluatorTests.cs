using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel;
using MyCoach.ViewModel.Services;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MyCoachTests.ViewModel.TrainingGenerationAndEvaluation
{
    [TestClass]
    public class TrainingEvaluatorTests : DataInterfaceTestBase
    {
        Training training;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();

            TrainingEvaluator.MessageBoxService = Mock.Of<IMessageBoxService>(service =>
                service.ShowMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>()) == MessageBoxResult.OK);

            this.TrainingSchedule.ScheduleType = ScheduleType.TimeBased;
            this.TrainingSchedule.Duration = 3;
            this.TrainingSchedule.StartMonth = DateTime.Now.Month == 1
                ? new DateTime(DateTime.Now.Year - 1, 12, 1)
                : new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);

            this.SetupData(TestMonths.ThreeMonthsWithCurrentInTheMiddle);
            this.SetupData(TestExercises.TwoOfEachCategory);

            this.training = new Training();
            foreach (var exercise in this.Exercises)
            {
                this.training.Add(new TrainingElementViewModel(TrainingElementType.exercise, exercise));
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            base.CleanupTestBase();
        }

        [TestMethod]
        public void Evaluate_TrainingWithCurentMonthInTimeBasedSchedule_ScoresAreUpdtedForTwoMonths()
        {
            this.CompleteTraining();

            this.AssertThatScoresForMonthsDifferByScoresFromExercises(
                TestMonths.ThreeMonthsWithCurrentInTheMiddle.First(),
                this.Months.First(),
                TestExercises.TwoOfEachCategory);

            this.AssertThatScoresForMonthsAreEqual(
                TestMonths.ThreeMonthsWithCurrentInTheMiddle.Skip(1).First(),
                this.Months.Skip(1).First());

            this.AssertThatScoresForMonthsDifferByScoresFromExercises(
                TestMonths.ThreeMonthsWithCurrentInTheMiddle.Skip(2).First(),
                this.Months.Skip(2).First(),
                TestExercises.TwoOfEachCategory);

            this.AssertThatScoresForMonthsAreEqual(
                TestMonths.ThreeMonthsWithCurrentInTheMiddle.Skip(3).First(),
                this.Months.Skip(3).First());

            Mock.Get(this.DataManager).Verify(dm => dm.SaveData<Month>(), Times.Once);
        }

        [TestMethod]
        public void Evaluate_TrainingWithCurentMonthInTimeBasedScheduleButIncompletedExercises_ScoresNotChanged()
        {
            this.training.Finish();

            this.AssertThatScoresForMonthsAreEqual(
                TestMonths.ThreeMonthsWithCurrentInTheMiddle.First(),
                this.Months.First());

            this.AssertThatScoresForMonthsAreEqual(
                TestMonths.ThreeMonthsWithCurrentInTheMiddle.Skip(1).First(),
                this.Months.Skip(1).First());

            this.AssertThatScoresForMonthsAreEqual(
                TestMonths.ThreeMonthsWithCurrentInTheMiddle.Skip(2).First(),
                this.Months.Skip(2).First());

            this.AssertThatScoresForMonthsAreEqual(
                TestMonths.ThreeMonthsWithCurrentInTheMiddle.Skip(3).First(),
                this.Months.Skip(3).First());

            Mock.Get(this.DataManager).Verify(dm => dm.SaveData<Month>(), Times.Once);
        }

        [TestMethod]
        public void Evaluate_TrainingWithCurentMonthNotInTimeBasedSchedule_ScoresAreUpdtedOnlyForCurrentMonth()
        {
            this.Months.First().StartDate = this.TrainingSchedule.StartMonth.AddYears(2);

            this.CompleteTraining();

            this.AssertThatScoresForMonthsDifferByScoresFromExercises(
                TestMonths.ThreeMonthsWithCurrentInTheMiddle.First(),
                this.Months.First(),
                TestExercises.TwoOfEachCategory);

            this.AssertThatScoresForMonthsAreEqual(
                TestMonths.ThreeMonthsWithCurrentInTheMiddle.Skip(1).First(),
                this.Months.Skip(1).First());

            this.AssertThatScoresForMonthsAreEqual(
                TestMonths.ThreeMonthsWithCurrentInTheMiddle.Skip(2).First(),
                this.Months.Skip(2).First());

            this.AssertThatScoresForMonthsAreEqual(
                TestMonths.ThreeMonthsWithCurrentInTheMiddle.Skip(3).First(),
                this.Months.Skip(3).First());

            Mock.Get(this.DataManager).Verify(dm => dm.SaveData<Month>(), Times.Once);
        }

        [TestMethod]
        public void Evaluate_TrainingForGenericSchedule_ScoresAreUpdtedCorrectly()
        {
            this.TrainingSchedule.ScheduleType = ScheduleType.Generic;

            this.CompleteTraining();

            this.AssertThatScoresForMonthsDifferByScoresFromExercises(
                TestMonths.ThreeMonthsWithCurrentInTheMiddle.First(),
                this.Months.First(),
                TestExercises.TwoOfEachCategory);

            Mock.Get(this.DataManager).Verify(dm => dm.SaveData<Month>(), Times.Once);
        }

        [TestMethod]
        public void Evaluate_TrainingForGenericSchedule_ScoresMultiplierUsedCorrectly()
        {
            this.TrainingSchedule.ScheduleType = ScheduleType.Generic;
            this.training.Clear();
            var exercise = new Exercise { ID = 0, Category = ExerciseCategory.Category1, Scores = 10 };
            var vm = new TrainingElementViewModel(TrainingElementType.exercise, exercise)
            {
                ScoresMultiplier = 0.55
            };

            this.training.Add(vm);
            var scoresCategory1BeforeCompletion = this.Months.First().Category1Scores;

            this.CompleteTraining();

            Assert.AreEqual(scoresCategory1BeforeCompletion + 6, this.Months.First().Category1Scores);
            Mock.Get(this.DataManager).Verify(dm => dm.SaveData<Month>(), Times.Once);
        }

        private void CompleteTraining()
        {
            // Cannot iterate over complete collection and set to completed, because collection will clear itself on last vm completed.
            this.training.Skip(1).All(e => (e as TrainingElementViewModel).Completed = true);
            (this.training.First() as TrainingElementViewModel).Completed = true;
        }

        private void AssertThatScoresForMonthsAreEqual(Month month1, Month month2)
        {
            Assert.AreEqual(month1.Category1Scores, month2.Category1Scores);
            Assert.AreEqual(month1.Category2Scores, month2.Category2Scores);
            Assert.AreEqual(month1.Category3Scores, month2.Category3Scores);
            Assert.AreEqual(month1.Category4Scores, month2.Category4Scores);
            Assert.AreEqual(month1.Category5Scores, month2.Category5Scores);
            Assert.AreEqual(month1.Category6Scores, month2.Category6Scores);
            Assert.AreEqual(month1.Category7Scores, month2.Category7Scores);
            Assert.AreEqual(month1.Category8Scores, month2.Category8Scores);
        }

        private void AssertThatScoresForMonthsDifferByScoresFromExercises(
            Month originalMonth,
            Month updatedMonth,
            List<Exercise> exercises)
        {
            var expectedScoresCategory1 = exercises.Where(e => e.Category == ExerciseCategory.Category1).Sum(e => e.Scores)
                + originalMonth.Category1Scores;
            var expectedScoresCategory2 = exercises.Where(e => e.Category == ExerciseCategory.Category2).Sum(e => e.Scores)
                + originalMonth.Category2Scores;
            var expectedScoresCategory3 = exercises.Where(e => e.Category == ExerciseCategory.Category3).Sum(e => e.Scores)
                + originalMonth.Category3Scores;
            var expectedScoresCategory4 = exercises.Where(e => e.Category == ExerciseCategory.Category4).Sum(e => e.Scores)
                + originalMonth.Category4Scores;
            var expectedScoresCategory5 = exercises.Where(e => e.Category == ExerciseCategory.Category5).Sum(e => e.Scores)
                + originalMonth.Category5Scores;
            var expectedScoresCategory6 = exercises.Where(e => e.Category == ExerciseCategory.Category6).Sum(e => e.Scores)
                + originalMonth.Category6Scores;
            var expectedScoresCategory7 = exercises.Where(e => e.Category == ExerciseCategory.Category7).Sum(e => e.Scores)
                + originalMonth.Category7Scores;
            var expectedScoresCategory8 = exercises.Where(e => e.Category == ExerciseCategory.Category8).Sum(e => e.Scores)
                + originalMonth.Category8Scores;

            Assert.AreEqual(expectedScoresCategory1, updatedMonth.Category1Scores);
            Assert.AreEqual(expectedScoresCategory2, updatedMonth.Category2Scores);
            Assert.AreEqual(expectedScoresCategory3, updatedMonth.Category3Scores);
            Assert.AreEqual(expectedScoresCategory4, updatedMonth.Category4Scores);
            Assert.AreEqual(expectedScoresCategory5, updatedMonth.Category5Scores);
            Assert.AreEqual(expectedScoresCategory6, updatedMonth.Category6Scores);
            Assert.AreEqual(expectedScoresCategory7, updatedMonth.Category7Scores);
            Assert.AreEqual(expectedScoresCategory8, updatedMonth.Category8Scores);
        }
    }
}
