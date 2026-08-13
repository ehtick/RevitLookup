using RevitLookup.UI.Playground.ViewModels.Pages.Navigation;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.Navigation;

/// <summary>
///     Represents a page that demonstrates the BreadcrumbBar control in the Playground.
/// </summary>
public sealed partial class BreadcrumbBarPage : INavigableView<BreadcrumbBarViewModel>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="BreadcrumbBarPage" /> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public BreadcrumbBarPage(BreadcrumbBarViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    /// <inheritdoc />
    public BreadcrumbBarViewModel ViewModel { get; }
}
