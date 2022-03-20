using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using System.Collections.ObjectModel;
using System.Linq;

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
                trainingSchedules.Add(DefaultDtos.TrainingSchedules.First());
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
