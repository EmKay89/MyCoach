using MyCoach.Defines;

namespace MyCoach.DataHandling.DataTransferObjects
{
    /// <summary>
    ///     Repräsentiert die Eckdaten eines Trainingsplans. Der gesamte Plan setzt sich aus den Daten dieser Klasse und
    ///     den monatsbezogenen Punktezielen für die jeweiligen Kategorien zusammen, welche in der Klasse TrainingScore
    ///     gespeichert sind.
    /// </summary>
    public class TrainingSchedule : DtoBase
    {
        /// <summary>
        ///     Ruft den Startmonat des Trainingsplans auf, oder legt ihn fest.
        /// </summary>
        public Month StartMonth { get; set; }

        /// <summary>
        ///     Ruft das Startjahr des Trainingsplans auf, oder legt es fest.
        /// </summary>
        public ushort StartYear { get; set; }

        /// <summary>
        ///     Ruft die Dauer des Trainingsplans in Monaten auf, oder legt sie fest.
        /// </summary>
        public ushort Duration { get; set; }
    }
}
