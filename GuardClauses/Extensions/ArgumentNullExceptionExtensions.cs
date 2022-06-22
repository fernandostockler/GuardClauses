namespace GuardClauses.Extensions;

using System;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

public static partial class GuardClausesExtensions
{
    /// <summary>
    /// Throws a ArgumentNullException ir the input is null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="guardClause"></param>
    /// <param name="input"></param>
    /// <param name="paramName"></param>
    /// <param name="message"></param>
    /// <returns>The input.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static T Null<T>([NotNull] this IGuardClause guardClause,
        T input,
        [NotNull][CallerArgumentExpression("input")] string paramName = "Undefined",
        string message = "Parameter can't be null.")
    {
        return input is null ? throw new ArgumentNullException(paramName, message) : input;
    }

    public static T NullOrEmpty<T>([NotNull] this IGuardClause guardClause,
        T input,
        [NotNull][CallerArgumentExpression("input")] string paramName = "Undefined",
        string message = "Parameter can't be empty.")
    {
        _ = Guard.Against.Null(input, paramName);

        return input switch
        {
            string value when value == string.Empty
                => throw new ArgumentException(message, paramName),

            Guid guid when guid == Guid.Empty
                => throw new ArgumentException(message, paramName),

            _ => input,
        };
    }

    public static IEnumerable<T> NullOrEmpty<T>([NotNull] this IGuardClause guardClause,
        IEnumerable<T> values,
        [NotNull][CallerArgumentExpression("values")] string paramName = "Undefined",
        string message = "Parameter can't be empty.")
    {
        _ = Guard.Against.Null(values, paramName);

        return !values.Any() ? throw new ArgumentException(message, paramName) : values;
    }

    public static string NullOrWhiteSpace([NotNull] this IGuardClause guardClause,
        string input,
        [NotNull][CallerArgumentExpression("input")] string paramName = "Undefined",
        string message = "Parameter can't be white spaces.")
    {
        _ = Guard.Against.NullOrEmpty(input, paramName);

        return input.Trim().Length == 0 ? throw new ArgumentException(message, paramName) : input;
    }
}
