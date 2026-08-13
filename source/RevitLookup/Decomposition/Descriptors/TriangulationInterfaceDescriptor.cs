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

using LookupEngine.Abstractions.Configuration;
using LookupEngine.Abstractions.Decomposition;

namespace RevitLookup.Decomposition.Descriptors;

/// <summary>
///     Represents the <see cref="Autodesk.Revit.DB.TriangulationInterface" /> exposed to LookupEngine.
/// </summary>
/// <param name="triangulation">The triangulation interface to expose.</param>
public sealed class TriangulationInterfaceDescriptor(TriangulationInterface triangulation) : Descriptor, IDescriptorConfigurator
{
    /// <inheritdoc />
    public void Configure(IMemberConfigurator configuration)
    {
        configuration.Member(nameof(TriangulationInterface.Dispose)).Disable();
        configuration.Extension(nameof(FacetingUtils.ConvertTrianglesToQuads)).Register(() => FacetingUtils.ConvertTrianglesToQuads(triangulation));
    }
}
