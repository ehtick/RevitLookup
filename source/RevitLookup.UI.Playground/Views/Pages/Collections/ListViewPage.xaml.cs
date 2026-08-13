using RevitLookup.UI.Playground.ViewModels.Pages.Collections;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.Collections;

/// <summary>
///     Represents a page that demonstrates the ListView control in the Playground.
/// </summary>
public sealed partial class ListViewPage : INavigableView<ListViewViewModel>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ListViewPage" /> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public ListViewPage(ListViewViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    /// <inheritdoc />
    public ListViewViewModel ViewModel { get; }
}
