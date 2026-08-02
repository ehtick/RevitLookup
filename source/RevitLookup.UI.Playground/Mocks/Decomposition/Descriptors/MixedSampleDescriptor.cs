using LookupEngine.Abstractions.Configuration;
using LookupEngine.Abstractions.Decomposition;
using RevitLookup.UI.Playground.Mocks.Decomposition.Samples;

namespace RevitLookup.UI.Playground.Mocks.Decomposition.Descriptors;

/// <summary>
///     Represents a descriptor for a <see cref="MixedSample"/> that combines a disabled member with computed, static, deferred, and unsupported extensions.
/// </summary>
public sealed class MixedSampleDescriptor : Descriptor, IDescriptorConfigurator
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MixedSampleDescriptor"/> class.
    /// </summary>
    /// <param name="sample">The sample to describe.</param>
    public MixedSampleDescriptor(MixedSample sample)
    {
        Name = sample.Name;
    }

    /// <inheritdoc/>
    public void Configure(IMemberConfigurator configuration)
    {
        configuration.Member(nameof(MixedSample.Delete)).Disable();
        configuration.Extension("Computed").Register(() => "synthetic value");
        configuration.Extension("Cached").AsStatic().Register(() => "static extension");
        configuration.Extension("Export").Defer(() => "export.bin");
        configuration.Extension("NativeHandle").NotSupported();
    }
}