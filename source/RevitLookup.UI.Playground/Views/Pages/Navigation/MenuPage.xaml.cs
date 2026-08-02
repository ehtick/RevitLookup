using System.Windows;
using System.Windows.Controls;
using RevitLookup.UI.Playground.ViewModels.Pages.Navigation;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.Navigation;

/// <summary>
/// Represents a page that demonstrates the Menu control in the Playground.
/// </summary>
public sealed partial class MenuPage : INavigableView<MenuViewModel>
{
    /// <inheritdoc/>
    public MenuViewModel ViewModel { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public MenuPage(MenuViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    private void OnMenuItemClicked(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem menuItem)
        {
            if (e.OriginalSource is MenuItem originalMenuItem && originalMenuItem == menuItem)
            {
                StatusMenuItem.Visibility = Visibility.Visible;
                StatusMenuItem.Text = menuItem.Tag != null ? $"You pressed {menuItem.Tag}" : $"You pressed {menuItem.Header}";
            }

            if (menuItem.Parent is MenuItem parentMenuItem)
            {
                parentMenuItem.Focus();
            }
            else
            {
                menuItem.Focus();
            }
        }
    }
}