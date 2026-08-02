using System.Windows;
using System.Windows.Automation.Peers;
using RevitLookup.Abstractions.Presentation;
using RevitLookup.UI.Framework.Controls.Automation;
using RevitLookup.UI.Playground.ViewModels;
using RevitLookup.UI.Playground.Views.Pages;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace RevitLookup.UI.Playground.Views;

/// <summary>
/// Represents the main window that hosts navigation between the Playground's demo pages.
/// </summary>
public sealed partial class PlaygroundView
{
    private readonly INavigationService _navigationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaygroundView"/> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the window.</param>
    /// <param name="navigationService">The service that navigates between pages hosted in the window.</param>
    /// <param name="dialogService">The service that hosts content dialogs in the window.</param>
    /// <param name="snackbarService">The service that hosts snackbars in the window.</param>
    /// <param name="intercomService">The service that exposes the window to other components.</param>
    public PlaygroundView(
        PlaygroundViewModel viewModel,
        INavigationService navigationService,
        IContentDialogService dialogService,
        ISnackbarService snackbarService,
        IWindowIntercomService intercomService)
    {
        _navigationService = navigationService;
        DataContext = viewModel;
        InitializeComponent();

        navigationService.SetNavigationControl(NavigationView);
        dialogService.SetDialogHost(DialogHost);
        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        intercomService.SetHost(this);

        Loaded += (sender, _) =>
        {
            var self = (PlaygroundView) sender;
            self._navigationService.Navigate(typeof(DashboardPage));
        };
    }

    private void OnNavigationSelectionChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not NavigationView navigationView) return;

        var onControlsPage = navigationView.SelectedItem?.TargetPageType != typeof(DashboardPage);
        var showHeader = onControlsPage ? Visibility.Visible : Visibility.Collapsed;

        NavigationView.SetCurrentValue(NavigationView.HeaderVisibilityProperty, showHeader);
    }

    /// <inheritdoc/>
    protected override AutomationPeer OnCreateAutomationPeer()
    {
        return new NoAutomationWindowPeer(this);
    }
}