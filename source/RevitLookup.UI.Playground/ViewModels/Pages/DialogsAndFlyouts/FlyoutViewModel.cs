using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RevitLookup.UI.Playground.ViewModels.Pages.DialogsAndFlyouts;

/// <summary>
///     Represents the sample data for the flyout gallery page.
/// </summary>
[UsedImplicitly]
public partial class FlyoutViewModel : ObservableObject
{
    /// <summary>
    ///     Gets or sets a value indicating whether the standard flyout sample is open.
    /// </summary>
    [ObservableProperty]
    public partial bool IsStandardFlyoutOpen { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the right-aligned flyout sample is open.
    /// </summary>
    [ObservableProperty]
    public partial bool IsRightFlyoutOpen { get; set; }

    [RelayCommand]
    private void OnStandardButtonClick()
    {
        if (!IsStandardFlyoutOpen)
        {
            IsStandardFlyoutOpen = true;
        }
    }

    [RelayCommand]
    private void OnRightButtonClick()
    {
        if (!IsRightFlyoutOpen)
        {
            IsRightFlyoutOpen = true;
        }
    }
}
