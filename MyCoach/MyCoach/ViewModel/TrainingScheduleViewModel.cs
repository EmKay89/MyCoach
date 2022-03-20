using MyCoach.ViewModel.Commands;

namespace MyCoach.ViewModel
{
    public class TrainingScheduleViewModel : BaseViewModel
    {
        private BaseViewModel selectedViewModel;

        public TrainingScheduleViewModel()
        {
            this.EditViewModel = new EditTrainingScheduleViewModel();
            this.ViewViewModel = new ViewTrainingScheduleViewModel();
            this.SelectedViewModel = this.ViewViewModel;
            this.UpdateSelectedViewModelCommand = new RelayCommand(this.SelectViewModel);
        }

        public EditTrainingScheduleViewModel EditViewModel { get; }

        public ViewTrainingScheduleViewModel ViewViewModel { get; }

        public BaseViewModel SelectedViewModel
        {
            get => this.selectedViewModel;

            set
            {
                if (value == this.selectedViewModel)
                {
                    return;
                }

                this.selectedViewModel = value;
                this.InvokePropertiesChanged(
                    nameof(this.SelectedViewModel), 
                    nameof(this.EditSelected), 
                    nameof(this.ViewSelected));
            }
        }

        public bool EditSelected => this.SelectedViewModel == this.EditViewModel;

        public bool ViewSelected => this.SelectedViewModel == this.ViewViewModel;

        public RelayCommand UpdateSelectedViewModelCommand { get; }

        private void SelectViewModel(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Edit":
                    this.SelectedViewModel = this.EditViewModel;
                    break;
                case "View":
                    this.SelectedViewModel = this.ViewViewModel;
                    break;
            }
        }
    }
}
