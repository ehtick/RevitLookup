using Microsoft.Extensions.DependencyInjection;

namespace RevitLookup.ViewModels;

public static class ViewModelsRegistration
{
    extension(IServiceCollection services)
    {
        public void AddViewModels()
        {
            services.Scan(selector => selector.FromAssemblyOf<Application>()
                .AddClasses(filter => filter.Where(static type => type.Name.EndsWith("ViewModel")))
                .AsImplementedInterfaces(static type => type.Name.EndsWith("ViewModel"))
                .WithScopedLifetime());
        }
    }
}