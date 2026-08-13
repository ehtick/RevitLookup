using Bogus;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RevitLookup.UI.Playground.ViewModels.Pages.BasicInput;

/// <summary>
///     Represents the sample data for the segmented control gallery page.
/// </summary>
public sealed class SegmentedViewModel : ObservableObject
{
    /// <summary>
    ///     Gets the sample labels shown in the segmented control.
    /// </summary>
    public List<string> SegmentedLabels { get; } = new Faker<string>()
        .CustomInstantiator(faker => faker.Music.Genre())
        .Generate(5);
}
