using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RevitLookup.ServiceDefaults.Services;

/// <summary>
///     Logs unhandled AppDomain exceptions while the host is running.
/// </summary>
public sealed partial class AppDomainExceptionsHandler(ILogger<AppDomainExceptionsHandler> logger) : IHostedService
{
    /// <summary>
    ///     Starts AppDomain exception logging.
    /// </summary>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Stops AppDomain exception logging.
    /// </summary>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        AppDomain.CurrentDomain.UnhandledException -= OnUnhandledException;
        return Task.CompletedTask;
    }

    private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
    {
        if (args.ExceptionObject is Exception exception)
        {
            LogDomainUnhandledException(logger, exception);
            return;
        }

        LogNonExceptionDomainUnhandledException(logger, args.IsTerminating);
    }

    [LoggerMessage(LogLevel.Critical, "Domain unhandled exception")]
    private static partial void LogDomainUnhandledException(ILogger<AppDomainExceptionsHandler> logger, Exception exception);

    [LoggerMessage(LogLevel.Critical, "Domain unhandled non-exception object, terminating: {isTerminating}")]
    private static partial void LogNonExceptionDomainUnhandledException(ILogger<AppDomainExceptionsHandler> logger, bool isTerminating);
}