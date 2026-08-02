using CommunityToolkit.Mvvm.ComponentModel;

namespace RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;

/// <summary>
///     Represents the sample data for the check box gallery page.
/// </summary>
[UsedImplicitly]
public sealed partial class CheckBoxViewModel : ObservableObject
{
    /// <summary>
    ///     Gets or sets a value indicating whether the standard check box sample is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool IsStandardCheckBoxEnabled { get; set; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether the three-state check box sample is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool IsThreeStateCheckBoxEnabled { get; set; } = true;
}