using MyCoach.Helpers.Mvvm.Commands;
using MyCoach.View.Windows;
using MyCoach.ViewModel.DataBaseValidation;
using MyCoach.ViewModel.Events;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace MyCoach.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel selectedViewModel;

        public MainViewModel()
        {
            DtoCollectionsValidator.ValidateAll();

            this.ExercisesViewModel = new ExercisesViewModel();
            this.SettingsViewModel = new SettingsViewModel();
            this.TrainingScheduleViewModel = new TrainingScheduleViewModel();
            this.TrainingViewModel = new TrainingViewModel();

            this.ExercisesViewModel.AddExerciseToTrainingExecuted += this.OnExerciveVmAddExerciseToTrainingExecuted;

            this.UpdateMainViewCommand = new RelayCommand(
                this.SelectViewModel,
                () => this.SelectedViewModel != this.TrainingViewModel || this.TrainingViewModel.TrainingActive == false);
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()) == false)
            {
                App.Current.Windows.OfType<MainWindow>().FirstOrDefault().Loaded += this.OnMainWindowLoaded;
            }
        }

        public bool ExerciseViewSelected => this.SelectedViewModel == this.ExercisesViewModel;

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

        public ExercisesViewModel ExercisesViewModel { get; }

        public SettingsViewModel SettingsViewModel { get; }

        public TrainingScheduleViewModel TrainingScheduleViewModel { get; }

        public TrainingViewModel TrainingViewModel { get; }

        public RelayCommand UpdateMainViewCommand { get; set; }

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.SelectViewModel("Training");
        }

        private void OnExerciveVmAddExerciseToTrainingExecuted(object sender, ExerciseEventArgs e)
        {
            this.TrainingViewModel.AddExerciseToTraining(e.Exercise);
        }

        private void SelectViewModel(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Exercise":
                    this.SelectedViewModel = ExercisesViewModel;
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
