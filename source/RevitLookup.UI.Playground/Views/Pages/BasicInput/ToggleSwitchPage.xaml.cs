using RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.BasicInput;

/// <summary>
///     Represents a page that demonstrates the ToggleSwitch control in the Playground.
/// </summary>
public sealed partial class ToggleSwitchPage : INavigableView<ToggleSwitchViewModel>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ToggleSwitchPage" /> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public ToggleSwitchPage(ToggleSwitchViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    /// <inheritdoc />
    public ToggleSwitchViewModel ViewModel { get; }
}
