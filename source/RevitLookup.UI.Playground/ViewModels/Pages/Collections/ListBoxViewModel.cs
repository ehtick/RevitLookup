using System.Windows.Controls;
using Bogus;
using CommunityToolkit.Mvvm.ComponentModel;
using Person = RevitLookup.UI.Playground.SampleData.Person;

namespace RevitLookup.UI.Playground.ViewModels.Pages.Collections;

/// <summary>
///     Represents the sample data for the list box gallery page.
/// </summary>
[UsedImplicitly]
public sealed partial class ListBoxViewModel : ObservableObject
{
    /// <summary>
    ///     Gets or sets the index of the selected selection mode option.
    /// </summary>
    [ObservableProperty]
    public partial int SelectionModeIndex { get; set; }

    /// <summary>
    ///     Gets the selection mode applied to the list box.
    /// </summary>
    [ObservableProperty]
    public partial SelectionMode SelectionMode { get; private set; } = SelectionMode.Single;

    /// <summary>
    ///     Gets or sets the sample people shown in the list box.
    /// </summary>
    [ObservableProperty]
    public partial List<Person> Persons { get; set; } = new Faker<Person>()
        .RuleFor(person => person.FirstName, faker => faker.Person.FirstName)
        .RuleFor(person => person.LastName, faker => faker.Person.LastName)
        .RuleFor(person => person.Company, faker => faker.Company.CompanyName("{{name.lastName}}"))
        .Generate(50);

    partial void OnSelectionModeIndexChanged(int value)
    {
        SelectionMode = value switch
        {
            1 => SelectionMode.Multiple,
            2 => SelectionMode.Extended,
            _ => SelectionMode.Single
        };
    }
}
