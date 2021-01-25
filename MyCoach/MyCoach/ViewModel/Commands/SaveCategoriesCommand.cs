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
    public class SaveCategoriesCommand : ICommand
    {
        private ExerciseViewModel exerciseViewModel;

        public SaveCategoriesCommand(ExerciseViewModel vm)
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
            DataInterface.GetInstance().SetDataTransferObjects<Category>(exerciseViewModel.Categories);
        }
    }
}
