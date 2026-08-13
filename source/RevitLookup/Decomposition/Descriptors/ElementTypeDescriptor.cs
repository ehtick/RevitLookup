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

#if REVIT2025_OR_GREATER
using Autodesk.Revit.DB.Structure;
#endif

#if REVIT2026_OR_GREATER
using Autodesk.Revit.DB.ExternalData;
using System.Runtime.CompilerServices;
using RevitLookup.Decomposition.Extensions;
#endif

namespace RevitLookup.Decomposition.Descriptors;

/// <summary>
///     Represents the <see cref="Autodesk.Revit.DB.ElementType" /> exposed to LookupEngine.
/// </summary>
/// <param name="elementType">The element type to expose.</param>
public sealed class ElementTypeDescriptor(ElementType elementType) : ElementDescriptor(elementType)
{
    /// <inheritdoc />
    public override void Configure(IMemberConfigurator configuration)
    {
        configuration.Member(nameof(ElementType.Dispose)).Disable();
#if REVIT2025_OR_GREATER
        if (elementType.Category?.Id.IsCategory(BuiltInCategory.OST_RebarSpliceType) == true)
        {
            configuration.Extension("GetRebarSpliceLapLengthMultiplier").Register(() => RebarSpliceTypeUtils.GetLapLengthMultiplier(elementType.Document, elementType.Id));
            configuration.Extension("GetRebarSpliceShiftOption").Register(() => RebarSpliceTypeUtils.GetShiftOption(elementType.Document, elementType.Id));
            configuration.Extension("GetRebarSpliceStaggerLengthMultiplier").Register(() => RebarSpliceTypeUtils.GetStaggerLengthMultiplier(elementType.Document, elementType.Id));
            configuration.Extension("SetRebarSpliceLapLengthMultiplier").Map(nameof(RebarSpliceTypeUtils.SetLapLengthMultiplier)).NotSupported();
            configuration.Extension("SetRebarSpliceShiftOption").Map(nameof(RebarSpliceTypeUtils.SetShiftOption)).NotSupported();
            configuration.Extension("SetRebarSpliceStaggerLengthMultiplier").Map(nameof(RebarSpliceTypeUtils.SetStaggerLengthMultiplier)).NotSupported();
        }
#endif
#if REVIT2026_OR_GREATER

        if (elementType.Category?.Id.IsCategory(BuiltInCategory.OST_RebarCrankType) == true)
        {
            configuration.Extension("GetRebarCrankLengthMultiplier").Register(() => RebarCrankTypeUtils.GetCrankLengthMultiplier(elementType.Document, elementType.Id));
            configuration.Extension("GetRebarCrankOffsetMultiplier").Register(() => RebarCrankTypeUtils.GetCrankOffsetMultiplier(elementType.Document, elementType.Id));
            configuration.Extension("GetRebarCrankRatio").Register(() => RebarCrankTypeUtils.GetCrankRatio(elementType.Document, elementType.Id));
            configuration.Extension("SetRebarCrankLengthMultiplier").Map(nameof(RebarCrankTypeUtils.SetCrankLengthMultiplier)).NotSupported();
            configuration.Extension("SetRebarCrankOffsetMultiplier").Map(nameof(RebarCrankTypeUtils.SetCrankOffsetMultiplier)).NotSupported();
            configuration.Extension("SetRebarCrankRatio").Map(nameof(RebarCrankTypeUtils.SetCrankRatio)).NotSupported();
        }

        if (RevitApiContext.Application.Version.Minor >= 3)
        {
            ConfigureCoordinationModelExtensions(configuration);
        }
#endif
    }
#if REVIT2026_OR_GREATER

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void ConfigureCoordinationModelExtensions(IMemberConfigurator configuration)
    {
        var isCoordinationModelType = SafeEvaluate(() => CoordinationModelLinkUtils.IsCoordinationModelType(elementType.Document, elementType));
        configuration.Extension(nameof(CoordinationModelLinkUtils.IsCoordinationModelType)).Register(() => isCoordinationModelType);

        if (isCoordinationModelType)
        {
            configuration.Extension("GetCoordinationModelTransparencyOverride").Register(() => CoordinationModelLinkUtils.GetTransparencyOverride(elementType.Document, elementType.Document.ActiveView, elementType));
            configuration.Extension(nameof(CoordinationModelLinkUtils.GetCoordinationModelTypeData)).Register(() => CoordinationModelLinkUtils.GetCoordinationModelTypeData(elementType.Document, elementType));
            configuration.Extension(nameof(CoordinationModelLinkUtils.ReloadLocalCoordinationModelFrom)).NotSupported();
            configuration.Extension(nameof(CoordinationModelLinkUtils.ReloadAutodeskDocsCoordinationModelFrom)).NotSupported();
            configuration.Extension("ContainsCoordinationModelCategory").Map(nameof(CoordinationModelLinkUtils.ContainsCategory)).NotSupported();
            configuration.Extension("GetCoordinationModelColorOverride").Map(nameof(CoordinationModelLinkUtils.GetColorOverride)).NotSupported();
            configuration.Extension("GetCoordinationModelColorOverrideForCategory").Map(nameof(CoordinationModelLinkUtils.GetColorOverrideForCategory)).NotSupported();
            configuration.Extension("GetCoordinationModelVisibilityOverride").Map(nameof(CoordinationModelLinkUtils.GetVisibilityOverride)).NotSupported();
            configuration.Extension("GetCoordinationModelVisibilityOverrideForCategory").Map(nameof(CoordinationModelLinkUtils.GetVisibilityOverrideForCategory)).NotSupported();
            configuration.Extension("ReloadCoordinationModel").Map(nameof(CoordinationModelLinkUtils.Reload)).NotSupported();
            configuration.Extension("UnloadCoordinationModel").Map(nameof(CoordinationModelLinkUtils.Unload)).NotSupported();
            configuration.Extension("SetCoordinationModelColorOverride").Map(nameof(CoordinationModelLinkUtils.SetColorOverride)).NotSupported();
            configuration.Extension("SetCoordinationModelColorOverrideForCategory").Map(nameof(CoordinationModelLinkUtils.SetColorOverrideForCategory)).NotSupported();
            configuration.Extension("SetCoordinationModelTransparencyOverride").Map(nameof(CoordinationModelLinkUtils.SetTransparencyOverride)).NotSupported();
            configuration.Extension("SetCoordinationModelVisibilityOverride").Map(nameof(CoordinationModelLinkUtils.SetVisibilityOverride)).NotSupported();
            configuration.Extension("SetCoordinationModelVisibilityOverrideForCategory").Map(nameof(CoordinationModelLinkUtils.SetVisibilityOverrideForCategory)).NotSupported();
        }
    }
#endif
}
