namespace GuardClauses.Extensions;

using System.Text.RegularExpressions;

public static partial class GuardClausesExtensions
{
    /// <summary>
    /// Guard against zero value.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="input">The value to be validate.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The input value.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static T Zero<T>([NotNull] this IGuardClause guardClause,
        T input,
        [NotNull, CallerArgumentExpression(nameof(input))] string paramName = "",
        string? message = null) where T : struct
    {
        return EqualityComparer<T>.Default.Equals(input, default)
            ? throw new ArgumentException(message ?? $"Input {paramName} cannot be zero.", paramName)
            : input;
    }

    /// <summary>
    /// Guard against negative value.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="input">The value to be validate.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The input value.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static T Negative<T>([NotNull] this IGuardClause guardClause,
        T input,
        [NotNull, CallerArgumentExpression(nameof(input))] string paramName = "",
        string? message = null) where T : struct, IComparable
    {
        return input.CompareTo(default(T)) < 0
            ? throw new ArgumentException(message ?? $"Input {paramName} cannot be negative.", paramName)
            : input;
    }

    /// <summary>
    /// Guard against zero or negative value.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="input">The value to be validate.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The input value.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static T NegativeOrZero<T>([NotNull] this IGuardClause guardClause,
        T input,
        [NotNull, CallerArgumentExpression(nameof(input))] string paramName = "",
        string? message = null) where T : struct, IComparable
    {
        return input.CompareTo(default(T)) <= 0
            ? throw new ArgumentException(message ?? $"Input {paramName} cannot be zero or negative.", paramName)
            : input;
    }

    /// <summary>
    /// Guard against a input that does not match with a Regex pattern.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="input">The value to be validate.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The input value.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static string InvalidRegexFormat([NotNull] this IGuardClause guardClause,
        [NotNull] string input,
        [NotNull] string pattern,
        [NotNull, CallerArgumentExpression(nameof(input))] string paramName = "",
        string? message = null)
    {
        var match = Regex.Match(input, pattern);

        return match.Success && input == match.Value
            ? input
            : throw new ArgumentException(message ?? $"Input {paramName} was not in required format.", paramName);
    }

    /// <summary>
    /// Guard against a input that does not pass a predicate function.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="input">The value to be validate.</param>
    /// <param name="predicate">A function to find if a input is valid or not.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The input value.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static T InvalidInput<T>([NotNull] this IGuardClause guardClause,
        [NotNull] T input,
        [NotNull] Predicate<T> predicate,
        [NotNull, CallerArgumentExpression(nameof(input))] string paramName = "",
        string? message = null)
    {
        if (input is null) throw new ArgumentNullException(paramName);

        return !predicate(input)
            ? throw new ArgumentException(message ?? $"Input {paramName} did not satisfy the conditions.", paramName)
            : input;
    }
}
