// Copyright (c) Lookup Foundation and Contributors
// 
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
// 
// THIS PROGRAM IS PROVIDED "AS IS" AND WITH ALL FAULTS.
// NO IMPLIED WARRANTY OF MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE IS PROVIDED.
// THERE IS NO GUARANTEE THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RevitLookup.Abstractions.Presentation;
using RevitLookup.Abstractions.Tools;
using RevitLookup.Abstractions.ViewModels.Tools;
using RevitLookup.UI.Framework.Extensions;
using Wpf.Ui;

namespace RevitLookup.UI.Framework.Views.Tools;

/// <summary>
///     Represents a dialog that lists the Revit postable commands available for execution.
/// </summary>
public sealed partial class PostableCommandsDialog
{
    private readonly IPostableCommandsViewModel _viewModel;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PostableCommandsDialog"/> class.
    /// </summary>
    /// <param name="dialogService">The service that supplies the dialog host this dialog is shown on.</param>
    /// <param name="viewModel">The view model that provides the data for the Postable Commands view.</param>
    /// <param name="themeWatcherService">The service that applies and tracks the current theme for this dialog.</param>
    public PostableCommandsDialog(
        IContentDialogService dialogService,
        IPostableCommandsViewModel viewModel,
        IThemeWatcherService themeWatcherService)
        : base(dialogService.GetDialogHostEx())
    {
        _viewModel = viewModel;

        DataContext = _viewModel;
        InitializeComponent();

        themeWatcherService.Watch(this);
    }

    /// <summary>
    ///     Initializes the postable commands and shows the dialog.
    /// </summary>
    /// <returns>A task that represents the asynchronous show operation.</returns>
    public async Task ShowAsync()
    {
        _viewModel.Initialize();
        await base.ShowAsync();
    }

    private void OnMouseEnter(object sender, RoutedEventArgs routedEventArgs)
    {
        var element = (FrameworkElement) sender;
        var commandInfo = (PostableCommandInfo) element.DataContext;
        CreateRowContextMenu(commandInfo, element);
    }

    private void CreateRowContextMenu(PostableCommandInfo info, FrameworkElement row)
    {
        var contextMenu = new ContextMenu
        {
            Resources = UiApplication.Current.Resources,
            PlacementTarget = row
        };

        contextMenu.AddMenuItem("ExecuteMenuItem")
            .SetHeader("Execute")
            .SetAvailability(_viewModel.CanExecute(info))
            .SetCommand(info, commandInfo =>
            {
                _viewModel.Execute(commandInfo);
            })
            .SetShortcut(ModifierKeys.Control, Key.X);

        row.ContextMenu = contextMenu;
    }
}
