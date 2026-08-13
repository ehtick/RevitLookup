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

namespace RevitLookup.Decomposition.Extensions;

/// <summary>
///     Provides extension methods for <see cref="Parameter" /> to read and write its value as a string.
/// </summary>
[PublicAPI]
public static class ParameterExtensions
{
    /// <param name="parameter">The parameter to read or write.</param>
    extension(Parameter parameter)
    {
        /// <summary>
        ///     Gets the value of the parameter formatted as a string according to its <see cref="StorageType" />.
        /// </summary>
        /// <returns>The parameter value formatted as a string.</returns>
        public string GetStringValue()
        {
            return parameter.StorageType switch
            {
                StorageType.Integer => parameter.AsInteger().ToString(),
                StorageType.Double => parameter.AsValueString(),
                StorageType.String => parameter.AsString(),
                StorageType.ElementId => parameter.AsElementId().ToString(),
                _ => parameter.AsValueString()
            };
        }

        /// <summary>
        ///     Attempts to set the value of the parameter by parsing <paramref name="value" /> according to its <see cref="StorageType" />.
        /// </summary>
        /// <param name="value">The string representation of the value to set.</param>
        /// <returns><see langword="true" /> if the value was parsed and set successfully; otherwise, <see langword="false" />.</returns>
        public bool TrySetStringValue(string value)
        {
            bool result;
            switch (parameter.StorageType)
            {
                case StorageType.Integer:
                    result = int.TryParse(value, out var intValue);
                    if (!result)
                    {
                        break;
                    }

                    result = parameter.Set(intValue);
                    break;
                case StorageType.Double:
                    result = parameter.SetValueString(value);
                    break;
                case StorageType.String:
                    result = parameter.Set(value);
                    break;
                case StorageType.ElementId:
#if REVIT2024_OR_GREATER
                    result = long.TryParse(value, out var idValue);
#else
                    result = int.TryParse(value, out var idValue);
#endif
                    if (!result)
                    {
                        break;
                    }

                    result = parameter.Set(new ElementId(idValue));
                    break;
                case StorageType.None:
                default:
                    result = parameter.SetValueString(value);
                    break;
            }

            return result;
        }
    }
}
