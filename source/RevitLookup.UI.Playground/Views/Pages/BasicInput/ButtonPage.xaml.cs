using RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.BasicInput;

/// <summary>
/// Represents a page that demonstrates the Button control in the Playground.
/// </summary>
public sealed partial class ButtonPage : INavigableView<ButtonViewModel>
{
    /// <inheritdoc/>
    public ButtonViewModel ViewModel { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public ButtonPage(ButtonViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}