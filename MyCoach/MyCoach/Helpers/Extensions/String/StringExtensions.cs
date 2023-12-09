namespace MyCoach.Helpers.Extensions.String
{
    /// <summary>
    ///     Extension methods to <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Returns, if a <see cref="string"/> is null or empty.
        /// </summary>
        /// <param name="stringToCheck">The <see cref="string"/> to check.</param>
        /// <returns>
        ///     True, if the <see cref="string"/> is null or empty, otherwise false.
        /// </returns>
        public static bool IsNullOrEmpty(this string stringToCheck) => stringToCheck == null || stringToCheck == string.Empty;
    }
}
