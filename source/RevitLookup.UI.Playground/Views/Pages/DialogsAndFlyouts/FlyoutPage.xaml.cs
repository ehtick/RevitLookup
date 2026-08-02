using RevitLookup.UI.Playground.ViewModels.Pages.DialogsAndFlyouts;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.DialogsAndFlyouts;

/// <summary>
/// Represents a page that demonstrates the Flyout control in the Playground.
/// </summary>
public sealed partial class FlyoutPage : INavigableView<FlyoutViewModel>
{
    /// <inheritdoc/>
    public FlyoutViewModel ViewModel { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FlyoutPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public FlyoutPage(FlyoutViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}