using CommunityToolkit.Mvvm.ComponentModel;

namespace RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;

/// <summary>
///     Represents the sample data for the hyperlink button gallery page.
/// </summary>
[UsedImplicitly]
public sealed partial class HyperlinkButtonViewModel : ObservableObject
{
    /// <summary>
    ///     Gets or sets a value indicating whether the standard hyperlink button sample is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool IsStandardButtonEnabled { get; set; } = true;
}
