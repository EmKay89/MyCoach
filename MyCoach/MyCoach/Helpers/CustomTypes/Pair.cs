namespace MyCoach.Helpers.CustomTypes
{
    /// <summary>
    ///     Represents a tuple with two values whose items are writeable.
    /// </summary>
    public class Pair<T1, T2>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Pair{T1, T2}"/> class.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        public Pair(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        /// <summary>
        ///     Gets or sets the value of the current <see cref="Pair{T1, T2}"/> object's first component.
        /// </summary>
        public T1 Item1 { get; set; }

        /// <summary>
        ///     Gets or sets the value of the current <see cref="Pair{T1, T2}"/> object's second component.
        /// </summary>
        public T2 Item2 { get; set; }
    }
}
