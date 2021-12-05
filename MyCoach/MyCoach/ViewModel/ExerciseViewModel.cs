using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using MyCoach.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCoach.ViewModel
{
    public class ExerciseViewModel : BaseViewModel
    {
        private readonly Exercise exercise;
        private readonly ExercisesViewModel parent;

        public ExerciseViewModel(Exercise exercise, ExercisesViewModel parent)
        {
            this.exercise = exercise;
            this.parent = parent;
            this.AddExerciseCommand = new RelayCommand(this.AddExerciseToTraining);
            this.RemoveExerciseCommand = new RelayCommand(this.RemoveExercise);
            this.exercise.PropertyChanged += delegate { this.parent.HasUnsavedExercises = true; };
        }

        public List<string> SelectableUnits { get; } = new List<string>
        {
            "Wiederholungen",
            "Minuten",
            "Sekunden"
        };

        public Exercise Exercise => this.exercise;

        public RelayCommand AddExerciseCommand { get; }

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

        public ExerciseCategory Category
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

        private void AddExerciseToTraining(object parameter)
        {
            if (parameter is Exercise exercise)
            {
                this.parent.InvokeAddExerciseExecuted(exercise);
            }
        }

        private void RemoveExercise(object parameter)
        {
            if (parameter is Exercise exercise)
            {
                this.parent.Exercises.Remove(exercise);
                this.parent.RefreshExercisesFilteredByCategory();
                this.parent.HasUnsavedExercises = true;
            }
        }
    }
}
