using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCoach.Helpers.Mvvm.Services
{
    public interface IMessageBoxService
    {
        MessageBoxResult ShowMessage(string text, string caption, MessageBoxButton button, MessageBoxImage image);
    }
}
