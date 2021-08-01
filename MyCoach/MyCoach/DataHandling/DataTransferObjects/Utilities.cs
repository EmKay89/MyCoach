﻿using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.DataHandling.DataTransferObjects
{
    public static class Utilities
    {
        public static bool AreEqual(ObservableCollection<Category> list1, ObservableCollection<Category> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].ID != list2[i].ID ||
                    list1[i].Name != list2[i].Name ||
                    list1[i].Type != list2[i].Type ||
                    list1[i].Count != list2[i].Count ||
                    list1[i].Active != list2[i].Active)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AreEqual(ObservableCollection<Exercise> list1, ObservableCollection<Exercise> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].ID != list2[i].ID ||
                    list1[i].Category != list2[i].Category ||
                    list1[i].Count != list2[i].Count ||
                    list1[i].Name != list2[i].Name ||
                    list1[i].Unit != list2[i].Unit ||
                    list1[i].RelatedCategory != list2[i].RelatedCategory ||
                    list1[i].Scores != list2[i].Scores ||
                    list1[i].Info != list2[i].Info ||
                    list1[i].Active != list2[i].Active)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AreEqual(ObservableCollection<Settings> list1, ObservableCollection<Settings> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].Permission != list2[i].Permission ||
                    list1[i].RepeatsRound1 != list2[i].RepeatsRound1 ||
                    list1[i].ScoresRound1 != list2[i].ScoresRound1 ||
                    list1[i].RepeatsRound2 != list2[i].RepeatsRound2 ||
                    list1[i].ScoresRound2 != list2[i].ScoresRound2 ||
                    list1[i].RepeatsRound3 != list2[i].RepeatsRound3 ||
                    list1[i].ScoresRound3 != list2[i].ScoresRound3 ||
                    list1[i].RepeatsRound4 != list2[i].RepeatsRound4 ||
                    list1[i].ScoresRound4 != list2[i].ScoresRound4)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AreEqual(ObservableCollection<TrainingSchedule> list1, ObservableCollection<TrainingSchedule> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].StartMonth != list2[i].StartMonth ||
                    list1[i].ScheduleType != list2[i].ScheduleType ||
                    list1[i].Duration != list2[i].Duration)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AreEqual(ObservableCollection<Month> list1, ObservableCollection<Month> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].Number != list2[i].Number ||
                    list1[i].StartDate != list2[i].StartDate ||
                    list1[i].Category1Goal != list2[i].Category1Goal ||
                    list1[i].Category1Scores != list2[i].Category1Scores ||
                    list1[i].Category2Goal != list2[i].Category2Goal ||
                    list1[i].Category2Scores != list2[i].Category2Scores ||
                    list1[i].Category3Goal != list2[i].Category3Goal ||
                    list1[i].Category3Scores != list2[i].Category3Scores ||
                    list1[i].Category4Goal != list2[i].Category4Goal ||
                    list1[i].Category4Scores != list2[i].Category4Scores ||
                    list1[i].Category5Goal != list2[i].Category5Goal ||
                    list1[i].Category5Scores != list2[i].Category5Scores ||
                    list1[i].Category6Goal != list2[i].Category6Goal ||
                    list1[i].Category6Scores != list2[i].Category6Scores ||
                    list1[i].Category7Goal != list2[i].Category7Goal ||
                    list1[i].Category7Scores != list2[i].Category7Scores ||
                    list1[i].Category8Goal != list2[i].Category8Goal ||
                    list1[i].Category8Scores != list2[i].Category8Scores ||
                    list1[i].TotalGoal != list2[i].TotalGoal)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AreEqual(ExerciseSet set1, ExerciseSet set2)
        {
            if(AreEqual(set1.Categories, set2.Categories) == false
                || AreEqual(set1.Exercises, set2.Exercises) == false)
            {
                return false;
            }

            return true;
        }

        public static bool AreEqual(DtoCollection collection1, DtoCollection collection2)
        {
            if (AreEqual(collection1.Categories, collection2.Categories) == false
                || AreEqual(collection1.Exercises, collection2.Exercises) == false
                || AreEqual(collection1.Settings, collection2.Settings) == false
                || AreEqual(collection1.TrainingSchedules, collection2.TrainingSchedules) == false
                || AreEqual(collection1.TrainingScores, collection2.TrainingScores) == false)
            {
                return false;
            }

            return true;
        }

        public static bool AreEqual<T>(T dto1, T dto2) where T : IDataTransferObject
        {
            switch (dto1)
            {
                case Category category:
                    var categories1 = new ObservableCollection<Category> { category };
                    var categories2 = new ObservableCollection<Category> { dto2 as Category };
                    return AreEqual(categories1, categories2);
                case Exercise exercise:
                    var exercises1 = new ObservableCollection<Exercise> { exercise };
                    var exercises2 = new ObservableCollection<Exercise> { dto2 as Exercise };
                    return AreEqual(exercises1, exercises2);
                case Settings settings:
                    var settings1 = new ObservableCollection<Settings> { settings };
                    var settings2 = new ObservableCollection<Settings> { dto2 as Settings };
                    return AreEqual(settings1, settings2);
                case TrainingSchedule schedule:
                    var schedules1 = new ObservableCollection<TrainingSchedule> { schedule };
                    var schedules2 = new ObservableCollection<TrainingSchedule> { dto2 as TrainingSchedule };
                    return AreEqual(schedules1, schedules2);
                case Month score:
                    var scores1 = new ObservableCollection<Month> { score };
                    var scores2 = new ObservableCollection<Month> { dto2 as Month };
                    return AreEqual(scores1, scores2);
                default:
                    return false;
            }
        }

        public static IEnumerable<Category> GetActiveTrainingCategories()
        {
            return DataInterface.GetInstance().GetData<Category>().Where(c => c.Active && c.Type == ExerciseType.Training);
        }
    }
}