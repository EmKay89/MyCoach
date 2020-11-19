using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Enumerations;
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
        private bool trainingActive;
        private List<Category> categories;

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

        public MainWindow()
        {
            this.InitializeData();
            this.Loaded += this.UpdateExerciseMenuItemsAndCheckboxes;
            this.InitializeComponent();
        }

        private void MenuItemExerciseCategories_Click(object sender, RoutedEventArgs e)
        {
            var exerciseCategoriesWindow = new Windows.ExerciseCategories();
            exerciseCategoriesWindow.ShowDialog();
        }

        private void MenuItemCategoryWarmUp_Click(object sender, RoutedEventArgs e)
        {
            var exerciseWindow = new Windows.Exercises();
            exerciseWindow.ShowDialog();
        }

        private void MenuItemCategory1_Click(object sender, RoutedEventArgs e)
        {
            var exerciseWindow = new Windows.Exercises();
            exerciseWindow.ShowDialog();
        }

        private void MenuItemCategory2_Click(object sender, RoutedEventArgs e)
        {
            var exerciseWindow = new Windows.Exercises();
            exerciseWindow.ShowDialog();
        }

        private void MenuItemCategory3_Click(object sender, RoutedEventArgs e)
        {
            var exerciseWindow = new Windows.Exercises();
            exerciseWindow.ShowDialog();
        }

        private void MenuItemCategory4_Click(object sender, RoutedEventArgs e)
        {
            var exerciseWindow = new Windows.Exercises();
            exerciseWindow.ShowDialog();
        }

        private void MenuItemCategory5_Click(object sender, RoutedEventArgs e)
        {
            var exerciseWindow = new Windows.Exercises();
            exerciseWindow.ShowDialog();
        }

        private void MenuItemCategory6_Click(object sender, RoutedEventArgs e)
        {
            var exerciseWindow = new Windows.Exercises();
            exerciseWindow.ShowDialog();
        }

        private void MenuItemCategory7_Click(object sender, RoutedEventArgs e)
        {
            var exerciseWindow = new Windows.Exercises();
            exerciseWindow.ShowDialog();
        }

        private void MenuItemCategory8_Click(object sender, RoutedEventArgs e)
        {
            var exerciseWindow = new Windows.Exercises();
            exerciseWindow.ShowDialog();
        }

        private void MenuItemCategoryCoolDown_Click(object sender, RoutedEventArgs e)
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

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            this.TrainingActive = this.TrainingActive ? false : true;
        }

        private void btnTimer_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.OfType<Windows.Timer>().Any())
            {
                return;
            }

            var timerWindow = new Windows.Timer();
            timerWindow.Show();
        }

        private void OnTrainingActiveChanged()
        {
            if (this.TrainingActive)
            {
                this.btnStart.Content = "Stop";
            }
            else
            {
                this.btnStart.Content = "Start";
            }
        }

        private void InitializeData()
        {
            this.categories = DataInterface.GetInstance().GetDataTransferObjects<Category>();
        }

        /// <summary>
        /// Besser über Bindings realisieren???
        /// https://stackoverflow.com/questions/3761672/wpf-how-to-hide-menu-item-if-commands-canexecute-is-false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void UpdateExerciseMenuItemsAndCheckboxes(object sender, EventArgs e)
        {
            this.mnuWarmUp.Visibility = GetExerciseVisibility(ExerciseCategory.WarmUp);
            this.mnuWarmUp.Header = this.GetExerciseName(ExerciseCategory.WarmUp);
            this.chkWarmUp.Visibility = GetExerciseVisibility(ExerciseCategory.WarmUp);
            this.chkWarmUp.Content = this.GetExerciseName(ExerciseCategory.WarmUp);

            this.mnuExercise1.Visibility = GetExerciseVisibility(ExerciseCategory.Category1);
            this.mnuExercise1.Header = this.GetExerciseName(ExerciseCategory.Category1);
            this.chkCat1.Visibility = GetExerciseVisibility(ExerciseCategory.Category1);
            this.chkCat1.Content = this.GetExerciseName(ExerciseCategory.Category1);

            this.mnuExercise2.Visibility = GetExerciseVisibility(ExerciseCategory.Category2);
            this.mnuExercise2.Header = this.GetExerciseName(ExerciseCategory.Category2);
            this.chkCat2.Visibility = GetExerciseVisibility(ExerciseCategory.Category2);
            this.chkCat2.Content = this.GetExerciseName(ExerciseCategory.Category2);

            this.mnuExercise3.Visibility = GetExerciseVisibility(ExerciseCategory.Category3);
            this.mnuExercise3.Header = this.GetExerciseName(ExerciseCategory.Category3);
            this.chkCat3.Visibility = GetExerciseVisibility(ExerciseCategory.Category3);
            this.chkCat3.Content = this.GetExerciseName(ExerciseCategory.Category3);

            this.mnuExercise4.Visibility = GetExerciseVisibility(ExerciseCategory.Category4);
            this.mnuExercise4.Header = this.GetExerciseName(ExerciseCategory.Category4);
            this.chkCat4.Visibility = GetExerciseVisibility(ExerciseCategory.Category4);
            this.chkCat4.Content = this.GetExerciseName(ExerciseCategory.Category4);

            this.mnuExercise5.Visibility = GetExerciseVisibility(ExerciseCategory.Category5);
            this.mnuExercise5.Header = this.GetExerciseName(ExerciseCategory.Category5);
            this.chkCat5.Visibility = GetExerciseVisibility(ExerciseCategory.Category5);
            this.chkCat5.Content = this.GetExerciseName(ExerciseCategory.Category5);

            this.mnuExercise6.Visibility = GetExerciseVisibility(ExerciseCategory.Category6);
            this.mnuExercise6.Header = this.GetExerciseName(ExerciseCategory.Category6);
            this.chkCat6.Visibility = GetExerciseVisibility(ExerciseCategory.Category6);
            this.chkCat6.Content = this.GetExerciseName(ExerciseCategory.Category6);

            this.mnuExercise7.Visibility = GetExerciseVisibility(ExerciseCategory.Category7);
            this.mnuExercise7.Header = this.GetExerciseName(ExerciseCategory.Category7);
            this.chkCat7.Visibility = GetExerciseVisibility(ExerciseCategory.Category7);
            this.chkCat7.Content = this.GetExerciseName(ExerciseCategory.Category7);

            this.mnuExercise8.Visibility = GetExerciseVisibility(ExerciseCategory.Category8);
            this.mnuExercise8.Header = this.GetExerciseName(ExerciseCategory.Category8);
            this.chkCat8.Visibility = GetExerciseVisibility(ExerciseCategory.Category8);
            this.chkCat8.Content = this.GetExerciseName(ExerciseCategory.Category8);

            this.mnuCoolDown.Visibility = GetExerciseVisibility(ExerciseCategory.CoolDown);
            this.mnuCoolDown.Header = this.GetExerciseName(ExerciseCategory.CoolDown);
            this.chkCoolDown.Visibility = GetExerciseVisibility(ExerciseCategory.CoolDown);
            this.chkCoolDown.Content = this.GetExerciseName(ExerciseCategory.CoolDown);
        }

        private Visibility GetExerciseVisibility(ExerciseCategory category)
        {
            return this.categories.Any(c => c.ID == (int)category && c.Active) ? Visibility.Visible : Visibility.Collapsed;
        }

        private string GetExerciseName(ExerciseCategory category)
        {
            return this.categories.Where(c => c.ID == (int)category)?.FirstOrDefault()?.Name;
        }
    }
}
