namespace GuardClauses;
using System.Diagnostics.CodeAnalysis;

public class Guard : IGuardClause
{
    [NotNull] public static IGuardClause Against { get; } = new Guard();
}
