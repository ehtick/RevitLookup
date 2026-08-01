using System.IO;

namespace RevitLookup.UI.Playground.Extensions;


/// <summary>
///     System.String Extensions
/// </summary>
public static class SystemExtensions
{
    /// <param name="obj">The object to cast.</param>
    extension(object obj)
    {
        /// <summary>
        ///     Converts an object's type to <typeparamref name="T"/> type
        /// </summary>
        [Pure]
        public T Cast<T>()
        {
            return (T)obj;
        }
    }

    /// <param name="source">The leading path to combine.</param>
    extension(string source)
    {
        /// <summary>
        ///     Combines strings into a path
        /// </summary>
        /// <returns>
        ///     The combined paths.
        ///     If one of the specified paths is a zero-length string, this method returns the other path.
        ///     If path2 contains an absolute path, this method returns path2.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        ///     NET Framework and .NET Core versions older than 2.1: path1 or path2 contains one or more of the invalid characters defined in <see cref="Path.GetInvalidPathChars" />
        /// </exception>
        /// <exception cref="System.ArgumentNullException">source or path is null</exception>
        [Pure]
        public string AppendPath(string path)
        {
            return Path.Combine(source, path);
        }
    }
}