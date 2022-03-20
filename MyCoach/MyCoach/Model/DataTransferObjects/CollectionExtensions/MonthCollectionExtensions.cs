using MyCoach.Model.Defines;
using System.Collections.Generic;
using System.Linq;

namespace MyCoach.Model.DataTransferObjects.CollectionExtensions
{
    public static class MonthCollectionExtensions
    {
        public static uint MaxScoreOrGoal(this IEnumerable<Month> months, ExerciseCategory category)
        {
            var highestScore = months.Any() ? months.Max(m => m.GetScores(category)) : (uint)0;
            var highestGoal = months.Any() ? months.Max(m => m.GetGoal(category)) : (uint)0;
            return highestScore > highestGoal ? highestScore : highestGoal;
        }

        public static void UpdateStartDatesBySchedule(this IEnumerable<Month> months, TrainingSchedule schedule)
        {
            foreach (var month in months)
            {
                if (month.Number == MonthNumber.Current)
                {
                    continue;
                }

                month.StartDate = schedule.StartMonth.AddMonths((int)month.Number - 1);
            }
        }
    }
}
