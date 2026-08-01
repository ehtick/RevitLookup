using System.Windows.Threading;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RevitLookup.Abstractions.Decomposition;
using RevitLookup.Abstractions.Presentation;
using RevitLookup.Abstractions.Settings;
using RevitLookup.Abstractions.Updater;
using RevitLookup.ServiceDefaults;
using RevitLookup.UI.Framework.Presentation;
using RevitLookup.UI.Playground.Mocks.Decomposition;
using RevitLookup.UI.Playground.Mocks.Presentation;
using RevitLookup.UI.Playground.Mocks.Settings;
using RevitLookup.UI.Playground.Mocks.Updater;
using RevitLookup.UI.Playground.ViewModels;
using RevitLookup.UI.Playground.Views;
using Wpf.Ui;
using Wpf.Ui.Abstractions;

namespace RevitLookup.UI.Playground;

/// <summary>
///     Provides a host for the application's services and manages their lifetimes.
/// </summary>
public static class Host
{
    private static IHost? _host;

    /// <summary>
    ///     Starts the host and configures the application's services
    /// </summary>
    public static void Start()
    {
        var builder = new HostApplicationBuilder(new HostApplicationBuilderSettings
        {
            ContentRootPath = AppContext.BaseDirectory
        });

        //Host
        builder.AddServiceDefaults();
        builder.Services.AddHostedService<HostBackgroundService>();

        //Presentation
        builder.Services.AddScoped<INavigationViewPageProvider, DependencyInjectionNavigationViewPageProvider>();
        builder.Services.AddScoped<INavigationService, NavigationService>();
        builder.Services.AddScoped<IContentDialogService, ContentDialogService>();
        builder.Services.AddScoped<ISnackbarService, SnackbarService>();
        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddScoped<IWindowIntercomService, WindowIntercomService>();
        builder.Services.AddScoped<IMessenger, WeakReferenceMessenger>();
        builder.Services.AddTransient<IUiOrchestratorService, MockUiOrchestratorService>();
        builder.Services.AddSingleton<IThemeWatcherService, MockThemeWatcherService>();
        builder.Services.AddViews();
        builder.Services.AddViewModels();

        //Decomposition
        builder.Services.AddScoped<IDecompositionService, MockDecompositionService>();
        builder.Services.AddScoped<IVisualDecompositionService, MockVisualDecompositionService>();
        builder.Services.AddScoped<IDecompositionSearchService, MockDecompositionSearchService>();

        //Settings
        builder.Services.AddSingleton<ISettingsService, MockSettingsService>();

        //Software update
        builder.Services.AddSingleton<ISoftwareUpdateService, MockSoftwareUpdateService>();

        _host = builder.Build();

        var frame = new DispatcherFrame();
        _host.StartAsync().ContinueWith(_ => frame.Continue = false);

        Dispatcher.PushFrame(frame);
    }

    /// <summary>
    ///     Stops the host and handle <see cref="IHostedService"/> services
    /// </summary>
    public static void Stop()
    {
        if (_host is null) throw new InvalidOperationException("Host is not running");

        var frame = new DispatcherFrame();
        _host.StopAsync().ContinueWith(_ => frame.Continue = false);

        Dispatcher.PushFrame(frame);
    }

    /// <summary>
    ///     Get service of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">The type of service object to get</typeparam>
    /// <exception cref="System.InvalidOperationException">There is no service of type <typeparamref name="T"/></exception>
    public static T GetService<T>() where T : class
    {
        return _host!.Services.GetRequiredService<T>();
    }
}