using LookupEngine.Abstractions.Decomposition;
using RevitLookup.UI.Playground.Mocks.Decomposition.Samples;

namespace RevitLookup.UI.Playground.Mocks.Decomposition.Descriptors;

public sealed class ExceptionSampleDescriptor : Descriptor
{
    public ExceptionSampleDescriptor(ExceptionSample sample)
    {
        Name = sample.Endpoint;
    }
}