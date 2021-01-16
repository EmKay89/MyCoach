using MyCoach.Defines;

namespace MyCoach.DataHandling.DataTransferObjects
{
    /// <summary>
    ///     Repräsentiert eine Übung.
    /// </summary>
    public class Exercise : IDataTransferObject
    {
        /// <summary>
        ///     Ruft den Übungstyp auf oder legt ihn fest.
        /// </summary>
        public ExerciseType Type { get; set; }

        /// <summary>
        ///     Ruft die Übungskategorie auf oder legt sie fest.
        /// </summary>
        public ushort Category { get; set; }

        /// <summary>
        ///     Ruft die standardmäßige Anzahl der Wiederholungen einer Übung in einem Training auf oder legt sie fest.
        /// </summary>
        public ushort Count { get; set; }

        /// <summary>
        ///     Ruft den Namen der Übung auf, oder legt ihn fest.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Ruft die Übungskategorie, auf die sich die Übung bezieht, auf, oder legt sie fest. Übungen der Kategorien 
        ///     WarmUp oder CoolDown können sich auf eine Kategorie des Typs Training beziehen. Übungen des Typs Training
        ///     können sich nur auf ihre eigene Kategorie beziehen.
        /// </summary>
        public ushort RelatedCategory { get; set; }

        /// <summary>
        ///     Ruft die standardmäßige Anzahl an Punkten auf, die für das Absolvieren einer Übung vergeben wird, oder legt sie fest.
        /// </summary>
        public ushort Scores { get; set; }

        /// <summary>
        ///     Ruft eine Beschreibung der Übung auf, oder legt sie fest.
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        ///     Gibt an, ob eine Übung aktiv ist. Nur aktive Übungen werden in ein Training eingeplant.
        /// </summary>
        public bool Active { get; set; }
    }
}
