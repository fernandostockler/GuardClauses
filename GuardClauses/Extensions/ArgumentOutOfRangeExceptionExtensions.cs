namespace GuardClauses.Extensions;

public static partial class GuardClausesExtensions
{
    /// <summary>
    /// Throw a ArgumentOutOfRangeException if the input is not in a valid range of values.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="input">The value to be validate.</param>
    /// <param name="minimunValue">The mininum value in the range of values.</param>
    /// <param name="maximunValue">The maximun value alowed.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The input value.</returns>
    /// <exception cref="ArgumentException">If the minimun value is grater than maximun value.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static T OutOfRange<T>([NotNull] this IGuardClause guardClause,
        T input,
        T minimunValue,
        T maximunValue,
        [NotNull, CallerArgumentExpression(nameof(input))] string paramName = "",
        string? message = null) where T : IComparable, IComparable<T>
    {
        if (minimunValue.CompareTo(maximunValue) > 0)
            throw new ArgumentException($"Minimun value ({minimunValue}) must be below or equal maximun value ({maximunValue}).", paramName);

        if (message is null)
            message = $"Input ({input}) was out of range. Minimun: {minimunValue}, Maximun: {maximunValue}.";

        if (input.CompareTo(minimunValue) < 0 || input.CompareTo(maximunValue) > 0)
            throw new ArgumentOutOfRangeException(nameof(input), input, message);

        return input;
    }

    /// <summary>
    /// Throw a ArgumentOutOfRangeException if any element in the input is not in a valid range of values.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="input">The value to be validate.</param>
    /// <param name="minimunValue">The mininum value in the range of values.</param>
    /// <param name="maximunValue">The maximun value alowed.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The input value.</returns>
    /// <exception cref="ArgumentException">If the minimun value is grater than maximun value.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static IEnumerable<T> OutOfRange<T>([NotNull] this IGuardClause guardClause,
        IEnumerable<T> values,
        T minimunValue,
        T maximunValue,
        [NotNull, CallerArgumentExpression(nameof(values))] string paramName = "",
        string? message = null) where T : IComparable, IComparable<T>
    {
        if (minimunValue.CompareTo(maximunValue) > 0)
            throw new ArgumentException($"Minimun value ({minimunValue}) must be below or equal maximun value ({maximunValue}).", paramName);

        foreach (T value in values.Where(value =>
            value.CompareTo(minimunValue) < 0 || value.CompareTo(maximunValue) > 0))
        {
            if (message is null)
                message = $"Input ({value}) was out of range. Minimun: {minimunValue}, Maximun: {maximunValue}";

            throw new ArgumentOutOfRangeException(nameof(values), value, message);
        }

        return values;
    }
}
