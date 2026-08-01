using System.Diagnostics;
using System.Reflection;
using System.Runtime.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RevitLookup.Abstractions.Application;
using RevitLookup.ServiceDefaults.FileSystem;

namespace RevitLookup.ServiceDefaults.Application;

[PublicAPI]
public static class AssemblyRegistration
{
    /// <param name="builder">The host application builder.</param>
    extension<TBuilder>(TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        /// <summary>
        ///     Binds the framework, version, and write-access facts of the running assembly.
        /// </summary>
        public TBuilder ConfigureAssembly()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyLocation = assembly.Location;
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var fileVersion = new Version(FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion!);
            var targetFrameworkAttribute = assembly.GetCustomAttribute<TargetFrameworkAttribute>()!;

            builder.Services.Configure<AssemblyOptions>(options =>
            {
                options.Framework = targetFrameworkAttribute.FrameworkDisplayName ?? targetFrameworkAttribute.FrameworkName;
                options.Version = new Version(fileVersion.Major, fileVersion.Minor, fileVersion.Build);
                options.HasAdminAccess = assemblyLocation.StartsWith(appDataPath) || !AccessUtils.CheckWriteAccess(assemblyLocation);
            });

            return builder;
        }
    }
}