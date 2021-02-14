using MyCoach.DataHandling.DataTransferObjects;
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
        private ExercisesViewModel exercisesViewModel;

        public AddExerciseCommand(ExercisesViewModel vm)
        {
            this.exercisesViewModel = vm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (this.exercisesViewModel.SelectedCategory == null)
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            this.exercisesViewModel.Exercises.Add(
                new Exercise { Name = "Neue Übung", Active = true, Scores = 10, Category = this.exercisesViewModel.SelectedCategory.ID });
            this.exercisesViewModel.RefreshExercisesFilteredByCategory();
            this.exercisesViewModel.HasUnsavedExercises = true;
        }
    }
}
