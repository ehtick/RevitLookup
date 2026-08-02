using System.Windows.Data;
using RevitLookup.UI.Playground.SampleData;
using RevitLookup.UI.Playground.ViewModels.Pages.Collections;
using Wpf.Ui.Abstractions.Controls;

namespace RevitLookup.UI.Playground.Views.Pages.Collections;

/// <summary>
/// Represents a page that demonstrates the DataGrid control in the Playground.
/// </summary>
public sealed partial class DataGridPage : INavigableView<DataGridViewModel>
{
    /// <inheritdoc/>
    public DataGridViewModel ViewModel { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataGridPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model that supplies data for the page.</param>
    public DataGridPage(DataGridViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();

        GroupingDataGrid.Items.GroupDescriptions!.Clear();
        GroupingDataGrid.Items.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Person.Company)));
    }
}