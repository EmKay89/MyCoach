namespace MyCoach.Defines
{
    /// <summary>
    ///     Trainingsplantyp, ein Trainingsplan kann entweder zeitbasiert über bis zu 12 Monate
    ///     oder generisch für jeden Monat gleich sein.
    /// </summary>
    public enum ScheduleType
    {
        Generic = 1,
        TimeBased
    }
}
