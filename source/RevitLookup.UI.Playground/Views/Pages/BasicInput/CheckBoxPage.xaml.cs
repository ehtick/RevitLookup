using RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.BasicInput;

/// <summary>
///     Represents a page that demonstrates the CheckBox control in the Playground.
/// </summary>
public sealed partial class CheckBoxPage : INavigableView<CheckBoxViewModel>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CheckBoxPage" /> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public CheckBoxPage(CheckBoxViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    /// <inheritdoc />
    public CheckBoxViewModel ViewModel { get; }
}
