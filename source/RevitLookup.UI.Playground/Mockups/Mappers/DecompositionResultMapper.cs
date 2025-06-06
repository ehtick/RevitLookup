﻿using LookupEngine.Abstractions;
using RevitLookup.Abstractions.ObservableModels.Decomposition;
using Riok.Mapperly.Abstractions;

namespace RevitLookup.UI.Playground.Mockups.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Source)]
public static partial class DecompositionResultMapper
{
    public static partial ObservableDecomposedObject Convert(DecomposedObject decomposedObject);
    public static partial ObservableDecomposedMember Convert(DecomposedMember decomposedMember);
}