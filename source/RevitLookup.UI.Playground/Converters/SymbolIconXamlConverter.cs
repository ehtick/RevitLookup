using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using Wpf.Ui.Controls;

namespace RevitLookup.UI.Playground.Converters;

/// <summary>
///     Converts a <see cref="SymbolRegular"/> value and a filled flag to the inline XAML markup for a <c>ui:SymbolIcon</c>.
/// </summary>
public sealed class SymbolIconXamlConverter : MarkupExtension, IMultiValueConverter
{
    /// <inheritdoc/>
    /// <remarks>
    ///     Returns <paramref name="values"/>[0] unchanged when it is already a <see cref="string"/>; otherwise builds
    ///     <c>ui:SymbolIcon</c> markup from <paramref name="values"/>[0] as a <see cref="SymbolRegular"/> and <paramref name="values"/>[1] as the filled flag.
    /// </remarks>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is string text) return text;

        var icon = (SymbolRegular)values[0];
        var filled = (bool)values[1];

        var builder = new StringBuilder();
        builder.Append("<ui:SymbolIcon Symbol=\"");
        builder.Append(icon);
        builder.Append('"');
        if (filled)
        {
            builder.Append(" Filled=\"");
            builder.Append(filled);
            builder.Append('"');
        }

        builder.Append(" />");

        return builder.ToString();
    }

    /// <inheritdoc/>
    /// <remarks>
    ///     This converter does not support two-way binding.
    /// </remarks>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc/>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
