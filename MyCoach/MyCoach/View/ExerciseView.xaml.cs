using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                DataContext = (this.DataContext as ExerciseViewModel).Exercise,
                AllowEdit = true,
            };

            exerciseInfoWinow.ShowDialog();
        }
    }
}
