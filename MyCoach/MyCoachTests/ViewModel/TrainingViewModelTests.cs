using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using MyCoach.ViewModel;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using MyExtensions.IEnumerable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class TrainingViewModelTests : ViewModelTestBase
    {
        #region Initialization and Cleanup

        TrainingViewModel sut;

        [TestInitialize]
        public void Init()
        {
            base.Initialize();
            this.sut = new TrainingViewModel();
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
        public void Construction_TrainingInitialized()
        {
            Assert.IsNotNull(this.sut.Training);
            Assert.AreEqual(0, this.sut.Training.Count);
        }

        [TestMethod]
        public void Construction_ActiveCategoriesSetupCorrectly()
        {
            foreach (var category in this.Categories)
            {
                if (category.Active)
                {
                    this.sut.ActiveCategories.Any(c => c == category);
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
            this.sut.TrainingMode = MyCoach.Defines.TrainingMode.UserDefinedTraining;
            this.PropertyChangedEvents.Clear();

            this.sut.TrainingMode = MyCoach.Defines.TrainingMode.CircleTraining;

            Assert.IsTrue(this.sut.CircleTrainingElementsVisible);
            Assert.IsFalse(this.sut.FocusTrainingElementsVisible);
            Assert.IsTrue(this.sut.CircleOrFocusTrainingElementsVisible);
            Assert.AreEqual(TrainingViewModel.DESCRIPTION_CIRCLETRAINING, this.sut.ModeExplanation);

            Assert.AreEqual(5, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.TrainingMode), this.PropertyChangedEvents[0]);
            Assert.AreEqual(nameof(this.sut.CircleTrainingElementsVisible), this.PropertyChangedEvents[1]);
            Assert.AreEqual(nameof(this.sut.FocusTrainingElementsVisible), this.PropertyChangedEvents[2]);
            Assert.AreEqual(nameof(this.sut.CircleOrFocusTrainingElementsVisible), this.PropertyChangedEvents[3]);
            Assert.AreEqual(nameof(this.sut.ModeExplanation), this.PropertyChangedEvents[4]);
        }

        [TestMethod]
        public void ModeChanges_ToFocusTraining_CorrectTrainingSettingsVisibility()
        {
            this.sut.TrainingMode = MyCoach.Defines.TrainingMode.CircleTraining;
            this.PropertyChangedEvents.Clear();

            this.sut.TrainingMode = MyCoach.Defines.TrainingMode.FocusTraining;

            Assert.IsFalse(this.sut.CircleTrainingElementsVisible);
            Assert.IsTrue(this.sut.FocusTrainingElementsVisible);
            Assert.IsTrue(this.sut.CircleOrFocusTrainingElementsVisible);
            Assert.AreEqual(TrainingViewModel.DESCRIPTION_FOCUSTRAINING, this.sut.ModeExplanation);

            Assert.AreEqual(5, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.TrainingMode), this.PropertyChangedEvents[0]);
            Assert.AreEqual(nameof(this.sut.CircleTrainingElementsVisible), this.PropertyChangedEvents[1]);
            Assert.AreEqual(nameof(this.sut.FocusTrainingElementsVisible), this.PropertyChangedEvents[2]);
            Assert.AreEqual(nameof(this.sut.CircleOrFocusTrainingElementsVisible), this.PropertyChangedEvents[3]);
            Assert.AreEqual(nameof(this.sut.ModeExplanation), this.PropertyChangedEvents[4]);
        }

        [TestMethod]
        public void ModeChanges_ToUserDefinedTraining_CorrectTrainingSettingsVisibility()
        {
            this.sut.TrainingMode = MyCoach.Defines.TrainingMode.FocusTraining;
            this.PropertyChangedEvents.Clear();

            this.sut.TrainingMode = MyCoach.Defines.TrainingMode.UserDefinedTraining;

            Assert.IsFalse(this.sut.CircleTrainingElementsVisible);
            Assert.IsFalse(this.sut.FocusTrainingElementsVisible);
            Assert.IsFalse(this.sut.CircleOrFocusTrainingElementsVisible);
            Assert.AreEqual(TrainingViewModel.DESCRIPTION_USERDEFINEDTRAINING, this.sut.ModeExplanation);

            Assert.AreEqual(5, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.TrainingMode), this.PropertyChangedEvents[0]);
            Assert.AreEqual(nameof(this.sut.CircleTrainingElementsVisible), this.PropertyChangedEvents[1]);
            Assert.AreEqual(nameof(this.sut.FocusTrainingElementsVisible), this.PropertyChangedEvents[2]);
            Assert.AreEqual(nameof(this.sut.CircleOrFocusTrainingElementsVisible), this.PropertyChangedEvents[3]);
            Assert.AreEqual(nameof(this.sut.ModeExplanation), this.PropertyChangedEvents[4]);
        }

        #endregion

        #region Command Tests

        [TestMethod]
        public void StartCommandCanExecute_CircleTraining_True()
        {            
            this.Categories.Where(c => c.ID == ExerciseCategory.Category1).FirstOrDefault().Active = true;
            this.sut.Category1EnabledForTraining = true;
            this.sut.TrainingMode = TrainingMode.CircleTraining;

            Assert.IsTrue(this.sut.StartTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartCommandCanExecute_CircleTrainingWithoutEnabledCategories_False()
        {            
            this.Categories.Foreach(c => c.Active = false);
            this.sut.TrainingMode = TrainingMode.CircleTraining;

            Assert.IsFalse(this.sut.StartTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartCommandCanExecute_FocusTraining_True()
        {
            this.sut.CategoryInFocus = this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First();
            this.sut.TrainingMode = TrainingMode.FocusTraining;

            Assert.IsTrue(this.sut.StartTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartCommandCanExecute_FocusTrainingWihtoutCategoryInFocus_False()
        {
            this.sut.CategoryInFocus = null;
            this.sut.TrainingMode = TrainingMode.FocusTraining;

            Assert.IsFalse(this.sut.StartTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartCommandCanExecute_UserDefinedTrainingWithExercises_True()
        {
            var exercise = new Exercise();
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.exercise, exercise));
            this.sut.TrainingMode = TrainingMode.UserDefinedTraining;

            Assert.IsTrue(this.sut.StartTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartCommandCanExecute_UserDefinedTrainingWithoutExercises_False()
        {
            this.sut.Training.Clear();
            this.sut.TrainingMode = TrainingMode.UserDefinedTraining;

            Assert.IsFalse(this.sut.StartTrainingCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartCommandExecute_ActivatesAndDeactivatesTraining()
        {
            var exercise = new Exercise();
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.exercise, exercise));
            this.sut.TrainingMode = TrainingMode.UserDefinedTraining;
            this.PropertyChangedEvents.Clear();

            Assert.IsFalse(this.sut.TrainingActive);

            this.sut.StartTrainingCommand.Execute(null);

            Assert.AreEqual(2, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.TrainingActive), this.PropertyChangedEvents[0]);
            Assert.AreEqual(nameof(this.sut.TrainingSettingsEnabled), this.PropertyChangedEvents[1]);
            Assert.IsTrue(this.sut.TrainingActive);

            this.sut.StartTrainingCommand.Execute(null);

            Assert.IsFalse(this.sut.TrainingActive);
        }

        #endregion

        #region Event Reactions

        [TestMethod]
        public void CategoryChanges_RemovedAndAdded_PropertyChangedEventsRaisedAndCategoriesAndCategoryNamesDisplayedCorrectly()
        {
            var cat1 = this.Categories.Where(c => c.ID == ExerciseCategory.Category1).First();
            this.Categories.Remove(cat1);

            this.AssertThatCategoryChangeEventsAreRaisedAfterCollectionChange();
            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();

            this.PropertyChangedEvents.Clear();
            this.Categories.Add(cat1);

            this.AssertThatCategoryChangeEventsAreRaisedAfterCollectionChange();
            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();
        }

        [TestMethod]
        public void CategoryChanges_Name_CategoriesAndCategoryNamesDisplayedCorrectly()
        {
            this.Categories.Foreach(c => c.Name = string.Concat(c.Name, "_Test"));
            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();
        }

        [TestMethod]
        public void CategoryChanges_Active_CategoriesAndCategoryNamesDisplayedCorrectly()
        {
            this.Categories.Foreach(c => c.Active = !c.Active);
            this.AssertThatCategoryNamesAndActivitiyValuesAreCorrect();
        }

        [TestMethod]
        public void TrainingExercises_AllCompleted_TrainingDeactivated()
        {
            // Actually belongs to a separate unit test class for Training class ... 
            var exercise1 = new Exercise() { Name = "Test1" };
            var exercise2 = new Exercise() { Name = "Test2" }; ;
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.exercise, exercise1));
            this.sut.Training.Add(new TrainingElementViewModel(TrainingElementType.exercise, exercise2));
            this.sut.StartTrainingCommand.Execute(null);

            (this.sut.Training.First() as TrainingElementViewModel).Completed = true;

            Assert.IsTrue(this.sut.TrainingActive);

            (this.sut.Training.Last() as TrainingElementViewModel).Completed = true;

            Assert.IsFalse(this.sut.TrainingActive);
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

        private void AssertThatCategoryChangeEventsAreRaisedAfterCollectionChange()
        {
            Assert.AreEqual(21, this.PropertyChangedEvents.Count);
            Assert.AreEqual(nameof(this.sut.ActiveCategories), this.PropertyChangedEvents[0]);
            Assert.AreEqual(nameof(this.sut.CategoryWarmUpName), this.PropertyChangedEvents[1]);
            Assert.AreEqual(nameof(this.sut.CategoryWarmUpActive), this.PropertyChangedEvents[2]);
            Assert.AreEqual(nameof(this.sut.Category1Name), this.PropertyChangedEvents[3]);
            Assert.AreEqual(nameof(this.sut.Category1Active), this.PropertyChangedEvents[4]);
            Assert.AreEqual(nameof(this.sut.Category2Name), this.PropertyChangedEvents[5]);
            Assert.AreEqual(nameof(this.sut.Category2Active), this.PropertyChangedEvents[6]);
            Assert.AreEqual(nameof(this.sut.Category3Name), this.PropertyChangedEvents[7]);
            Assert.AreEqual(nameof(this.sut.Category3Active), this.PropertyChangedEvents[8]);
            Assert.AreEqual(nameof(this.sut.Category4Name), this.PropertyChangedEvents[9]);
            Assert.AreEqual(nameof(this.sut.Category4Active), this.PropertyChangedEvents[10]);
            Assert.AreEqual(nameof(this.sut.Category5Name), this.PropertyChangedEvents[11]);
            Assert.AreEqual(nameof(this.sut.Category5Active), this.PropertyChangedEvents[12]);
            Assert.AreEqual(nameof(this.sut.Category6Name), this.PropertyChangedEvents[13]);
            Assert.AreEqual(nameof(this.sut.Category6Active), this.PropertyChangedEvents[14]);
            Assert.AreEqual(nameof(this.sut.Category7Name), this.PropertyChangedEvents[15]);
            Assert.AreEqual(nameof(this.sut.Category7Active), this.PropertyChangedEvents[16]);
            Assert.AreEqual(nameof(this.sut.Category8Name), this.PropertyChangedEvents[17]);
            Assert.AreEqual(nameof(this.sut.Category8Active), this.PropertyChangedEvents[18]);
            Assert.AreEqual(nameof(this.sut.CategoryCoolDownName), this.PropertyChangedEvents[19]);
            Assert.AreEqual(nameof(this.sut.CategoryCoolDownActive), this.PropertyChangedEvents[20]);
        }

        #endregion
    }
}
