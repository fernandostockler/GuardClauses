namespace GuardClauses;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Guard clause.
/// </summary>
public class Guard : IGuardClause
{
    /// <summary>
    /// A entry point for the extensions methods.
    /// </summary>
    [NotNull] public static IGuardClause Against { get; } = new Guard();
}