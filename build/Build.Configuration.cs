﻿using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.Git;

sealed partial class Build
{
    /// <summary>
    ///     Patterns of solution configurations for compiling.
    /// </summary>
    string[] Configurations =
    [
        "Release*"
    ];

    /// <summary>
    ///     Mapping configurations and their assembly versions.
    /// </summary>
    Dictionary<string, string> AssemblyVersionsMap = new()
    {
        { "Release Engine", "1.0.0" },
        { "Release R21", "2021.4.0" },
        { "Release R22", "2022.4.0" },
        { "Release R23", "2023.4.0" },
        { "Release R24", "2024.2.0" },
        { "Release R25", "2025.1.0" },
        { "Release R26", "2026.0.0" }
    };
    
    /// <summary>
    ///     Projects packed in the Autodesk Bundle.
    /// </summary>
    Project[] Bundles =>
    [
        Solution.Revit.RevitLookup
    ];
    
    /// <summary>
    ///     Mapping between used installer project and the project containing the installation files.
    /// </summary>
    Dictionary<Project, Project> InstallersMap => new()
    {
        { Solution.Automation.Installer, Solution.Revit.RevitLookup }
    };

    /// <summary>
    ///     Path to build output.
    /// </summary>
    readonly AbsolutePath ArtifactsDirectory = RootDirectory / "output";

    /// <summary>
    ///     Releases changelog path.
    /// </summary>
    readonly AbsolutePath ChangelogPath = RootDirectory / "Changelog.md";

    /// <summary>
    ///     Add-in release version, includes version number and release stage.
    /// </summary>
    /// <remarks>Supported version format: <c>version-environment.n.date</c>.</remarks>
    /// <example>
    ///     1.0.0-alpha.1.250101 <br/>
    ///     1.0.0-beta.2.250101 <br/>
    ///     1.0.0
    /// </example>
    [Parameter] string ReleaseVersion;

    /// <summary>
    ///     The previous release version.
    /// </summary>
    /// <remarks>
    ///     Can be used to compare versions or analyze changes between versions.
    /// </remarks>
    string PreviousReleaseVersion => GitTasks.Git("tag --list", logInvocation: false, logOutput: false).ToArray() switch
    {
        var tags when tags.Length >= 2 => tags[^2].Text,
        var tags when tags.Length == 0 => throw new InvalidOperationException("The pipeline must be triggered by pushing a new tag"),
        _ => GitTasks.Git("rev-list --max-parents=0 HEAD", logOutput: false, logInvocation: false).First().Text
    };

    /// <summary>
    ///     Numeric release version without a stage.
    /// </summary>
    string ReleaseVersionNumber => ReleaseVersion?.Split('-')[0];

    /// <summary>
    ///     Release stage.
    /// </summary>
    /// <example>
    ///     alpha for 1.0.0-alpha.1.250101<br/>
    ///     beta for 1.0.0-beta.2.250101 => beta <br/>
    ///     production for 1.0.0
    /// </example>
    string ReleaseStage => IsPrerelease ? ReleaseVersion.Split('-')[1].Split('.')[0] : "production";

    /// <summary>
    ///     Determines whether the Revit add-ins release is preview.
    /// </summary>
    bool IsPrerelease => ReleaseVersion != ReleaseVersionNumber;

    /// <summary>
    ///     Git repository metadata.
    /// </summary>
    [GitRepository] readonly GitRepository GitRepository;

    /// <summary>
    ///     Solution structure metadata.
    /// </summary>
    [Solution(GenerateProjects = true)] Solution Solution;
    
    /// <summary>
    ///     Azure Key Vault URI
    /// </summary>
    [Parameter] [Secret] readonly string AzureVaultUri = EnvironmentInfo.GetVariable("AZURE_VAULT_URI");

    /// <summary>
    ///    Azure Key Vault tenant ID
    /// </summary>
    [Parameter] [Secret] readonly string AzureVaultTenantId = EnvironmentInfo.GetVariable("AZURE_VAULT_TENANT_ID");
    
    /// <summary>
    ///     Azure Key Vault client ID
    /// </summary>
    [Parameter] [Secret] readonly string AzureVaultClientId = EnvironmentInfo.GetVariable("AZURE_VAULT_CLIENT_ID");

    /// <summary>
    ///     Azure Key Vault client secret
    /// </summary>
    [Parameter] [Secret] readonly string AzureVaultClientSecret = EnvironmentInfo.GetVariable("AZURE_VAULT_CLIENT_SECRET");

    /// <summary>
    ///     Azure Key Vault certificate name
    /// </summary>
    [Parameter] [Secret] readonly string AzureVaultCertificateName = EnvironmentInfo.GetVariable("AZURE_VAULT_CERTIFICATE_NAME");

    /// <summary>
    ///     Set not-defined properties.
    /// </summary>
    protected override void OnBuildInitialized()
    {
        ReleaseVersion ??= GitRepository.Tags.SingleOrDefault();
    }
}