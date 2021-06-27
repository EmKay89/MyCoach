using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.DataHandling.DataTransferObjects.CollectionExtensions;
using MyCoach.Defines;
using MyCoach.ViewModel.Commands;
using MyCoach.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCoach.ViewModel
{
    public class EditTrainingScheduleViewModel : BaseViewModel
    {
        private readonly IMessageBoxService messageBoxService;
        private ScheduleType type;
        private bool hasUnsavedChanges;
        private TrainingSchedule schedule;

        public EditTrainingScheduleViewModel(
            IMessageBoxService messageBoxService = null)
        {
            this.CheckCurrentMonth();
            this.LoadBuffers();
            this.UpdateAvailableCategories();

            this.messageBoxService = messageBoxService ?? new MessageBoxService();

            DataInterface.GetInstance().GetData<Category>().CollectionChanged += this.OnCategoriesChanged;

            this.DeleteScheduleCommand = new RelayCommand(() => this.DeleteSchedule());
            this.DeleteScoresCommand = new RelayCommand(() => this.DeleteScores());
            this.SaveCommand = new RelayCommand(() => this.SaveBuffers(), () => this.HasUnsavedChanges);
            this.ResetCommand = new RelayCommand(() => this.LoadBuffers(), () => this.HasUnsavedChanges);
        }

        public const string CHANGE_SCHEDULE_TEXT = "Achtung, Änderungen an Typ oder Startdatum des Trainingsplans löschen alle gespeicherten Trainingspunkte. Möchten Sie fortfahren?";
        public const string RESET_SCHEDULE_CAPTION = "Trainingsplan löschen";
        public const string RESET_SCORES_CAPTION = "Trainingspunkte löschen";
        public const string RESET_SCHEDULE_TEXT = "Achtung, hierdurch werden alle gespeicherten Trainingspunkte gelöscht. Möchten Sie fortfahren?";
        public const string RESET_SCORES_TEXT = "Achtung, hierdurch wird ihr Trainingsplan gelöscht. Möchten Sie fortfahren?";
        
        public TrainingSchedule Schedule
        {
            get => this.schedule; 
            
            set
            {
                if (this.schedule == value)
                {
                    return;
                }

                this.schedule.PropertyChanged -= this.OnScheduleBufferChanged;
                this.schedule = value;
                this.InvokePropertyChanged();
                this.schedule.PropertyChanged += this.OnScheduleBufferChanged;
            }
        }

        public ObservableCollection<Month> Months { get; } = new ObservableCollection<Month>();

        public ObservableCollection<Category> AvaiableCategories { get; } = new ObservableCollection<Category>();

        public ObservableCollection<EditMonthViewModel> EditMonthViewModels { get; } = new ObservableCollection<EditMonthViewModel>();

        public ObservableCollection<ushort> NumbersOneToTwelve { get; } = new ObservableCollection<ushort>
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

        public Dictionary<ScheduleType, string> ScheduleTypesWithCaption { get; } = new Dictionary<ScheduleType, string>
        {
            { ScheduleType.Generic, "Fortlaufend" },
            { ScheduleType.TimeBased, "Zeitbasiert" }
        };

        public RelayCommand DeleteScheduleCommand { get; }

        public RelayCommand DeleteScoresCommand { get; }

        public RelayCommand ResetCommand { get; }

        public RelayCommand SaveCommand { get; }

        public ScheduleType Type
        {
            get => this.type;

            set
            {
                if (value == this.type)
                {
                    return;
                }

                this.type = value;
                this.InvokePropertyChanged();
            }
        }

        public bool HasUnsavedChanges
        {
            get => this.hasUnsavedChanges;

            set
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

            DataInterface.GetInstance().GetData<Month>().Foreach(m => ((Month)m).ResetScores());

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
            DataInterface.GetInstance().GetData<Month>().Foreach(m => ((Month)m).ResetGoals());

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
            this.Schedule = DataInterface.GetInstance().GetData<TrainingSchedule>().First();
            var savedMonths = DataInterface.GetInstance().GetData<Month>();
            this.Months.Clear();
            foreach (var month in savedMonths)
            {
                this.Months.Add((Month)month.Clone());
            }

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

            this.UpdateStartDatesOfMonths();
            var savedSchedule = DataInterface.GetInstance().GetData<TrainingSchedule>().First();
            this.Schedule.CopyValuesTo(savedSchedule);

            var savedMonths = DataInterface.GetInstance().GetData<Month>();

            foreach (var month in this.Months)
            {
                var savedMonth = savedMonths.Where(m => m.Number == month.Number).FirstOrDefault();
                if (savedMonth != null)
                {
                    month.CopyValuesTo(savedMonth);
                }
            }

            if (changesWillDeleteScores)
            {
                savedMonths.Foreach(m => ((Month)m).ResetScores());
            }

            var result = DataInterface.GetInstance().SaveData<Month>() && DataInterface.GetInstance().SaveData<TrainingSchedule>();
            if (result == false)
            {
                this.messageBoxService.ShowMessage(SAVING_ERROR_TEXT, SAVING_ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.HasUnsavedChanges = false;
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
            if (e.PropertyName == nameof(Category.Active))
            {
                this.UpdateAvailableCategories();
            }
        }

        private void OnScheduleBufferChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(TrainingSchedule.StartMonth))
            {
                this.UpdateEditMonthViewModels();
            }
        }

        private void UpdateAvailableCategories()
        {
            this.AvaiableCategories.Clear();
            Utilities.GetActiveTrainingCategories().Foreach(c => this.AvaiableCategories.Add((Category)c));
        }

        private void UpdateEditMonthViewModels()
        {
            this.EditMonthViewModels.Clear();

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

        private void UpdateStartDatesOfMonths()
        {
            foreach (var month in this.Months)
            {
                if (month.Number == MonthNumber.Current)
                {
                    continue;
                }

                if ((int)month.Number <= this.Schedule.Duration)
                {
                    month.StartDate = this.Schedule.StartMonth.AddMonths((int)month.Number - 1);
                }
                else
                {
                    month.StartDate = DateTime.MinValue;
                }
            }
        }
    }
}
