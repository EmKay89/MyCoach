using MyCoach.DataHandling.DataTransferObjects;

namespace MyCoach.Defines
{
    /// <summary>
    ///     Repräsentiert die Nummer eines Monats im Trainigsplan beginnend mit 1 ab dem Monat gespeichert in
    ///     <see cref="TrainingSchedule.StartMonth"/>. Der Monat 0 repräsentiert den aktuellen Monat, der
    ///     nicht zwangsläufig im Trainingsplanzeitraum liegen muss.
    /// </summary>
    public enum MonthNumber
    {
        Current = 0,
        Month1,
        Month2,
        Month3,
        Month4,
        Month5,
        Month6,
        Month7,
        Month8,
        Month9,
        Month10,
        Month11,
        Month12
    }
}