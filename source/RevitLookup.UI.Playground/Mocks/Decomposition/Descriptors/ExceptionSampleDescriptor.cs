using LookupEngine.Abstractions.Decomposition;
using RevitLookup.UI.Playground.Mocks.Decomposition.Samples;

namespace RevitLookup.UI.Playground.Mocks.Decomposition.Descriptors;

/// <summary>
///     Represents a descriptor for an <see cref="ExceptionSample" /> whose members throw when evaluated.
/// </summary>
public sealed class ExceptionSampleDescriptor : Descriptor
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ExceptionSampleDescriptor" /> class.
    /// </summary>
    /// <param name="sample">The sample to describe.</param>
    public ExceptionSampleDescriptor(ExceptionSample sample)
    {
        Name = sample.Endpoint;
    }
}
