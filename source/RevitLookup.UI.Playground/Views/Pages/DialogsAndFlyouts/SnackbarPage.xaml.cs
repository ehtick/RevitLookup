using RevitLookup.UI.Playground.ViewModels.Pages.DialogsAndFlyouts;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.DialogsAndFlyouts;

/// <summary>
///     Represents a page that demonstrates the Snackbar control in the Playground.
/// </summary>
public sealed partial class SnackbarPage : INavigableView<SnackbarViewModel>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SnackbarPage" /> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public SnackbarPage(SnackbarViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    /// <inheritdoc />
    public SnackbarViewModel ViewModel { get; }
}
