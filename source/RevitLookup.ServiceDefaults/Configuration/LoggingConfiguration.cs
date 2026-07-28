using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using RevitLookup.ServiceDefaults.Services;

namespace RevitLookup.ServiceDefaults.Configuration;

/// <summary>
///     Application logging configuration shared by both hosts.
/// </summary>
/// <example>
/// <code lang="csharp">
/// public partial class Class(ILogger&lt;Class&gt; logger)
/// {
///     private void Execute()
///     {
///         LogMessage(logger);
///     }
///
///     [LoggerMessage(LogLevel.Information, "Message")]
///     private static partial void LogMessage(ILogger&lt;Class&gt; logger);
/// }
/// </code>
/// </example>
[PublicAPI]
public static class LoggingConfiguration
{
    /// <summary>
    ///     Configures the default logging builders.
    /// </summary>
    public static TBuilder ConfigureLoggingDefaults<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

        builder.Logging.AddDebug();
        builder.Logging.SilenceEventLog();
        builder.Logging.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
        });

        builder.Services.AddHostedService<AppDomainExceptionsHandler>();

        return builder;
    }

    extension(ILoggingBuilder logging)
    {
        /// <summary>
        ///     Silences the event log provider the host adds on Windows, which a desktop application has no business writing to.
        /// </summary>
        private void SilenceEventLog()
        {
            logging.AddFilter<EventLogLoggerProvider>(null, LogLevel.None);
        }
    }
}