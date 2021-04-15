using MyCoach.Defines;
using System;

namespace MyCoach.DataHandling.DataTransferObjects
{
    /// <summary>
    ///     Repräsentiert Istwerte und Ziele für das Erreichen von Trainingspunkten der jeweiligen Übungskategorien bezogen auf einen Monat.
    /// </summary>
    public class Month : DtoBase, IDataTransferObject
    {
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
        ///     Gibt das Startdatum des Monats berechnet aus dem Startdatum eines Trainingsplans zurück.
        /// </summary>
        /// <param name="schedule">Der Trainingsplan, dessen Startdaum zur Berechnung verwendet wird.</param>
        /// <returns>Das Startdatum des Monats (erster Tag um 0:00:00 Uhr).</returns>
        public DateTime GetStartDateFromSchedule(TrainingSchedule schedule)
        {
            if (this.Number == MonthNumber.Current)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }

            return schedule.StartMonth.AddMonths((int)this.Number - 1);
        }
    }
}
