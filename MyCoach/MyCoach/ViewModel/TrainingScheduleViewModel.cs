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
    public class TrainingScheduleViewModel : BaseViewModel
    {
        public TrainingScheduleViewModel()
        {
            this.Categories = DataInterface.GetInstance().GetDataTransferObjects<Category>();
            this.TrainingSchedule = DataInterface.GetInstance().GetDataTransferObjects<TrainingSchedule>().FirstOrDefault();
            this.Months = DataInterface.GetInstance().GetDataTransferObjects<Month>();

            this.BuildMonths();

            this.EditViewModel = new EditTrainingScheduleViewModel();
            this.ViewViewModel = new ViewTriainingScheduleViewModel(this);
        }

#region Proerties

        public ObservableCollection<Category> Categories { get; }

        public EditTrainingScheduleViewModel EditViewModel { get; }

        public ObservableCollection<Month> Months { get; }

        public TrainingSchedule TrainingSchedule { get; }

        public ViewTriainingScheduleViewModel ViewViewModel { get; }

#endregion

#region Methods

        private void BuildMonths()
        {
            // ToDo: Make sure, that there are always 13 months in the DataInterface Buffer.
        }

#endregion
    }
}
