using MyCoach.Defines;
using System;

namespace MyCoach.DataHandling.DataTransferObjects
{
    /// <summary>
    ///     Repräsentiert die Eckdaten eines Trainingsplans. Der gesamte Plan setzt sich aus den Daten dieser Klasse und
    ///     den monatsbezogenen Punktezielen für die jeweiligen Kategorien zusammen, welche in der Klasse <see cref="Month"/>
    ///     gespeichert sind.
    /// </summary>
    public class TrainingSchedule : DtoBase, IDataTransferObject
    {
        /// <summary>
        ///     Ruft den Trainingsplantyp an, oder legt ihn fest.
        /// </summary>
        public ScheduleType ScheduleType { get; set; }

        /// <summary>
        ///     Ruft den Startmonat des Trainingsplans auf, oder legt ihn fest.
        /// </summary>
        public DateTime StartMonth { get; set; }

        /// <summary>
        ///     Ruft die Dauer des Trainingsplans in Monaten auf, oder legt sie fest.
        /// </summary>
        public ushort Duration { get; set; }
    }
}
