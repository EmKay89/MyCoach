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
        public ExerciseViewModel()
        {
            this.RemoveExerciseCommand = new RemoveExerciseCommand(this);
            this.PropertyChanged += delegate { this.Parent.HasUnsavedChanges = true; };
        }

        public Exercise Exercise { get; set; }

        public ExercisesViewModel Parent { get; set; }

        public ICommand RemoveExerciseCommand { get; }

        public bool Active
        {
            get => this.Exercise.Active;

            set
            {
                if (this.Exercise.Active == value)
                {
                    return;
                }

                this.Exercise.Active = value;
                this.InvokePropertyChanged();
            }
        }

        public ExerciseCategory Category
        {
            get => this.Exercise.Category;

            set
            {
                if (this.Exercise.Category == value)
                {
                    return;
                }

                this.Exercise.Category = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Count
        {
            get => this.Exercise.Count;

            set
            {
                if (this.Exercise.Count == value)
                {
                    return;
                }

                this.Exercise.Count = value;
                this.InvokePropertyChanged();
            }
        }

        public string Info
        {
            get => this.Exercise.Info;

            set
            {
                if (this.Exercise.Info == value)
                {
                    return;
                }

                this.Exercise.Info = value;
                this.InvokePropertyChanged();
            }
        }

        public string Name
        {
            get => this.Exercise.Name;

            set
            {
                if (this.Exercise.Name == value)
                {
                    return;
                }

                this.Exercise.Name = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort Scores
        {
            get => this.Exercise.Scores;

            set
            {
                if (this.Exercise.Scores == value)
                {
                    return;
                }

                this.Exercise.Scores = value;
                this.InvokePropertyChanged();
            }
        }
    }
}
