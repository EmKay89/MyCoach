﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCoach.DataHandling.DataManager;
using MyCoach.Helpers.Extensions.IEnumerable;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using System.Collections.Generic;
using System.Linq;

namespace MyCoachTests.ViewModel.TrainingGenerationAndEvaluation
{
    [TestClass]
    public class TrainingGeneratorTests : DataInterfaceTestBase
    {
        private List<ExerciseCategory> activeCategoriesForTrainingSettings;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.SetupData(TestCategories.AllCategoriesActive);
            this.SetupData(TestExercises.SixOfEachCategory);
            this.Settings.Permission = ExerciseSchedulingRepetitionPermission.No;
        }

        [TestCleanup]
        public void Cleanup()
        {
            base.CleanupTestBase();
        }

        [TestMethod]
        public void CreateTraining_CircleTrainingWithAllCategories()
        {
            var settings = this.GetTrainingSettings(TrainingMode.CircleTraining, 4);

            var training = TrainingGenerator.CreateTraining(settings);

            this.AssertLapSeparatorsPresenceAndName(training, true, 4, true); 
            this.AssertExercisesOfCategory(training, ExerciseCategory.WarmUp, 3, false);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category1, 4, false, 1, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category2, 4, false, 1, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category3, 4, false, 1, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category4, 4, false, 1, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category5, 4, false, 1, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category6, 4, false, 1, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category7, 4, false, 1, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category8, 4, false, 1, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.CoolDown, 3, false);
        }

        [TestMethod]
        public void CreateTraining_CircleTrainingWithFourCategoriesLimitedByCategoryAvailability()
        {
            var settings = this.GetTrainingSettings(TrainingMode.CircleTraining, 4);
            this.activeCategoriesForTrainingSettings.Remove(ExerciseCategory.WarmUp);
            this.activeCategoriesForTrainingSettings.Remove(ExerciseCategory.Category5);
            this.activeCategoriesForTrainingSettings.Remove(ExerciseCategory.Category6);
            this.activeCategoriesForTrainingSettings.Remove(ExerciseCategory.Category7);
            this.activeCategoriesForTrainingSettings.Remove(ExerciseCategory.Category8);
            this.activeCategoriesForTrainingSettings.Remove(ExerciseCategory.CoolDown);

            var training = TrainingGenerator.CreateTraining(settings);

            this.AssertLapSeparatorsPresenceAndName(training, false, 4, false);
            this.AssertExercisesOfCategory(training, ExerciseCategory.WarmUp, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category1, 4, false, 1, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category2, 4, false, 1, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category3, 4, false, 1, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category4, 4, false, 1, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category5, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category6, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category7, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category8, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.CoolDown, 0);
        }

        [TestMethod]
        public void CreateTraining_CircleTrainingLimitedByExerciseAvailabilityAndRepetitionPermission()
        {
            var settings = this.GetTrainingSettings(TrainingMode.CircleTraining, 4);
            this.SetupData(TestExercises.TwoOfEachCategoryWithCategory2Inactive);

            var training = TrainingGenerator.CreateTraining(settings);

            this.AssertLapSeparatorsPresenceAndName(training, true, 2, true);
            this.AssertExercisesOfCategory(training, ExerciseCategory.WarmUp, 2, false);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category1, 2, false, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category2, 0, false);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category3, 2, false, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category4, 2, false, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category5, 2, false, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category6, 2, false, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category7, 2, false, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category8, 2, false, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.CoolDown, 2, false);
        }

        [TestMethod]
        public void CreateTraining_CircleTrainingWithRepeatsNotPreferred()
        {
            var settings = this.GetTrainingSettings(TrainingMode.CircleTraining, 3);
            this.SetupData(TestExercises.TwoOfEachCategory);
            this.Settings.Permission = ExerciseSchedulingRepetitionPermission.NotPreferred;

            var training = TrainingGenerator.CreateTraining(settings);

            this.AssertLapSeparatorsPresenceAndName(training, true, 3, true);
            this.AssertExercisesOfCategory(training, ExerciseCategory.WarmUp, 3, true);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category1, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category2, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category3, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category4, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category5, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category6, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category7, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category8, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.CoolDown, 3, true);
        }

        [TestMethod]
        public void CreateTraining_CircleTrainingWithRepeats()
        {
            var settings = this.GetTrainingSettings(TrainingMode.CircleTraining, 3);
            this.SetupData(TestExercises.TwoOfEachCategory);
            this.Settings.Permission = ExerciseSchedulingRepetitionPermission.Yes;

            var training = TrainingGenerator.CreateTraining(settings);

            this.AssertLapSeparatorsPresenceAndName(training, true, 3, true);
            this.AssertExercisesOfCategory(training, ExerciseCategory.WarmUp, 3, true);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category1, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category2, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category3, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category4, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category5, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category6, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category7, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category8, 3, true, 1, 1, 1);
            this.AssertExercisesOfCategory(training, ExerciseCategory.CoolDown, 3, true);
        }

        [TestMethod]
        [DataRow(ExerciseSchedulingRepetitionPermission.No)]
        [DataRow(ExerciseSchedulingRepetitionPermission.NotPreferred)]
        public void CreateTraining_RandomTrainingWithEnoughExercises_CorrectTotalCountAndNoRepetitions(
            ExerciseSchedulingRepetitionPermission permission)
        {
            // There are just enough exercises .. not a single one more than needed.
            this.SetupData(TestExercises.TwoOfEachCategory);
            this.Categories.Single(c => c.ID == ExerciseCategory.WarmUp).Count = 2;
            this.Categories.Single(c => c.ID == ExerciseCategory.CoolDown).Count = 2;
            this.Settings.Permission = permission;
            var settings = this.GetTrainingSettings(TrainingMode.RandomTraining, 4, 4);

            var training = TrainingGenerator.CreateTraining(settings);

            this.AssertLapSeparatorsPresenceAndName(training, true, 4, true);
            this.AssertExercisesOfCategory(training, ExerciseCategory.WarmUp, 2, false);
            this.AssertThatTrainingExerciseCountIsEqualTo(training, 16);
            this.AssertThatTrainingHasExerciseRepetitions(training, false);
            this.AssertExercisesOfCategory(training, ExerciseCategory.CoolDown, 2, false);
        }

        [TestMethod]
        [DataRow(ExerciseSchedulingRepetitionPermission.No, 15, 0)]
        [DataRow(ExerciseSchedulingRepetitionPermission.NotPreferred, 16, 1)]
        [DataRow(ExerciseSchedulingRepetitionPermission.Yes, 16, 1)]
        public void CreateTraining_RandomTrainingWithNotEnoughExercises_CorrectTotalCountAndRepetitionsDependingOnSettings(
            ExerciseSchedulingRepetitionPermission permission,
            int expectedTrainingExerciseCount,
            int expectedRepetitions)
        {
            // There ist just a single exericse less than needed
            this.SetupData(TestExercises.TwoOfEachCategory);
            this.Categories.Single(c => c.ID == ExerciseCategory.WarmUp).Count = 2;
            this.Categories.Single(c => c.ID == ExerciseCategory.CoolDown).Count = 2;
            this.Exercises.Remove(this.Exercises.First(ex => ex.Category == ExerciseCategory.Category1));
            this.Settings.Permission = permission;
            var settings = this.GetTrainingSettings(TrainingMode.RandomTraining, 4, 4);

            var training = TrainingGenerator.CreateTraining(settings);

            this.AssertLapSeparatorsPresenceAndName(training, true, 4, true);
            this.AssertExercisesOfCategory(training, ExerciseCategory.WarmUp, 2);
            this.AssertThatTrainingExerciseCountIsEqualTo(training, expectedTrainingExerciseCount);
            if (permission != ExerciseSchedulingRepetitionPermission.Yes)
            {
                this.AssertExerciseRepetitionCount(training, expectedRepetitions);
            }
            else
            {
                this.AssertThatTrainingHasExerciseRepetitions(training, true);
            }
            
            this.AssertExercisesOfCategory(training, ExerciseCategory.CoolDown, 2);
        }

        [TestMethod]
        public void CreateTraining_FocusTraining()
        {
            var settings = this.GetTrainingSettings(TrainingMode.FocusTraining, 3, 2, ExerciseCategory.Category1);

            var training = TrainingGenerator.CreateTraining(settings);

            this.AssertLapSeparatorsPresenceAndName(training, false, 3, false);
            this.AssertExercisesOfCategory(training, ExerciseCategory.WarmUp, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category1, 6, false, 2, 2, 2);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category2, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category3, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category4, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category5, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category6, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category7, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category8, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.CoolDown, 0);
        }

        [TestMethod]
        public void CreateTraining_FocusTrainingLimitedByExerciseAvailabilityAndRepetitionPermission()
        {
            var settings = this.GetTrainingSettings(TrainingMode.FocusTraining, 4, 2, ExerciseCategory.WarmUp);
            this.Exercises.Where(e => e.Category == ExerciseCategory.WarmUp).First().Active = false;

            var training = TrainingGenerator.CreateTraining(settings);

            this.AssertLapSeparatorsPresenceAndName(training, false, 3, false);
            this.AssertExercisesOfCategory(training, ExerciseCategory.WarmUp, 5, false, 2, 2, 1, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category1, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category2, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category3, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category4, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category5, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category6, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category7, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category8, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.CoolDown, 0);
        }

        [TestMethod]
        public void CreateTraining_FocusTrainingWithRepeatsNotPreferred()
        {
            var settings = this.GetTrainingSettings(TrainingMode.FocusTraining, 4, 2, ExerciseCategory.CoolDown);
            this.Settings.Permission = ExerciseSchedulingRepetitionPermission.NotPreferred;

            var training = TrainingGenerator.CreateTraining(settings);

            this.AssertLapSeparatorsPresenceAndName(training, false, 4, false);
            this.AssertExercisesOfCategory(training, ExerciseCategory.WarmUp, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category1, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category2, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category3, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category4, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category5, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category6, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category7, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category8, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.CoolDown, 8, true, 2, 2, 2, 2);
        }

        [TestMethod]
        public void CreateTraining_FocusTrainingWithRepeats()
        {
            var settings = this.GetTrainingSettings(TrainingMode.FocusTraining, 4, 2, ExerciseCategory.Category8);
            this.Settings.Permission = ExerciseSchedulingRepetitionPermission.Yes;

            var training = TrainingGenerator.CreateTraining(settings);

            this.AssertLapSeparatorsPresenceAndName(training, false, 4, false);
            this.AssertExercisesOfCategory(training, ExerciseCategory.WarmUp, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category1, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category2, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category3, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category4, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category5, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category6, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category7, 0);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category8, 8, true, 2, 2, 2, 2);
            this.AssertExercisesOfCategory(training, ExerciseCategory.CoolDown, 0);
        }

        [TestMethod]
        public void CreateTraining_MultipliersAreSetCorrectly()
        {
            this.Settings.RepeatsRound1 = 200;
            this.Settings.RepeatsRound2 = 100;
            this.Settings.RepeatsRound3 = 50;
            this.Settings.RepeatsRound4 = 33;
            this.Settings.ScoresRound1 = 300;
            this.Settings.ScoresRound2 = 150;
            this.Settings.ScoresRound3 = 75;
            this.Settings.ScoresRound4 = 40;
            this.Settings.RepeatsAndScoresMultiplier = 150;
            this.SetupData(TestExercises.TwoOfEachCategory);
            this.Exercises.Where(e => e.Category == ExerciseCategory.Category1).Last().Active = false;
            var settings = this.GetTrainingSettings(TrainingMode.FocusTraining, 4, 1, ExerciseCategory.Category1);
            this.Settings.Permission = ExerciseSchedulingRepetitionPermission.Yes;

            var training = TrainingGenerator.CreateTraining(settings);

            this.AssertLapSeparatorsPresenceAndName(training, false, 4, false);
            this.AssertExercisesOfCategory(training, ExerciseCategory.Category1, 4, true, 1, 1, 1, 1);
            var viewModels = training.Where(e => e.Type == TrainingElementType.Exercise).ToList();
            Assert.AreEqual(viewModels[0].RepeatsMultiplier, 3.0);
            Assert.AreEqual(viewModels[1].RepeatsMultiplier, 1.5);
            Assert.AreEqual(viewModels[2].RepeatsMultiplier, 0.75);
            Assert.AreEqual(viewModels[3].RepeatsMultiplier, 0.495);
            Assert.AreEqual(viewModels[0].ScoresMultiplier, 4.5);
            Assert.AreEqual(viewModels[1].ScoresMultiplier, 2.25);
            Assert.AreEqual(viewModels[2].ScoresMultiplier, 1.125);
            Assert.AreEqual(viewModels[3].ScoresMultiplier, 0.6);
        }

        private TrainingSettings GetTrainingSettings(
            TrainingMode mode,
            ushort lapCount,
            ushort exercisesPerLap = 0,
            ExerciseCategory categoryInFocus = ExerciseCategory.Category1)
        {
            this.activeCategoriesForTrainingSettings = new List<ExerciseCategory>()
            {
                ExerciseCategory.WarmUp,
                ExerciseCategory.Category1,
                ExerciseCategory.Category2,
                ExerciseCategory.Category3,
                ExerciseCategory.Category4,
                ExerciseCategory.Category5,
                ExerciseCategory.Category6,
                ExerciseCategory.Category7,
                ExerciseCategory.Category8,
                ExerciseCategory.CoolDown
            };

            return new TrainingSettings(
                mode,
                lapCount,
                exercisesPerLap,
                categoryInFocus,
                activeCategoriesForTrainingSettings);
        }

        private void AssertLapSeparatorsPresenceAndName(
            Training training,
            bool expectedWarmUp,
            uint expectedLapCount,
            bool expectedCoolDown)
        {
            if (expectedWarmUp)
            {
                var warmUpSeparator = training.First();
                Assert.AreEqual(this.Categories.Where(c => c.ID == ExerciseCategory.WarmUp).First().Name, warmUpSeparator.Headline);
            }

            if (expectedCoolDown)
            {
                var coolDownSeparator = training.Where(e => e.Type == TrainingElementType.Headline).Last();
                Assert.AreEqual(this.Categories.Where(c => c.ID == ExerciseCategory.CoolDown).First().Name, coolDownSeparator.Headline);
            }

            var lapCountOffset = expectedWarmUp ? 1 : 0;

            for (int i = lapCountOffset; i < expectedLapCount + lapCountOffset; i++)
            {
                var lapSeparator = training.Where(e => e.Type == TrainingElementType.Headline).ToArray()[i];
                Assert.AreEqual(TrainingElementViewModel.LAPDESIGNATION + " " + (i + 1 - lapCountOffset).ToString(), lapSeparator.Headline);
                
                if (i == expectedLapCount + lapCountOffset - 1 
                    && training.Any(
                        e => e.Type == TrainingElementType.Headline
                        && training.IndexOf(e) > training.IndexOf(lapSeparator) 
                        && e.Headline.StartsWith(TrainingElementViewModel.LAPDESIGNATION)))
                {
                    throw new AssertFailedException("More laps detected that expected.");
                }
            }
        }

        private void AssertExercisesOfCategory(
            Training training,
            ExerciseCategory category,
            int expectedTotalCount,
            bool? repeatsExpected = null,
            int? expectedCountLap1 = null,
            int? expectedCountLap2 = null,
            int? expectedCountLap3 = null,
            int? expectedCountLap4 = null)
        {
            var allExercisesOfCategory = training.Where(e => e.Type == TrainingElementType.Exercise && e.Exercise.Category == category).ToList();

            Assert.AreEqual(expectedTotalCount, allExercisesOfCategory.Count);

            if (repeatsExpected != null)
            {
                var repeatsFound = allExercisesOfCategory.Any(
                    e1 => allExercisesOfCategory.Any(
                        e2 => e2.Exercise.ID == e1.Exercise.ID
                            && ReferenceEquals(e1, e2) == false));

                Assert.AreEqual(repeatsExpected, repeatsFound);
            }

            if (expectedCountLap1 != null)
            {
                this.AssertExerciseCountForCategoryAndLap(training, category, (int)expectedCountLap1, 1);
            }

            if (expectedCountLap2 != null)
            {
                this.AssertExerciseCountForCategoryAndLap(training, category, (int)expectedCountLap2, 2);
            }

            if (expectedCountLap3 != null)
            {
                this.AssertExerciseCountForCategoryAndLap(training, category, (int)expectedCountLap3, 3);
            }

            if (expectedCountLap4 != null)
            {
                this.AssertExerciseCountForCategoryAndLap(training, category, (int)expectedCountLap4, 4);
            }
        }

        private void AssertExerciseCountForCategoryAndLap(
            Training training,
            ExerciseCategory category,
            int expectedCountLap,
            int lap)
        {
            var previousSeparator = training.Where(
                e => e.Type == TrainingElementType.Headline 
                && e.Headline == TrainingElementViewModel.LAPDESIGNATION + " " + lap).FirstOrDefault();

            if (previousSeparator == null)
            {
                if (expectedCountLap == 0)
                {
                    return;
                }
                else
                {
                    throw new AssertFailedException($"LapSeparator not found for lap {lap}");
                }
            }

            var nextSeparator = training.Where(
                e => e.Type == TrainingElementType.Headline 
                && training.IndexOf(e) > training.IndexOf(previousSeparator)).FirstOrDefault();

            var exercisesLap1 = nextSeparator == null
                ? training.Where(e => training.IndexOf(e) > training.IndexOf(previousSeparator)).ToList()
                : training.Where(e => training.IndexOf(e) > training.IndexOf(previousSeparator)
                && training.IndexOf(e) < training.IndexOf(nextSeparator)).ToList();
            var actualCountLap1 = exercisesLap1.Count(e => e is TrainingElementViewModel model && model.Exercise.Category == category);

            Assert.AreEqual(expectedCountLap, actualCountLap1);
        }

        private void AssertThatTrainingExerciseCountIsEqualTo(Training training, int expectedCount)
        {
            Assert.AreEqual(expectedCount, training.Count(
                e => e.Exercise is Exercise ex
                && ex.Category != ExerciseCategory.WarmUp
                && ex.Category != ExerciseCategory.CoolDown));
        }

        private void AssertExerciseRepetitionCount(Training training, int expectedRepetitions)
        {
            int actualRepetitions = 0;
            var bufferTraining = new Training();
            training.ForEach(e => bufferTraining.Add(e));

            foreach (var element in training)
            {
                if (element.Exercise == null)
                {
                    continue;
                }

                var matches = bufferTraining.Where(otherElement =>
                    otherElement.Exercise != null && otherElement.Exercise == element.Exercise).ToList();

                if (matches.Any())
                {
                    matches.ForEach(m => bufferTraining.Remove(m));
                    actualRepetitions += (matches.Count - 1);
                }
            }

            Assert.AreEqual(expectedRepetitions, actualRepetitions);
        }

        private void AssertThatTrainingHasExerciseRepetitions(Training training, bool expected)
        {
            var found = false;

            foreach (var element in training)
            {
                if (element.Exercise == null)
                {
                    continue;
                }

                var repeats = training.Where(otherElement => otherElement != element
                    && otherElement.Exercise != null
                    && otherElement.Exercise == element.Exercise).ToList();

                if (repeats.Any())
                {
                    found = true;
                }
            }

            Assert.AreEqual(expected, found);
        }
    }
}
