namespace RevitLookup.Decomposition.Schemas;

/// <summary>
///     Thrown when the extensible storage access check cannot be located or patched.
/// </summary>
public sealed class SchemaAccessException(string message) : Exception(message);