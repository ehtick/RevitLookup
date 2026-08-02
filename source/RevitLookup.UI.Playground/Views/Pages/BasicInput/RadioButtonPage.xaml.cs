using RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.BasicInput;

/// <summary>
/// Represents a page that demonstrates the RadioButton control in the Playground.
/// </summary>
public sealed partial class RadioButtonPage : INavigableView<RadioButtonViewModel>
{
    /// <inheritdoc/>
    public RadioButtonViewModel ViewModel { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RadioButtonPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public RadioButtonPage(RadioButtonViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}