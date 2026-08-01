using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ModularPipelines.Context;
using ModularPipelines.Engine;

namespace Build.Azure;

public static class AzureSignToolExtensions
{
    [ModuleInitializer]
    public static void RegisterAzureSignToolContext()
    {
        ModularPipelinesContextRegistry.RegisterContext(collection => collection.RegisterAzureSignToolContext());
    }

    extension(IServiceCollection services)
    {
        private IServiceCollection RegisterAzureSignToolContext()
        {
            services.TryAddScoped<AzureSignTool>();
            return services;
        }
    }

    extension(IPipelineContext context)
    {
        public AzureSignTool Azure() => context.Services.Get<AzureSignTool>();
    }
}