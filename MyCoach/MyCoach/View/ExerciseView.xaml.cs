using MyCoach.View.Windows;
using MyCoach.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace MyCoach.View
{
    /// <summary>
    /// Interaction logic for ExerciseView.xaml
    /// </summary>
    public partial class ExerciseView : UserControl
    {
        public ExerciseView()
        {
            InitializeComponent();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            var exerciseInfoWinow = new ExerciseInfoWindow
            {
                DataContext = (this.DataContext as ExerciseViewModel).Exercise
            };

            exerciseInfoWinow.ShowDialog();
        }
    }
}
