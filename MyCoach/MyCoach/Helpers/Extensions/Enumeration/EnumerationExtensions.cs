using System;

namespace MyCoach.Helpers.Extensions.Enumeration
{
    /// <summary>
    ///     Extension methods to Enumerations.
    /// </summary>
    public static class EnumerationExtensions
    {
        /// <summary>
        ///     Returns the value after the given value of an enumeration or the first value of the enumeration, if the given value is the last one.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="enum">The given enumeration value.</param>
        /// <exception cref="ArgumentException">
        ///     Thrown, if the type of the given value is not an enumeration.
        /// </exception>
        public static T Next<T>(this T @enum) where T : struct
        {
            if (typeof(T).IsEnum == false)
            {
                throw new ArgumentException($"'{@enum}' is not an Enum.");
            }

            T[] array = (T[])Enum.GetValues(@enum.GetType());
            int j = Array.IndexOf(array, @enum) + 1;
            return j == array.Length ? array[0] : array[j];
        }

        /// <summary>
        ///     Returns the value before the given value of an enumeration or the last value of the enumeration, if the given value is the first one.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="enum">The given enumeration value.</param>
        /// <exception cref="ArgumentException">
        ///     Thrown, if the type of the given value is not an enumeration.
        /// </exception>
        public static T Previous<T>(this T @enum) where T : struct
        {
            if (typeof(T).IsEnum == false)
            {
                throw new ArgumentException($"'{@enum}' is not an Enum.");
            }

            T[] array = (T[])Enum.GetValues(@enum.GetType());
            int j = Array.IndexOf(array, @enum) - 1;
            return j < 0 ? array[array.Length - 1] : array[j];
        }
    }
}
