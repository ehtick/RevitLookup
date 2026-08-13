using LookupEngine.Abstractions.Configuration;
using LookupEngine.Abstractions.Decomposition;
using RevitLookup.UI.Playground.Mocks.Decomposition.Samples;

namespace RevitLookup.UI.Playground.Mocks.Decomposition.Descriptors;

/// <summary>
///     Represents a descriptor for a <see cref="DisabledSample" /> that disables its destructive members.
/// </summary>
public sealed class DisabledSampleDescriptor : Descriptor, IDescriptorConfigurator
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DisabledSampleDescriptor" /> class.
    /// </summary>
    /// <param name="sample">The sample to describe.</param>
    public DisabledSampleDescriptor(DisabledSample sample)
    {
        Name = sample.FileName;
    }

    /// <inheritdoc />
    public void Configure(IMemberConfigurator configuration)
    {
        configuration.Member(nameof(DisabledSample.Delete)).Disable();
        configuration.Member(nameof(DisabledSample.Overwrite)).Disable();
    }
}
