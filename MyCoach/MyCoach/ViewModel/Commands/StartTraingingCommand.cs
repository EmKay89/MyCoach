using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCoach.ViewModel.Commands
{
    public class StartTraingingCommand : ICommand
    {
        private TrainingViewModel trainingViewModel;

        public StartTraingingCommand(TrainingViewModel vm)
        {
            this.trainingViewModel = vm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.trainingViewModel.TrainingActive = this.trainingViewModel.TrainingActive ? false : true;
        }
    }
}
