using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ModularPipelines.Context;
using ModularPipelines.Engine;

namespace Build.Komac;

public static class KomacExtensions
{
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

    extension(IPipelineContext context)
    {
        public Komac Komac() => context.Services.Get<Komac>();
    }
}
