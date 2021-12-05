using MyCoach.DataHandling.DataTransferObjects;
using System;

namespace MyCoach.ViewModel.Events
{
    public class AddExerciseExecutedEventArgs : EventArgs
    {
        public AddExerciseExecutedEventArgs(Exercise exercise)
        {
            this.Exercise = exercise;
        }

        public Exercise Exercise { get; }
    }
}
