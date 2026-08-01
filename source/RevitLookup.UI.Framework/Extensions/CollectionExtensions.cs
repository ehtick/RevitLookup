using System.Collections.ObjectModel;

namespace RevitLookup.UI.Framework.Extensions;

public static class CollectionExtensions
{
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1"/> source to convert.</param>
    extension<T>(IEnumerable<T> source)
    {
        /// <summary>
        /// Converts an <see cref="T:System.Collections.Generic.IEnumerable`1"/> to an <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> containing the elements of the source.</returns>
        public ObservableCollection<T> ToObservableCollection()
        {
            return new ObservableCollection<T>(source);
        }
    }

    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The <see cref="T:System.Collections.Generic.List`1"/> source to convert.</param>
    extension<T>(List<T> source)
    {
        /// <summary>
        /// Converts an <see cref="T:System.Collections.Generic.List`1"/> to an <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> containing the elements of the source.</returns>
        public ObservableCollection<T> ToObservableCollection()
        {
            return new ObservableCollection<T>(source);
        }
    }
}