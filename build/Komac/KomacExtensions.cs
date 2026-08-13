using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ModularPipelines.Context;
using ModularPipelines.Engine;

namespace Build.Komac;

/// <summary>
///     Provides extension methods for registering and resolving the <see cref="Komac" /> pipeline service.
/// </summary>
public static class KomacExtensions
{
    /// <summary>
    ///     Registers the <see cref="Komac" /> service with the ModularPipelines context registry.
    /// </summary>
    [ModuleInitializer]
    public static void RegisterKomacContext()
    {
        ModularPipelinesContextRegistry.RegisterContext(collection => collection.RegisterKomacContext());
    }

    extension(IServiceCollection services)
    {
        private IServiceCollection RegisterKomacContext()
        {
            services.TryAddScoped<Komac>();
            return services;
        }
    }

    /// <param name="context">The pipeline context to resolve the <see cref="Komac" /> service from.</param>
    extension(IPipelineContext context)
    {
        /// <summary>
        ///     Gets the <see cref="Komac" /> service registered for the current pipeline.
        /// </summary>
        /// <returns>The <see cref="Komac" /> instance resolved from the pipeline's service provider.</returns>
        public Komac Komac()
        {
            return context.Services.Get<Komac>();
        }
    }
}
