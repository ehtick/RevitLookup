using Bogus;

namespace RevitLookup.UI.Playground.Mocks.Decomposition.Samples;

/// <summary>
///     Demonstrates deferred members.
/// </summary>
/// <remarks>
///     Cheap metadata is evaluated eagerly, while the expensive operations are deferred by
///     <see cref="RevitLookup.UI.Playground.Mocks.Decomposition.Descriptors.DeferredSampleDescriptor"/> and shown with a "Force evaluate" button.
/// </remarks>
[PublicAPI]
public sealed class DeferredSample
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DeferredSample"/> class with fake report metadata.
    /// </summary>
    public DeferredSample()
    {
        var faker = new Faker();
        Title = $"{faker.Commerce.ProductName()} Report";
        Owner = faker.Name.FullName();
        GeneratedOn = faker.Date.Recent();
        RecordCount = faker.Random.Int(1_000, 1_000_000);
    }

    /// <summary>
    ///     Gets the report title.
    /// </summary>
    public string Title { get; }

    /// <summary>
    ///     Gets the name of the report owner.
    /// </summary>
    public string Owner { get; }

    /// <summary>
    ///     Gets the date the report was generated.
    /// </summary>
    public DateTime GeneratedOn { get; }

    /// <summary>
    ///     Gets the number of records aggregated in the report.
    /// </summary>
    public int RecordCount { get; }

    /// <summary>
    ///     Aggregates <see cref="RecordCount"/> into a summary message.
    /// </summary>
    /// <returns>A message reporting the number of aggregated records.</returns>
    public string CalculateTotals()
    {
        return $"Aggregated {RecordCount} records";
    }

    /// <summary>
    ///     Builds a chart from the report data.
    /// </summary>
    /// <returns>A message confirming the chart was rendered.</returns>
    public string BuildChart()
    {
        return "Chart rendered";
    }

    /// <summary>
    ///     Exports the report to a PDF file.
    /// </summary>
    /// <returns>The generated file name.</returns>
    public string ExportToPdf()
    {
        return "report.pdf";
    }
}