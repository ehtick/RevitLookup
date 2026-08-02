using CommunityToolkit.Mvvm.ComponentModel;

namespace RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;

/// <summary>
///     Represents the sample data for the toggle switch gallery page.
/// </summary>
[UsedImplicitly]
public sealed partial class ToggleSwitchViewModel : ObservableObject
{
    /// <summary>
    ///     Gets or sets a value indicating whether the standard toggle switch sample is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool IsStandardToggleSwitchEnabled { get; set; } = true;
}