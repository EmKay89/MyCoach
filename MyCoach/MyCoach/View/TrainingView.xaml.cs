using MyCoach.ViewModel;
using MyCoach.Windows;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MyCoach.View
{
    /// <summary>
    /// Interaction logic for TrainingView.xaml
    /// </summary>
    public partial class TrainingView : UserControl
    {
        public TrainingView()
        {
            InitializeComponent();
            this.DataContextChanged += delegate
            {
                if (this.DataContext is TrainingViewModel vm)
                {
                    vm.PropertyChanged += this.OnVmPropertyChanged;
                }
            };
        }

        private void TimerButton_Click(object sender, RoutedEventArgs e)
        {
            var timerWindow = App.Current.Windows.OfType<TimerWindow>().SingleOrDefault();

            if (timerWindow != null)
            {
                timerWindow.Focus();
                return;
            }

            new TimerWindow().Show();
        }

        private void OnVmPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var showSb = this.FindResource("ShowTrainingSettings") as Storyboard;
            var hideSb = this.FindResource("HideTrainingSettings") as Storyboard;

            if (e.PropertyName == nameof(TrainingViewModel.TrainingActive)
                && this.DataContext is TrainingViewModel vm)
            {
                if (vm.TrainingActive && hideSb != null)
                {
                    hideSb.Begin();
                }
                else if (vm.TrainingActive == false && showSb != null)
                {
                    showSb.Begin();
                }
            }
        }
    }
}
