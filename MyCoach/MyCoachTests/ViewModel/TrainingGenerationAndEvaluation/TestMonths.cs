using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;

namespace MyCoachTests.ViewModel.TrainingGenerationAndEvaluation
{
    public static class TestMonths
    {
        public static List<Month> ThreeMonthsWithCurrentInTheMiddle => new List<Month>
        {
                new Month
                {
                    Number = MonthNumber.Current,
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                    Category1Goal = 110,
                    Category1Scores = 50,
                    Category2Goal = 110,
                    Category2Scores = 50,
                    Category3Goal = 110,
                    Category3Scores = 50,
                    Category4Goal = 110,
                    Category4Scores = 50,
                    Category5Goal = 110,
                    Category5Scores = 50,
                    Category6Goal = 110,
                    Category6Scores = 50,
                    Category7Goal = 110,
                    Category7Scores = 50,
                    Category8Goal = 110,
                    Category8Scores = 50
                },

                new Month
                {
                    Number = MonthNumber.Month1,
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1),
                    Category1Goal = 100,
                    Category1Scores = 100,
                    Category2Goal = 100,
                    Category2Scores = 100,
                    Category3Goal = 100,
                    Category3Scores = 100,
                    Category4Goal = 100,
                    Category4Scores = 100,
                    Category5Goal = 100,
                    Category5Scores = 100,
                    Category6Goal = 100,
                    Category6Scores = 100,
                    Category7Goal = 100,
                    Category7Scores = 100,
                    Category8Goal = 100,
                    Category8Scores = 100
                },

                new Month
                {
                    Number = MonthNumber.Month2,
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                    Category1Goal = 110,
                    Category1Scores = 50,
                    Category2Goal = 110,
                    Category2Scores = 50,
                    Category3Goal = 110,
                    Category3Scores = 50,
                    Category4Goal = 110,
                    Category4Scores = 50,
                    Category5Goal = 110,
                    Category5Scores = 50,
                    Category6Goal = 110,
                    Category6Scores = 50,
                    Category7Goal = 110,
                    Category7Scores = 50,
                    Category8Goal = 110,
                    Category8Scores = 50
                },

                new Month
                {
                    Number = MonthNumber.Month3,
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1),
                    Category1Goal = 120,
                    Category1Scores = 0,
                    Category2Goal = 120,
                    Category2Scores = 0,
                    Category3Goal = 120,
                    Category3Scores = 0,
                    Category4Goal = 120,
                    Category4Scores = 0,
                    Category5Goal = 120,
                    Category5Scores = 0,
                    Category6Goal = 120,
                    Category6Scores = 0,
                    Category7Goal = 120,
                    Category7Scores = 0,
                    Category8Goal = 120,
                    Category8Scores = 0
                }
        };
    }
}
