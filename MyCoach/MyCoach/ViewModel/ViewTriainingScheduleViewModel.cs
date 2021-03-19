using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel
{
    public class ViewTriainingScheduleViewModel : BaseViewModel
    {
        private TrainingScheduleViewModel parent;

        public ViewTriainingScheduleViewModel(TrainingScheduleViewModel parent)
        {
            this.parent = parent;
            this.MonthViewModels = new ObservableCollection<MonthViewModel>();
            this.PopulateMonthViewModels();
            // ToDo: Schauen, ob das sinnvoll ist, alles über parent properties zu machen
        }

        public TrainingSchedule TrainingSchedule
        {
            get => this.parent.TrainingSchedule;
        }

        public DateTime StartDate
        {
            get => this.TrainingSchedule.StartMonth;
        }

        public ObservableCollection<Month> Months
        {
            get => this.parent.Months;
        }

        public ObservableCollection<MonthViewModel> MonthViewModels { get; }

        private void PopulateMonthViewModels()
        {
            this.MonthViewModels.Clear();
            var type = DataInterface.GetInstance().GetDataTransferObjects<TrainingSchedule>().FirstOrDefault().ScheduleType;
            
            if (type == Defines.ScheduleType.TimeBased)
            {
                foreach (var Month in this.Months)
                {
                    this.MonthViewModels.Add(new MonthViewModel(this, Month));
                }

                return;
            }

            this.MonthViewModels.Add(new MonthViewModel(this, this.Months.FirstOrDefault()));
        }
    }
}
