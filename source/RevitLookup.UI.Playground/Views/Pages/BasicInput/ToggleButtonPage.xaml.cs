using RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.BasicInput;

/// <summary>
///     Represents a page that demonstrates the ToggleButton control in the Playground.
/// </summary>
public sealed partial class ToggleButtonPage : INavigableView<ToggleButtonViewModel>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ToggleButtonPage" /> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public ToggleButtonPage(ToggleButtonViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    /// <inheritdoc />
    public ToggleButtonViewModel ViewModel { get; }
}
