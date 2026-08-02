namespace RevitLookup.Abstractions.ViewModels.Tools;

/// <summary>
///     Defines a contract that represents the data for the Search Elements view.
/// </summary>
public interface ISearchElementsViewModel
{
    /// <summary>
    ///     Gets or sets the search query used to filter elements.
    /// </summary>
    string SearchText { get; set; }

    /// <summary>
    ///     Searches for elements matching <see cref="SearchText"/> in the current document and visualizes them.
    /// </summary>
    /// <returns>A task that represents the asynchronous search operation. The result is <see langword="true"/> if matching elements were found and visualized; otherwise, <see langword="false"/>.</returns>
    Task<bool> SearchElementsAsync();
}