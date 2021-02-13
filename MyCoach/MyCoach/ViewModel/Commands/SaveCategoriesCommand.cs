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
        private ExercisesViewModel exerciseViewModel;

        public SaveCategoriesCommand(ExercisesViewModel vm)
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
            var savedCategories = DataInterface.GetInstance().GetDataTransferObjects<Category>();
            savedCategories.Clear();
            foreach (var category in this.exerciseViewModel.Categories)
            {
                savedCategories.Add((Category)category.Clone());
            }
            
            DataInterface.GetInstance().SetDataTransferObjects<Category>(savedCategories);
        }
    }
}
