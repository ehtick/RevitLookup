using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RevitLookup.Abstractions.Decomposition;
using RevitLookup.Abstractions.Presentation;
using RevitLookup.Abstractions.Settings;
using RevitLookup.Abstractions.Updater;
using RevitLookup.Commands;
using RevitLookup.Decomposition;
using RevitLookup.Logging;
using RevitLookup.Presentation;
using RevitLookup.ServiceDefaults;
using RevitLookup.Settings;
using RevitLookup.UI.Framework.Presentation;
using RevitLookup.Updater;
using RevitLookup.ViewModels;
using RevitLookup.Views;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using EventsMonitoringService = RevitLookup.Decomposition.EventsMonitor.EventsMonitoringService;

namespace RevitLookup;

/// <summary>
///     Provides a host for the application's services and manages their lifetimes.
/// </summary>
public static class Host
{
    private static IHost? _host;

    /// <summary>
    ///     Starts the host and configures the application's services.
    /// </summary>
    /// <returns>A task that represents the asynchronous host startup operation.</returns>
    public static async Task StartAsync()
    {
        var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
        {
            ContentRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            DisableDefaults = true,
#if ENVIRONMENT_PRODUCTION
            EnvironmentName = Environments.Production
#else
            EnvironmentName = Environments.Development
#endif
        });

        //Host
        builder.AddRevitLogging();
        builder.AddServiceDefaults();
        builder.Services.AddHostedService<HostBackgroundService>();

        //Presentation
        builder.Services.AddScoped<INavigationViewPageProvider, DependencyInjectionNavigationViewPageProvider>();
        builder.Services.AddScoped<INavigationService, NavigationService>();
        builder.Services.AddScoped<IContentDialogService, ContentDialogService>();
        builder.Services.AddScoped<ISnackbarService, SnackbarService>();
        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddScoped<IWindowIntercomService, WindowIntercomService>();
        builder.Services.AddTransient<IUiOrchestratorService, UiOrchestratorService>();
        builder.Services.AddSingleton<IThemeWatcherService, ThemeWatcherService>();
        builder.Services.AddViews();
        builder.Services.AddViewModels();

        //Decomposition
        builder.Services.AddScoped<IDecompositionService, DecompositionService>();
        builder.Services.AddScoped<IVisualDecompositionService, VisualDecompositionService>();
        builder.Services.AddScoped<IDecompositionSearchService, DecompositionSearchService>();
        builder.Services.AddTransient<EventsMonitoringService>();

        //Settings
        builder.Services.AddSingleton<ISettingsService, SettingsService>();

        //Revit
        builder.Services.AddSingleton<RevitRibbonService>();

        //Updater
        builder.AddGitHubClient();
        builder.Services.AddSingleton<ISoftwareUpdateService, SoftwareUpdateService>();

        _host = builder.Build();
        await _host.StartAsync();
    }

    /// <summary>
    ///     Stops the host and handles <see cref="IHostedService" /> services.
    /// </summary>
    /// <returns>A task that represents the asynchronous host shutdown operation.</returns>
    public static async Task StopAsync()
    {
        if (_host is null)
        {
            return;
        }

        await _host.StopAsync();
    }

    /// <summary>
    ///     Gets a service of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The type of service object to get.</typeparam>
    /// <returns>The requested service instance.</returns>
    /// <exception cref="System.InvalidOperationException">There is no service of type <typeparamref name="T" />.</exception>
    public static T GetService<T>() where T : class
    {
        return _host!.Services.GetRequiredService<T>();
    }
}
