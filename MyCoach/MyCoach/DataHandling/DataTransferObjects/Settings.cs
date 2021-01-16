using MyCoach.Defines;

namespace MyCoach.DataHandling.DataTransferObjects
{
    /// <summary>
    ///     Repräsentiert einen Einstellungssatz. 
    /// </summary>
    public class Settings : IDataTransferObject
    {
        /// <summary>
        ///     Ruft die Erlaubnis, eine Übung mehrfach in ein Training einzuplanen, auf, oder legt sie fest.
        /// </summary>
        public ExerciseSchedulingRepetitionPermission Permission { get; set; }

        /// <summary>
        ///     Ruft den prozentualen Multiplikator für die Wiederholungsanzahl einer Übung in Runde 1 ab, oder legt ihn fest.
        /// </summary>
        public ushort RepeatsRound1 { get; set; }

        /// <summary>
        ///     Ruft den prozentualen Multiplikator für das Absolvieren einer Übung in Runde 1 ab, oder legt ihn fest.
        /// </summary>
        public ushort ScoresRound1 { get; set; }

        /// <summary>
        ///     Ruft den prozentualen Multiplikator für die Wiederholungsanzahl einer Übung in Runde 2 ab, oder legt ihn fest.
        /// </summary>
        public ushort RepeatsRound2 { get; set; }

        /// <summary>
        ///     Ruft den prozentualen Multiplikator für das Absolvieren einer Übung in Runde 2 ab, oder legt ihn fest.
        /// </summary>
        public ushort ScoresRound2 { get; set; }

        /// <summary>
        ///     Ruft den prozentualen Multiplikator für die Wiederholungsanzahl einer Übung in Runde 3 ab, oder legt ihn fest.
        /// </summary>
        public ushort RepeatsRound3 { get; set; }

        /// <summary>
        ///     Ruft den prozentualen Multiplikator für das Absolvieren einer Übung in Runde 3 ab, oder legt ihn fest.
        /// </summary>
        public ushort ScoresRound3 { get; set; }

        /// <summary>
        ///     Ruft den prozentualen Multiplikator für die Wiederholungsanzahl einer Übung in Runde 4 ab, oder legt ihn fest.
        /// </summary>
        public ushort RepeatsRound4 { get; set; }

        /// <summary>
        ///     Ruft den prozentualen Multiplikator für das Absolvieren einer Übung in Runde 4 ab, oder legt ihn fest.
        /// </summary>
        public ushort ScoresRound4 { get; set; }
    }
}
