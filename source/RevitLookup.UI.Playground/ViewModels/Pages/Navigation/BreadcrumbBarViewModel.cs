using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RevitLookup.UI.Playground.ViewModels.Pages.Navigation;

/// <summary>
///     Represents the sample data for the breadcrumb bar gallery page.
/// </summary>
[UsedImplicitly]
public partial class BreadcrumbBarViewModel : ObservableObject
{
    private readonly DirectoryInfo[] _baseDirectories =
    [
        new("Home"),
        new("Folder1"),
        new("Folder2"),
        new("Folder3")
    ];

    /// <summary>
    ///     Initializes a new instance of the <see cref="BreadcrumbBarViewModel" /> class.
    /// </summary>
    public BreadcrumbBarViewModel()
    {
        ResetFoldersCollection();
    }

    /// <summary>
    ///     Gets the sample path segments shown as string breadcrumbs.
    /// </summary>
    public ObservableCollection<string> Strings { get; } =
    [
        "Home",
        "Document",
        "Design",
        "Folder1",
        "Folder2",
        "Folder3"
    ];

    /// <summary>
    ///     Gets the directories currently shown as breadcrumb items.
    /// </summary>
    public ObservableCollection<DirectoryInfo> Directories { get; } = [];

    [RelayCommand]
    private void OnStringSelected(object item)
    {
        // No-op: selection is demonstrated only
    }

    [RelayCommand]
    private void OnDirectorySelected(object item)
    {
        if (item is not DirectoryInfo selectedFolder)
        {
            return;
        }

        var index = Directories.IndexOf(selectedFolder);
        if (index < 0)
        {
            return;
        }

        Directories.Clear();

        for (var i = 0; i <= index && i < _baseDirectories.Length; i++)
        {
            Directories.Add(_baseDirectories[i]);
        }
    }

    [RelayCommand]
    private void OnResetFolders()
    {
        ResetFoldersCollection();
    }

    private void ResetFoldersCollection()
    {
        Directories.Clear();
        foreach (var folder in _baseDirectories)
        {
            Directories.Add(folder);
        }
    }
}
