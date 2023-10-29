using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.DataTransferObjects.CollectionExtensions;
using MyCoach.Model.Defines;
using MyCoach.ViewModel.Defines;
using MyExtensions.IEnumerable;
using MyMvvm.Commands;
using MyMvvm.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace MyCoach.ViewModel
{
    public class EditTrainingScheduleViewModel : BaseViewModel
    {
        private readonly IMessageBoxService messageBoxService;
        private bool hasUnsavedChanges;
        private ScheduleEditingType scheduleEditingType;

        public EditTrainingScheduleViewModel(
            IMessageBoxService messageBoxService = null)
        {
            this.CheckCurrentMonth();
            this.LoadBuffers();
            this.UpdateAvailableCategories();

            this.messageBoxService = messageBoxService ?? new MessageBoxService();

            DataInterface.GetInstance().GetData<Category>().CollectionChanged += this.OnCategoriesChanged;
            DataInterface.GetInstance().GetData<Category>().ForEach(c => c.PropertyChanged += this.OnCategoryChanged);

            this.DeleteScheduleCommand = new RelayCommand(() => this.DeleteSchedule());
            this.DeleteScoresCommand = new RelayCommand(() => this.DeleteScores());
            this.SaveCommand = new RelayCommand(() => this.SaveBuffers(), () => this.HasUnsavedChanges);
            this.ResetCommand = new RelayCommand(() => this.LoadBuffers(), () => this.HasUnsavedChanges);
        }

        public const string CHANGE_SCHEDULE_TEXT = "Achtung, Änderungen an Typ oder Startdatum des Trainingsplans löschen alle gespeicherten Trainingspunkte. Möchtest du fortfahren?";
        public const string RESET_SCHEDULE_CAPTION = "Trainingsplan löschen";
        public const string RESET_SCORES_CAPTION = "Trainingspunkte löschen";
        public const string RESET_SCHEDULE_TEXT = "Achtung, hierdurch wird dein Trainingsplan gelöscht. Möchtest du fortfahren?";
        public const string RESET_SCORES_TEXT = "Achtung, hierdurch werden alle gespeicherten Trainingspunkte gelöscht. Möchtest du fortfahren?";

        public TrainingSchedule Schedule { get; private set; }

        public ObservableCollection<Month> Months { get; } = new ObservableCollection<Month>();

        public ObservableCollection<string> AvailableCategories { get; } = new ObservableCollection<string>();

        public ObservableCollection<EditMonthViewModel> EditMonthViewModels { get; } = new ObservableCollection<EditMonthViewModel>();

        public Dictionary<ushort, string> NumbersOneToTwelveWithCaption { get; } = new Dictionary<ushort, string>
        {
            { 1, "1 Monat" },
            { 2, "2 Monate" },
            { 3, "3 Monate" },
            { 4, "4 Monate" },
            { 5, "5 Monate" },
            { 6, "6 Monate" },
            { 7, "7 Monate" },
            { 8, "8 Monate" },
            { 9, "9 Monate" },
            { 10, "10 Monate" },
            { 11, "11 Monate" },
            { 12, "12 Monate" },
        };

        public Dictionary<ScheduleType, string> ScheduleTypesWithCaption { get; } = new Dictionary<ScheduleType, string>
        {
            { ScheduleType.Generic, "Fortlaufend" },
            { ScheduleType.TimeBased, "Zeitbasiert" }
        };

        public ScheduleEditingType ScheduleEditingType
        {
            get => this.scheduleEditingType;

            set
            {
                if (value == this.scheduleEditingType)
                {
                    return;
                }

                this.scheduleEditingType = value;             
                this.InvokePropertyChanged();
                this.EditMonthViewModels.ForEach(vm => vm.ScheduleEditingType = value);
            }
        }

        public RelayCommand DeleteScheduleCommand { get; }

        public RelayCommand DeleteScoresCommand { get; }

        public RelayCommand ResetCommand { get; }

        public RelayCommand SaveCommand { get; }

        public ushort Duration
        {
            get => this.Schedule.Duration;

            set
            {
                if (value == this.Schedule.Duration)
                {
                    return;
                }

                this.Schedule.Duration = value;
                this.UpdateEditMonthViewModels();
                this.HasUnsavedChanges = true;
            }
        }

        public ScheduleType Type
        {
            get => this.Schedule.ScheduleType;

            set
            {
                if (value == this.Schedule.ScheduleType)
                {
                    return;
                }

                this.Schedule.ScheduleType = value;
                this.InvokePropertyChanged("TimeBasedScheduleElementsVisible");
                this.UpdateEditMonthViewModels();
                this.HasUnsavedChanges = true;
            }
        }

        public DateTime StartMonth
        {
            get => this.Schedule.StartMonth;

            set
            {
                if (value == this.Schedule.StartMonth)
                {
                    return;
                }

                this.Schedule.StartMonth = value;
                this.Months.UpdateStartDatesBySchedule(this.Schedule);
                this.UpdateEditMonthViewModels();
                this.HasUnsavedChanges = true;
            }
        }

        public bool TimeBasedScheduleElementsVisible
        {
            get => this.Type == ScheduleType.TimeBased;
        }

        public bool HasUnsavedChanges
        {
            get => this.hasUnsavedChanges;

            private set
            {
                if (value == this.hasUnsavedChanges)
                {
                    return;
                }

                this.hasUnsavedChanges = value;
                this.InvokePropertyChanged();
            }
        }

        private void CheckCurrentMonth()
        {
            var savedCurrentMonth = DataInterface.GetInstance().GetData<Month>().Where(m => m.Number == MonthNumber.Current).FirstOrDefault();

            if (savedCurrentMonth.StartDate != new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
            {
                savedCurrentMonth.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                savedCurrentMonth.ResetScores();
                DataInterface.GetInstance().SaveData<Month>();
            }
        }

        private void DeleteScores()
        {
            var questionResult = this.messageBoxService.ShowMessage(
                RESET_SCORES_TEXT,
                RESET_SCORES_CAPTION,
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (questionResult == MessageBoxResult.No)
            {
                return;
            }

            DataInterface.GetInstance().GetData<Month>().ForEach(m => m.ResetScores());

            var resultSaving = DataInterface.GetInstance().SaveData<Month>();
            if (resultSaving == false)
            {
                this.messageBoxService.ShowMessage(
                    SAVING_ERROR_TEXT,
                    SAVING_ERROR_CAPTION,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            this.LoadBuffers();
        }

        private void DeleteSchedule()
        {
            var questionResult = this.messageBoxService.ShowMessage(
                RESET_SCHEDULE_TEXT,
                RESET_SCHEDULE_CAPTION,
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (questionResult == MessageBoxResult.No)
            {
                return;
            }

            DataInterface.GetInstance().SetDefaults<TrainingSchedule>();
            var savedSchedule = DataInterface.GetInstance().GetData<TrainingSchedule>().FirstOrDefault();
            var savedMonths = DataInterface.GetInstance().GetData<Month>();
            savedMonths.ForEach(m => m.ResetGoals());
            savedMonths.UpdateStartDatesBySchedule(savedSchedule);

            var resultSaving = DataInterface.GetInstance().SaveData<Month>() && DataInterface.GetInstance().SaveData<TrainingSchedule>();
            if (resultSaving == false)
            {
                this.messageBoxService.ShowMessage(
                    SAVING_ERROR_TEXT,
                    SAVING_ERROR_CAPTION,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            this.LoadBuffers();
        }

        private void LoadBuffers()
        {
            this.Schedule = (TrainingSchedule)DataInterface.GetInstance().GetData<TrainingSchedule>().First().Clone();
            this.InvokePropertiesChanged(
                nameof(this.Duration),
                nameof(this.TimeBasedScheduleElementsVisible),
                nameof(this.Type));

            var savedMonths = DataInterface.GetInstance().GetData<Month>();
            this.Months.ForEach(m => m.PropertyChanged -= this.OnMonthChanged);
            this.Months.Clear();
            savedMonths.ForEach(m => this.Months.Add((Month)m.Clone()));
            this.Months.UpdateStartDatesBySchedule(this.Schedule);
            this.Months.ForEach(m => m.PropertyChanged += this.OnMonthChanged);

            this.HasUnsavedChanges = false;
            this.UpdateEditMonthViewModels();
        }

        private void SaveBuffers()
        {
            var changesWillDeleteScores = this.GetIfChangesWillDeleteScores();

            if (changesWillDeleteScores)
            {
                var questionResult = this.messageBoxService.ShowMessage(
                    CHANGE_SCHEDULE_TEXT,
                    RESET_SCORES_CAPTION,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (questionResult == MessageBoxResult.No)
                {
                    return;
                }
            }

            // Note that order of saving matters at here: Correct update of MonthViewModels in
            // ViewTrainingScheduleViewModel requires update of saved schedule after update of saved months.
            // ToDo: Stabiler bauen.
            this.SaveMonths(changesWillDeleteScores);
            this.SaveSchedule();

            var result = DataInterface.GetInstance().SaveData<Month>() && DataInterface.GetInstance().SaveData<TrainingSchedule>();
            if (result == false)
            {
                this.messageBoxService.ShowMessage(SAVING_ERROR_TEXT, SAVING_ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.HasUnsavedChanges = false;
        }

        private void SaveMonths(bool changesWillDeleteScores)
        {
            this.UpdateScoresOfMonths();
            this.Months.UpdateStartDatesBySchedule(this.Schedule);

            if (changesWillDeleteScores)
            {
                this.Months.ForEach(m => m.ResetScores());
            }

            var savedMonths = DataInterface.GetInstance().GetData<Month>();
            foreach (var month in this.Months)
            {
                var savedMonth = savedMonths.Where(m => m.Number == month.Number).FirstOrDefault();
                if (savedMonth != null)
                {
                    month.CopyValuesTo(savedMonth);
                }
            }
        }

        private void SaveSchedule()
        {
            var savedSchedule = DataInterface.GetInstance().GetData<TrainingSchedule>().First();
            this.Schedule.CopyValuesTo(savedSchedule);
        }

        private bool GetIfChangesWillDeleteScores()
        {
            var savedSchedule = DataInterface.GetInstance().GetData<TrainingSchedule>().FirstOrDefault();
            return this.Schedule.StartMonth != savedSchedule.StartMonth
                || this.Schedule.ScheduleType != savedSchedule.ScheduleType;
        }

        private void OnCategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var category in e.OldItems)
                {
                    ((Category)category).PropertyChanged -= this.OnCategoryChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (var category in e.NewItems)
                {
                    ((Category)category).PropertyChanged += this.OnCategoryChanged;
                }
            }

            this.UpdateAvailableCategories();
        }

        private void OnCategoryChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateAvailableCategories();
        }

        private void OnMonthChanged(object sender, PropertyChangedEventArgs e)
        {
            this.HasUnsavedChanges = true;
        }

        private void UpdateAvailableCategories()
        {
            this.AvailableCategories.Clear();
            Model.DataTransferObjects.Utilities.GetActiveTrainingCategories().ForEach(c => this.AvailableCategories.Add(c.Name));
            this.AvailableCategories.Add("Gesamt");
        }

        private void UpdateEditMonthViewModels()
        {
            this.EditMonthViewModels.Clear();
            this.ScheduleEditingType = ScheduleEditingType.FreeEntry;

            if (this.Schedule.ScheduleType == ScheduleType.Generic)
            {
                this.EditMonthViewModels.Add(
                    new EditMonthViewModel(
                        this.Months.Where(m => m.Number == MonthNumber.Current).FirstOrDefault()));
                return;
            }

            for (int i = 1; i <= this.Schedule.Duration; i++)
            {
                this.EditMonthViewModels.Add(
                    new EditMonthViewModel(
                        this.Months.Where(m => (int)m.Number == i).FirstOrDefault()));
            }
        }

        private void UpdateScoresOfMonths()
        {
            var savedMonths = DataInterface.GetInstance().GetData<Month>();
            savedMonths.ForEach(sm => sm.CopyScoresTo(this.Months.Where(m => m.Number == sm.Number).First()));
        }
    }
}
