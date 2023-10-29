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
            this.Settings = new Settings();
            this.LoadSettingsBuffer();

            this.AddUnitCommand = new RelayCommand(this.AddUnit, () => this.NewUnit != null && this.NewUnit != string.Empty);
            this.DeleteUnitCommand = new RelayCommand(this.DeleteUnit, () => this.SelectedUnit != null);
            this.SaveSettingsCommand = new RelayCommand(this.SaveSettings, () => this.HasUnsavedChanges);
            this.SetDefaultsCommand = new RelayCommand(this.SetDefaultSettings);
            this.ResetSettingsCommand = new RelayCommand(this.LoadSettingsBuffer, () => this.HasUnsavedChanges);

            this.PropertyChanged += delegate { this.HasUnsavedChanges = true; };
        }

        public bool HasUnsavedChanges { get; private set; }

        public Dictionary<ExerciseSchedulingRepetitionPermission, string> PremissionsWithCaption { get; } = new Dictionary<ExerciseSchedulingRepetitionPermission, string>
        {
            { ExerciseSchedulingRepetitionPermission.No, "Nein" },
            { ExerciseSchedulingRepetitionPermission.NotPreferred, "Nicht bevorzugt" },
            { ExerciseSchedulingRepetitionPermission.Yes, "Ja" }
        };

        public RelayCommand AddUnitCommand { get; }

        public RelayCommand DeleteUnitCommand { get; }

        public RelayCommand SaveSettingsCommand { get; }

        public RelayCommand SetDefaultsCommand { get; }

        public RelayCommand ResetSettingsCommand { get; }

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

        private void LoadSettingsBuffer()
        {
            this.Units.CollectionChanged -= this.OnUnitsChanged;
            var savedSettings = DataInterface.GetInstance().GetData<Settings>()?.FirstOrDefault();            
            if (savedSettings == null)
            {
                DefaultDtos.Settings.Single().CopyValuesTo(this.Settings);
            }
            else
            {
                savedSettings.CopyValuesTo(this.Settings);
            }            

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
            this.Units.CollectionChanged += this.OnUnitsChanged;
            this.HasUnsavedChanges = false;
        }

        private void OnUnitsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.HasUnsavedChanges = true;
        }

        private void SaveSettings()
        {
            this.Settings.CopyValuesTo(DataInterface.GetInstance().GetData<Settings>().First());
            var result = DataInterface.GetInstance().SaveData<Settings>();
            if (result == false)
            {
                this.messageBoxService.ShowMessage(SAVING_ERROR_TEXT,
                    SAVING_ERROR_CAPTION,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            this.HasUnsavedChanges = false;
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
