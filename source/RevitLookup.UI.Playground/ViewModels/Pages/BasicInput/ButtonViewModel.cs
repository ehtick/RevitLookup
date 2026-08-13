using CommunityToolkit.Mvvm.ComponentModel;

namespace RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;

/// <summary>
///     Represents the sample data for the button gallery page.
/// </summary>
[UsedImplicitly]
public partial class ButtonViewModel : ObservableObject
{
    /// <summary>
    ///     Gets or sets a value indicating whether the standard button sample is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool IsStandardButtonEnabled { get; set; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether the primary button sample is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool IsPrimaryButtonEnabled { get; set; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether the secondary button sample is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool IsSecondaryButtonEnabled { get; set; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether the danger button sample is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool IsDangerButtonEnabled { get; set; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether the transparent button sample is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool IsTransparentButtonEnabled { get; set; } = true;
}
