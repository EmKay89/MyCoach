using MyCoach.Model.Defines;
using MyExtensions.IEnumerable;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyCoach.Model.DataTransferObjects
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

        /// <summary>
        ///     Ruft eine Liste mit vordefinierten Einheiten ab, die Übungen zugewiesen werden können (z.B. Wiederholungen oder Minuten).
        /// </summary>
        public ObservableCollection<string> Units { get; } = new ObservableCollection<string>();

        /// <inheritdoc/>
        public override void CopyValuesTo(DtoBase target)
        {
            if (target is Settings targetSettings)
            {
                targetSettings.Permission = Permission;
                targetSettings.RepeatsRound1 = RepeatsRound1;
                targetSettings.ScoresRound1 = ScoresRound1;
                targetSettings.RepeatsRound2 = RepeatsRound2;
                targetSettings.ScoresRound2 = ScoresRound2;
                targetSettings.RepeatsRound3 = RepeatsRound3;
                targetSettings.ScoresRound3 = ScoresRound3;
                targetSettings.RepeatsRound4 = RepeatsRound4;
                targetSettings.ScoresRound4 = ScoresRound4;
                targetSettings.Units.Clear();
                this.Units.ForEach(u => targetSettings.Units.Add(u));
            }
        }

        /// <inheritdoc/>
        public override bool ValuesAreEqual(DtoBase dto)
        {
            return dto is Settings settings
                && this.Permission == settings.Permission
                && this.RepeatsRound1 == settings.RepeatsRound1
                && this.ScoresRound1 == settings.ScoresRound1
                && this.RepeatsRound2 == settings.RepeatsRound2
                && this.ScoresRound2 == settings.ScoresRound2
                && this.RepeatsRound3 == settings.RepeatsRound3
                && this.ScoresRound3 == settings.ScoresRound3
                && this.RepeatsRound4 == settings.RepeatsRound4
                && this.ScoresRound4 == settings.ScoresRound4
                && this.Units.Count == settings.Units.Count
                && this.Units.TrueForAll(u => settings.Units[this.Units.IndexOf(u)] == u);
        }
    }
}
