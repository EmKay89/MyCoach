﻿using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.DataHandling.DataTransferObjects.CollectionExtensions;
using MyCoach.Defines;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.Events;
using MyCoach.ViewModel.TrainingGenerationAndEvaluation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCoach.ViewModel
{
    public class TrainingViewModel : BaseViewModel
    {
        private Category categoryInFocus;
        private ushort lapCount = 2;
        private ushort exercisesPerLap = 2;
        private TrainingMode trainingMode;
        private string modeExplanation;
        private Training training;

        public TrainingViewModel()
        {
            this.Categories = DataInterface.GetInstance().GetData<Category>();
            this.Categories.CollectionChanged += this.OnCategoriesChanged;
            this.StartTrainingCommand = new RelayCommand(this.StartTraining, this.CanStartTraining);
            this.TrainingMode = TrainingMode.CircleTraining;
            this.Training = new Training();
        }

        public const string DESCRIPTION_CIRCLETRAINING = "In diesem Modus wird pro Runde je eine Übung der als aktiv markierten Kategorien ins Training eingeplant. " +
            "Vor und nach dem eigentlichen Zirkeltraining folgt je ein Block Auf- und Abwärmübungen.";
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
                    nameof(this.CircleOrFocusTrainingElementsVisible));
                this.OnTrainingModeChanged();
            }
        }

        public string ModeExplanation
        {
            get { return this.modeExplanation; }
            set
            {
                if (value == this.modeExplanation)
                {
                    return;
                }

                this.modeExplanation = value;
                this.InvokePropertyChanged();
            }
        }

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
                this.InvokePropertyChanged();
            }
        }

        public string CategoryWarmUpName => this.Categories.GetName(ExerciseCategory.WarmUp);

        public bool CategoryWarmUpActive => this.Categories.IsActive(ExerciseCategory.WarmUp);

        public bool CategoryWarmUpEnabledForTraining { get; set; } = true;

        public string Category1Name => this.Categories.GetName(ExerciseCategory.Category1);

        public bool Category1Active => this.Categories.IsActive(ExerciseCategory.Category1);

        public bool Category1EnabledForTraining { get; set; } = true;

        public string Category2Name => this.Categories.GetName(ExerciseCategory.Category2);

        public bool Category2Active => this.Categories.IsActive(ExerciseCategory.Category2);

        public bool Category2EnabledForTraining { get; set; } = true;

        public string Category3Name => this.Categories.GetName(ExerciseCategory.Category3);

        public bool Category3Active => this.Categories.IsActive(ExerciseCategory.Category3);

        public bool Category3EnabledForTraining { get; set; } = true;

        public string Category4Name => this.Categories.GetName(ExerciseCategory.Category4);

        public bool Category4Active => this.Categories.IsActive(ExerciseCategory.Category4);

        public bool Category4EnabledForTraining { get; set; } = true;

        public string Category5Name => this.Categories.GetName(ExerciseCategory.Category5);

        public bool Category5Active => this.Categories.IsActive(ExerciseCategory.Category5);

        public bool Category5EnabledForTraining { get; set; } = true;

        public string Category6Name => this.Categories.GetName(ExerciseCategory.Category6);

        public bool Category6Active => this.Categories.IsActive(ExerciseCategory.Category6);

        public bool Category6EnabledForTraining { get; set; } = true;

        public string Category7Name => this.Categories.GetName(ExerciseCategory.Category7);

        public bool Category7Active => this.Categories.IsActive(ExerciseCategory.Category7);

        public bool Category7EnabledForTraining { get; set; } = true;

        public string Category8Name => this.Categories.GetName(ExerciseCategory.Category8);

        public bool Category8Active => this.Categories.IsActive(ExerciseCategory.Category8);

        public bool Category8EnabledForTraining { get; set; } = true;

        public string CategoryCoolDownName => this.Categories.GetName(ExerciseCategory.CoolDown);

        public bool CategoryCoolDownActive => this.Categories.IsActive(ExerciseCategory.CoolDown);

        public bool CategoryCoolDownEnabledForTraining { get; set; } = true;

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
                this.InvokePropertyChanged();
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
                this.InvokePropertyChanged();
            }
        }

        public RelayCommand StartTrainingCommand { get; }

        public bool TrainingActive => this.Training?.IsActive == true;

        public bool TrainingSettingsEnabled => !this.TrainingActive;

        public bool CircleTrainingElementsVisible => this.TrainingMode == TrainingMode.CircleTraining;

        public bool FocusTrainingElementsVisible => this.TrainingMode == TrainingMode.FocusTraining;

        public bool CircleOrFocusTrainingElementsVisible => this.TrainingMode != TrainingMode.UserDefinedTraining;

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

        private void OnCategoriesChanged(object sender, EventArgs e)
        {
            if (this.CategoryInFocus != null && this.Categories.Where(c => c.ID == CategoryInFocus.ID && c.Active).Any() == false)
            {
                this.CategoryInFocus = null;
            }

            this.InvokePropertiesChanged(
                nameof(this.ActiveCategories),
                nameof(this.CategoryWarmUpName),
                nameof(this.CategoryWarmUpActive),
                nameof(this.Category1Name),
                nameof(this.Category1Active),
                nameof(this.Category2Name),
                nameof(this.Category2Active),
                nameof(this.Category3Name),
                nameof(this.Category3Active),
                nameof(this.Category4Name),
                nameof(this.Category4Active),
                nameof(this.Category5Name),
                nameof(this.Category5Active),
                nameof(this.Category6Name),
                nameof(this.Category6Active),
                nameof(this.Category7Name),
                nameof(this.Category7Active),
                nameof(this.Category8Name),
                nameof(this.Category8Active),
                nameof(this.CategoryCoolDownName),
                nameof(this.CategoryCoolDownActive)
                );
        }

        private void OnTrainingModeChanged()
        {
            switch (this.TrainingMode)
            {
                case TrainingMode.CircleTraining:
                    this.ModeExplanation = DESCRIPTION_CIRCLETRAINING;
                    break;
                case TrainingMode.FocusTraining:
                    this.ModeExplanation = DESCRIPTION_FOCUSTRAINING;
                    break;
                case TrainingMode.UserDefinedTraining:
                    this.ModeExplanation = DESCRIPTION_USERDEFINEDTRAINING;
                    break;
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
