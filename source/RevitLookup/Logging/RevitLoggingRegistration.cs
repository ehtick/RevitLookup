using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RevitLookup.Logging;

/// <summary>
///     Revit-specific logging configuration.
/// </summary>
public static class RevitLoggingRegistration
{
    /// <param name="builder">The host application builder to configure.</param>
    extension<TBuilder>(TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        /// <summary>
        ///     Seeds the log levels the add-in runs on, adds the Revit journal logging provider, and silences the WPF resource dictionary traces.
        /// </summary>
        /// <returns>The <see cref="TBuilder" /> for chaining.</returns>
        public TBuilder AddRevitLogging()
        {
            builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Logging:LogLevel:Default"] = builder.Environment.IsDevelopment() ? nameof(LogLevel.Debug) : nameof(LogLevel.Information),
                ["Logging:LogLevel:Microsoft.Extensions.Http.DefaultHttpClientFactory"] = nameof(LogLevel.Warning),
                ["Logging:RevitJournal:LogLevel:Default"] = nameof(LogLevel.Error)
            });

            var journalProvider = new RevitJournalLoggerProvider(nameof(RevitLookup));
            builder.Services.AddSingleton<ILoggerProvider>(journalProvider);

            PresentationTraceSources.ResourceDictionarySource.Switch.Level = SourceLevels.Critical;

            return builder;
        }
    }
}
