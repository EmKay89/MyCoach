using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling.DataManager;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel;
using MyCoach.ViewModel.Services;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using System.Linq;
using System.Windows;

namespace MyCoachTests.ViewModel.TrainingGenerationAndEvaluation
{
    [TestClass]
    public class TrainingTests : DataInterfaceTestBase
    {
        private Training sut;
        private Exercise ex1;
        private Exercise ex2;
        private Exercise ex3;
        private Exercise ex4;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.sut = new Training();
            this.SetupData<Category>(TestCategories.AllCategoriesActive);
            this.SetupData<Exercise>(TestExercises.SixOfEachCategory);
            this.Categories.Single(c => c.ID == ExerciseCategory.Category1).Active = false;
            this.Categories.Remove(this.Categories.Single(c => c.ID == ExerciseCategory.Category2));

            this.ex1 = this.Exercises.Single(e => e.ID == 1 && e.Category == ExerciseCategory.WarmUp);
            this.ex2 = this.Exercises.Single(e => e.ID == 1 && e.Category == ExerciseCategory.Category1);
            this.ex3 = this.Exercises.Single(e => e.ID == 1 && e.Category == ExerciseCategory.Category2);
            this.ex4 = this.Exercises.Single(e => e.ID == 1 && e.Category == ExerciseCategory.Category3);

            this.sut.Add(new TrainingElementViewModel(TrainingElementType.Headline, null));
            this.sut[0].Headline = "Lap1";
            this.sut.Add(new TrainingElementViewModel(TrainingElementType.Exercise, ex1));
            this.sut.Add(new TrainingElementViewModel(TrainingElementType.Exercise, ex2));
            this.sut.Add(new TrainingElementViewModel(TrainingElementType.Exercise, ex3));
            this.sut.Add(new TrainingElementViewModel(TrainingElementType.Exercise, ex4));

            TrainingEvaluator.MessageBoxService = Mock.Of<IMessageBoxService>(service =>
                service.ShowMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>()) == MessageBoxResult.OK);
        }

        [TestCleanup]
        public void Cleanup()
        {
            base.CleanupTestBase();
        }

        [TestMethod]
        public void AddElements_VariousKinds_TextsForDisplayAreSetCorrectly()
        {
            Assert.AreEqual("Lap1", this.sut[0].Headline);
            Assert.AreEqual($"{ex1.Count} {ex1.Unit} {ex1.Name}", this.sut[1].NameAndRepeats);
            Assert.AreEqual(string.Empty, this.sut[1].ScoresForCategory);
            Assert.AreEqual($"{ex2.Count} {ex2.Unit} {ex2.Name}", this.sut[2].NameAndRepeats);
            Assert.AreEqual(string.Empty, this.sut[2].ScoresForCategory);
            Assert.AreEqual($"{ex3.Count} {ex3.Unit} {ex3.Name}", this.sut[3].NameAndRepeats);
            Assert.AreEqual(string.Empty, this.sut[3].ScoresForCategory);
            Assert.AreEqual($"{ex4.Count} {ex4.Unit} {ex4.Name}", this.sut[4].NameAndRepeats);
            Assert.AreEqual($" --- {ex4.Scores} Punkte für Kategorie " +
                $"'{this.Categories.Single(c => c.ID == ExerciseCategory.Category3).Name}'", this.sut[4].ScoresForCategory);
        }

        [TestMethod]
        public void Start_SetsIsActiveToTrueAndActivatesAllExercises()
        {
            Assert.IsTrue(this.sut[1].EditingAllowed);
            Assert.IsFalse(this.sut[1].CompletionAllowed);
            Assert.IsTrue(this.sut[2].EditingAllowed);
            Assert.IsFalse(this.sut[2].CompletionAllowed);
            Assert.IsTrue(this.sut[3].EditingAllowed);
            Assert.IsFalse(this.sut[3].CompletionAllowed);
            Assert.IsTrue(this.sut[4].EditingAllowed);
            Assert.IsFalse(this.sut[4].CompletionAllowed);

            this.sut.Start();

            Assert.IsTrue(this.sut.IsActive);
            Assert.IsFalse(this.sut[1].EditingAllowed);
            Assert.IsTrue(this.sut[1].CompletionAllowed);
            Assert.IsFalse(this.sut[2].EditingAllowed);
            Assert.IsTrue(this.sut[2].CompletionAllowed);
            Assert.IsFalse(this.sut[3].EditingAllowed);
            Assert.IsTrue(this.sut[3].CompletionAllowed);
            Assert.IsFalse(this.sut[4].EditingAllowed);
            Assert.IsTrue(this.sut[4].CompletionAllowed);
        }

        [TestMethod]
        public void Finish_SetsIsActiveToFalseAndClearsElements()
        {
            this.sut.Start();
            this.sut.Finish();

            Assert.IsFalse(this.sut.IsActive);
            Assert.AreEqual(0, this.sut.Count);
        }
    }
}
