namespace GuardClauses.Extensions;

/// <summary>
/// Some Guard clauses
/// </summary>
public static partial class GuardClausesExtensions
{
    /// <summary>
    /// Guard against null value.<para/>
    /// Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A <see cref="IGuardClause"/>.</param>
    /// <param name="input">The value to be validate.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The <paramref name="input"/>  value.</returns>
    /// <exception cref="ArgumentNullException">Thrown if input is null.</exception>
    public static T Null<T>([NotNull] this IGuardClause guardClause,
        [NotNull] T input,
        [NotNull, CallerArgumentExpression(nameof(input))] string paramName = "",
        string? message = null)
    {
        return input is null
            ? throw new ArgumentNullException(paramName, message ?? $"{paramName} cannot be null.")
            : input;
    }

    /// <summary>
    /// Guard aganist null o empty values.<para/>
    /// Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.<para/>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is an empty string or a empty Guid.<para/>
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="input">The value to be validate.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The <paramref name="input"/>  value.</returns>
    /// <exception cref="ArgumentException"></exception>
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

    /// <summary>
    /// Guard against null or empty values in IEnumerable.<para/>
    /// Throws an <see cref="ArgumentNullException" /> if <paramref name="values" /> is null.<para/>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="values" /> contains any empty string or a empty Guid.<para/>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="values" /> do not contains any element.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="values">The value to be validate.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The <paramref name="values"/>  value.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<T> NullOrEmpty<T>([NotNull] this IGuardClause guardClause,
        [NotNull] IEnumerable<T> values,
        [NotNull, CallerArgumentExpression(nameof(values))] string paramName = "",
        string message = "Parameter cannot be empty.")
    {
        _ = Guard.Against.Null(values, paramName);

        return values.Any()
            ? values
            : throw new ArgumentException(message, paramName);
    }

    /// <summary>
    /// Guard against null or empty values in List.<para/>
    /// Throws an <see cref="ArgumentNullException" /> if <paramref name="values" /> is null.<para/>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="values" /> contains any empty string or a empty Guid.<para/>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="values" /> do not contains any element.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="values">The value to be validate.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The <paramref name="values"/>  value.</returns>
    /// <exception cref="ArgumentException"></exception>
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

    /// <summary>
    /// Guard against null or white spaces in a string value.<para/>
    /// Throws an <see cref="ArgumentNullException" /> if <paramref name="input" /> is null.<para/>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> is an empty string or a empty Guid.<para/>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="input" /> contains only white spaces.<para/>
    /// </summary>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="input">The value to be validate.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The <paramref name="input"/>  value.</returns>
    /// <exception cref="ArgumentException"></exception>
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

    /// <summary>
    /// Guard against null or white spaces in a IEnumerable{string} value.<para/>
    /// Throws an <see cref="ArgumentNullException" /> if <paramref name="values" /> is null.<para/>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="values" /> contains any element with empty string or a empty Guid.<para/>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="values" /> do not contains any element.<para/>
    /// Throws an <see cref="ArgumentException" /> if <paramref name="values" /> contains any element with only white spaces.<para/>
    /// </summary>
    /// <param name="guardClause">A IGuardClause.</param>
    /// <param name="values">The value to be validate.</param>
    /// <param name="paramName">Optional: The parameter's name. (automatically generated).</param>
    /// <param name="message">Optional: A custom message.</param>
    /// <returns>The <paramref name="values"/>  value.</returns>
    /// <exception cref="ArgumentException"></exception>
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
