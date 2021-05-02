using MyCoach.DataHandling;
using MyCoach.View;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.DataBaseValidation;
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
            DtoCollectionsValidator.ValidateAll();
            this.ExerciseViewModel = new ExercisesViewModel();
            this.SettingsViewModel = new SettingsViewModel();
            this.TrainingScheduleViewModel = new TrainingScheduleViewModel();
            this.TrainingViewModel = new TrainingViewModel();
            this.UpdateMainViewCommand = new RelayCommand(
                this.SelectViewModel,
                () => this.SelectedViewModel != this.TrainingViewModel || this.TrainingViewModel.TrainingActive == false);
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()) == false)
            {
                App.Current.Windows.OfType<MainWindow>().FirstOrDefault().Loaded += this.OnMainWindowLoaded;
            }
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

        public RelayCommand UpdateMainViewCommand { get; set; }

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.SelectViewModel("Training");
        }

        private void SelectViewModel(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Exercise":
                    this.SelectedViewModel = ExerciseViewModel;
                    break;
                case "Settings":
                    this.SelectedViewModel = SettingsViewModel;
                    break;
                case "TrainingSchedule":
                    this.SelectedViewModel = TrainingScheduleViewModel;
                    break;
                case "Training":
                    this.SelectedViewModel = TrainingViewModel;
                    break;
            }
        }
    }
}
