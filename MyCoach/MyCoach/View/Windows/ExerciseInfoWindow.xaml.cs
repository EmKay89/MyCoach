using MyCoach.Model.DataTransferObjects;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;

namespace MyCoach.View.Windows
{
    /// <summary>
    ///     Interaction logic for ExerciseInfoWindow.xaml
    /// </summary>
    public partial class ExerciseInfoWindow : WindowWithoutMenuAndIcon
    {
        public ExerciseInfoWindow()
        {
            InitializeComponent();
            this.Loaded += this.OnWindowLoaded;
            this.Closing += this.OnWindowClosed;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as Exercise;
            this.Title = context.Name;
            this.EditTextBox.Document.Blocks.Clear();
            this.EditTextBox.Document.Blocks.Add(new Paragraph(new Run(context.Info)));
        }

        private void OnWindowClosed(object sender, CancelEventArgs e)
        {
            var context = this.DataContext as Exercise;
            context.Info = new TextRange(
                this.EditTextBox.Document.ContentStart,
                this.EditTextBox.Document.ContentEnd).Text.Trim(' ', '\n', '\r');
        }
    }
}
