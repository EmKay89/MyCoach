using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCoach.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private SuperViewModel viewModel;

        public UpdateViewCommand(SuperViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (this.viewModel is MainViewModel mainVm
                && mainVm.SelectedViewModel is TrainingViewModel trainingVm
                && trainingVm.TrainingActive)
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Exercise":
                    this.viewModel.SelectedViewModel = new ExerciseViewModel();
                    break;
                case "Settings":
                    this.viewModel.SelectedViewModel = new SettingsViewModel();
                    break;
                case "TrainingSchedule":
                    this.viewModel.SelectedViewModel = new TrainingScheduleViewModel();
                    break;
                case "Training":
                    this.viewModel.SelectedViewModel = new TrainingViewModel();
                    break;
            }
        }
    }
}
