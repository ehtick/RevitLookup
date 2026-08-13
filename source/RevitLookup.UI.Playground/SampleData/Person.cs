namespace RevitLookup.UI.Playground.SampleData;

/// <summary>
///     Provides a sample <see cref="Person" /> instance for the Playground.
/// </summary>
public sealed record Person
{
    /// <summary>
    ///     Gets or sets the person's first name.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    ///     Gets or sets the person's last name.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    ///     Gets or sets the company the person belongs to.
    /// </summary>
    public required string Company { get; set; }

    /// <summary>
    ///     Gets the person's full name, combining <see cref="FirstName" /> and <see cref="LastName" />.
    /// </summary>
    public string Name => $"{FirstName} {LastName}";

    /// <summary>
    ///     Gets or sets the person's children, or <see langword="null" /> if the person has none.
    /// </summary>
    public List<Person>? Children { get; set; }
}
