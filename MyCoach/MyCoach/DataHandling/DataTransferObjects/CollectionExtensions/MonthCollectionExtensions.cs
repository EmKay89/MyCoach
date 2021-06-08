using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.DataHandling.DataTransferObjects.CollectionExtensions
{
    public static class MonthCollectionExtensions
    {
        public static uint MaxScoreOrGoal(this IEnumerable<Month> months, ExerciseCategory category)
        {
            var highestScore = months.Any() ? months.Max(m => m.GetScores(category)) : (uint)0;
            var highestGoal = months.Any() ? months.Max(m => m.GetGoal(category)) : (uint)0;
            return highestScore > highestGoal ? highestScore : highestGoal;
        }
    }
}
