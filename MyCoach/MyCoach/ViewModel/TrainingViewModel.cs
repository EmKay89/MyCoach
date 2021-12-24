using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.DataHandling.DataTransferObjects.CollectionExtensions;
using MyCoach.Defines;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.Events;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace MyCoach.ViewModel
{
    public class TrainingViewModel : BaseViewModel
    {
        private Category categoryInFocus;
        private ushort lapCount = 2;
        private ushort exercisesPerLap = 2;
        private TrainingMode trainingMode;
        private Training training;
        private bool categoryWarmUpEnabledForTraining = true;
        private bool category1EnabledForTraining = true;
        private bool category2EnabledForTraining = true;
        private bool category3EnabledForTraining = true;
        private bool category4EnabledForTraining = true;
        private bool category5EnabledForTraining = true;
        private bool category6EnabledForTraining = true;
        private bool category7EnabledForTraining = true;
        private bool category8EnabledForTraining = true;
        private bool categoryCoolDownEnabledForTraining = true;

        public TrainingViewModel()
        {
            this.Categories = DataInterface.GetInstance().GetData<Category>();
            this.Categories.CollectionChanged += this.OnCategoriesChanged;
            this.StartTrainingCommand = new RelayCommand(this.StartTraining, this.CanStartTraining);
            this.TrainingMode = TrainingMode.CircleTraining;
            this.Training = new Training();
        }

        public const string DESCRIPTION_FOCUSTRAINING = "In diesem Modus werden nur Übungen der ausgewählten Kategorie ins Training eingeplant.";
        public const string DESCRIPTION_USERDEFINEDTRAINING = "In diesem Modus kann ein Training selbst aus Übungen aus dem gleichnamigen Menü auf der linken Seite " +
            "zusammengestellt werden.";

        public List<Category> ActiveCategories => this.Categories.Where(c => c.Active).ToList();

        public ObservableCollection<Category> Categories { get; }

        public ObservableCollection<ushort> NumbersOneToTen { get; } = new ObservableCollection<ushort> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public ObservableCollection<ushort> NumbersOneToFour { get; } = new ObservableCollection<ushort> { 1, 2, 3, 4 };

        public Dictionary<TrainingMode, string> ModesWithCaption { get; } = new Dictionary<TrainingMode, string>
        {
            { TrainingMode.CircleTraining, "Zirkeltraining" },
            { TrainingMode.FocusTraining, "Fokustraining" },
            { TrainingMode.UserDefinedTraining, "Benutzerdefiniertes Training" }
        };

        public Training Training
        {
            get => this.training;

            private set
            {
                if (value == this.training)
                {
                    return;
                }

                this.training = value;
                this.training.TrainingActiveChanged += this.OnTrainingActiveChanged;
                this.InvokePropertyChanged();
            }
        }

        public TrainingMode TrainingMode
        {
            get { return this.trainingMode; }
            set
            {
                if (value == this.trainingMode)
                {
                    return;
                }

                this.trainingMode = value;
                this.InvokePropertiesChanged(
                    nameof(this.TrainingMode),
                    nameof(this.CircleTrainingElementsVisible),
                    nameof(this.FocusTrainingElementsVisible),
                    nameof(this.CircleOrFocusTrainingElementsVisible),
                    nameof(this.NotEnoughExercisesAvailable),
                    nameof(this.ModeExplanation));
            }
        }

        public string ModeExplanation => GetTrainingModeExplanation();

        public Category CategoryInFocus
        {
            get { return this.categoryInFocus; }
            set
            {
                if (value == this.categoryInFocus)
                {
                    return;
                }

                this.categoryInFocus = value;
                this.InvokePropertiesChanged(
                    nameof(this.CategoryInFocus),
                    nameof(NotEnoughExercisesAvailable));
            }
        }

        public string CategoryWarmUpName => this.Categories.GetName(ExerciseCategory.WarmUp);

        public bool CategoryWarmUpActive => this.Categories.IsActive(ExerciseCategory.WarmUp);

        public bool CategoryWarmUpEnabledForTraining
        {
            get => this.categoryWarmUpEnabledForTraining;

            set
            {
                if (value == this.categoryWarmUpEnabledForTraining)
                {
                    return;
                }

                this.categoryWarmUpEnabledForTraining = value;
                this.InvokePropertiesChanged(
                    nameof(this.CategoryWarmUpEnabledForTraining),
                    nameof(this.NotEnoughExercisesAvailable));
            }
        }

        public string Category1Name => this.Categories.GetName(ExerciseCategory.Category1);

        public bool Category1Active => this.Categories.IsActive(ExerciseCategory.Category1);

        public bool Category1EnabledForTraining
        {
            get => this.category1EnabledForTraining;

            set
            {
                if (value == this.category1EnabledForTraining)
                {
                    return;
                }

                this.category1EnabledForTraining = value;
                this.InvokePropertiesChanged(
                    nameof(this.Category1EnabledForTraining),
                    nameof(this.NotEnoughExercisesAvailable));
            }
        }

        public string Category2Name => this.Categories.GetName(ExerciseCategory.Category2);

        public bool Category2Active => this.Categories.IsActive(ExerciseCategory.Category2);

        public bool Category2EnabledForTraining
        {
            get => this.category2EnabledForTraining;

            set
            {
                if (value == this.category2EnabledForTraining)
                {
                    return;
                }

                this.category2EnabledForTraining = value;
                this.InvokePropertiesChanged(
                    nameof(this.Category2EnabledForTraining),
                    nameof(this.NotEnoughExercisesAvailable));
            }
        }

        public string Category3Name => this.Categories.GetName(ExerciseCategory.Category3);

        public bool Category3Active => this.Categories.IsActive(ExerciseCategory.Category3);

        public bool Category3EnabledForTraining
        {
            get => this.category3EnabledForTraining;

            set
            {
                if (value == this.category3EnabledForTraining)
                {
                    return;
                }

                this.category3EnabledForTraining = value;
                this.InvokePropertiesChanged(
                    nameof(this.Category3EnabledForTraining),
                    nameof(this.NotEnoughExercisesAvailable));
            }
        }

        public string Category4Name => this.Categories.GetName(ExerciseCategory.Category4);

        public bool Category4Active => this.Categories.IsActive(ExerciseCategory.Category4);

        public bool Category4EnabledForTraining
        {
            get => this.category4EnabledForTraining;

            set
            {
                if (value == this.category4EnabledForTraining)
                {
                    return;
                }

                this.category4EnabledForTraining = value;
                this.InvokePropertiesChanged(
                    nameof(this.Category4EnabledForTraining),
                    nameof(this.NotEnoughExercisesAvailable));
            }
        }

        public string Category5Name => this.Categories.GetName(ExerciseCategory.Category5);

        public bool Category5Active => this.Categories.IsActive(ExerciseCategory.Category5);

        public bool Category5EnabledForTraining
        {
            get => this.category5EnabledForTraining;

            set
            {
                if (value == this.category5EnabledForTraining)
                {
                    return;
                }

                this.category5EnabledForTraining = value;
                this.InvokePropertiesChanged(
                    nameof(this.Category5EnabledForTraining),
                    nameof(this.NotEnoughExercisesAvailable));
            }
        }

        public string Category6Name => this.Categories.GetName(ExerciseCategory.Category6);

        public bool Category6Active => this.Categories.IsActive(ExerciseCategory.Category6);

        public bool Category6EnabledForTraining
        {
            get => this.category6EnabledForTraining;

            set
            {
                if (value == this.category6EnabledForTraining)
                {
                    return;
                }

                this.category6EnabledForTraining = value;
                this.InvokePropertiesChanged(
                    nameof(this.Category6EnabledForTraining),
                    nameof(this.NotEnoughExercisesAvailable));
            }
        }

        public string Category7Name => this.Categories.GetName(ExerciseCategory.Category7);

        public bool Category7Active => this.Categories.IsActive(ExerciseCategory.Category7);

        public bool Category7EnabledForTraining
        {
            get => this.category7EnabledForTraining;

            set
            {
                if (value == this.category7EnabledForTraining)
                {
                    return;
                }

                this.category7EnabledForTraining = value;
                this.InvokePropertiesChanged(
                    nameof(this.Category7EnabledForTraining),
                    nameof(this.NotEnoughExercisesAvailable));
            }
        }

        public string Category8Name => this.Categories.GetName(ExerciseCategory.Category8);

        public bool Category8Active => this.Categories.IsActive(ExerciseCategory.Category8);

        public bool Category8EnabledForTraining
        {
            get => this.category8EnabledForTraining;

            set
            {
                if (value == this.category8EnabledForTraining)
                {
                    return;
                }

                this.category8EnabledForTraining = value;
                this.InvokePropertiesChanged(
                    nameof(this.Category8EnabledForTraining),
                    nameof(this.NotEnoughExercisesAvailable));
            }
        }

        public string CategoryCoolDownName => this.Categories.GetName(ExerciseCategory.CoolDown);

        public bool CategoryCoolDownActive => this.Categories.IsActive(ExerciseCategory.CoolDown);

        public bool CategoryCoolDownEnabledForTraining
        {
            get => this.categoryCoolDownEnabledForTraining;

            set
            {
                if (value == this.categoryCoolDownEnabledForTraining)
                {
                    return;
                }

                this.categoryCoolDownEnabledForTraining = value;
                this.InvokePropertiesChanged(
                    nameof(this.CategoryCoolDownEnabledForTraining),
                    nameof(this.NotEnoughExercisesAvailable));
            }
        }

        public ushort LapCount
        {
            get => this.lapCount;
            set
            {
                if (this.lapCount == value)
                {
                    return;
                }

                this.lapCount = value;
                this.InvokePropertiesChanged(
                    nameof(this.LapCount),
                    nameof(this.NotEnoughExercisesAvailable));
            }
        }

        public ushort ExercisesPerLap
        {
            get => this.exercisesPerLap;
            set
            {
                if (this.exercisesPerLap == value)
                {
                    return;
                }

                this.exercisesPerLap = value;
                this.InvokePropertiesChanged(
                    nameof(this.ExercisesPerLap),
                    nameof(this.NotEnoughExercisesAvailable));
            }
        }

        public RelayCommand StartTrainingCommand { get; }

        public bool TrainingActive => this.Training?.IsActive == true;

        public bool TrainingSettingsEnabled => !this.TrainingActive;

        public bool CircleTrainingElementsVisible => this.TrainingMode == TrainingMode.CircleTraining;

        public bool FocusTrainingElementsVisible => this.TrainingMode == TrainingMode.FocusTraining;

        public bool CircleOrFocusTrainingElementsVisible => this.TrainingMode != TrainingMode.UserDefinedTraining;

        public bool NotEnoughExercisesAvailable => this.AreEnoughExercisesAvailable() == false;

        public void OnAddExerciseExecuted(object sender, AddExerciseExecutedEventArgs args)
        {
            var vm = new TrainingElementViewModel(TrainingElementType.exercise, args.Exercise);
            this.Training.Add(vm);
        }

        private void StartTraining()
        {
            if (this.TrainingActive)
            {
                this.Training.Finish();
                return;
            }

            if (this.Training.Any() == false)
            {
                this.Training = TrainingGenerator.CreateTraining(
                    new TrainingSettings(
                        this.TrainingMode,
                        this.LapCount,
                        this.ExercisesPerLap,
                        this.CategoryInFocus?.ID ?? default,
                        this.GetCategoriesEnabledForTraining()));
            }

            this.Training.Start();
        }

        private bool CanStartTraining()
        {
            if (this.Training.Count > 0)
            {
                return true;
            }

            if (this.TrainingMode == TrainingMode.CircleTraining && this.GetCategoriesEnabledForTraining().Any())
            {
                return true;
            }

            if (this.TrainingMode == TrainingMode.FocusTraining && this.CategoryInFocus != null)
            {
                return true;
            }

            return false;
        }

        private List<ExerciseCategory> GetCategoriesEnabledForTraining()
        {
            var categories = new List<ExerciseCategory>();

            if (this.CategoryWarmUpActive && this.CategoryWarmUpEnabledForTraining)
            {
                categories.Add(ExerciseCategory.WarmUp);
            }

            if (this.Category1Active && this.Category1EnabledForTraining)
            {
                categories.Add(ExerciseCategory.Category1);
            }

            if (this.Category2Active && this.Category2EnabledForTraining)
            {
                categories.Add(ExerciseCategory.Category2);
            }

            if (this.Category3Active && this.Category3EnabledForTraining)
            {
                categories.Add(ExerciseCategory.Category3);
            }

            if (this.Category4Active && this.Category4EnabledForTraining)
            {
                categories.Add(ExerciseCategory.Category4);
            }

            if (this.Category5Active && this.Category5EnabledForTraining)
            {
                categories.Add(ExerciseCategory.Category5);
            }

            if (this.Category6Active && this.Category6EnabledForTraining)
            {
                categories.Add(ExerciseCategory.Category6);
            }

            if (this.Category7Active && this.Category7EnabledForTraining)
            {
                categories.Add(ExerciseCategory.Category7);
            }

            if (this.Category8Active && this.Category8EnabledForTraining)
            {
                categories.Add(ExerciseCategory.Category8);
            }

            if (this.CategoryCoolDownActive && this.CategoryCoolDownEnabledForTraining)
            {
                categories.Add(ExerciseCategory.CoolDown);
            }

            return categories;
        }

        private bool AreEnoughExercisesAvailable()
        {
            var permission = DataInterface.GetInstance().GetData<Settings>().Single().Permission;
            var exercises = DataInterface.GetInstance().GetData<Exercise>().Where(e => e.Active).ToList();

            switch (this.TrainingMode)
            {
                case TrainingMode.CircleTraining:
                    return AreEnoughExercisesAvailableForCircleTraining(permission, exercises);

                case TrainingMode.FocusTraining:
                    return AreEnoughExercisesAvailableForFocusTraining(permission, exercises);

                case TrainingMode.UserDefinedTraining:
                default:
                    return true;
            }
        }

        private bool AreEnoughExercisesAvailableForCircleTraining(
            ExerciseSchedulingRepetitionPermission permission,
            List<Exercise> exercises)
        {
            foreach (var category in GetCategoriesEnabledForTraining())
            {
                int compare;

                if ((category == ExerciseCategory.WarmUp || category == ExerciseCategory.CoolDown))
                {
                    compare = this.Categories.Single(c => c.ID == category).Count;
                }
                else if (permission == ExerciseSchedulingRepetitionPermission.No)
                {
                    compare = this.LapCount;
                }
                else
                {
                    compare = 1;
                }

                if (exercises.Count(e => e.Category == category) < compare)
                {
                    return false;
                }
            }

            return true;
        }

        private bool AreEnoughExercisesAvailableForFocusTraining(ExerciseSchedulingRepetitionPermission permission, List<Exercise> exercises)
        {
            if (this.CategoryInFocus == null)
            {
                return true;
            }

            return permission == ExerciseSchedulingRepetitionPermission.No
                ? exercises.Count(e => e.Category == this.CategoryInFocus.ID) >= (this.LapCount * this.ExercisesPerLap)
                : exercises.Any(e => e.Category == this.CategoryInFocus.ID);
        }

        private string GetTrainingModeExplanation()
        {
            switch (this.TrainingMode)
            {
                case TrainingMode.CircleTraining:
                    var warmUp = this.Categories.Where(c => c.Type == ExerciseType.WarmUp).FirstOrDefault();
                    var coolDown = this.Categories.Where(c => c.Type == ExerciseType.CoolDown).FirstOrDefault();

                    var sb = new StringBuilder("In diesem Modus wird pro Runde je eine Übung aller aktiven Kategorien ins Training eingeplant.");

                    if (warmUp?.Active == true)
                    {
                        sb.Append($" Vor dem eigentlichen Zirkeltraining steht ein Block {warmUp}");
                    }

                    if (coolDown?.Active == true && warmUp?.Active == true)
                    {
                        sb.Append(" und danach");
                    }

                    if (coolDown?.Active == true && warmUp?.Active != true)
                    {
                        sb.Append(" Nach dem Zirkeltraining");
                    }

                    if (coolDown?.Active == true)
                    {
                        sb.Append($" folgt ein Block {coolDown}.");
                    }

                    return sb.ToString();
                case TrainingMode.FocusTraining:
                    return DESCRIPTION_FOCUSTRAINING;
                case TrainingMode.UserDefinedTraining:
                    return DESCRIPTION_USERDEFINEDTRAINING;
                default:
                    return string.Empty;
            }
        }

        private void OnCategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.CategoryInFocus != null && this.Categories.Where(c => c.ID == CategoryInFocus.ID && c.Active).Any() == false)
            {
                this.CategoryInFocus = null;
            }
        }

        private void OnTrainingActiveChanged(object sender, EventArgs e)
        {
            this.InvokePropertiesChanged(
                nameof(this.TrainingActive),
                nameof(this.TrainingSettingsEnabled));
        }
    }
}
