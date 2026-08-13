using LookupEngine.Abstractions.Configuration;
using LookupEngine.Abstractions.Decomposition;
using RevitLookup.UI.Playground.Mocks.Decomposition.Samples;

namespace RevitLookup.UI.Playground.Mocks.Decomposition.Descriptors;

/// <summary>
///     Represents a descriptor for a <see cref="DeferredSample" /> that defers its members until explicitly evaluated.
/// </summary>
public sealed class DeferredSampleDescriptor : Descriptor, IDescriptorConfigurator
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DeferredSampleDescriptor" /> class.
    /// </summary>
    /// <param name="sample">The sample to describe.</param>
    public DeferredSampleDescriptor(DeferredSample sample)
    {
        Name = sample.Title;
    }

    /// <inheritdoc />
    public void Configure(IMemberConfigurator configuration)
    {
        configuration.Member(nameof(DeferredSample.CalculateTotals)).Defer();
        configuration.Member(nameof(DeferredSample.BuildChart)).Defer();
        configuration.Member(nameof(DeferredSample.ExportToPdf)).Defer();
    }
}
