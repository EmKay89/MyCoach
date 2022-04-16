using MyCoach.Model.Defines;
using System;

namespace MyCoach.Model.DataTransferObjects
{
    /// <summary>
    ///     Repräsentiert die Eckdaten eines Trainingsplans. Der gesamte Plan setzt sich aus den Daten dieser Klasse und
    ///     den monatsbezogenen Punktezielen für die jeweiligen Kategorien zusammen, welche in der Klasse <see cref="Month"/>
    ///     gespeichert sind.
    /// </summary>
    public class TrainingSchedule : DtoBase, IDataTransferObject
    {
        private ScheduleType scheduleType;
        private DateTime startMonth;
        private ushort duration;

        /// <summary>
        ///     Ruft den Trainingsplantyp an, oder legt ihn fest.
        /// </summary>
        public ScheduleType ScheduleType
        {
            get => scheduleType;

            set
            {
                if (value == scheduleType)
                {
                    return;
                }

                scheduleType = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft den Startmonat des Trainingsplans auf, oder legt ihn fest.
        /// </summary>
        public DateTime StartMonth
        {
            get => startMonth;

            set
            {
                if (value == startMonth)
                {
                    return;
                }

                startMonth = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die Dauer des Trainingsplans in Monaten auf, oder legt sie fest.
        /// </summary>
        public ushort Duration
        {
            get => duration;

            set
            {
                if (value == duration)
                {
                    return;
                }

                duration = value;
                InvokePropertyChanged();
            }
        }

        /// <inheritdoc/>
        public override void CopyValuesTo(DtoBase target)
        {
            if (target is TrainingSchedule targetSchedule)
            {
                targetSchedule.ScheduleType = ScheduleType;
                targetSchedule.StartMonth = StartMonth;
                targetSchedule.Duration = Duration;
            }
        }

        /// <inheritdoc/>
        public override bool ValuesAreEqual(DtoBase dto)
        {
            return dto is TrainingSchedule schedule
                && this.ScheduleType == schedule.ScheduleType
                && this.StartMonth == schedule.StartMonth
                && this.Duration == schedule.Duration;
        }
    }
}
