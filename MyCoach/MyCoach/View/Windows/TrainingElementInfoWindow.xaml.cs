using MyCoach.Helpers.Extensions.String;
using MyCoach.ViewModel;
using System.Windows;
using System.Windows.Documents;

namespace MyCoach.View.Windows
{
    /// <summary>
    ///     Interaction logic for TrainingElementInfoWindow.xaml
    /// </summary>
    public partial class TrainingElementInfoWindow : WindowWithoutMenuAndIcon
    {
        public TrainingElementInfoWindow()
        {
            InitializeComponent();
            this.Loaded += this.OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as TrainingElementViewModel;
            this.Title = context.Exercise.Name;

            if (context.ScoresForCategory.IsNullOrEmpty())
            {
                this.ScoresText.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.ScoresText.Text = context.ScoresForCategory;
            }

            this.EditTextBox.Document.Blocks.Clear();

            if (context.Info == null || context.Info == string.Empty)
            {
                this.EditTextBox.Document.Blocks.Add(new Paragraph(new Run("Noch keine Beschreibung vorhanden ...")));
                this.EditTextBox.FontWeight = FontWeights.Light;
                this.EditTextBox.FontStyle = FontStyles.Italic;                
                return;
            }

            this.EditTextBox.Document.Blocks.Add(new Paragraph(new Run(context.Info)));
        }
    }
}
