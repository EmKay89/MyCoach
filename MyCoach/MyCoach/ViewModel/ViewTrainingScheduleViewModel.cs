using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel
{
    public class ViewTrainingScheduleViewModel : BaseViewModel
    {
        private MonthViewModel currentMonthViewModel;
        private bool timeBasedScheduleElementsVisible;

        public ViewTrainingScheduleViewModel()
        {
            this.MonthViewModelsInTimeBasedSchedule = new ObservableCollection<MonthViewModel>();
            this.TrainingSchedule = DataInterface.GetInstance().GetDataTransferObjects<TrainingSchedule>().FirstOrDefault();
            this.UpdateMonthViewModels();
        }

        public TrainingSchedule TrainingSchedule { get; }

        public DateTime StartDate => this.TrainingSchedule.StartMonth;

        public MonthViewModel CurrentMonthViewModel
        {
            get => this.currentMonthViewModel; 
            
            private set
            {
                if (this.currentMonthViewModel == value)
                {
                    return;
                }

                this.currentMonthViewModel = value;
                this.InvokePropertyChanged();
            }
        }

        public ObservableCollection<MonthViewModel> MonthViewModelsInTimeBasedSchedule { get; }

        public bool TimeBasedScheduleElementsVisible
        {
            get => this.timeBasedScheduleElementsVisible;

            private set
            {
                if (this.timeBasedScheduleElementsVisible == value)
                {
                    return;
                }

                this.timeBasedScheduleElementsVisible = value;
                this.InvokePropertyChanged();
            }
        }

        private void UpdateMonthViewModels()
        {
            var months = DataInterface.GetInstance().GetDataTransferObjects<Month>();
            this.UpdateCurrentMonthViewModel(months);
            UpdateMonthViewModelsInTimeBasedSchedule(months);
        }

        private void UpdateCurrentMonthViewModel(ObservableCollection<Month> months)
        {
            var currentMonth = months?.Where(m => m.Number == MonthNumber.Current).FirstOrDefault();

            if (currentMonth == null)
            {
                return;
            }

            var currentMonthInTimeBasedSchedule = months.Where(
                m => this.GetStartDate(m) == this.GetStartDate(currentMonth) 
                    && m.Number != MonthNumber.Current).FirstOrDefault();

            if (currentMonthInTimeBasedSchedule != null
                && this.TrainingSchedule.ScheduleType == ScheduleType.TimeBased)
            {
                currentMonth = currentMonthInTimeBasedSchedule;
            }

            this.CurrentMonthViewModel = new MonthViewModel(this.GetStartDate(currentMonth), currentMonth);
        }

        private void UpdateMonthViewModelsInTimeBasedSchedule(ObservableCollection<Month> months)
        {
            this.MonthViewModelsInTimeBasedSchedule.Clear();

            foreach (var month in months)
            {
                if (month.Number == MonthNumber.Current || (int)month.Number > this.TrainingSchedule.Duration)
                {
                    continue;
                }

                this.MonthViewModelsInTimeBasedSchedule.Add(new MonthViewModel(this.GetStartDate(month), month));
            }
        }

        private DateTime GetStartDate(Month month)
        {
            if (month.Number == MonthNumber.Current)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }

            return this.TrainingSchedule.StartMonth.AddMonths((int)month.Number - 1);
        }
    }
}
