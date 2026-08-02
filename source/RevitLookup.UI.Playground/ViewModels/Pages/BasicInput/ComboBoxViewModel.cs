using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;

/// <summary>
///     Represents the sample data for the combo box gallery page.
/// </summary>
[UsedImplicitly]
public sealed partial class ComboBoxViewModel : ObservableObject
{
    /// <summary>
    ///     Gets or sets a value indicating whether the standard combo box sample is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool IsStandardComboBoxEnabled { get; set; } = true;

    /// <summary>
    ///     Gets the sample items shown in the combo box.
    /// </summary>
    public ObservableCollection<string> Items { get; } =
    [
        "Item 1",
        "Item 2",
        "Item 3",
        "Item 4",
        "Item 5"
    ];
}