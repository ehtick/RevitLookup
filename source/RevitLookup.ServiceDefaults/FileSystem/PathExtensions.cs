namespace RevitLookup.ServiceDefaults.FileSystem;

/// <summary>
///     Provides extension methods for <see cref="string" /> to combine file system paths.
/// </summary>
[PublicAPI]
public static class PathExtensions
{
    /// <param name="source">The path the other segments are combined onto.</param>
    extension(string source)
    {
        /// <summary>
        ///     Combines <paramref name="source" /> with <paramref name="path" /> into a single path.
        /// </summary>
        /// <param name="path">The path segment to append.</param>
        /// <returns>The combined path.</returns>
        /// <exception cref="ArgumentException"><paramref name="source" /> or <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="Path.GetInvalidPathChars" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="path" /> is <see langword="null" />.</exception>
        /// <remarks>If <paramref name="path" /> is a zero-length string, this method returns <paramref name="source" />. If <paramref name="path" /> is rooted, this method returns <paramref name="path" />.</remarks>
        [Pure]
        public string AppendPath(string path)
        {
            return Path.Combine(source, path);
        }

        /// <summary>
        ///     Combines <paramref name="source" /> with the specified path segments into a single path.
        /// </summary>
        /// <param name="paths">The path segments to append.</param>
        /// <returns>The combined path.</returns>
        /// <exception cref="ArgumentException"><paramref name="source" /> or one of <paramref name="paths" /> contains one or more of the invalid characters defined in <see cref="Path.GetInvalidPathChars" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> or one of <paramref name="paths" /> is <see langword="null" />.</exception>
        /// <remarks>If a segment is a zero-length string, this method skips it. If a segment is rooted, this method discards every preceding segment.</remarks>
        [Pure]
        public string AppendPath(params string[] paths)
        {
            var strings = new string[paths.Length + 1];
            strings[0] = source;
            for (var i = 1; i < strings.Length; i++)
            {
                strings[i] = paths[i - 1];
            }

            return Path.Combine(strings);
        }
    }
}
