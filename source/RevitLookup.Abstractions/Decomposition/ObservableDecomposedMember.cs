using CommunityToolkit.Mvvm.ComponentModel;
using LookupEngine.Abstractions;
using LookupEngine.Abstractions.Enums;

namespace RevitLookup.Abstractions.Decomposition;

/// <summary>
///     Represents the observable model for the LookupEngine decomposed member.
/// </summary>
public sealed partial class ObservableDecomposedMember : ObservableObject
{
    /// <summary>
    ///     Gets or sets the nesting depth of the member within the decomposition tree.
    /// </summary>
    public required int Depth { get; set; }

    /// <summary>
    ///     Gets or sets the member's name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Gets or sets the simple name of the type that declares the member.
    /// </summary>
    public required string DeclaringTypeName { get; set; }

    /// <summary>
    ///     Gets or sets the fully qualified name of the type that declares the member.
    /// </summary>
    public required string DeclaringTypeFullName { get; set; }

    /// <summary>
    ///     Gets or sets the kind and visibility modifiers of the member.
    /// </summary>
    public MemberAttributes MemberAttributes { get; set; }

    /// <summary>
    ///     Gets or sets the observable model for the member's value.
    /// </summary>
    [ObservableProperty]
    public required partial ObservableDecomposedValue Value { get; set; }

    /// <summary>
    ///     Gets or sets the time, in milliseconds, spent evaluating the member.
    /// </summary>
    [ObservableProperty]
    public partial double ComputationTime { get; set; }

    /// <summary>
    ///     Gets or sets the number of bytes allocated while evaluating the member.
    /// </summary>
    [ObservableProperty]
    public partial long AllocatedBytes { get; set; }

    /// <summary>
    ///     Gets or sets the evaluation state of the member.
    /// </summary>
    [ObservableProperty]
    public partial MemberEvaluationPolicy EvaluationPolicy { get; set; }

    /// <summary>
    ///     Gets or sets the engine origin used to evaluate this member on demand.
    /// </summary>
    public DecomposedMember? Member { get; set; }
}
