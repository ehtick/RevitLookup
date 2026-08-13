using CommunityToolkit.Mvvm.ComponentModel;

namespace RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;

/// <summary>
///     Represents the sample data for the toggle button gallery page.
/// </summary>
[UsedImplicitly]
public sealed partial class ToggleButtonViewModel : ObservableObject
{
    /// <summary>
    ///     Gets or sets a value indicating whether the standard toggle button sample is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool IsStandardToggleButtonEnabled { get; set; } = true;
}
