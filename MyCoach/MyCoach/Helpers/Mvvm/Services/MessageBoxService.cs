using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCoach.Helpers.Mvvm.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public MessageBoxResult ShowMessage(string text, string caption, MessageBoxButton button, MessageBoxImage image)
        {
            return MessageBox.Show(text, caption, button, image);
        }
    }
}
