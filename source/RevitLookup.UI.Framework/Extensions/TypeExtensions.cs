namespace RevitLookup.UI.Framework.Extensions;

/// <summary>
///     Provides extension methods for <see cref="Type"/> to classify it as primitive.
/// </summary>
[PublicAPI]
public static class TypeExtensions
{
    /// <param name="type">The type to classify.</param>
    extension(Type type)
    {
        /// <summary>
        ///     Determines whether the type is a primitive type.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> if the type is a primitive type, an enumeration, or a <see cref="string"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsPrimitiveType() => type.IsPrimitive || type.IsEnum || type == typeof(string);
    }
}
