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

namespace RevitLookup.Tests.Unit.Coverage.Models;

/// <summary>
///     A single row of the utility method report.
/// </summary>
public sealed record ApiMethodRow
{
    /// <summary>
    ///     The short name of the method return type.
    /// </summary>
    public required string ReturnType { get; init; }

    /// <summary>
    ///     The <c>Type.Method</c> name of the reported method.
    /// </summary>
    public required string QualifiedName { get; init; }

    /// <summary>
    ///     The method parameters rendered as a comma separated <c>Type name</c> list.
    /// </summary>
    public required string Parameters { get; init; }

    /// <summary>
    ///     Names of the descriptor source files mentioning <see cref="QualifiedName"/>.
    ///     An empty list marks a method no descriptor resolves yet.
    /// </summary>
    public required IReadOnlyList<string> DescriptorFiles { get; init; }
}