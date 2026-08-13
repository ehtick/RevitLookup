using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace RevitLookup.UI.Playground.Converters;

/// <summary>
///     Converts a <see langword="null" /> value to <see cref="Visibility.Collapsed" />, and any other value to <see cref="Visibility.Visible" />.
/// </summary>
internal sealed class NullToVisibilityConverter : MarkupExtension, IValueConverter
{
    /// <inheritdoc />
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is null ? Visibility.Collapsed : Visibility.Visible;
    }

    /// <inheritdoc />
    /// <remarks>
    ///     This converter does not support two-way binding.
    /// </remarks>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
