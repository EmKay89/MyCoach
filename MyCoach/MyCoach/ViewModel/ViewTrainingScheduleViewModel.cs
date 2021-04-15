﻿using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using MyCoach.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel
{
    public class ViewTrainingScheduleViewModel : BaseViewModel
    {
        private MonthViewModel currentMonthViewModel;
        private bool timeBasedScheduleElementsVisible;
        private bool overviewElementsVisible = true;
        private bool detailsElementsVisible;

        public ViewTrainingScheduleViewModel()
        {
            this.MonthViewModelsInTimeBasedSchedule = new ObservableCollection<MonthViewModel>();
            DataInterface.GetInstance().GetData<TrainingSchedule>().FirstOrDefault().PropertyChanged += this.OnTraininScheduleChanged;
            this.DisplayTimeBasedElementsCommand = new RelayCommand(this.DisplayTimeBasedElements);
            this.UpdateView();
        }

        public RelayCommand DisplayTimeBasedElementsCommand { get; }

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
                this.InvokePropertiesChanged(
                    nameof(this.TimeBasedScheduleElementsVisible),
                    nameof(this.OverviewElementsVisible),
                    nameof(this.DetailsElementsVisible));
            }
        }

        public bool OverviewElementsVisible
        {
            get => this.overviewElementsVisible && this.timeBasedScheduleElementsVisible;

            set
            {
                if (this.overviewElementsVisible == value)
                {
                    return;
                }

                this.overviewElementsVisible = value;
                this.InvokePropertyChanged();
            }
        }

        public bool DetailsElementsVisible
        {
            get => this.detailsElementsVisible && this.timeBasedScheduleElementsVisible;

            set
            {
                if (this.detailsElementsVisible == value)
                {
                    return;
                }

                this.detailsElementsVisible = value;
                this.InvokePropertyChanged();
            }
        }

        private void OnTraininScheduleChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateView();
        }

        private void UpdateView()
        {
            var months = DataInterface.GetInstance().GetData<Month>();
            var schedule = DataInterface.GetInstance().GetData<TrainingSchedule>().FirstOrDefault();
            this.TimeBasedScheduleElementsVisible = schedule.ScheduleType == ScheduleType.TimeBased;
            this.UpdateCurrentMonthViewModel(months, schedule);
            this.UpdateMonthViewModelsInTimeBasedSchedule(months, schedule);
        }

        private void UpdateCurrentMonthViewModel(ObservableCollection<Month> months, TrainingSchedule schedule)
        {
            var currentMonth = months?.Where(m => m.Number == MonthNumber.Current).FirstOrDefault();

            if (currentMonth == null)
            {
                return;
            }

            var currentMonthInTimeBasedSchedule = months.Where(
                m => m.GetStartDateFromSchedule(schedule) == currentMonth.GetStartDateFromSchedule(schedule)
                    && m.Number != MonthNumber.Current).FirstOrDefault();

            if (currentMonthInTimeBasedSchedule != null && schedule.ScheduleType == ScheduleType.TimeBased)
            {
                currentMonth = currentMonthInTimeBasedSchedule;
            }

            this.CurrentMonthViewModel = new MonthViewModel(currentMonth);
        }

        private void UpdateMonthViewModelsInTimeBasedSchedule(ObservableCollection<Month> months, TrainingSchedule schedule)
        {
            this.MonthViewModelsInTimeBasedSchedule.Clear();

            foreach (var month in months)
            {
                if (month.Number == MonthNumber.Current || (int)month.Number > schedule.Duration)
                {
                    continue;
                }

                this.MonthViewModelsInTimeBasedSchedule.Add(new MonthViewModel(month));
            }
        }

        private void DisplayTimeBasedElements(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Overview":
                    this.OverviewElementsVisible = true;
                    this.DetailsElementsVisible = false;
                    break;
                case "Details":
                    this.OverviewElementsVisible = false;
                    this.DetailsElementsVisible = true;
                    break;
            }
        }
    }
}
