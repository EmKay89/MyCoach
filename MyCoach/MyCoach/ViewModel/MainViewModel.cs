using MyCoach.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCoach.ViewModel
{
    public class MainViewModel : SuperViewModel
    {
        public MainViewModel()
        {
            App.Current.Windows.OfType<MainWindow>().FirstOrDefault().Loaded += this.OnMainWindowLoaded;
            base.PropertyChanged += this.OnSelectedViewModelChanged;
        }
        public bool ExerciseViewSelected => this.SelectedViewModel?.GetType() == typeof(ExerciseViewModel);

        public bool SettingsViewSelected => this.SelectedViewModel?.GetType() == typeof(SettingsViewModel);

        public bool TrainingViewSelected => this.SelectedViewModel?.GetType() == typeof(TrainingViewModel);

        public bool TrainingScheduleViewSelected => this.SelectedViewModel?.GetType() == typeof(TrainingScheduleViewModel);

        private void OnSelectedViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedViewModel")
            {
                this.InvokePropertiesChanged("ExerciseViewSelected", "SettingsViewSelected", "TrainingViewSelected", "TrainingScheduleViewSelected");
            }
        }

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateViewCommand.Execute("Training");
        }
    }
}
