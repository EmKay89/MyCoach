using MyCoach.View;
using MyCoach.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyCoach.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel selectedViewModel;

        public MainViewModel()
        {
            this.ExerciseViewModel = new ExercisesViewModel();
            this.SettingsViewModel = new SettingsViewModel();
            this.TrainingScheduleViewModel = new TrainingScheduleViewModel();
            this.TrainingViewModel = new TrainingViewModel();
            this.UpdateMainViewCommand = new UpdateMainViewCommand(this);
            App.Current.Windows.OfType<MainWindow>().FirstOrDefault().Loaded += this.OnMainWindowLoaded;
        }

        public bool ExerciseViewSelected => this.SelectedViewModel == this.ExerciseViewModel;

        public bool SettingsViewSelected => this.SelectedViewModel == this.SettingsViewModel;

        public bool TrainingScheduleViewSelected => this.SelectedViewModel == this.TrainingScheduleViewModel;

        public bool TrainingViewSelected => this.SelectedViewModel == this.TrainingViewModel;

        public BaseViewModel SelectedViewModel
        {
            get
            {
                return selectedViewModel;
            }

            set
            {
                if (this.selectedViewModel == value)
                {
                    return;
                }

                selectedViewModel = value;
                this.InvokePropertiesChanged(
                    "SelectedViewModel",
                    "ExerciseViewSelected", 
                    "SettingsViewSelected",
                    "TrainingScheduleViewSelected",
                    "TrainingViewSelected");
            }
        }

        public ExercisesViewModel ExerciseViewModel { get; }

        public SettingsViewModel SettingsViewModel { get; }

        public TrainingScheduleViewModel TrainingScheduleViewModel { get; }

        public TrainingViewModel TrainingViewModel { get; }

        public ICommand UpdateMainViewCommand { get; set; }

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateMainViewCommand.Execute("Training");
        }
    }
}
