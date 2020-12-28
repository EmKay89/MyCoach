using MyCoach.ViewModels;
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

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
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
