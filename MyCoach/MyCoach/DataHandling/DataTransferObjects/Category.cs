using MyCoach.Defines;

namespace MyCoach.DataHandling.DataTransferObjects
{
    /// <summary>
    ///     Repräsentiert eine Übungskategorie. Übungen können in Typen unterteilt werden, die Typen wiederum in Kategorien. Die Typen
    ///     WarmUp und CoolDown haben jeweils nur eine gleichnamige Kategorie.
    /// </summary>
    public class Category : DtoBase, IDataTransferObject
    {
        /// <summary>
        ///     Ruft den eindeutigen Identifizierer der Übungskategorie auf oder legt ihn fest.
        /// </summary>
        public ExerciseCategory ID { get; set; }

        /// <summary>
        ///     Ruft den Namen der Übungskategorie auf oder legt ihn fest.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Ruft den Übungstyp auf oder legt ihn fest.
        /// </summary>
        public ExerciseType Type { get; set; }

        /// <summary>
        ///     Ruft die Anzahl der Übungen der Übungskategorie in einem Training auf oder legt sie fest. Dieser Wert ist nur
        ///     relevant für die Übungstypen WarmUp und CoolDown. Für den Typ Training wird der Wert in der GUI beim Start des
        ///     Trainings festgelegt.
        /// </summary>
        public ushort Count { get; set; }

        /// <summary>
        ///     Gibt an, ob eine Übungskategorie aktiv ist. Nur Übungen einer aktiven Kategorie werden in ein Training eingeplant.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        ///     <see cref="ToString"/> Methode ist überschrieben und gibt den Namen der Kategorie zurück.
        /// </summary>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
