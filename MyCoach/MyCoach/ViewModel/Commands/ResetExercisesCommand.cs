using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCoach.ViewModel.Commands
{
    public class ResetExercisesCommand : ICommand
    {
        private ExercisesViewModel exercisesViewModel;

        public ResetExercisesCommand(ExercisesViewModel vm)
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
            return this.exercisesViewModel.HasUnsavedExercises;
        }

        public void Execute(object parameter)
        {
            this.exercisesViewModel.LoadExerciseBuffer();
        }
    }
}
