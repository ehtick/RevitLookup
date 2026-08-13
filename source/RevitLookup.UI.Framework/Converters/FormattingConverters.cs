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
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace RevitLookup.UI.Framework.Converters;

/// <summary>
///     Provides <see cref="IValueConverter" /> instances that format a value into its display text.
/// </summary>
public static class FormattingConverters
{
    /// <summary>
    ///     Gets a converter that formats an <see cref="ApplicationTheme" /> into its display text.
    /// </summary>
    public static IValueConverter ApplicationThemeDisplayText { get; } = new ApplicationThemeDisplayTextConverter();

    /// <summary>
    ///     Gets a converter that formats a <see cref="WindowBackdropType" /> into its display text.
    /// </summary>
    public static IValueConverter BackgroundTypeDisplayText { get; } = new BackgroundTypeDisplayTextConverter();

    /// <summary>
    ///     Gets a converter that formats a byte count into a human-readable size string.
    /// </summary>
    public static IValueConverter BytesDisplayText { get; } = new BytesDisplayTextConverter();

    /// <summary>
    ///     Gets a converter that formats a duration in milliseconds into a human-readable time string.
    /// </summary>
    public static IValueConverter TimeDisplayText { get; } = new TimeDisplayTextConverter();

    private sealed class ApplicationThemeDisplayTextConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <exception cref="NotSupportedException"><paramref name="value" /> is <see cref="ApplicationTheme.Unknown" />.</exception>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var applicationTheme = (ApplicationTheme)value!;
            return applicationTheme switch
            {
                ApplicationTheme.Auto => "Auto",
                ApplicationTheme.Light => "Light",
                ApplicationTheme.Dark => "Dark",
                ApplicationTheme.HighContrast => "High contrast",
                ApplicationTheme.Unknown => throw new NotSupportedException(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        /// <inheritdoc />
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    private sealed class BackgroundTypeDisplayTextConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var backgroundType = (WindowBackdropType)value!;
            return backgroundType switch
            {
                WindowBackdropType.None => "Disabled",
                WindowBackdropType.Acrylic => "Acrylic",
                WindowBackdropType.Tabbed => "Blur",
                WindowBackdropType.Mica => "Mica",
                WindowBackdropType.Auto => "Windows default",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        /// <inheritdoc />
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    private sealed class BytesDisplayTextConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var bytes = (long)value!;
            return bytes switch
            {
                0 => string.Empty,
                < 1_000 => $"{value} B",
                < 1_000_000 => $"{bytes / 1000d:F3} KB",
                _ => $"{bytes / 1_000_000d:F3} MB"
            };
        }

        /// <inheritdoc />
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    private sealed class TimeDisplayTextConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var milliseconds = (double)value!;
            return milliseconds switch
            {
                0 => string.Empty,
                < 1e-3 => "0.001 ms",
                < 10 => $"{milliseconds:F3} ms",
                < 100 => $"{milliseconds:F2} ms",
                < 1000 => $"{milliseconds:F1} ms",
                _ => $"{milliseconds:0} ms"
            };
        }

        /// <inheritdoc />
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
