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
        private ScheduleType scheduleType;
        private DateTime startMonth;
        private ushort duration;

        /// <summary>
        ///     Ruft den Trainingsplantyp an, oder legt ihn fest.
        /// </summary>
        public ScheduleType ScheduleType
        {
            get => this.scheduleType;

            set
            {
                if (value == this.scheduleType)
                {
                    return;
                }

                this.scheduleType = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft den Startmonat des Trainingsplans auf, oder legt ihn fest.
        /// </summary>
        public DateTime StartMonth
        {
            get => this.startMonth;

            set
            {
                if (value == this.startMonth)
                {
                    return;
                }

                this.startMonth = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die Dauer des Trainingsplans in Monaten auf, oder legt sie fest.
        /// </summary>
        public ushort Duration
        {
            get => this.duration; 
            
            set
            {
                if (value == this.duration)
                {
                    return;
                }

                this.duration = value;
                this.InvokePropertyChanged();
            }
        }
    }
}
