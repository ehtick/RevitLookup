// Copyright (c) Lookup Foundation and Contributors
// 
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
// 
// THIS PROGRAM IS PROVIDED "AS IS" AND WITH ALL FAULTS.
// NO IMPLIED WARRANTY OF MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE IS PROVIDED.
// THERE IS NO GUARANTEE THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.

using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RevitLookup.UI.Framework.Converters;

/// <summary>
///     Provides <see cref="IMultiValueConverter" /> instances that show the settings empty-state placeholder.
/// </summary>
public static class SettingsVisibilityConverters
{
    /// <summary>
    ///     Gets a converter that shows the placeholder when a search yields no results.
    /// </summary>
    /// <remarks>
    ///     Expects exactly three bound values: the result collection, the search text length, and a flag indicating whether search is active.
    /// </remarks>
    public static IMultiValueConverter VisibleWhenEmptySearchResults { get; } = new VisibleWhenEmptySearchResultsConverter();

    /// <summary>
    ///     Gets a converter that shows the placeholder once initialization has completed with no items.
    /// </summary>
    /// <remarks>
    ///     Expects exactly two bound values: the item count and a flag indicating whether initialization has completed.
    /// </remarks>
    public static IMultiValueConverter VisibleWhenEmptyAfterInitialization { get; } = new VisibleWhenEmptyAfterInitializationConverter();

    private sealed class VisibleWhenEmptySearchResultsConverter : IMultiValueConverter
    {
        /// <inheritdoc />
        /// <exception cref="ArgumentException"><paramref name="values" /> does not contain exactly three elements.</exception>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 3)
            {
                throw new ArgumentException("Invalid parameters");
            }

            var items = (ICollection)values[0]!;
            if (items.Count > 0)
            {
                return Visibility.Collapsed;
            }

            if (values[1] is > 0)
            {
                return Visibility.Collapsed;
            }

            if (values[2] is false)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    private sealed class VisibleWhenEmptyAfterInitializationConverter : IMultiValueConverter
    {
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is not int collectionSize)
            {
                return Visibility.Collapsed;
            }

            if (values[1] is not bool isInitialized)
            {
                return Visibility.Collapsed;
            }

            if (!isInitialized)
            {
                return Visibility.Collapsed;
            }

            if (collectionSize > 0)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
