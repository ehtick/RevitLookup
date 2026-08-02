using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

namespace RevitLookup.ServiceDefaults.Logging;

/// <summary>
///     Provides extension methods for <see cref="IHostApplicationBuilder"/> to add the logging configuration shared by both hosts.
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
public static class LoggingRegistration
{
    /// <param name="builder">The host application builder.</param>
    extension<TBuilder>(TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        /// <summary>
        ///     Adds the default logging providers to the specified <see cref="IHostApplicationBuilder"/>.
        /// </summary>
        /// <returns>The <see cref="TBuilder"/> for chaining.</returns>
        public TBuilder AddLoggingDefaults()
        {
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

            builder.Logging.AddDebug();
            builder.Logging.SilenceEventLog();
            builder.Logging.AddSimpleConsole(options =>
            {
                options.SingleLine = true;
                options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
            });

            builder.Services.AddHostedService<Diagnostics.AppDomainExceptionsHandler>();
            builder.Services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);

            return builder;
        }
    }

    extension(ILoggingBuilder logging)
    {
        /// <summary>
        ///     Silences the event log provider the host adds on Windows.
        /// </summary>
        private void SilenceEventLog()
        {
            logging.AddFilter<EventLogLoggerProvider>(null, LogLevel.None);
        }
    }
}