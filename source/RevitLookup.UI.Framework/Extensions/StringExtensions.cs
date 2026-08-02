namespace RevitLookup.UI.Framework.Extensions;

/// <summary>
///     Provides extension methods for string collections and arrays.
/// </summary>
[PublicAPI]
public static class StringExtensions
{
    /// <param name="source">The collection or array of strings to join. Null strings are treated as empty strings.</param>
    extension(IEnumerable<string?> source)
    {
        /// <summary>
        ///     Joins the elements of the source into a single string, separated by the specified separator.
        /// </summary>
        /// <param name="separator">The string to use as a separator between the joined elements.</param>
        /// <returns>A single concatenated string consisting of the elements in the source, separated by <paramref name="separator"/>.</returns>
        public string Join(string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        ///     Joins the elements of the source into a single string, separated by the specified separator character.
        /// </summary>
        /// <param name="separator">The character to use as a separator between the joined elements.</param>
        /// <returns>A single concatenated string consisting of the elements in the source, separated by <paramref name="separator"/>.</returns>
        public string Join(char separator)
        {
            return string.Join(separator, source);
        }
    }

    /// <param name="source">The array of strings to join. Null strings are treated as empty strings.</param>
    extension(string[] source)
    {
        /// <summary>
        ///     Joins the elements of the source into a single string, separated by the specified separator.
        /// </summary>
        /// <param name="separator">The string to use as a separator between the joined elements.</param>
        /// <returns>A single concatenated string consisting of the elements in the source, separated by <paramref name="separator"/>.</returns>
        public string Join(string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        ///     Joins the elements of the source into a single string, separated by the specified separator character.
        /// </summary>
        /// <param name="separator">The character to use as a separator between the joined elements.</param>
        /// <returns>A single concatenated string consisting of the elements in the source, separated by <paramref name="separator"/>.</returns>
        public string Join(char separator)
        {
            return string.Join(separator, source);
        }
    }
}
