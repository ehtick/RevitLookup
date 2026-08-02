using Bogus;
using CommunityToolkit.Mvvm.ComponentModel;
using Person = RevitLookup.UI.Playground.SampleData.Person;

namespace RevitLookup.UI.Playground.ViewModels.Pages.Collections;

/// <summary>
///     Represents the sample data for the data grid gallery page.
/// </summary>
[UsedImplicitly]
public sealed partial class DataGridViewModel : ObservableObject
{
    /// <summary>
    ///     Gets or sets the sample people shown in the data grid.
    /// </summary>
    [ObservableProperty]
    public partial List<Person> Persons { get; set; } = new Faker<Person>()
        .RuleFor(person => person.FirstName, faker => faker.Person.FirstName)
        .RuleFor(person => person.LastName, faker => faker.Person.LastName)
        .RuleFor(person => person.Company, faker => faker.PickRandom("RevitLookup", "Autodesk"))
        .Generate(25);
}