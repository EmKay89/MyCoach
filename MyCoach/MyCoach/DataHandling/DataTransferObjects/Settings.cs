using MyCoach.Defines;

namespace MyCoach.DataHandling.DataTransferObjects
{
    /// <summary>
    ///     Repräsentiert einen Einstellungssatz. 
    /// </summary>
    public class Settings : DtoBase, IDataTransferObject
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

        /// <inheritdoc/>
        public override void CopyValuesTo(DtoBase target)
        {
            if (target is Settings targetSettings)
            {
                targetSettings.Permission = this.Permission;
                targetSettings.RepeatsRound1 = this.RepeatsRound1;
                targetSettings.ScoresRound1 = this.ScoresRound1;
                targetSettings.RepeatsRound2 = this.RepeatsRound2;
                targetSettings.ScoresRound2 = this.ScoresRound2;
                targetSettings.RepeatsRound3 = this.RepeatsRound3;
                targetSettings.ScoresRound3 = this.ScoresRound3;
                targetSettings.RepeatsRound4 = this.RepeatsRound4;
                targetSettings.ScoresRound4 = this.ScoresRound4;
            }          
        }
    }
}
