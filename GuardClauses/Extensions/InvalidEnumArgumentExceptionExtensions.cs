namespace GuardClauses.Extensions;

public static partial class GuardClausesExtensions
{
    /// <summary>
    /// Guard against a not defined enum.<para/>
    /// Throw a <see cref="InvalidEnumArgumentException"/> if the <paramref name="input"/> is not a defined enum.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="input">The value to be validate.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The <paramref name="input"/> value.</returns>
    /// <exception cref="InvalidEnumArgumentException"></exception>
    public static T EnumOutOfRange<T>([NotNull] this IGuardClause guardClause,
            [NotNull] T input,
            [NotNull, CallerArgumentExpression(nameof(input))] string paramName = "",
            string? message = null) where T : struct, Enum
    {
        _ = Guard.Against.Null(input, paramName);

        if (!Enum.IsDefined(typeof(T), input))
        {
            throw message is null
                ? new InvalidEnumArgumentException(paramName, Convert.ToInt32(input), typeof(T))
                : new InvalidEnumArgumentException(message);
        }

        return input;
    }
}