using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCoach.ViewModel.Commands
{
    public class RemoveExerciseCommand : ICommand
    {
        private ExerciseViewModel exerciseViewModel;

        public RemoveExerciseCommand(ExerciseViewModel vm)
        {
            this.exerciseViewModel = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var exercise = parameter as Exercise;

            if (exercise != null)
            {
                this.exerciseViewModel.Parent.Exercises.Remove(exercise);
                this.exerciseViewModel.Parent.RefreshExercisesFilteredByCategory();
            }
        }
    }
}
