﻿using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCoach.ViewModel.Commands
{
    public class AddExerciseCommand : ICommand
    {
        private ExercisesViewModel exerciseViewModel;

        public AddExerciseCommand(ExercisesViewModel vm)
        {
            this.exerciseViewModel = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (this.exerciseViewModel.SelectedCategory == null)
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            this.exerciseViewModel.Exercises.Add(
                new Exercise { Name = "Neue Übung", Active = true, Scores = 10, Category = this.exerciseViewModel.SelectedCategory.ID });
            this.exerciseViewModel.RefreshExercisesFilteredByCategory();
            this.exerciseViewModel.HasUnsavedExercises = true;
        }
    }
}
