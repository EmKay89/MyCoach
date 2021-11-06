using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.TrainingGenerationAndEvaluation
{
    /// <summary>
    ///     Provides functions for calculating and saving scores from a <see cref="Training"/>.
    /// </summary>
    public static class TrainingEvaluator
    {
        /// <summary>
        ///     Calulates and saves the scores from a given <see cref="Training"/>.
        /// </summary>
        /// <param name="training">The given Training.</param>
        public static void Evaluate(Training training)
        {
            foreach (var trainingElement in training)
            {
                CalculateAndSetScores(trainingElement);
            }

            DataInterface.GetInstance().SaveData<Month>();
        }

        private static void CalculateAndSetScores(TrainingElementViewModel trainingElement)
        {
            if ((trainingElement.Type != TrainingElementType.exercise)
                || trainingElement.Exercise.Category == ExerciseCategory.WarmUp
                || trainingElement.Exercise.Category == ExerciseCategory.CoolDown
                || trainingElement.Completed == false)
            {
                return;
            }

            var category = trainingElement.Exercise.Category;
            var scores = (ushort)Math.Round(trainingElement.ScoresMultiplier * trainingElement.Exercise.Scores);
            
            SetScoresForCategory(scores, category);
        }

        private static void SetScoresForCategory(ushort scores, ExerciseCategory category)
        {
            var scheduleType = DataInterface.GetInstance().GetData<TrainingSchedule>().First().ScheduleType;
            var currentMonth = DataInterface.GetInstance().GetData<Month>().Where(m => m.Number == MonthNumber.Current).First();
            var currentMonthInTimeBasedSchedule = DataInterface.GetInstance().GetData<Month>()
                .Where(m => m.StartDate == currentMonth.StartDate && m.Number != MonthNumber.Current).FirstOrDefault();

            var newScores = (ushort)(currentMonth.GetScores(category) + scores);
            currentMonth.SetScores(category, newScores);

            if (scheduleType == ScheduleType.TimeBased && currentMonthInTimeBasedSchedule != null)
            {
                var newScoresForTimeBasedMonth = (ushort)(currentMonthInTimeBasedSchedule.GetScores(category) + scores);
                currentMonthInTimeBasedSchedule.SetScores(category, newScoresForTimeBasedMonth);
            }
        }
    }
}
