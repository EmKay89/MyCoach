using MyCoach.Defines;

namespace MyCoach.DataHandling.DataTransferObjects
{
    /// <summary>
    ///     Repräsentiert Istwerte und Ziele für das Erreichen von Trainingspunkten der jeweiligen Übungskategorien bezogen auf einen Monat.
    /// </summary>
    public class Month : DtoBase, IDataTransferObject
    {
        /// <summary>
        ///     Ruft den Bezugsmonat auf, oder legt ihn fest.
        /// </summary>
        public MonthNumber Number { get; set; }

        /// <summary>
        ///     Ruft die für Kategorie 1 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category1Scores { get; set; }

        /// <summary>
        ///     Ruft das für Kategorie 1 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category1Goal { get; set; }

        /// <summary>
        ///     Ruft die für Kategorie 2 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category2Scores { get; set; }

        /// <summary>
        ///     Ruft das für Kategorie 2 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category2Goal { get; set; }

        /// <summary>
        ///     Ruft die für Kategorie 3 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category3Scores { get; set; }

        /// <summary>
        ///     Ruft das für Kategorie 3 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category3Goal { get; set; }

        /// <summary>
        ///     Ruft die für Kategorie 4 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category4Scores { get; set; }

        /// <summary>
        ///     Ruft das für Kategorie 4 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category4Goal { get; set; }

        /// <summary>
        ///     Ruft die für Kategorie 5 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category5Scores { get; set; }

        /// <summary>
        ///     Ruft das für Kategorie 5 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category5Goal { get; set; }

        /// <summary>
        ///     Ruft die für Kategorie 6 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category6Scores { get; set; }

        /// <summary>
        ///     Ruft das für Kategorie 6 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category6Goal { get; set; }

        /// <summary>
        ///     Ruft die für Kategorie 7 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category7Scores { get; set; }

        /// <summary>
        ///     Ruft das für Kategorie 7 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category7Goal { get; set; }

        /// <summary>
        ///     Ruft die für Kategorie 8 erreichten Punkte auf, oder legt sie fest.
        /// </summary>
        public ushort Category8Scores { get; set; }

        /// <summary>
        ///     Ruft das für Kategorie 8 gesetzte Punkteziel auf, oder legt es fest.
        /// </summary>
        public ushort Category8Goal { get; set; }

        /// <summary>
        ///     Ruft die für alle Kategorien erreichten Punkte auf.
        /// </summary>
        public int TotalScores { get => this.Category1Scores
                + this.Category2Scores
                + this.Category3Scores
                + this.Category4Scores
                + this.Category5Scores
                + this.Category6Scores
                + this.Category7Scores
                + this.Category8Scores;
        }

        /// <summary>
        ///     Ruft das Punkteziel für alle Monate ab, oder legt es fest.
        /// </summary>
        public ushort TotalGoal { get; set; }
    }
}
