namespace GuardClauses.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

public static partial class GuardClausesExtensions
{
    public static T OutOfRange<T>(this IGuardClause guardClause,
        T input,
        T minimunValue,
        T maximunValue,
        string paramName,
        string? message = null) where T : IComparable, IComparable<T>
    {
        if (minimunValue.CompareTo(maximunValue) > 0)
            throw new ArgumentException($"Minimun value ({minimunValue}) must be below or equal maximun value ({maximunValue}).", nameof(minimunValue));

        if (message is null)
            message = $"Input ({input}) was out of range. Minimun: {minimunValue}, Maximun: {maximunValue}";

        if (input.CompareTo(minimunValue) < 0 || input.CompareTo(maximunValue) > 0)
            throw new ArgumentOutOfRangeException(nameof(input), input, message);

        return input;
    }

    public static IEnumerable<T> OutOfRange<T>(this IGuardClause guardClause,
        IEnumerable<T> values,
        T minimunValue,
        T maximunValue,
        string paramName,
        string? message = null) where T : IComparable, IComparable<T>
    {
        if (minimunValue.CompareTo(maximunValue) > 0)
            throw new ArgumentException($"Minimun value ({minimunValue}) must be below or equal maximun value ({maximunValue}).", nameof(minimunValue));

        foreach (T value in values.Where(value =>
            value.CompareTo(minimunValue) < 0 || value.CompareTo(maximunValue) > 0))
        {
            if (message is null)
                message = $"Input ({value}) was out of range. Minimun: {minimunValue}, Maximun: {maximunValue}";

            throw new ArgumentOutOfRangeException(nameof(values), values, message);
        }

        return values;
    }
}
