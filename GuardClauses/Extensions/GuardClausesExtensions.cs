namespace GuardClauses.Extensions;

using System;

/// <summary>
/// Extensions
/// </summary>
public static partial class GuardClausesExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input"></param>
    /// <param name="predicate"></param>
    /// <param name="paramName"></param>
    /// <param name="message"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static T And<T>(this T input,
        Func<T, bool> predicate,
        string paramName,
        string? message = null,
        [CallerArgumentExpression(nameof(predicate))] string? condition = null)
    {
        _ = Guard.Against.Null(predicate, paramName);

        return predicate(Guard.Against.Null(input, paramName))
            ? input
            : throw new ArgumentException(
                message ?? $"Input {paramName} did not satisfy the conditions ({condition}).", paramName);
    }
}