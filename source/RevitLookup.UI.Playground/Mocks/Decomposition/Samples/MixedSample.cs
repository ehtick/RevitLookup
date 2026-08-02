using System.Windows.Media;
using Bogus;

namespace RevitLookup.UI.Playground.Mocks.Decomposition.Samples;

/// <summary>
///     Brings together every value kind and member kind the grid can render.
/// </summary>
/// <remarks>
///     One window can exercise all cell templates, row styles, and decomposition options at once.
///     Synthetic computed, deferred, disabled, and unsupported members are added by
///     <see cref="RevitLookup.UI.Playground.Mocks.Decomposition.Descriptors.MixedSampleDescriptor"/>.
/// </remarks>
[PublicAPI]
public sealed class MixedSample
{
    private readonly string _checksum;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MixedSample"/> class with fake data covering every rendered value kind.
    /// </summary>
    public MixedSample()
    {
        var faker = new Faker();
        Name = faker.Commerce.ProductName();
        Description = faker.Lorem.Sentence();
        Tint = Color.FromArgb(faker.Random.Byte(), faker.Random.Byte(), faker.Random.Byte(), faker.Random.Byte());
        Revision = faker.Random.Int(1, 10);
        Author = new PlaceholderSample();
        _checksum = faker.Random.Guid().ToString();

        var swatches = new List<Color>(8);
        for (var i = 0; i < swatches.Capacity; i++)
        {
            swatches.Add(Color.FromArgb(faker.Random.Byte(), faker.Random.Byte(), faker.Random.Byte(), faker.Random.Byte()));
        }

        Swatches = swatches;
    }

    /// <summary>
    ///     Gets the product name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Gets the description.
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///     Gets the tint color.
    /// </summary>
    public Color Tint { get; }

    /// <summary>
    ///     Gets the comment.
    /// </summary>
    /// <value>An empty string.</value>
    public string Comment => string.Empty;

    /// <summary>
    ///     Gets the note.
    /// </summary>
    /// <value><see langword="null"/>.</value>
    public string? Note => null;

    /// <summary>
    ///     Gets the reference URI.
    /// </summary>
    /// <value><see langword="null"/>.</value>
    public Uri? Reference => null;

    /// <summary>
    ///     Gets the diagnostics report.
    /// </summary>
    /// <exception cref="InvalidOperationException">Always thrown; the sample element failed during evaluation.</exception>
    public string Diagnostics => throw new InvalidOperationException("The sample element failed during evaluation");

    /// <summary>
    ///     Gets the color swatches.
    /// </summary>
    public IReadOnlyList<Color> Swatches { get; }

    /// <summary>
    ///     Gets the author.
    /// </summary>
    public PlaceholderSample Author { get; }

    /// <summary>
    ///     Gets the default category shared by every instance.
    /// </summary>
    public static string DefaultCategory { get; } = "Samples";

    /// <summary>
    ///     The current revision number.
    /// </summary>
    public int Revision;

    private string Checksum => _checksum;

    /// <summary>
    ///     An event that is raised when <see cref="Refresh"/> updates the revision.
    /// </summary>
    public event EventHandler? Changed;

    /// <summary>
    ///     Raises <see cref="Changed"/> and reports the current revision.
    /// </summary>
    /// <returns>A message combining <see cref="Revision"/> and the checksum.</returns>
    public string Refresh()
    {
        Changed?.Invoke(this, EventArgs.Empty);
        return $"Revision {Revision} ({Checksum})";
    }

    /// <summary>
    ///     Does nothing.
    /// </summary>
    /// <remarks>
    ///     This member is left enabled and has no effect.
    /// </remarks>
    public void Delete()
    {
    }

    /// <summary>
    ///     A void method left enabled to demonstrate deferred evaluation and the "No return value" result.
    /// </summary>
    public void Recalculate()
    {
        Revision++;
    }
}