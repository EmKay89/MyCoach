using MyCoach.Model.Defines;
using System;

namespace MyCoach.Model.DataTransferObjects
{
    /// <summary>
    ///     Repräsentiert Istwerte und Ziele für das Erreichen von Trainingspunkten der jeweiligen Übungskategorien bezogen auf einen Monat.
    /// </summary>
    public class Month : DtoBase, IDataTransferObject
    {
        private DateTime startMonth;
        private ushort category1Scores;
        private ushort category1Goal;
        private ushort category2Scores;
        private ushort category2Goal;
        private ushort category3Scores;
        private ushort category3Goal;
        private ushort category4Scores;
        private ushort category4Goal;
        private ushort category5Scores;
        private ushort category5Goal;
        private ushort category6Scores;
        private ushort category6Goal;
        private ushort category7Scores;
        private ushort category7Goal;
        private ushort category8Scores;
        private ushort category8Goal;
        private uint totalGoal;

        /// <summary>
        ///     Ruft die Nummer des Monats im Trainingsplan auf, oder legt sie fest.
        /// </summary>
        public MonthNumber Number { get; set; }

        /// <summary>
        ///     Ruft den Startzeitpunkt des Monats auf, oder legt ihn fest.
        /// </summary>
        public DateTime StartDate
        {
            get => startMonth;

            set
            {
                if (value == startMonth)
                {
                    return;
                }

                startMonth = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 1 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category1Scores
        {
            get => category1Scores;
            set
            {
                if (value == category1Scores)
                {
                    return;
                }

                category1Scores = value;
                InvokePropertyChanged();
                InvokePropertyChanged(nameof(TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 1 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category1Goal
        {
            get => category1Goal;
            set
            {
                if (value == category1Goal)
                {
                    return;
                }

                category1Goal = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 2 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category2Scores
        {
            get => category2Scores;
            set
            {
                if (value == category2Scores)
                {
                    return;
                }

                category2Scores = value;
                InvokePropertyChanged();
                InvokePropertyChanged(nameof(TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 2 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category2Goal
        {
            get => category2Goal;
            set
            {
                if (value == category2Goal)
                {
                    return;
                }

                category2Goal = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 3 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category3Scores
        {
            get => category3Scores;
            set
            {
                if (value == category3Scores)
                {
                    return;
                }

                category3Scores = value;
                InvokePropertyChanged();
                InvokePropertyChanged(nameof(TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 3 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category3Goal
        {
            get => category3Goal;
            set
            {
                if (value == category3Goal)
                {
                    return;
                }

                category3Goal = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 4 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category4Scores
        {
            get => category4Scores;
            set
            {
                if (value == category4Scores)
                {
                    return;
                }

                category4Scores = value;
                InvokePropertyChanged();
                InvokePropertyChanged(nameof(TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 4 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category4Goal
        {
            get => category4Goal;
            set
            {
                if (value == category4Goal)
                {
                    return;
                }

                category4Goal = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 5 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category5Scores
        {
            get => category5Scores;
            set
            {
                if (value == category5Scores)
                {
                    return;
                }

                category5Scores = value;
                InvokePropertyChanged();
                InvokePropertyChanged(nameof(TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 5 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category5Goal
        {
            get => category5Goal;
            set
            {
                if (value == category5Goal)
                {
                    return;
                }

                category5Goal = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 6 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category6Scores
        {
            get => category6Scores;
            set
            {
                if (value == category6Scores)
                {
                    return;
                }

                category6Scores = value;
                InvokePropertyChanged();
                InvokePropertyChanged(nameof(TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 6 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category6Goal
        {
            get => category6Goal;
            set
            {
                if (value == category6Goal)
                {
                    return;
                }

                category6Goal = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 7 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category7Scores
        {
            get => category7Scores;
            set
            {
                if (value == category7Scores)
                {
                    return;
                }

                category7Scores = value;
                InvokePropertyChanged();
                InvokePropertyChanged(nameof(TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 7 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category7Goal
        {
            get => category7Goal;
            set
            {
                if (value == category7Goal)
                {
                    return;
                }

                category7Goal = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 8 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category8Scores
        {
            get => category8Scores;
            set
            {
                if (value == category8Scores)
                {
                    return;
                }

                category8Scores = value;
                InvokePropertyChanged();
                InvokePropertyChanged(nameof(TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 8 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category8Goal
        {
            get => category8Goal;
            set
            {
                if (value == category8Goal)
                {
                    return;
                }

                category8Goal = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die Summe der für alle Kategorien erreichten Punkte ab.
        /// </summary>
        public uint TotalScores => (uint)Category1Scores
                    + Category2Scores
                    + Category3Scores
                    + Category4Scores
                    + Category5Scores
                    + Category6Scores
                    + Category7Scores
                    + Category8Scores;

        /// <summary>
        ///     Ruft das Punkteziel für alle Monate ab, oder legt es fest.
        /// </summary>
        public uint TotalGoal
        {
            get => totalGoal;
            set
            {
                if (value == totalGoal)
                {
                    return;
                }

                totalGoal = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Kopierte die Trainingspunkte für alle Kategorien in einen anderen Monat.
        /// </summary>
        /// <param name="targetMonth">Der Monat, dessen Trainingspunkte aktualisiert werden sollen.</param>
        public void CopyScoresTo(Month targetMonth)
        {
            targetMonth.Category1Scores = Category1Scores;
            targetMonth.Category2Scores = Category2Scores;
            targetMonth.Category3Scores = Category3Scores;
            targetMonth.Category4Scores = Category4Scores;
            targetMonth.Category5Scores = Category5Scores;
            targetMonth.Category6Scores = Category6Scores;
            targetMonth.Category7Scores = Category7Scores;
            targetMonth.Category8Scores = Category8Scores;
        }

        /// <inheritdoc/>
        public override void CopyValuesTo(DtoBase target)
        {
            if (target is Month targetMonth)
            {
                targetMonth.StartDate = StartDate;
                targetMonth.Category1Scores = Category1Scores;
                targetMonth.Category1Goal = Category1Goal;
                targetMonth.Category2Scores = Category2Scores;
                targetMonth.Category2Goal = Category2Goal;
                targetMonth.Category3Scores = Category3Scores;
                targetMonth.Category3Goal = Category3Goal;
                targetMonth.Category4Scores = Category4Scores;
                targetMonth.Category4Goal = Category4Goal;
                targetMonth.Category5Scores = Category5Scores;
                targetMonth.Category5Goal = Category5Goal;
                targetMonth.Category6Scores = Category6Scores;
                targetMonth.Category6Goal = Category6Goal;
                targetMonth.Category7Scores = Category7Scores;
                targetMonth.Category7Goal = Category7Goal;
                targetMonth.Category8Scores = Category8Scores;
                targetMonth.Category8Goal = Category8Goal;
                targetMonth.TotalGoal = TotalGoal;
            }
        }

        /// <summary>
        ///     Gibt das gespeicherte Punkteziel einer Übungskategorie zurück.
        /// </summary>
        /// <param name="category">Enumeration zur Auswahl der Kategorie.</param>
        /// <returns>Das Punkteziel als positive Ganzzahl.</returns>
        public ushort GetGoal(ExerciseCategory category)
        {
            switch (category)
            {
                case ExerciseCategory.Category1:
                    return Category1Goal;
                case ExerciseCategory.Category2:
                    return Category2Goal;
                case ExerciseCategory.Category3:
                    return Category3Goal;
                case ExerciseCategory.Category4:
                    return Category4Goal;
                case ExerciseCategory.Category5:
                    return Category5Goal;
                case ExerciseCategory.Category6:
                    return Category6Goal;
                case ExerciseCategory.Category7:
                    return Category7Goal;
                case ExerciseCategory.Category8:
                    return Category8Goal;
                default:
                    return 0;
            }
        }

        /// <summary>
        ///     Gibt die gespeicherten Trainingspunkte einer Übungskategorie zurück.
        /// </summary>
        /// <param name="category">Enumeration zur Auswahl der Kategorie.</param>
        /// <returns>Die Trainingspunkte als positive Ganzzahl.</returns>
        public ushort GetScores(ExerciseCategory category)
        {
            switch (category)
            {
                case ExerciseCategory.Category1:
                    return Category1Scores;
                case ExerciseCategory.Category2:
                    return Category2Scores;
                case ExerciseCategory.Category3:
                    return Category3Scores;
                case ExerciseCategory.Category4:
                    return Category4Scores;
                case ExerciseCategory.Category5:
                    return Category5Scores;
                case ExerciseCategory.Category6:
                    return Category6Scores;
                case ExerciseCategory.Category7:
                    return Category7Scores;
                case ExerciseCategory.Category8:
                    return Category8Scores;
                default:
                    return 0;
            }
        }

        /// <summary>
        ///     Setzt die Punkteziele des Monats auf 0.
        /// </summary>
        public void ResetGoals()
        {
            Category1Goal = 0;
            Category2Goal = 0;
            Category3Goal = 0;
            Category4Goal = 0;
            Category5Goal = 0;
            Category6Goal = 0;
            Category7Goal = 0;
            Category8Goal = 0;
            TotalGoal = 0;
        }

        /// <summary>
        ///     Setzt die erreichten Trainingspunkte des Monats auf 0.
        /// </summary>
        public void ResetScores()
        {
            Category1Scores = 0;
            Category2Scores = 0;
            Category3Scores = 0;
            Category4Scores = 0;
            Category5Scores = 0;
            Category6Scores = 0;
            Category7Scores = 0;
            Category8Scores = 0;
        }

        /// <summary>
        ///     Setzt die Trainingspunkte für eine Übungskategorie.
        /// </summary>
        /// <param name="category">Enumeration zur Auswahl der Kategorie.</param>
        /// <param name="value">Die Trainingspunkte als positive Ganzzahl.</param>
        public void SetScores(ExerciseCategory category, ushort value)
        {
            switch (category)
            {
                case ExerciseCategory.Category1:
                    Category1Scores = value;
                    break;
                case ExerciseCategory.Category2:
                    Category2Scores = value;
                    break;
                case ExerciseCategory.Category3:
                    Category3Scores = value;
                    break;
                case ExerciseCategory.Category4:
                    Category4Scores = value;
                    break;
                case ExerciseCategory.Category5:
                    Category5Scores = value;
                    break;
                case ExerciseCategory.Category6:
                    Category6Scores = value;
                    break;
                case ExerciseCategory.Category7:
                    Category7Scores = value;
                    break;
                case ExerciseCategory.Category8:
                    Category8Scores = value;
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        ///     Gibt einen ganzzahligen Wert zwischen 0 und 100 zurück, der angibt wie viel Prozent 
        ///     des Punkteziels für eine Kategorie bereits erreicht. Ist kein Punkteziel gegeben,
        ///     ist der Rückgabewert 0.
        /// </summary>
        /// <param name="category">Enumeration zur Auswahl der Kategorie.</param>
        /// <returns>Das Erreichen das Trainigspunkteziels für eine Kategorie in Prozent.</returns>
        public int GetPercentage(ExerciseCategory category)
        {
            switch (category)
            {
                case ExerciseCategory.Category1:
                    return GetPercentageFromGoalAndScores(Category1Goal, Category1Scores);
                case ExerciseCategory.Category2:
                    return GetPercentageFromGoalAndScores(Category2Goal, Category2Scores);
                case ExerciseCategory.Category3:
                    return GetPercentageFromGoalAndScores(Category3Goal, Category3Scores);
                case ExerciseCategory.Category4:
                    return GetPercentageFromGoalAndScores(Category4Goal, Category4Scores);
                case ExerciseCategory.Category5:
                    return GetPercentageFromGoalAndScores(Category5Goal, Category5Scores);
                case ExerciseCategory.Category6:
                    return GetPercentageFromGoalAndScores(Category6Goal, Category6Scores);
                case ExerciseCategory.Category7:
                    return GetPercentageFromGoalAndScores(Category7Goal, Category7Scores);
                case ExerciseCategory.Category8:
                    return GetPercentageFromGoalAndScores(Category8Goal, Category8Scores);
                default:
                    return 0;
            }
        }

        private int GetPercentageFromGoalAndScores(ushort goal, ushort scores) => goal == 0 ? 0 : scores < goal ? (int)Math.Round(scores * 100.0 / goal, 0) : 100;
    }
}
