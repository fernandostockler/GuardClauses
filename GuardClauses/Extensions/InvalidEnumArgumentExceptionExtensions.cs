﻿namespace GuardClauses.Extensions;

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

public static class InvalidEnumArgumentExceptionExtensions
{
    public static T EnumOutOfRange<T>([NotNull] this IGuardClause guardClause,
            T input,
            [NotNull, CallerArgumentExpression(nameof(input))] string paramName = "",
            string? message = null) where T : struct, Enum
    {
        if (!Enum.IsDefined(typeof(T), input))
        {
            throw message is null
                ? new InvalidEnumArgumentException(paramName, Convert.ToInt32(input), typeof(T))
                : new InvalidEnumArgumentException(message);
        }

        return input;
    }
}