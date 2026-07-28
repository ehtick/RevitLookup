using Microsoft.Extensions.Logging;
using RevitApplication = Autodesk.Revit.ApplicationServices.Application;

namespace RevitLookup.Services.Logging;

/// <summary>
///     Writes log records to the journal of the running Revit session.
/// </summary>
/// <param name="addinName">Name opening the record token, identifying the add-in among the entries of the whole session.</param>
/// <remarks>The journal is the only log destination outliving the session. The console the host writes to has no window attached inside <c>Revit.exe</c>.</remarks>
[ProviderAlias("RevitJournal")]
public sealed class RevitJournalLoggerProvider(string addinName) : ILoggerProvider
{
    /// <summary>
    ///     Returns the journal logger of <paramref name="categoryName"/>.
    /// </summary>
    /// <param name="categoryName">Category the records are tagged with.</param>
    /// <returns>The logger of the category.</returns>
    /// <remarks>Logging scopes are discarded. The journal records carry the category and the message alone.</remarks>
    public ILogger CreateLogger(string categoryName)
    {
        return new RevitJournalLogger(addinName, categoryName);
    }

    /// <summary>
    ///     Does nothing.
    /// </summary>
    /// <remarks>The provider does not own the Revit application the records are written to.</remarks>
    public void Dispose()
    {
    }
}