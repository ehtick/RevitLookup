using RevitLookup.UI.Playground.ViewModels.Pages.Collections;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.Collections;

/// <summary>
///     Represents a page that demonstrates the ListBox control in the Playground.
/// </summary>
public sealed partial class ListBoxPage : INavigableView<ListBoxViewModel>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ListBoxPage" /> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public ListBoxPage(ListBoxViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    /// <inheritdoc />
    public ListBoxViewModel ViewModel { get; }
}
