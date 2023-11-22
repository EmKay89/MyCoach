using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using MyMvvm.Commands;
using MyMvvm.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace MyCoach.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        private string permissionText;
        private string newUnit = string.Empty;
        private readonly IMessageBoxService messageBoxService;

        public SettingsViewModel(IMessageBoxService messageBoxService = null)
        {
            this.messageBoxService = messageBoxService ?? new MessageBoxService();
            this.Settings = DataInterface.GetInstance().GetData<Settings>().Single();

            this.AddUnitCommand = new RelayCommand(this.AddUnit, () => this.NewUnit != null && this.NewUnit != string.Empty);
            this.DeleteUnitCommand = new RelayCommand(this.DeleteUnit, () => this.SelectedUnit != null);
            this.SetDefaultsCommand = new RelayCommand(this.SetDefaultSettings);

            this.Units.CollectionChanged += this.OnUnitsChanged;
            this.UpdatePermissionText();
        }

        public Dictionary<ExerciseSchedulingRepetitionPermission, string> PremissionsWithCaption { get; } = new Dictionary<ExerciseSchedulingRepetitionPermission, string>
        {
            { ExerciseSchedulingRepetitionPermission.No, "Nein" },
            { ExerciseSchedulingRepetitionPermission.NotPreferred, "Nicht bevorzugt" },
            { ExerciseSchedulingRepetitionPermission.Yes, "Ja" }
        };

        public RelayCommand AddUnitCommand { get; }

        public RelayCommand DeleteUnitCommand { get; }

        public RelayCommand SetDefaultsCommand { get; }

        public Settings Settings { get; set; }

        public ObservableCollection<string> Units => this.Settings.Units;

        public string NewUnit
        {
            get => newUnit;

            set
            {
                if (value == newUnit)
                {
                    return;
                }

                newUnit = value;
                this.InvokePropertyChanged();
            }
        }

        public string SelectedUnit { get; set; }

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
                this.SaveSettings();
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
                this.SaveSettings();
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
                this.SaveSettings();
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
                this.SaveSettings();
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
                this.SaveSettings();
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
                this.SaveSettings();
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
                this.SaveSettings();
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
                this.SaveSettings();
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
                this.SaveSettings();
            }
        }

        public ushort RepeatsAndScoresMultiplier
        {
            get => this.Settings.RepeatsAndScoresMultiplier;

            set
            {
                if (this.Settings.RepeatsAndScoresMultiplier == value)
                {
                    return;
                }

                this.Settings.RepeatsAndScoresMultiplier = value;
                this.InvokePropertyChanged();
                this.SaveSettings();
            }
        }

        private void AddUnit()
        {
            this.Units.Add(this.NewUnit);
            this.NewUnit = string.Empty;
        }

        private void DeleteUnit()
        {
            if (this.Units.Contains(SelectedUnit))
            {
                this.Units.Remove(SelectedUnit);
            }
        }

        private void OnUnitsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.SaveSettings();
        }

        private void SaveSettings()
        {
            DataInterface.GetInstance().SaveData<Settings>();
        }

        private void SetDefaultSettings()
        {
            var result = this.messageBoxService.ShowMessage("Achtung, hierdurch gehen deine gespeicherten Einstellungen verloren. Möchtest du fortfahren?",
                "Zurücksetzen",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                DataInterface.GetInstance().SetDefaults<Settings>();
                this.InvokePropertiesChanged(
                    nameof(this.Permission),
                    nameof(this.RepeatsRound1),
                    nameof(this.RepeatsRound2),
                    nameof(this.RepeatsRound3),
                    nameof(this.RepeatsRound4),
                    nameof(this.ScoresRound1),
                    nameof(this.ScoresRound2),
                    nameof(this.ScoresRound3),
                    nameof(this.ScoresRound4),
                    nameof(this.RepeatsAndScoresMultiplier));
                this.UpdatePermissionText();
            }
        }

        private void UpdatePermissionText()
        {
            switch (this.Permission)
            {
                case ExerciseSchedulingRepetitionPermission.Yes:
                    this.PermissionText = "Eine Übung wird unabhängig davon in eine Trainingsrunde " +
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
        }
    }
}
