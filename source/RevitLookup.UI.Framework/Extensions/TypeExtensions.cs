namespace RevitLookup.UI.Framework.Extensions;

[PublicAPI]
public static class TypeExtensions
{
    extension(Type type)
    {
        /// <summary>
        ///     Determines whether the specified type is a primitive type.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> if the type is a primitive type, an enumeration, or a string; otherwise, false.
        /// </returns>
        public bool IsPrimitiveType() => type.IsPrimitive || type.IsEnum || type == typeof(string);
    }
}
