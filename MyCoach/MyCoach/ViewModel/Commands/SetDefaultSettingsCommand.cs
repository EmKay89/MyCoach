﻿using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCoach.ViewModel.Commands
{
    public class SetDefaultSettingsCommand : ICommand
    {
        private SettingsViewModel settingsViewModel;

        public SetDefaultSettingsCommand(SettingsViewModel vm)
        {
            this.settingsViewModel = vm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.settingsViewModel.SetDefaultSettings();
        }
    }
}
