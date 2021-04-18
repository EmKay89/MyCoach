using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.ModelExtensions
{
    public static class MonthExtensionMethods
    {
        public static uint MaxScoreOrGoal(this IEnumerable<Month> months, ExerciseCategory category)
        {
            var highestScore = months.Max(m => m.GetScores(category));
            var highestGoal = months.Max(m => m.GetGoal(category));
            return highestScore > highestGoal ? highestScore : highestGoal;
        }
    }
}
