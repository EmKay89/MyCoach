using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCoach.Windows
{
    /// <summary>
    /// Interaktionslogik für Evaluation.xaml
    /// </summary>
    public partial class Evaluation : Window, INotifyPropertyChanged
    {
        public Evaluation()
        {
            DataContext = this;
            InitializeComponent();
            this.IsColumnVisible = false;
        }

        private bool isColumnVisible;
        public bool IsColumnVisible
        {
            get { return isColumnVisible; }
            set
            {
                if (value = isColumnVisible)
                {
                    return;
                }

                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
