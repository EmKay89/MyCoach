using MyCoach.Model.DataTransferObjects;
using System;

namespace MyCoach.ViewModel.Events
{
    public class ExerciseEventArgs : EventArgs
    {
        public ExerciseEventArgs(Exercise exercise)
        {
            this.Exercise = exercise;
        }

        public Exercise Exercise { get; }
    }
}
