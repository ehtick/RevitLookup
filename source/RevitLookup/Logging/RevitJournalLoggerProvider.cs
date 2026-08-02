using Microsoft.Extensions.Logging;

namespace RevitLookup.Logging;

/// <summary>
///     Writes log records to the journal of the running Revit session.
/// </summary>
/// <param name="addinName">Name opening the record token, identifying the add-in among the entries of the whole session.</param>
/// <remarks>The journal is the only log destination outliving the session. The console the host writes to has no window attached inside <c>Revit.exe</c>.</remarks>
[ProviderAlias("RevitJournal")]
public sealed class RevitJournalLoggerProvider(string addinName) : ILoggerProvider
{
    /// <inheritdoc/>
    /// <remarks>Logging scopes are discarded. The journal records carry the category and the message alone.</remarks>
    public ILogger CreateLogger(string categoryName)
    {
        return new RevitJournalLogger(addinName, categoryName);
    }

    /// <inheritdoc/>
    /// <remarks>This method does nothing. The provider does not own the Revit application the records are written to.</remarks>
    public void Dispose()
    {
    }
}