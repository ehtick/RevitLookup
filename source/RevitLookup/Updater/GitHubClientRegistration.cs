using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;

namespace RevitLookup.Updater;

/// <summary>
///     The GitHub releases client the update check runs on.
/// </summary>
public static class GitHubClientRegistration
{
    /// <param name="builder">The host application builder to configure.</param>
    extension<TBuilder>(TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        /// <summary>
        ///     Adds the named client that reads the repository releases, with the request logging the add-in has no journal room for stripped out.
        /// </summary>
        /// <returns>The <see cref="TBuilder"/> for chaining.</returns>
        public TBuilder AddGitHubClient()
        {
            builder.Services.AddHttpClient("GitHubSource", client => client.BaseAddress = new Uri("https://api.github.com/repos/lookup-foundation/RevitLookup/"));

            builder.Services.ConfigureHttpClientDefaults(clientBuilder => clientBuilder.RemoveAllLoggers());
            builder.Services.RemoveAll<IHttpMessageHandlerBuilderFilter>();

            return builder;
        }
    }
}
