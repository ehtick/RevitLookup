using Bogus;

namespace RevitLookup.UI.Playground.Mocks.Decomposition.Samples;

/// <summary>
///     Demonstrates placeholder values.
/// </summary>
/// <remarks>
///     Members that evaluate to content with nothing to display, such as <see langword="null" /> or an empty string, are shown with a placeholder instead of a value.
/// </remarks>
[PublicAPI]
public sealed class PlaceholderSample
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PlaceholderSample" /> class with fake contact metadata.
    /// </summary>
    public PlaceholderSample()
    {
        var faker = new Faker();
        FullName = faker.Name.FullName();
        Email = faker.Internet.Email();
    }

    /// <summary>
    ///     Gets the full name.
    /// </summary>
    public string FullName { get; }

    /// <summary>
    ///     Gets the email address.
    /// </summary>
    public string Email { get; }

    /// <summary>
    ///     Gets the middle name.
    /// </summary>
    /// <value>An empty string.</value>
    public string MiddleName => string.Empty;

    /// <summary>
    ///     Gets the notes.
    /// </summary>
    /// <value>An empty string.</value>
    public string Notes => string.Empty;

    /// <summary>
    ///     Gets the nickname.
    /// </summary>
    /// <value><see langword="null" />.</value>
    public string? Nickname => null;

    /// <summary>
    ///     Gets the website.
    /// </summary>
    /// <value><see langword="null" />.</value>
    public Uri? Website => null;
}
