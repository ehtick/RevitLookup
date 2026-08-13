using RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.BasicInput;

/// <summary>
///     Represents a page that demonstrates the HyperlinkButton control in the Playground.
/// </summary>
public sealed partial class HyperlinkButtonPage : INavigableView<HyperlinkButtonViewModel>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HyperlinkButtonPage" /> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public HyperlinkButtonPage(HyperlinkButtonViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    /// <inheritdoc />
    public HyperlinkButtonViewModel ViewModel { get; }
}
