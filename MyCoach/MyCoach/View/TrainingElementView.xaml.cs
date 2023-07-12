using MyCoach.View.Windows;
using MyCoach.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace MyCoach.View
{
    /// <summary>
    /// Interaction logic for TrainingExerciseView.xaml
    /// </summary>
    public partial class TrainingElementView : UserControl
    {
        public TrainingElementView()
        {
            InitializeComponent();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            var trainingElementInfoWindow = new TrainingElementInfoWindow()
            {
                DataContext = this.DataContext
            };

            trainingElementInfoWindow.ShowDialog();
        }

        private void Headline_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var vm = this.DataContext as TrainingElementViewModel;
            if (vm.EditingAllowed)
            {
                var window = new HeadlineWindow();
                window.DataContext = this.DataContext;
                window.ShowDialog();
            }
        }

        private void Exercise_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var vm = this.DataContext as TrainingElementViewModel;
            if (vm.EditingAllowed)
            {
                var window = new ExerciseWindow();
                window.DataContext = new ExerciseViewModel((this.DataContext as TrainingElementViewModel).Exercise);
                window.ShowDialog();
            }
        }
    }
}
