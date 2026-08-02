using Bogus;

namespace RevitLookup.UI.Playground.Mocks.Decomposition.Samples;

/// <summary>
///     Demonstrates unsupported members.
/// </summary>
/// <remarks>
///     <see cref="RevitLookup.UI.Playground.Mocks.Decomposition.Descriptors.UnsupportedSampleDescriptor"/> registers members the engine cannot evaluate.
///     They appear only when the "Unsupported" filter is enabled in the grid context menu.
/// </remarks>
[PublicAPI]
public sealed class UnsupportedSample
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UnsupportedSample"/> class with fake host metadata.
    /// </summary>
    public UnsupportedSample()
    {
        var faker = new Faker();
        Host = faker.Internet.DomainName();
        Port = faker.Random.Int(1024, 65535);
    }

    /// <summary>
    ///     Gets the host name.
    /// </summary>
    public string Host { get; }

    /// <summary>
    ///     Gets the port number.
    /// </summary>
    public int Port { get; }

    /// <summary>
    ///     Gets the protocol identifier.
    /// </summary>
    public string Protocol => "legacy-rpc";
}