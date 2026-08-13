namespace RevitLookup.Abstractions.Application;

/// <summary>
///     Represents runtime information about the application assembly.
/// </summary>
public sealed class AssemblyOptions
{
    /// <summary>
    ///     Gets or sets the display name of the target framework the assembly runs on.
    /// </summary>
    public required string Framework { get; set; }

    /// <summary>
    ///     Gets or sets the version of the running assembly.
    /// </summary>
    public required Version Version { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the current installation has administrator-level write access.
    /// </summary>
    public required bool HasAdminAccess { get; set; }
}
