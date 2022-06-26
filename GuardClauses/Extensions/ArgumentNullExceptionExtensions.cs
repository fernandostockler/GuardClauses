namespace GuardClauses.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

public static partial class GuardClausesExtensions
{
    /// <summary>
    /// Guard against null value.
    /// </summary>
    /// <typeparam name="T">Any type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="input">The value to be validate.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The input value.</returns>
    /// <exception cref="ArgumentNullException">Thrown if input is null.</exception>
    public static T Null<T>([NotNull] this IGuardClause guardClause,
        T input,
        [NotNull, CallerArgumentExpression(nameof(input))] string paramName = "",
        string message = "Parameter cannot be null.")
    {
        return input is null
            ? throw new ArgumentNullException(paramName, message)
            : input;
    }

    public static T NullOrEmpty<T>([NotNull] this IGuardClause guardClause,
        T input,
        [NotNull, CallerArgumentExpression(nameof(input))] string paramName = "",
        string message = "Parameter cannot be empty.")
    {
        _ = Guard.Against.Null(input, paramName);

        return input switch
        {
            string value when string.IsNullOrEmpty(value)
                => throw new ArgumentException(message, paramName),

            Guid guid when guid == Guid.Empty
                => throw new ArgumentException(message, paramName),

            _ => input,
        };
    }

    public static IEnumerable<T> NullOrEmpty<T>([NotNull] this IGuardClause guardClause,
        [NotNull] IEnumerable<T>? values,
        [NotNull, CallerArgumentExpression(nameof(values))] string paramName = "",
        string message = "Parameter cannot be empty.")
    {
        _ = Guard.Against.Null(values, paramName);

        return values.Any()
            ? values
            : throw new ArgumentException(message, paramName);
    }

    public static List<T> NullOrEmpty<T>([NotNull] this IGuardClause guardClause,
    [NotNull] List<T> values,
    [NotNull, CallerArgumentExpression(nameof(values))] string paramName = "",
    string message = "Parameter cannot be empty.")
    {
        _ = Guard.Against.Null(values, paramName);

        return values.Any()
            ? values
            : throw new ArgumentException(message, paramName);
    }

    public static string NullOrWhiteSpace([NotNull] this IGuardClause guardClause,
        string input,
        [NotNull, CallerArgumentExpression(nameof(input))] string paramName = "",
        string message = "Parameter cannot be white spaces.")
    {
        _ = Guard.Against.NullOrEmpty(input, paramName);

        return string.IsNullOrWhiteSpace(input)
            ? throw new ArgumentException(message, paramName)
            : input;
    }

    public static IEnumerable<string> NullOrWhiteSpace([NotNull] this IGuardClause guardClause,
        IEnumerable<string> values,
        [NotNull, CallerArgumentExpression(nameof(values))] string paramName = "",
        string message = "Parameter cannot contain white spaces.")
    {
        _ = Guard.Against.NullOrEmpty(values, paramName);

        foreach (var _ in values
            .Where(value => string.IsNullOrWhiteSpace(value))
            .Select(value => new { }))
        {
            throw new ArgumentException(message, paramName);
        }

        return values;
    }
}
