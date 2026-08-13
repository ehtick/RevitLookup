using Autodesk.Revit.DB.ExtensibleStorage;

namespace RevitLookup.Decomposition.Schemas;

/// <summary>
///     Provides extension methods for <see cref="Schema" /> to temporarily elevate access permissions.
/// </summary>
[PublicAPI]
public static class SchemaExtensions
{
    /// <param name="schema">The schema to grant access to.</param>
    extension(Schema schema)
    {
        /// <summary>
        ///     Begins a scope that grants unrestricted read access to the schema.
        /// </summary>
        /// <returns>A scope that represents the granted access.</returns>
        /// <remarks>
        ///     Access is automatically revoked when the returned scope is disposed.
        /// </remarks>
        /// <example>
        ///     <code>
        ///         using (schema.GrantAccess())
        ///         {
        ///             var entity = element.GetEntity(schema);
        ///         }
        ///     </code>
        /// </example>
        public IDisposable GrantAccess()
        {
            return SchemaAccessScope.Open();
        }
    }
}
