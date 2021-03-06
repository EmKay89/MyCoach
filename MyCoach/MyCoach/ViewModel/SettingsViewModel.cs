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
using System.Windows.Forms;
using System.Windows.Input;

namespace MyCoach.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        private string permissionText;

        public SettingsViewModel()
        {
            this.Settings = new Settings();
            this.LoadSettingsBuffer();
            this.SaveSettingsCommand = new RelayCommand(this.SaveSettings, () => this.HasUnsavedChanges);
            this.SetDefaultsCommand = new RelayCommand(this.SetDefaultSettings);
            this.ResetSettingsCommand = new RelayCommand(this.LoadSettingsBuffer, () => this.HasUnsavedChanges);
            this.PropertyChanged += delegate { this.HasUnsavedChanges = true; };
            this.PremissionsWithCaption = new Dictionary<ExerciseSchedulingRepetitionPermission, string>
            {
                { ExerciseSchedulingRepetitionPermission.No, "Nein" },
                { ExerciseSchedulingRepetitionPermission.NotPreferred, "Nicht bevorzugt" },
                { ExerciseSchedulingRepetitionPermission.Yes, "Ja" }
            };
        }

        public bool HasUnsavedChanges { get; private set; }

        public Dictionary<ExerciseSchedulingRepetitionPermission, string> PremissionsWithCaption { get; }

        public RelayCommand SaveSettingsCommand { get; }

        public RelayCommand SetDefaultsCommand { get; }

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
                this.UpdatePermissionText();
            }
        }

        public string PermissionText
        {
            get => this.permissionText;

            set
            {
                if (this.permissionText == value)
                {
                    return;
                }

                this.permissionText = value;
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
            this.UpdatePermissionText();
            this.InvokePropertiesChanged(   
                nameof(this.Permission),
                nameof(this.PermissionText),
                nameof(this.RepeatsRound1),
                nameof(this.RepeatsRound2),
                nameof(this.RepeatsRound3),
                nameof(this.RepeatsRound4),
                nameof(this.ScoresRound1),
                nameof(this.ScoresRound2),
                nameof(this.ScoresRound3),
                nameof(this.ScoresRound4));
            this.HasUnsavedChanges = false;
        }

        private void SaveSettings()
        {
            // ToDo: Fehlermeldung einbauen, für den Fall, dass das Speichern nicht erfolgreich war.
            ObservableCollection<Settings> settingsToSave = new ObservableCollection<Settings> { this.Settings };
            DataInterface.GetInstance().SetDataTransferObjects<Settings>(settingsToSave);
            this.HasUnsavedChanges = false;
        }

        private void SetDefaultSettings()
        {
            // ToDo: Unittestbarkeit herstellen
            var result = MessageBox.Show("Achtung, hierdurch gehen Ihre gespeicherten Übungen verlohren. Möchten Sie fortfahren?",
                "Zurücksetzen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DataInterface.GetInstance().SetDefaults<Settings>();
                this.LoadSettingsBuffer();
                this.HasUnsavedChanges = false;
            }
        }

        private void UpdatePermissionText()
        {
            var savedSettings = this.HasUnsavedChanges;
            switch (this.Permission)
            {
                case ExerciseSchedulingRepetitionPermission.Yes:
                    this.PermissionText = "Eine Übung wird zufällig und somit unabhängig davon in eine Trainingsrunde " +
                        "eingeplant, ob sie bereits in einer vorherigen Runde eingeplant wurde " +
                        "und ob noch andere Übungen derselben Kategorie nicht eingeplant wurden.";
                    break;
                case ExerciseSchedulingRepetitionPermission.NotPreferred:
                    this.PermissionText = "Eine Übung wird nur dann erneut in eine Trainingsrunde eingeplant, " +
                        "wenn bereits alle anderen Übungen derselben Kategorie einmal eingeplant wurden.";
                    break;
                case ExerciseSchedulingRepetitionPermission.No:
                    this.PermissionText = "Eine Übung wird kein zweites Mal ins Training eingeplant. Sind weniger Übungen einer " +
                        "Kategorie vorhanden, als das Training Runden hat, werden für diese Kategorie nicht in jeder Runde " +
                        "Übungen zur Verfügung stehen.";
                    break;
                default:
                    this.PermissionText = "Keine Auswahl";
                    break;
            }

            this.HasUnsavedChanges = savedSettings;
        }
    }
}
