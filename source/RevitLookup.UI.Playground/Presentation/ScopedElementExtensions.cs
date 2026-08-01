using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace RevitLookup.UI.Playground.Presentation;

/// <summary>
///     Provides extension methods for working with dependency injection in WPF applications
/// </summary>
public static class ScopedElementExtensions
{
    /// <param name="serviceProvider">The service provider to use for obtaining services.</param>
    extension(IServiceProvider serviceProvider)
    {
        /// <summary>
        ///     Creates a FrameworkElement with the scope lifetime and manages the scope lifecycle.
        /// </summary>
        /// <typeparam name="T">The type of FrameworkElement to get.</typeparam>
        /// <returns>A FrameworkElement of type T with managed scope lifecycle.</returns>
        /// <remarks>
        ///     The scope is automatically disposed when the element is unloaded or,
        ///     in the case of a Window, when it is closed.
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">There is no service of type <typeparamref name="T"/></exception>
        public T CreateScopedFrameworkElement<T>() where T : FrameworkElement
        {
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();

            var element = scope.ServiceProvider.GetRequiredService<T>();

            if (element is Window window)
            {
                window.Closed += (_, _) => scope.Dispose();
            }
            else
            {
                element.Unloaded += (_, _) => scope.Dispose();
            }

            return element;
        }
    }
}