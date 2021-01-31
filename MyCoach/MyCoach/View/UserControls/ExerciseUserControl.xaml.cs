using MyCoach.DataHandling.DataTransferObjects;
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

namespace MyCoach.View.UserControls
{
    /// <summary>
    /// Interaction logic for ExerciseUserControl.xaml
    /// </summary>
    public partial class ExerciseUserControl : UserControl
    {
        public ExerciseUserControl()
        {
            InitializeComponent();
        }

        public Exercise Exercise
        {
            get { return (Exercise)GetValue(ExerciseProperty); }
            set { SetValue(ExerciseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Exercise.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExerciseProperty =
            DependencyProperty.Register("Exercise", typeof(Exercise), typeof(ExerciseUserControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExerciseUserControl exerciseUserControl = d as ExerciseUserControl;

            if (exerciseUserControl != null)
            {
                exerciseUserControl.DataContext = exerciseUserControl.Exercise;
            }
        }
    }
}
