using RevitLookup.UI.Playground.ViewModels.Pages.Navigation;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.Navigation;

/// <summary>
///     Represents a page that demonstrates the TabControl in the Playground.
/// </summary>
public sealed partial class TabControlPage : INavigableView<TabControlViewModel>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TabControlPage" /> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public TabControlPage(TabControlViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    /// <inheritdoc />
    public TabControlViewModel ViewModel { get; }
}
