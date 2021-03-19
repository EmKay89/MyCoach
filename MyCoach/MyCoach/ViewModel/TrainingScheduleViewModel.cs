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
            this.EditViewModel = new EditTrainingScheduleViewModel();
            this.ViewViewModel = new ViewTriainingScheduleViewModel();
        }

#region Proerties

        public EditTrainingScheduleViewModel EditViewModel { get; }

        public ViewTriainingScheduleViewModel ViewViewModel { get; }

        public ObservableCollection<Category> Categories { get; set; }

#endregion




    }
}
