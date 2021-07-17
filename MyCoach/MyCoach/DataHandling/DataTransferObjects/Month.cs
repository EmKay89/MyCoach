using MyCoach.Defines;
using System;

namespace MyCoach.DataHandling.DataTransferObjects
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
            get => this.startMonth;

            set
            {
                if (value == this.startMonth)
                {
                    return;
                }

                this.startMonth = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 1 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category1Scores
        {
            get => this.category1Scores; 
            set
            {
                if (value == this.category1Scores)
                {
                    return;
                }

                this.category1Scores = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged(nameof(this.TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 1 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category1Goal
        {
            get => this.category1Goal;
            set
            {
                if (value == this.category1Goal)
                {
                    return;
                }

                this.category1Goal = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 2 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category2Scores
        {
            get => this.category2Scores;
            set
            {
                if (value == this.category2Scores)
                {
                    return;
                }

                this.category2Scores = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged(nameof(this.TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 2 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category2Goal
        {
            get => this.category2Goal;
            set
            {
                if (value == this.category2Goal)
                {
                    return;
                }

                this.category2Goal = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 3 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category3Scores
        {
            get => this.category3Scores;
            set
            {
                if (value == this.category3Scores)
                {
                    return;
                }

                this.category3Scores = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged(nameof(this.TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 3 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category3Goal
        {
            get => this.category3Goal;
            set
            {
                if (value == this.category3Goal)
                {
                    return;
                }

                this.category3Goal = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 4 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category4Scores
        {
            get => this.category4Scores;
            set
            {
                if (value == this.category4Scores)
                {
                    return;
                }

                this.category4Scores = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged(nameof(this.TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 4 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category4Goal
        {
            get => this.category4Goal;
            set
            {
                if (value == this.category4Goal)
                {
                    return;
                }

                this.category4Goal = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 5 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category5Scores
        {
            get => this.category5Scores;
            set
            {
                if (value == this.category5Scores)
                {
                    return;
                }

                this.category5Scores = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged(nameof(this.TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 5 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category5Goal
        {
            get => this.category5Goal;
            set
            {
                if (value == this.category5Goal)
                {
                    return;
                }

                this.category5Goal = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 6 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category6Scores
        {
            get => this.category6Scores;
            set
            {
                if (value == this.category6Scores)
                {
                    return;
                }

                this.category6Scores = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged(nameof(this.TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 6 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category6Goal
        {
            get => this.category6Goal;
            set
            {
                if (value == this.category6Goal)
                {
                    return;
                }

                this.category6Goal = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 7 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category7Scores
        {
            get => this.category7Scores;
            set
            {
                if (value == this.category7Scores)
                {
                    return;
                }

                this.category7Scores = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged(nameof(this.TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 7 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category7Goal
        {
            get => this.category7Goal;
            set
            {
                if (value == this.category7Goal)
                {
                    return;
                }

                this.category7Goal = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die für Kategorie 8 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category8Scores
        {
            get => this.category8Scores;
            set
            {
                if (value == this.category8Scores)
                {
                    return;
                }

                this.category8Scores = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged(nameof(this.TotalScores));
            }
        }

        /// <summary>
        ///     Ruft das für Kategorie 8 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category8Goal
        {
            get => this.category8Goal;
            set
            {
                if (value == this.category8Goal)
                {
                    return;
                }

                this.category8Goal = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die Summe der für alle Kategorien erreichten Punkte ab.
        /// </summary>
        public uint TotalScores => (uint)this.Category1Scores
                    + this.Category2Scores
                    + this.Category3Scores
                    + this.Category4Scores
                    + this.Category5Scores
                    + this.Category6Scores
                    + this.Category7Scores
                    + this.Category8Scores;

        /// <summary>
        ///     Ruft das Punkteziel für alle Monate ab, oder legt es fest.
        /// </summary>
        public uint TotalGoal
        {
            get => this.totalGoal;
            set
            {
                if (value == this.totalGoal)
                {
                    return;
                }

                this.totalGoal = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Kopierte die Trainingspunkte für alle Kategorien in einen anderen Monat.
        /// </summary>
        /// <param name="targetMonth">Der Monat, dessen Trainingspunkte aktualisiert werden sollen.</param>
        public void CopyScoresTo(Month targetMonth)
        {
            targetMonth.Category1Scores = this.Category1Scores;
            targetMonth.Category2Scores = this.Category2Scores;
            targetMonth.Category3Scores = this.Category3Scores;
            targetMonth.Category4Scores = this.Category4Scores;
            targetMonth.Category5Scores = this.Category5Scores;
            targetMonth.Category6Scores = this.Category6Scores;
            targetMonth.Category7Scores = this.Category7Scores;
            targetMonth.Category8Scores = this.Category8Scores;
        }

        /// <inheritdoc/>
        public override void CopyValuesTo(DtoBase target)
        {
            if (target is Month targetMonth)
            {
                targetMonth.Category1Scores = this.Category1Scores;
                targetMonth.Category1Goal = this.Category1Goal;
                targetMonth.Category2Scores = this.Category2Scores;
                targetMonth.Category2Goal = this.Category2Goal;
                targetMonth.Category3Scores = this.Category3Scores;
                targetMonth.Category3Goal = this.Category3Goal;
                targetMonth.Category4Scores = this.Category4Scores;
                targetMonth.Category4Goal = this.Category4Goal;
                targetMonth.Category5Scores = this.Category5Scores;
                targetMonth.Category5Goal = this.Category5Goal;
                targetMonth.Category6Scores = this.Category6Scores;
                targetMonth.Category6Goal = this.Category6Goal;
                targetMonth.Category7Scores = this.Category7Scores;
                targetMonth.Category7Goal = this.Category7Goal;
                targetMonth.Category8Scores = this.Category8Scores;
                targetMonth.Category8Goal = this.Category8Goal;
                targetMonth.TotalGoal = this.TotalGoal;
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
                    return this.Category1Goal;
                case ExerciseCategory.Category2:
                    return this.Category2Goal;
                case ExerciseCategory.Category3:
                    return this.Category3Goal;
                case ExerciseCategory.Category4:
                    return this.Category4Goal;
                case ExerciseCategory.Category5:
                    return this.Category5Goal;
                case ExerciseCategory.Category6:
                    return this.Category6Goal;
                case ExerciseCategory.Category7:
                    return this.Category7Goal;
                case ExerciseCategory.Category8:
                    return this.Category8Goal;
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
                    return this.Category1Scores;
                case ExerciseCategory.Category2:
                    return this.Category2Scores;
                case ExerciseCategory.Category3:
                    return this.Category3Scores;
                case ExerciseCategory.Category4:
                    return this.Category4Scores;
                case ExerciseCategory.Category5:
                    return this.Category5Scores;
                case ExerciseCategory.Category6:
                    return this.Category6Scores;
                case ExerciseCategory.Category7:
                    return this.Category7Scores;
                case ExerciseCategory.Category8:
                    return this.Category8Scores;
                default:
                    return 0;
            }
        }

        /// <summary>
        ///     Setzt die Punkteziele des Monats auf 0.
        /// </summary>
        public void ResetGoals()
        {
            this.Category1Goal = 0;
            this.Category2Goal = 0;
            this.Category3Goal = 0;
            this.Category4Goal = 0;
            this.Category5Goal = 0;
            this.Category6Goal = 0;
            this.Category7Goal = 0;
            this.Category8Goal = 0;
            this.TotalGoal = 0;
        }

        /// <summary>
        ///     Setzt die erreichten Trainingspunkte des Monats auf 0.
        /// </summary>
        public void ResetScores()
        {
            this.Category1Scores = 0;
            this.Category2Scores = 0;
            this.Category3Scores = 0;
            this.Category4Scores = 0;
            this.Category5Scores = 0;
            this.Category6Scores = 0;
            this.Category7Scores = 0;
            this.Category8Scores = 0;
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
                    this.Category1Scores = value;
                    break;
                case ExerciseCategory.Category2:
                    this.Category2Scores = value;
                    break;
                case ExerciseCategory.Category3:
                    this.Category3Scores = value;
                    break;
                case ExerciseCategory.Category4:
                    this.Category4Scores = value;
                    break;
                case ExerciseCategory.Category5:
                    this.Category5Scores = value;
                    break;
                case ExerciseCategory.Category6:
                    this.Category6Scores = value;
                    break;
                case ExerciseCategory.Category7:
                    this.Category7Scores = value;
                    break;
                case ExerciseCategory.Category8:
                    this.Category8Scores = value;
                    break;
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
                    return this.GetPercentageFromGoalAndScores(this.Category1Goal, this.Category1Scores);
                case ExerciseCategory.Category2:
                    return this.GetPercentageFromGoalAndScores(this.Category2Goal, this.Category2Scores);
                case ExerciseCategory.Category3:
                    return this.GetPercentageFromGoalAndScores(this.Category3Goal, this.Category3Scores);
                case ExerciseCategory.Category4:
                    return this.GetPercentageFromGoalAndScores(this.Category4Goal, this.Category4Scores);
                case ExerciseCategory.Category5:
                    return this.GetPercentageFromGoalAndScores(this.Category5Goal, this.Category5Scores);
                case ExerciseCategory.Category6:
                    return this.GetPercentageFromGoalAndScores(this.Category6Goal, this.Category6Scores);
                case ExerciseCategory.Category7:
                    return this.GetPercentageFromGoalAndScores(this.Category7Goal, this.Category7Scores);
                case ExerciseCategory.Category8:
                    return this.GetPercentageFromGoalAndScores(this.Category8Goal, this.Category8Scores);
                default:
                    return 0;
            }
        }

        private int GetPercentageFromGoalAndScores(ushort goal, ushort scores) => goal == 0 ? 0 : scores < goal ? (int)Math.Round(scores * 100.0 / goal, 0) : 100;
    }
}
