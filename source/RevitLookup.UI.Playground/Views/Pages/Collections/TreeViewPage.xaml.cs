using RevitLookup.UI.Playground.ViewModels.Pages.Collections;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.Collections;

/// <summary>
///     Represents a page that demonstrates the TreeView control in the Playground.
/// </summary>
public sealed partial class TreeViewPage : INavigableView<TreeViewViewModel>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TreeViewPage" /> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public TreeViewPage(TreeViewViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    /// <inheritdoc />
    public TreeViewViewModel ViewModel { get; }
}
