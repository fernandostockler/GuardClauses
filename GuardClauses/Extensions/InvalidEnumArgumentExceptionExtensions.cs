namespace GuardClauses.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public static class InvalidEnumArgumentExceptionExtensions
{
    public static T EnumOutOfRange<T>([NotNull] this IGuardClause guardClause,
            T input,
            [NotNull][CallerArgumentExpression("input")] string parameterName = "Undefined",
            string? message = null) where T : struct, Enum
    {
        if (!Enum.IsDefined(typeof(T), input))
        {
            throw message is null
                ? new InvalidEnumArgumentException(parameterName, Convert.ToInt32(input), typeof(T))
                : new InvalidEnumArgumentException(message);
        }

        return input;
    }
}
