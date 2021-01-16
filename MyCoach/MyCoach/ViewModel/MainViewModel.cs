using MyCoach.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCoach.ViewModel
{
    public class MainViewModel : SuperViewModel
    {
        public MainViewModel()
        {
            App.Current.Windows.OfType<MainWindow>().FirstOrDefault().Loaded += this.OnMainWindowLoaded;
        }

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateViewCommand.Execute("Training");
        }
    }
}
