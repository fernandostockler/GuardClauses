namespace GuardClauses.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public static partial class GuardClausesExtensions
{
    public static T Zero<T>([NotNull] this IGuardClause guardClause,
        T input,
        [NotNull][CallerArgumentExpression("input")] string paramName = "Undefined",
        string? message = null) where T : struct
    {
        return EqualityComparer<T>.Default.Equals(input, default(T))
            ? throw new ArgumentException(message ?? $"Input {paramName} cannot be zero.", paramName)
            : input;
    }

    public static T Negative<T>([NotNull] this IGuardClause guardClause,
            T input,
            [NotNull][CallerArgumentExpression("input")] string paramName = "Undefined",
            string? message = null) where T : struct, IComparable
    {
        return input.CompareTo(default(T)) < 0
            ? throw new ArgumentException(message ?? $"Input {paramName} cannot be negative.", paramName)
            : input;
    }

    public static T NegativeOrZero<T>([NotNull] this IGuardClause guardClause,
        T input,
        [NotNull][CallerArgumentExpression("input")] string paramName = "Undefined",
        string? message = null) where T : struct, IComparable
    {
        return input.CompareTo(default(T)) <= 0
            ? throw new ArgumentException(message ?? $"Input {paramName} cannot be zero or negative.", paramName)
            : input;
    }

    public static string InvalidFormat([NotNull] this IGuardClause guardClause,
        [NotNull] string input,
        [NotNull] string pattern,
        [NotNull][CallerArgumentExpression("input")] string paramName = "Undefined",
        string? message = null)
    {
        var match = Regex.Match(input, pattern);

        return match.Success && input == match.Value
            ? input
            : throw new ArgumentException(message ?? $"Input {paramName} was not in required format.", paramName);
    }

    public static T InvalidInput<T>([NotNull] this IGuardClause guardClause,
        [NotNull] T input,
        [NotNull] Predicate<T> predicate,
        [NotNull][CallerArgumentExpression("input")] string paramName = "Undefined",
        string? message = null)
    {
        if (input == null) throw new ArgumentNullException(nameof(predicate));

        return !predicate(input)
            ? throw new ArgumentException(message ?? $"Input {paramName} did not satisfy the conditions.", paramName)
            : input;
    }
}
