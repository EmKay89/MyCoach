using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel.Events;
using MyExtensions.IEnumerable;
using MyMvvm.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace MyCoach.ViewModel
{
    public class ExerciseViewModel : BaseViewModel
    {
        private readonly Exercise exercise;

        public ExerciseViewModel(Exercise exercise)
        {
            this.exercise = exercise;
            this.AddExerciseToTrainingCommand = new RelayCommand(this.InvokeAddExerciseToTrainingExecuted);
            this.RemoveExerciseCommand = new RelayCommand(this.InvokeDeleteExerciseExecuted);
            this.exercise.PropertyChanged += this.InvokeExerciseChanged;
            this.SelectableUnits = DataInterface.GetInstance().GetData<Settings>().Single().Units;
            this.Categories = DataInterface.GetInstance().GetData<Category>();
        }

        public event ExerciseEventHandler AddExerciseToTrainingExecuted;
        public event ExerciseEventHandler DeleteExerciseExecuted;
        public event ExerciseEventHandler ExerciseChanged;

        public ObservableCollection<string> SelectableUnits { get; } = new ObservableCollection<string>();

        public ObservableCollection<Category> Categories = new ObservableCollection<Category>();

        public Dictionary<ExerciseCategory, string> ActiveCategories
        {
            get
            {
                var dictionary = new Dictionary<ExerciseCategory, string>();
                this.Categories.ForEach(c =>
                {
                    if (c.Active)
                    {
                        dictionary.Add(c.ID, c.Name);
                    }
                });

                return dictionary;
            }
        }

        public Exercise Exercise => this.exercise;

        public RelayCommand AddExerciseToTrainingCommand { get; }

        public RelayCommand RemoveExerciseCommand { get; }

        public bool Active
        {
            get => this.exercise.Active;

            set
            {
                if (this.exercise.Active == value)
                {
                    return;
                }

                this.exercise.Active = value;
                this.InvokePropertyChanged();
            }
        }

        public ExerciseCategory? Category
        {
            get => this.exercise.Category;

            set
            {
                if (this.exercise.Category == value)
                {
                    return;
                }

                this.exercise.Category = value;

                if (this.IsTrainingExercise == false)
                {
                    this.Scores = 0;
                }

                this.InvokePropertiesChanged(
                    nameof(this.Category), 
                    nameof(this.IsTrainingExercise));
            }
        }

        public ushort Count
        {
            get => this.exercise.Count;

            set
            {
                if (this.exercise.Count == value)
                {
                    return;
                }

                this.exercise.Count = value;
                this.InvokePropertyChanged();
            }
        }

        public string Info
        {
            get => this.exercise.Info;

            set
            {
                if (this.exercise.Info == value)
                {
                    return;
                }

                this.exercise.Info = value;
                this.InvokePropertyChanged();
            }
        }

        public string Name
        {
            get => this.exercise.Name;

            set
            {
                if (this.exercise.Name == value)
                {
                    return;
                }

                this.exercise.Name = value;
                this.InvokePropertyChanged();
            }
        }

        public string Unit
        {
            get => this.exercise.Unit;

            set
            {
                if (this.exercise.Unit == value)
                {
                    return;
                }

                this.exercise.Unit = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Scores
        {
            get => this.exercise.Scores;

            set
            {
                if (this.exercise.Scores == value)
                {
                    return;
                }

                this.exercise.Scores = value;
                this.InvokePropertyChanged();
            }
        }

        public bool IsTrainingExercise => this.Category != null 
            && this.Category != ExerciseCategory.WarmUp
            && this.Category != ExerciseCategory.CoolDown;

        private void InvokeAddExerciseToTrainingExecuted(object parameter)
        {
            if (parameter is Exercise exercise)
            {
                // Use a copy so further changes or possible nulling won't affect the exercise in training.
                var copy = new Exercise();
                exercise.CopyValuesTo(copy);
                this.AddExerciseToTrainingExecuted?.Invoke(this, new ExerciseEventArgs(copy));
            }
        }

        private void InvokeDeleteExerciseExecuted(object parameter)
        {
            if (parameter is Exercise exercise)
            {
                this.DeleteExerciseExecuted?.Invoke(this, new ExerciseEventArgs(exercise));
            }
        }

        private void InvokeExerciseChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Exercise exercise)
            {
                this.ExerciseChanged?.Invoke(this, new ExerciseEventArgs(exercise));
            }
        }
    }
}
