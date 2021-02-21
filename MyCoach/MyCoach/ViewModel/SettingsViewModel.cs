using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using MyCoach.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCoach.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel()
        {
            this.Settings = new Settings();
            this.LoadSettingsBuffer();
            this.SaveSettingsCommand = new RelayCommand(this.SaveSettings, () => this.HasUnsavedSettings);
            this.SetDefaultSettingsCommand = new RelayCommand(this.SetDefaultSettings);
            this.ResetSettingsCommand = new RelayCommand(this.LoadSettingsBuffer, () => this.HasUnsavedSettings);
        }

        public bool HasUnsavedSettings { get; set; }

        public RelayCommand SaveSettingsCommand { get; }

        public RelayCommand SetDefaultSettingsCommand { get; }

        public RelayCommand ResetSettingsCommand { get; }

        public Settings Settings { get; private set; }

        public ExerciseSchedulingRepetitionPermission Permission
        {
            get => this.Settings.Permission;

            set
            {
                if (this.Settings.Permission == value)
                {
                    return;
                }

                this.Settings.Permission = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort RepeatsRound1
        {
            get => this.Settings.RepeatsRound1;

            set
            {
                if (this.Settings.RepeatsRound1 == value)
                {
                    return;
                }

                this.Settings.RepeatsRound1 = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort RepeatsRound2
        {
            get => this.Settings.RepeatsRound2;

            set
            {
                if (this.Settings.RepeatsRound2 == value)
                {
                    return;
                }

                this.Settings.RepeatsRound2 = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort RepeatsRound3
        {
            get => this.Settings.RepeatsRound3;

            set
            {
                if (this.Settings.RepeatsRound3 == value)
                {
                    return;
                }

                this.Settings.RepeatsRound3 = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort RepeatsRound4
        {
            get => this.Settings.RepeatsRound4;

            set
            {
                if (this.Settings.RepeatsRound4 == value)
                {
                    return;
                }

                this.Settings.RepeatsRound4 = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort ScoresRound1
        {
            get => this.Settings.ScoresRound1;

            set
            {
                if (this.Settings.ScoresRound1 == value)
                {
                    return;
                }

                this.Settings.ScoresRound1 = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort ScoresRound2
        {
            get => this.Settings.ScoresRound2;

            set
            {
                if (this.Settings.ScoresRound2 == value)
                {
                    return;
                }

                this.Settings.ScoresRound2 = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort ScoresRound3
        {
            get => this.Settings.ScoresRound3;

            set
            {
                if (this.Settings.ScoresRound3 == value)
                {
                    return;
                }

                this.Settings.ScoresRound3 = value;
                this.InvokePropertyChanged();
            }
        }

        public ushort ScoresRound4
        {
            get => this.Settings.ScoresRound4;

            set
            {
                if (this.Settings.ScoresRound4 == value)
                {
                    return;
                }

                this.Settings.ScoresRound4 = value;
                this.InvokePropertyChanged();
            }
        }

        private void LoadSettingsBuffer()
        {
            var savedSettings = DataInterface.GetInstance().GetDataTransferObjects<Settings>()?.FirstOrDefault();

            if (savedSettings == null)
            {
                this.SetDefaultSettings();
                return;
            }

            this.Settings = (Settings)savedSettings.Clone();
        }

        private void SaveSettings()
        {
            ObservableCollection<Settings> settingsToSave = new ObservableCollection<Settings> { this.Settings };
            DataInterface.GetInstance().SetDataTransferObjects<Settings>(settingsToSave);
        }

        private void SetDefaultSettings()
        {
            this.Settings = new Settings
            {
                Permission = ExerciseSchedulingRepetitionPermission.NotPreferred,
                RepeatsRound1 = 100,
                RepeatsRound2 = 75,
                RepeatsRound3 = 60,
                RepeatsRound4 = 50,
                ScoresRound1 = 100,
                ScoresRound2 = 100,
                ScoresRound3 = 100,
                ScoresRound4 = 100
            };

            this.SaveSettings();
        }
    }
}
