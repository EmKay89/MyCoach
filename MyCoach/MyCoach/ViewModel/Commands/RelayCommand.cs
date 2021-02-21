using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCoach.ViewModel.Commands
{
    public class RelayCommand : ICommand
    {
        private Func<bool> canExecuteCallback;
        private Action executeCallback;
        private Action<object> executeCallbackWithParameter;

        public RelayCommand(Action executeCallback, Func<bool> canExecuteCallback = null)
        {
            this.canExecuteCallback = canExecuteCallback;
            this.executeCallback = executeCallback;
        }

        public RelayCommand(Action<object> executeCallback, Func<bool> canExecuteCallback = null)
        {
            this.canExecuteCallback = canExecuteCallback;
            this.executeCallbackWithParameter = executeCallback;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecuteCallback == null ? true : this.canExecuteCallback.Invoke();
        }

        public void Execute(object parameter)
        {
            this.executeCallback?.Invoke();
            this.executeCallbackWithParameter?.Invoke(parameter);
        }
    }
}
