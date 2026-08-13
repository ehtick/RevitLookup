using Bogus;

namespace RevitLookup.UI.Playground.Mocks.Decomposition.Samples;

/// <summary>
///     Demonstrates disabled members.
/// </summary>
/// <remarks>
///     The destructive operations are permanently disabled by
///     <see cref="RevitLookup.UI.Playground.Mocks.Decomposition.Descriptors.DisabledSampleDescriptor" /> and shown as greyed-out placeholder text.
/// </remarks>
[PublicAPI]
public sealed class DisabledSample
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DisabledSample" /> class with fake file metadata.
    /// </summary>
    public DisabledSample()
    {
        var faker = new Faker();
        FileName = faker.System.FileName();
        Owner = faker.Name.FullName();
        SizeBytes = faker.Random.Long(1_000, 50_000_000);
    }

    /// <summary>
    ///     Gets the file name.
    /// </summary>
    public string FileName { get; }

    /// <summary>
    ///     Gets the name of the file owner.
    /// </summary>
    public string Owner { get; }

    /// <summary>
    ///     Gets the size of the file, in bytes.
    /// </summary>
    public long SizeBytes { get; }

    /// <summary>
    ///     Gets a value indicating whether the file is read-only.
    /// </summary>
    public bool IsReadOnly => true;

    /// <summary>
    ///     Always throws to demonstrate a permanently disabled destructive member.
    /// </summary>
    /// <exception cref="InvalidOperationException">Always thrown; this member must never be invoked.</exception>
    public void Delete()
    {
        throw new InvalidOperationException("Delete must never be invoked");
    }

    /// <summary>
    ///     Always throws to demonstrate a permanently disabled destructive member.
    /// </summary>
    /// <exception cref="InvalidOperationException">Always thrown; this member must never be invoked.</exception>
    public void Overwrite()
    {
        throw new InvalidOperationException("Overwrite must never be invoked");
    }
}
