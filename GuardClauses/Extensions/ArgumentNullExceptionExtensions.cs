namespace GuardClauses.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

public static partial class GuardClausesExtensions
{
    public static T Null<T>(this IGuardClause guardClause,
        T input,
        string paramName,
        string message = "Parameter can't be null.")
    {
        return input is null ? throw new ArgumentNullException(paramName, message) : input;
    }

    public static T NullOrEmpty<T>(this IGuardClause guardClause,
        T input,
        string paramName,
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

    public static IEnumerable<T> NullOrEmpty<T>(this IGuardClause guardClause,
        IEnumerable<T> values,
        string paramName,
        string message = "Parameter can't be empty.")
    {
        _ = Guard.Against.Null(values, paramName);

        return !values.Any() ? throw new ArgumentException(message, paramName) : values;
    }

    public static string NullOrWhiteSpace(this IGuardClause guardClause,
        string input,
        string paramName,
        string message = "Parameter can't be white spaces.")
    {
        _ = Guard.Against.NullOrEmpty(input, paramName);

         return input.Trim().Length == 0 ? throw new ArgumentException(message, paramName) : input;
    }
}
