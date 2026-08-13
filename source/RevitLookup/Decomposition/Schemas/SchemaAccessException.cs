namespace RevitLookup.Decomposition.Schemas;

/// <summary>
///     Represents an error that occurs when the extensible storage access check cannot be located or patched.
/// </summary>
/// <param name="message">The message that describes the error.</param>
public sealed class SchemaAccessException(string message) : Exception(message);
