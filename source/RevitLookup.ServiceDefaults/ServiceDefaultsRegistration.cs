using Microsoft.Extensions.Hosting;
using RevitLookup.ServiceDefaults.Application;
using RevitLookup.ServiceDefaults.Logging;
using RevitLookup.ServiceDefaults.Serialization;

namespace RevitLookup.ServiceDefaults;

/// <summary>
///     Applies the hosting concerns every desktop application shares.
/// </summary>
public static class ServiceDefaultsRegistration
{
    /// <param name="builder">The host application builder.</param>
    extension<TBuilder>(TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        /// <summary>
        ///     Adds the default logging, assembly, serialization, and resource-location services to the specified <see cref="IHostApplicationBuilder" />.
        /// </summary>
        /// <returns>The <see cref="TBuilder" /> for chaining.</returns>
        public TBuilder AddServiceDefaults()
        {
            builder.AddLoggingDefaults();
            builder.ConfigureAssembly();
            builder.ConfigureJsonSerializerDefaults();
            builder.ConfigureResourceLocations();

            return builder;
        }
    }
}
