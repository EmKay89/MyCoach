namespace MyCoach.Helpers.CustomTypes
{
    /// <summary>
    ///     Represents tuple with three values whose items are writeable.
    /// </summary>
    public class Triple<T1, T2, T3>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Triple{T1, T2, T3}"/> class.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        /// <param name="item3">The value of the tuple's third component.</param>
        public Triple(T1 item1, T2 item2, T3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        /// <summary>
        ///     Gets or sets the value of the current <see cref="Triple{T1, T2, T3}"/> object's first component.
        /// </summary>
        public T1 Item1 { get; set; }

        /// <summary>
        ///     Gets or sets the value of the current <see cref="Triple{T1, T2, T3}"/> object's second component.
        /// </summary>
        public T2 Item2 { get; set; }

        /// <summary>
        ///     Gets or sets the value of the current <see cref="Triple{T1, T2, T3}"/> object's third component.
        /// </summary>
        public T3 Item3 { get; set; }
    }
}
