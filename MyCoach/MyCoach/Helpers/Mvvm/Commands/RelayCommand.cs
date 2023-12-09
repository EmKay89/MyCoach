using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCoach.Helpers.Mvvm.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Func<bool> canExecuteCallback;
        private readonly Action executeCallback;
        private readonly Action<object> executeCallbackWithParameter;

        public RelayCommand(Action executeCallback, Func<bool> canExecuteCallback = null)
        {
            this.canExecuteCallback = canExecuteCallback;
            this.executeCallback = executeCallback;
        }

        public RelayCommand(Action<object> executeCallback, Func<bool> canExecuteCallback = null)
        {
            this.canExecuteCallback = canExecuteCallback;
            executeCallbackWithParameter = executeCallback;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteCallback == null || canExecuteCallback.Invoke();
        }

        public void Execute(object parameter)
        {
            executeCallback?.Invoke();
            executeCallbackWithParameter?.Invoke(parameter);
        }
    }
}
