using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            this.exercise.PropertyChanged += this.IvokeExerciseChanged;
            this.SelectableUnits = DataInterface.GetInstance().GetData<Settings>().Single().Units;
        }

        public event ExerciseEventHandler AddExerciseToTrainingExecuted;
        public event ExerciseEventHandler DeleteExerciseExecuted;
        public event ExerciseEventHandler ExerciseChanged;

        public ObservableCollection<string> SelectableUnits { get; } = new ObservableCollection<string>();

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
                this.InvokePropertyChanged();
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

        public bool IsTrainingExercise => this.Exercise.Category != ExerciseCategory.WarmUp
            && this.Exercise.Category != ExerciseCategory.CoolDown;

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

        private void IvokeExerciseChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Exercise exercise)
            {
                this.ExerciseChanged?.Invoke(this, new ExerciseEventArgs(exercise));
            }
        }
    }
}
