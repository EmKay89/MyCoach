﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.DataHandling.DataTransferObjects
{
    public static class DtoUtilities
    {
        public static bool AreEqual(List<Category> list1, List<Category> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].ID != list2[i].ID ||
                    list1[i].Name != list2[i].Name ||
                    list1[i].Count != list2[i].Count ||
                    list1[i].Active != list2[i].Active)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AreEqual(List<Exercise> list1, List<Exercise> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].Type != list2[i].Type ||
                    list1[i].Category != list2[i].Category ||
                    list1[i].Count != list2[i].Count ||
                    list1[i].Name != list2[i].Name ||
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

        public static bool AreEqual(List<Settings> list1, List<Settings> list2)
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

        public static bool AreEqual(List<TrainingSchedule> list1, List<TrainingSchedule> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].StartMonth != list2[i].StartMonth ||
                    list1[i].StartYear != list2[i].StartYear ||
                    list1[i].Duration != list2[i].Duration)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AreEqual(List<TrainingScore> list1, List<TrainingScore> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].Month != list2[i].Month ||
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
                    list1[i].Category8Scores != list2[i].Category8Scores)
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
    }
}
