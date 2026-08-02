using CommunityToolkit.Mvvm.ComponentModel;

namespace RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;

/// <summary>
///     Represents the sample data for the radio button gallery page.
/// </summary>
[UsedImplicitly]
public sealed partial class RadioButtonViewModel : ObservableObject
{
    /// <summary>
    ///     Gets or sets a value indicating whether the standard radio button sample is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool IsStandardRadioButtonEnabled { get; set; } = true;
}