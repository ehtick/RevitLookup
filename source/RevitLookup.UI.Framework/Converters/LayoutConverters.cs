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

using System.Globalization;
using System.Windows.Data;

namespace RevitLookup.UI.Framework.Converters;

/// <summary>
///     Provides <see cref="IValueConverter" /> instances that compute a layout dimension from an available size.
/// </summary>
public static class LayoutConverters
{
    /// <summary>
    ///     Gets a converter that computes a uniform grid column count from an available width.
    /// </summary>
    public static IValueConverter UniformColumnsByWidth { get; } = new UniformColumnsByWidthConverter();

    private sealed class UniformColumnsByWidthConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <remarks>
        ///     Divides the available width into columns of 400 device-independent units and always returns at least 1.
        /// </remarks>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var width = (double)value!;
            var columns = (int)Math.Floor(width / 400d);
            return columns > 0 ? columns : 1;
        }

        /// <inheritdoc />
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
