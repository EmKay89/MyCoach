using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.DataBaseValidation
{
    public static class TrainingScheduleValidator
    {
        public static void Validate()
        {
            var trainingSchedules = DataInterface.GetInstance().GetData<TrainingSchedule>();

            if (trainingSchedules == null)
            {
                trainingSchedules = new ObservableCollection<TrainingSchedule>();
            }

            if (trainingSchedules.Any() == false)
            {
                trainingSchedules.Add(new TrainingSchedule { ScheduleType = ScheduleType.Generic });
            }

            var dublicates = trainingSchedules.Skip(1);

            foreach (var dublicate in dublicates)
            {
                trainingSchedules.Remove(dublicate);
            }

            DataInterface.GetInstance().SaveData<TrainingSchedule>();
        }
    }
}
