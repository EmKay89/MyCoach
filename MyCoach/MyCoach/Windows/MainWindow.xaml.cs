using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MyCoach
{
    /// <summary>
    ///     Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool plnTrainingSettingsVisible;
        private bool trainingActive;

        public bool PnlTrainingSettingsVisible
        {
            get => this.plnTrainingSettingsVisible;

            set
            {
                if (value == this.plnTrainingSettingsVisible)
                {
                    return;
                }

                plnTrainingSettingsVisible = value;
                this.OnTrainingSettingsVisibleChanged();
            }
        }

        public bool TrainingActive
        {
            get => this.trainingActive;

            set
            {
                if (value == this.trainingActive)
                {
                    return;
                }

                this.trainingActive = value;
                this.OnTrainingActiveChanged();
            }
        }

        public bool IsTimerWindowOpen { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnExpand_Click(object sender, RoutedEventArgs e)
        {
            this.PnlTrainingSettingsVisible = this.PnlTrainingSettingsVisible ? false : true;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            this.TrainingActive = this.TrainingActive ? false : true;
        }

        private void btnTimer_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsTimerWindowOpen)
            {
                return;
            }

            var timerWindow = new Windows.Timer();
            timerWindow.Closing += this.OnTimerWindowClosed;
            timerWindow.Show();
            this.IsTimerWindowOpen = true;
        }

        private void MenuItemExerciseCategories_Click(object sender, RoutedEventArgs e)
        {
            var exerciseCategoriesWindow = new Windows.ExerciseCategories();
            exerciseCategoriesWindow.ShowDialog();
        }

        private void MenuItemSpecificCategory_Click(object sender, RoutedEventArgs e)
        {
            var exerciseWindow = new Windows.Exercises();
            exerciseWindow.ShowDialog();
        }

        private void MenuItemEvaluation_Click(object sender, RoutedEventArgs e)
        {
            var evaluatioWindow = new Windows.Evaluation();
            evaluatioWindow.ShowDialog();
        }

        private void MenuItemTrainingSchedule_Click(object sender, RoutedEventArgs e)
        {
            var trainingScheduleWindow = new Windows.TrainingSchedule();
            trainingScheduleWindow.ShowDialog();
        }

        private void OnTrainingSettingsVisibleChanged ()
        {
            if (this.PnlTrainingSettingsVisible)
            {
                this.pnlTrainingSettings.Width = 300;
                this.btnExpand.Content = ">";
            }
            else
            {
                this.pnlTrainingSettings.Width = 15;
                this.btnExpand.Content = "<";
            }
        }

        private void OnTrainingActiveChanged()
        {
            if (this.TrainingActive)
            {
                this.PnlTrainingSettingsVisible = false;
                this.btnExpand.IsEnabled = false;
                this.btnStart.Content = "Stop";
            }
            else
            {
                this.btnExpand.IsEnabled = true;
                this.btnStart.Content = "Start";
            }
        }

        private void OnTimerWindowClosed(object sender, CancelEventArgs e)
        {
            this.IsTimerWindowOpen = false;
        }
    }
}
