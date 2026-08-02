using LookupEngine.Abstractions.Configuration;
using LookupEngine.Abstractions.Decomposition;
using RevitLookup.UI.Playground.Mocks.Decomposition.Samples;

namespace RevitLookup.UI.Playground.Mocks.Decomposition.Descriptors;

/// <summary>
///     Represents a descriptor for an <see cref="UnsupportedSample"/> whose extensions are marked unsupported.
/// </summary>
public sealed class UnsupportedSampleDescriptor : Descriptor, IDescriptorConfigurator
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UnsupportedSampleDescriptor"/> class.
    /// </summary>
    /// <param name="sample">The sample to describe.</param>
    public UnsupportedSampleDescriptor(UnsupportedSample sample)
    {
        Name = $"{sample.Host}:{sample.Port}";
    }

    /// <inheritdoc/>
    public void Configure(IMemberConfigurator configuration)
    {
        configuration.Extension("ExecuteNativeCall").NotSupported();
        configuration.Extension("GetRawHandle").NotSupported();
    }
}