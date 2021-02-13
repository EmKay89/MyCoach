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
    public class SaveExercisesCommand : ICommand
    {
        private ExercisesViewModel exerciseViewModel;

        public SaveExercisesCommand(ExercisesViewModel vm)
        {
            this.exerciseViewModel = vm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.exerciseViewModel.HasUnsavedChanges;
        }

        public void Execute(object parameter)
        {
            var savedExercises = DataInterface.GetInstance().GetDataTransferObjects<Exercise>();
            savedExercises.Clear();
            foreach (var exercise in this.exerciseViewModel.Exercises)
            {
                savedExercises.Add((Exercise)exercise.Clone());
            }
            
            DataInterface.GetInstance().SetDataTransferObjects<Exercise>(savedExercises);
            this.exerciseViewModel.HasUnsavedChanges = false;
        }
    }
}
