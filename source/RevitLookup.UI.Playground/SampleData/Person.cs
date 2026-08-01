namespace RevitLookup.UI.Playground.SampleData;

public sealed record Person
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Company { get; set; }
    public string Name => $"{FirstName} {LastName}";
    public List<Person>? Children { get; set; }
}
