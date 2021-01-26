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
        private ExerciseViewModel exerciseViewModel;

        public SaveExercisesCommand(ExerciseViewModel vm)
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
            var savedCategories = DataInterface.GetInstance().GetDataTransferObjects<Exercise>();
            savedCategories.Clear();
            foreach (var category in this.exerciseViewModel.Categories)
            {
                savedCategories.Add((Exercise)category.Clone());
            }
            
            DataInterface.GetInstance().SetDataTransferObjects<Exercise>(savedCategories);
        }
    }
}
