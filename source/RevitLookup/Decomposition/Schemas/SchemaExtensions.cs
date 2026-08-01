using Autodesk.Revit.DB.ExtensibleStorage;

namespace RevitLookup.Decomposition.Schemas;

[PublicAPI]
public static class SchemaExtensions
{
    /// <param name="schema">The schema to grant access to.</param>
    extension(Schema schema)
    {
        /// <summary>
        ///     Begins a scope that grants unrestricted read access to the schema.
        ///     Access is automatically revoked when the returned scope is disposed.
        /// </summary>
        /// <returns>A disposable scope. Call Dispose or use a 'using' statement to revoke access.</returns>
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