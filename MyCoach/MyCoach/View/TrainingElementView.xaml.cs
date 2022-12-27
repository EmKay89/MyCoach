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
    }
}
