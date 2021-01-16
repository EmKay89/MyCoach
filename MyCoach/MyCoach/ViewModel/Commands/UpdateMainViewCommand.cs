using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCoach.ViewModel.Commands
{
    public class UpdateMainViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public UpdateMainViewCommand(MainViewModel viewModel)
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
            if (this.viewModel.SelectedViewModel == this.viewModel.TrainingViewModel
                && this.viewModel.TrainingViewModel.TrainingActive)
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
                    this.viewModel.SelectedViewModel = viewModel.ExerciseViewModel;
                    break;
                case "Settings":
                    this.viewModel.SelectedViewModel = viewModel.SettingsViewModel;
                    break;
                case "TrainingSchedule":
                    this.viewModel.SelectedViewModel = viewModel.TrainingScheduleViewModel;
                    break;
                case "Training":
                    this.viewModel.SelectedViewModel = viewModel.TrainingViewModel;
                    break;
            }
        }
    }
}
