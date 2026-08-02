using RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.BasicInput;

/// <summary>
/// Represents a page that demonstrates the ComboBox control in the Playground.
/// </summary>
public sealed partial class ComboBoxPage : INavigableView<ComboBoxViewModel>
{
    /// <inheritdoc/>
    public ComboBoxViewModel ViewModel { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ComboBoxPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public ComboBoxPage(ComboBoxViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}