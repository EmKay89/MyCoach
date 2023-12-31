﻿using MyCoach.ViewModel;
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
using System.Windows.Shapes;

namespace MyCoach.View.Windows
{
    /// <summary>
    /// Interaction logic for ExerciseWindow.xaml
    /// </summary>
    public partial class ExerciseWindow : WindowWithoutMenuAndIcon
    {
        public ExerciseWindow()
        {
            InitializeComponent();
        }

        // ToDo: Das ExerciseView enthält den gleichen Code ... das sollte von einem Kommando erledigt werden
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
