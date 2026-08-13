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

using RevitLookup.Abstractions.Tools;

namespace RevitLookup.Abstractions.ViewModels.Tools;

/// <summary>
///     Defines a contract that represents the data for the Postable Commands view.
/// </summary>
public interface IPostableCommandsViewModel
{
    /// <summary>
    ///     Gets the list of all commands.
    /// </summary>
    List<PostableCommandInfo> Commands { get; }

    /// <summary>
    ///     Gets the list of filtered commands.
    /// </summary>
    List<PostableCommandInfo> FilteredCommands { get; }

    /// <summary>
    ///     Gets or sets the search query used to filter commands.
    /// </summary>
    string SearchText { get; set; }

    /// <summary>
    ///     Initializes the commands for representation.
    /// </summary>
    void Initialize();

    /// <summary>
    ///     Executes the specified command.
    /// </summary>
    /// <param name="commandInfo">The command to execute.</param>
    void Execute(PostableCommandInfo commandInfo);

    /// <summary>
    ///     Determines whether the specified command can be executed.
    /// </summary>
    /// <param name="commandInfo">The command to check.</param>
    /// <returns><see langword="true" /> if the command can be executed; otherwise, <see langword="false" />.</returns>
    bool CanExecute(PostableCommandInfo commandInfo);
}
