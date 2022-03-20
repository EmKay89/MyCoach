using MyCoach.Model.DataTransferObjects;
using MyCoach.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace MyCoach.View
{
    /// <summary>
    /// Interaction logic for ExerciseInfoWindow.xaml
    /// </summary>
    public partial class ExerciseInfoWindow : Window
    {
        public ExerciseInfoWindow()
        {
            InitializeComponent();
            this.Loaded += this.OnWindowLoaded;
            this.Closing += this.OnWindowClosed;
        }

        public bool AllowEdit { get; internal set; }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as Exercise;
            this.EditTextBox.IsReadOnly = !this.AllowEdit;
            this.EditTextBox.Document.Blocks.Clear();

            if (this.AllowEdit == false && (context.Info == null || context.Info == string.Empty))
            {
                this.EditTextBox.Document.Blocks.Add(new Paragraph(new Run("Noch keine Beschreibung vorhanden ...")));
                this.EditTextBox.FontWeight = FontWeights.Light;
                this.EditTextBox.FontStyle = FontStyles.Italic;
                return;
            }
            
            this.EditTextBox.Document.Blocks.Add(new Paragraph(new Run(context.Info)));
        }

        private void OnWindowClosed(object sender, CancelEventArgs e)
        {
            if (this.AllowEdit == false)
            {
                return;
            }

            var context = this.DataContext as Exercise;
            context.Info = new TextRange(
                this.EditTextBox.Document.ContentStart, 
                this.EditTextBox.Document.ContentEnd).Text.Trim(' ', '\n', '\r');
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
