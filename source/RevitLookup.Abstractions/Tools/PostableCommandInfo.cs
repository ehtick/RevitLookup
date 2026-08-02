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

namespace RevitLookup.Abstractions.Tools;

/// <summary>
///     Represents information about the Revit postable command.
/// </summary>
public sealed class PostableCommandInfo
{
    /// <summary>
    ///     Gets or sets the command name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    ///     Gets or sets the underlying Revit postable command value used to invoke the command.
    /// </summary>
    public required object Value { get; init; }
}
