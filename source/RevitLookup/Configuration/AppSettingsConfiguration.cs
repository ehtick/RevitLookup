using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RevitLookup.Configuration;

/// <summary>
///     The add-in host settings.
/// </summary>
public static class AppSettingsConfiguration
{
    /// <summary>
    ///     Adds the in-memory configuration the add-in runs on.
    /// </summary>
    public static TBuilder ConfigureAppSettings<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Logging:LogLevel:Default"] = builder.Environment.IsDevelopment() ? nameof(LogLevel.Debug) : nameof(LogLevel.Information),
            ["Logging:LogLevel:Microsoft.Extensions.Http.DefaultHttpClientFactory"] = nameof(LogLevel.Warning),
            ["Logging:RevitJournal:LogLevel:Default"] = nameof(LogLevel.Error)
        });

        return builder;
    }
}