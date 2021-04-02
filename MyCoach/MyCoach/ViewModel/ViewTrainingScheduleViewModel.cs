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
        public ViewTrainingScheduleViewModel()
        {
            this.MonthViewModelsInTimeBasedSchedule = new ObservableCollection<MonthViewModel>();
            this.Months = DataInterface.GetInstance().GetDataTransferObjects<Month>();
            this.TrainingSchedule = DataInterface.GetInstance().GetDataTransferObjects<TrainingSchedule>().FirstOrDefault();
            this.PopulateMonthViewModels();
        }

        public TrainingSchedule TrainingSchedule { get; }

        public DateTime StartDate => this.TrainingSchedule.StartMonth;

        public ObservableCollection<Month> Months { get; }

        public MonthViewModel MonthViewModelCurrent { get; private set; }

        public ObservableCollection<MonthViewModel> MonthViewModelsInTimeBasedSchedule { get; }

        private void PopulateMonthViewModels()
        {
            var currentMonth = this.Months?.Where(m => m.Number == MonthNumber.Current).FirstOrDefault();

            if (currentMonth != null)
            {
                this.MonthViewModelCurrent = new MonthViewModel(this.CalculateStartDate(currentMonth), currentMonth);
            }

            this.MonthViewModelsInTimeBasedSchedule.Clear();

            foreach (var month in this.Months)
            {
                if (month.Number == MonthNumber.Current)
                {
                    continue;
                }

                this.MonthViewModelsInTimeBasedSchedule.Add(new MonthViewModel(this.CalculateStartDate(month), month));
            }
        }

        private DateTime CalculateStartDate(Month month)
        {
            if (month.Number == MonthNumber.Current)
            {
                return DateTime.Now;
            }

            return this.TrainingSchedule.StartMonth.AddMonths((int)month.Number - 1);
        }
    }
}
