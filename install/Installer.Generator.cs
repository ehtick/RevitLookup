using WixSharp;

namespace Installer;

/// <summary>
///     Provides methods to generate the WiX entities for the installer.
/// </summary>
public static class Generator
{
    /// <summary>
    ///     Generates the WiX features and directories that install the add-in files for each Revit version.
    /// </summary>
    /// <param name="versioning">The resolved version the generated entities are named after.</param>
    /// <param name="directory">The directory the add-in files are read from.</param>
    /// <returns>The generated <see cref="WixEntity" /> array.</returns>
    public static WixEntity[] GenerateWixEntities(ResolveVersioningResult versioning, string directory)
    {
        var versionStorages = new Dictionary<string, List<WixEntity>>();
        var revitFeature = new Feature
        {
            Name = "Revit Add-in",
            Description = "Revit add-in installation files",
            Display = FeatureDisplay.expand
        };

        var fileVersion = versioning.VersionPrefix.Major.ToString();
        var feature = new Feature
        {
            Name = fileVersion,
            Description = $"Install add-in for Revit {fileVersion}",
            ConfigurableDir = $"INSTALL{fileVersion}"
        };

        revitFeature.Add(feature);

        var files = new Files(feature, $@"{directory}\*.*");
        if (versionStorages.TryGetValue(fileVersion, out var storage))
        {
            storage.Add(files);
        }
        else
        {
            versionStorages.Add(fileVersion, [files]);
        }

        LogFeatureFiles(directory, fileVersion);

        return versionStorages
            .Select(versionPair => new Dir(new Id($"INSTALL{versionPair.Key}"), versionPair.Key, versionPair.Value.ToArray()))
            .Cast<WixEntity>()
            .ToArray();
    }

    /// <summary>
    ///     Write a list of installer files.
    /// </summary>
    private static void LogFeatureFiles(string directory, string fileVersion)
    {
        var assemblies = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
        Console.WriteLine($"Installer files for version {fileVersion}:");

        foreach (var assembly in assemblies)
        {
            Console.WriteLine($"- {assembly}");
        }
    }
}
