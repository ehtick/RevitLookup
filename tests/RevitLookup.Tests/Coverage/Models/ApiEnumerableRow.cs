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
///     A single row of the enumerable report.
/// </summary>
public sealed record ApiEnumerableRow
{
    /// <summary>
    ///     The shape the enumerable exposes.
    /// </summary>
    public required ApiEnumerableKind Kind { get; init; }

    /// <summary>
    ///     The short name of the enumerable type.
    /// </summary>
    public required string TypeName { get; init; }

    /// <summary>
    ///     The namespace declaring the enumerable type.
    /// </summary>
    public required string Namespace { get; init; }

    /// <summary>
    ///     The short name of the element type the enumerable holds.
    /// </summary>
    public required string ElementType { get; init; }

    /// <summary>
    ///     Whether the type derives from <see cref="Autodesk.Revit.DB.APIObject"/>, the interop base holding a native handle.
    /// </summary>
    public required bool IsApiObject { get; init; }

    /// <summary>
    ///     Whether the type exposes a <c>bool IsEmpty</c> property.
    /// </summary>
    public required bool HasIsEmpty { get; init; }

    /// <summary>
    ///     Whether the type exposes an <c>int Count</c> property.
    /// </summary>
    public required bool HasCount { get; init; }

    /// <summary>
    ///     The type named by the <c>EnumerableDescriptor</c> switch arm reading <c>IsEmpty</c> or <c>Count</c> of this type.
    ///     <c>null</c> marks a type no arm matches. A base type or an interface here marks a type an arm reaches through the hierarchy.
    /// </summary>
    public required string? DescriptorArm { get; init; }

    /// <summary>
    ///     How the descriptor finds out whether an instance of this type contains any elements.
    /// </summary>
    public ApiEnumerableCoverage Coverage
    {
        get
        {
            if (DescriptorArm is not null)
            {
                return ApiEnumerableCoverage.Covered;
            }

            if (HasIsEmpty || HasCount)
            {
                return ApiEnumerableCoverage.Missing;
            }

            return ApiEnumerableCoverage.Iterated;
        }
    }
}