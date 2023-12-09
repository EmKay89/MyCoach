using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.Helpers.CustomTypes;
using MyCoach.Helpers.Mvvm.Services;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCoach.ViewModel.TrainingGenerationAndEvaluation
{
    /// <summary>
    ///     Provides functions for calculating and saving scores from a <see cref="Training"/>.
    ///     Also shows a message summarizing the scores obtained.
    /// </summary>
    public static class TrainingEvaluator
    {
        public static IMessageBoxService MessageBoxService { get; set; } = new MessageBoxService();

        /// <summary>
        ///     Calulates and saves the scores from a given <see cref="Training"/>.
        /// </summary>
        /// <param name="training">The given Training.</param>
        public static void Evaluate(Training training)
        {
            var scores = new List<Pair<ExerciseCategory, ushort>>();

            foreach (var trainingElement in training)
            {
                UpdateScores(trainingElement, scores);
            }

            foreach (var pair in scores)
            {
                SetScoresForCategory(pair.Item1, pair.Item2);
            }

            DataInterface.GetInstance().SaveData<Month>();
            ShowEvaluationMessage(scores);
        }

        private static void UpdateScores(
            TrainingElementViewModel trainingElement, 
            List<Pair<ExerciseCategory, ushort>> scores)
        {
            var categoryActive = DataInterface.GetInstance().GetData<Category>()
                .SingleOrDefault(c => c.ID == trainingElement.Exercise?.Category)?.Active;

            if ((trainingElement.Type != TrainingElementType.Exercise)
                || trainingElement.Exercise.Category == null
                || trainingElement.Exercise.Category == ExerciseCategory.WarmUp
                || trainingElement.Exercise.Category == ExerciseCategory.CoolDown
                || trainingElement.Completed == false
                || categoryActive != true)
            {
                return;
            }

            var category = trainingElement.Exercise.Category;
            var scoresToBeAdded = (ushort)Math.Round(trainingElement.ScoresMultiplier * trainingElement.Exercise.Scores);

            if (scores.Any(s => s.Item1 == category))
            {
                scores.Where(s => s.Item1 == category).Single().Item2 += scoresToBeAdded;
                return;
            }

            scores.Add(new Pair<ExerciseCategory, ushort>((ExerciseCategory)trainingElement.Exercise.Category, scoresToBeAdded));
        }

        private static void SetScoresForCategory(ExerciseCategory category, ushort scores)
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

        private static void ShowEvaluationMessage(List<Pair<ExerciseCategory, ushort>> results)
        {
            var sb = new StringBuilder();
            if (results.Any() == false)
            {
                sb.Append("Training beendet ... Punkte gab es dafür aber nicht.");
            }
            else
            {
                sb.Append("Folgende Punkte wurden gutgeschrieben:");
                sb.AppendLine();
                sb.AppendLine();

                foreach (var result in results)
                {
                    var categoryName = DataInterface.GetInstance().GetData<Category>().Where(c => c.ID == result.Item1).Single();
                    sb.AppendLine(categoryName + ": " + result.Item2.ToString());
                }
            }

            Task.Run(() => MessageBoxService.ShowMessage(sb.ToString(), "Training beendet.", MessageBoxButton.OK, MessageBoxImage.Information));            
        }
    }
}
