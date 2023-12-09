using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCoach.Helpers.Extensions.IEnumerable
{
    /// <summary>
    ///     Extension methods to <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class IEnumerableExtensions
    {
        private static readonly Random rnd = new Random();

        /// <summary>
        ///     Performs a given action on each element of an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="collection"/>.</typeparam>
        /// <param name="collection">The <see cref="IEnumerable{T}"/> on whose elements the given action should be performed.</param>
        /// <param name="action">The action to perform.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown, if <paramref name="collection"/> is null.
        /// </exception>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (var element in collection)
            {
                action(element);
            }
        }

        /// <summary>
        ///     Returns a randomly selected element of an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="collection"/>.</typeparam>
        /// <param name="collection">The <see cref="IEnumerable{T}"/> to select a radom element from.</param>
        /// <returns>
        ///     A randomly selected element or the default type of <typeparamref name="T"/> if the list is empty.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown, if <paramref name="collection"/> is null.
        /// </exception>
        public static T GetRandom<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Count() == 0)
            {
                return default;
            }

            var index = rnd.Next(0, collection.Count());
            return collection.ElementAt(index);
        }

        /// <summary>
        ///     Returns, if an <see cref="IEnumerable{T}"/> is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="collection"/>.</typeparam>
        /// <param name="collection">The <see cref="IEnumerable{T}"/> to check for null or emptiness.</param>
        /// <returns>
        ///     True, if the given <see cref="IEnumerable{T}"/> is null or empty, otherwise false.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || collection.Count() == 0;

        /// <summary>
        ///     Removes all elements from an <see cref="IEnumerable{T}"/> that match a given predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="collection"/>.</typeparam>
        /// <param name="collection">The <see cref="IEnumerable{T}"/> to remove elements from.</param>
        /// <param name="predicate">The given predicate that defines which elements should be removed.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown, if <paramref name="collection"/> or <paramref name="predicate"/> is null.
        /// </exception>
        public static void RemoveAll<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            collection = collection.Where(element => predicate(element) == false);
        }

        /// <summary>
        ///     Performs a check defined by a given predicate on each element of an <see cref="IEnumerable{T}"/>
        ///     and returns, if the result of each check is true.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="collection"/>.</typeparam>
        /// <param name="collection">The <see cref="IEnumerable{T}"/> whose elements should be checked.</param>
        /// <param name="predicate">The given predicate that defines the check to be performed.</param>
        /// <returns>
        ///     True, if either each check was successful or the specified collection is empty, otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown, if <paramref name="collection"/> or <paramref name="predicate"/> is null.
        /// </exception>
        public static bool TrueForAll<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (var element in collection)
            {
                if (predicate(element) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
