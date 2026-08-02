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
///     Represents the <see cref="Family"/> exposed to LookupEngine.
/// </summary>
/// <param name="family">The family to expose.</param>
public sealed class FamilyDescriptor(Family family) : ElementDescriptor(family)
{
    /// <inheritdoc/>
    public override void Configure(IMemberConfigurator configuration)
    {
        configuration.Member(nameof(Family.Dispose)).Disable();
        configuration.Extension(nameof(FamilySizeTableManager.GetFamilySizeTableManager)).Register(() => FamilySizeTableManager.GetFamilySizeTableManager(family.Document, family.Id));
        configuration.Extension(nameof(FamilyUtils.GetProfileSymbols)).Register(RegisterProfileSymbols);
        configuration.Extension(nameof(LoadedFamilyIntegrityCheck.CheckFamily)).Register(() => LoadedFamilyIntegrityCheck.CheckFamily(family.Document, family.Id));
        configuration.Extension("CanBeConvertedToFaceHostBased").Register(() => FamilyUtils.FamilyCanConvertToFaceHostBased(family.Document, family.Id));
        configuration.Extension("ConvertToFaceHostBased").Map(nameof(FamilyUtils.ConvertFamilyToFaceHostBased)).NotSupported();

        var isAdaptiveComponentFamily = SafeEvaluate(() => AdaptiveComponentFamilyUtils.IsAdaptiveComponentFamily(family));
        configuration.Extension(nameof(AdaptiveComponentFamilyUtils.IsAdaptiveComponentFamily)).Register(() => isAdaptiveComponentFamily);

        if (isAdaptiveComponentFamily)
        {
            configuration.Extension(nameof(AdaptiveComponentFamilyUtils.GetNumberOfAdaptivePoints)).Register(() => AdaptiveComponentFamilyUtils.GetNumberOfAdaptivePoints(family));
            configuration.Extension(nameof(AdaptiveComponentFamilyUtils.GetNumberOfPlacementPoints)).Register(() => AdaptiveComponentFamilyUtils.GetNumberOfPlacementPoints(family));
            configuration.Extension(nameof(AdaptiveComponentFamilyUtils.GetNumberOfShapeHandlePoints)).Register(() => AdaptiveComponentFamilyUtils.GetNumberOfShapeHandlePoints(family));
        }

        return;

        IVariant RegisterProfileSymbols()
        {
            var values = Enum.GetValues<ProfileFamilyUsage>();
            var capacity = values.Length * 2;
            var variants = Variants.Values<ICollection<ElementId>>(capacity);
            foreach (var value in values)
            {
                variants.Add(FamilyUtils.GetProfileSymbols(family.Document, value, false), $"{value}, with multiple curve loops");
                variants.Add(FamilyUtils.GetProfileSymbols(family.Document, value, true), $"{value}, with single curve loop");
            }

            return variants.Consume();
        }
    }
}