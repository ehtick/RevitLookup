using Bogus;

namespace RevitLookup.UI.Playground.Mocks.Decomposition.Samples;

/// <summary>
///     Demonstrates members that fail during evaluation.
/// </summary>
/// <remarks>
///     Each diagnostic property throws; the engine captures the exception as the member value and renders it with the
///     critical "Exception" formatting on a red row.
/// </remarks>
[PublicAPI]
public sealed class ExceptionSample
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ExceptionSample"/> class with fake endpoint metadata.
    /// </summary>
    public ExceptionSample()
    {
        var faker = new Faker();
        Endpoint = faker.Internet.Url();
        TimeoutSeconds = faker.Random.Int(1, 30);
    }

    /// <summary>
    ///     Gets the endpoint URL.
    /// </summary>
    public string Endpoint { get; }

    /// <summary>
    ///     Gets the request timeout, in seconds.
    /// </summary>
    public int TimeoutSeconds { get; }

    /// <summary>
    ///     Gets the sensor reading.
    /// </summary>
    /// <exception cref="InvalidOperationException">Always thrown; the sensor returned a malformed payload.</exception>
    public string SensorReading => throw new InvalidOperationException("The sensor returned a malformed payload");

    /// <summary>
    ///     Gets the last response from the endpoint.
    /// </summary>
    /// <exception cref="TimeoutException">Always thrown; the remote endpoint did not respond in time.</exception>
    public string LastResponse => throw new TimeoutException("The remote endpoint did not respond in time");
}