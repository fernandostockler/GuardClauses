namespace GuardClauses.Extensions;

public static partial class GuardClausesExtensions
{
    /// <summary>
    /// Guard against a out of range argument.<para/>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="minimunValue"/> value is grater than <paramref name="maximunValue"/> value.<para/>
    /// Throw an <see cref="ArgumentException" /> if the <paramref name="input"/> is not in a valid range of values.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="input">The value to be validate.</param>
    /// <param name="minimunValue">The mininum value in the range of values.</param>
    /// <param name="maximunValue">The maximun value alowed.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The <paramref name="input"/> value.</returns>
    /// <exception cref="ArgumentException"></exception>
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

        message ??= $"Input ({input}) was out of range. Minimun: {minimunValue}, Maximun: {maximunValue}.";

        if (input.CompareTo(minimunValue) < 0 ||
            input.CompareTo(maximunValue) > 0)
            throw new ArgumentOutOfRangeException(nameof(input), input, message);

        return input;
    }

    /// <summary>
    /// Guard against an out of range value.<para/>
    /// Throw a <see cref="ArgumentOutOfRangeException" /> if any element in the <paramref name="values"/> is not in a valid range of values.<para/>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="minimunValue"/> contains any element with a value grater than <paramref name="maximunValue"/> value.<para/>
    /// Throw an <see cref="ArgumentException" /> if the <paramref name="values"/> contains any element that is not in a valid range of values.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="values">The value to be validate.</param>
    /// <param name="minimunValue">The mininum value in the range of values.</param>
    /// <param name="maximunValue">The maximun value alowed.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The <paramref name="values"/> value.</returns>
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
            throw new ArgumentException($"The minimum value (Min: {minimunValue}) cannot be greater than the maximum value (Max: {maximunValue}).", paramName);

        foreach (T value in values.Where(value =>
            value.CompareTo(minimunValue) < 0 ||
            value.CompareTo(maximunValue) > 0))
        {
            message ??= $"Input ({value}) was out of range. Minimun: {minimunValue}, Maximun: {maximunValue}";
            throw new ArgumentOutOfRangeException(nameof(values), value, message);
        }

        return values;
    }
}