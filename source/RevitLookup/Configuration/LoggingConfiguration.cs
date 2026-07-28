using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RevitLookup.Services.Logging;

namespace RevitLookup.Configuration;

/// <summary>
///     Revit-specific logging configuration.
/// </summary>
public static class LoggingConfiguration
{
    /// <summary>
    ///     Adds the Revit journal logging provider and silences the WPF resource dictionary traces.
    /// </summary>
    public static TBuilder ConfigureRevitLogging<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        var journalProvider = new RevitJournalLoggerProvider(nameof(RevitLookup));
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider>(journalProvider));

        PresentationTraceSources.ResourceDictionarySource.Switch.Level = SourceLevels.Critical;

        return builder;
    }
}