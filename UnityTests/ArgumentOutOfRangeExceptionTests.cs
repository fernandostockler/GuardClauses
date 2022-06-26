namespace UnitTests;

public class ArgumentOutOfRangeExceptionTests
{
    [TestCase(5, 4, 3)]
    [TestCase(5f, 4f, 3f)]
    [TestCase(5d, 4d, 3d)]
    public void OutOfRangeTestWithMinimumGraterThanMaximun<T>(T min, T max, T value)
        where T : IComparable, IComparable<T>
    {
        _ = Invoking(() => Guard.Against.OutOfRange(value, min, max, "minimunValue"))
                .Should().Throw<ArgumentException>()
                .WithMessage($"Minimun value ({min}) must be below or equal maximun value ({max}). (Parameter 'minimunValue')")
                .WithParameterName("minimunValue");
    }

    [TestCase(5, 10, 3)]
    [TestCase(5f, 10f, 11f)]
    [TestCase(5d, 10d, 4d)]
    public void OutOfRangeTestWithValueOutOfRange<T>(T min, T max, T value)
        where T : IComparable, IComparable<T>
    {
        _ = Invoking(() => Guard.Against.OutOfRange(value, min, max, "input"))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage($"Input ({value}) was out of range. Minimun: {min}, Maximun: {max}. (Parameter 'input'){Environment.NewLine}Actual value was {value}.")
                .WithParameterName("input");
    }

    [TestCase(5, 10, 5)]
    [TestCase(5f, 10f, 10f)]
    [TestCase(5d, 10d, 6d)]
    public void OutOfRangeTestWithValidValue<T>(T min, T max, T value)
        where T : IComparable, IComparable<T>
    {
        _ = Invoking(() => Guard.Against.OutOfRange(value, min, max, "input"))
                .Should().NotThrow();
    }

    [TestCase(1, 2, 6, 0, 5, 6)]
    [TestCase(-2, 2, 5, 0, 5, -2)]
    [TestCase(1.3, 2.4, 5.1, 0.0, 5.0, 5.1)]
    [TestCase(-2.0, 2.1, 5, 0.0, 5.0, -2.0)]
    [TestCase(1.3f, 2.4f, 7.5f, 0.0f, 5.0f, 7.5f)]
    [TestCase(-1.0f, 2.1f, 5.0f, 0.0f, 5.0f, -1.0f)]
    public void OutOfRangeTestWithOutOfRangeItemInList<T>(T Item0, T Item1, T Item2, T min, T max, T actual)
        where T : IComparable, IComparable<T>
    {
        List<T> list = new() { Item0, Item1, Item2 };

        _ = Invoking(() => Guard.Against.OutOfRange(list, min, max, "values"))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage($"Input ({actual}) was out of range. Minimun: {min}, Maximun: {max} (Parameter 'values'){Environment.NewLine}Actual value was {actual}.")
            .WithParameterName("values");
    }

    [TestCase(1, 2, 3, 0, 5)]
    [TestCase(0, 2, 5, 0, 5)]
    [TestCase(1.3, 2.4, 3.5, 0.0, 5.0)]
    [TestCase(0, 2.1, 5, 0.0, 5.0)]
    [TestCase(1.3f, 2.4f, 3.5f, 0.0f, 5.0f)]
    [TestCase(0f, 2.1f, 5.0f, 0.0f, 5.0f)]
    public void OutOfRangeTestWithInRangeItemsInList<T>(T Item0, T Item1, T Item2, T min, T max)
        where T : IComparable, IComparable<T>
    {
        var list = new List<T>() { Item0, Item1, Item2 };

        _ = Invoking(() => Guard.Against.OutOfRange(list, min, max, "values"))
            .Should().NotThrow();
    }

    [TestCase(1, 2, 3, 4, 3)]
    [TestCase(-3, -2, 0, 0, -3)]
    public void OutOfRangeTestWithMinimumGraterThanMaximunInList<T>(T Item0, T Item1, T Item2, T min, T max)
        where T : IComparable, IComparable<T>
    {
        _ = Invoking(() => Guard.Against.OutOfRange(values: new List<T>() { Item0, Item1, Item2 }, minimunValue: min, maximunValue: max, paramName: "values"))
            .Should().Throw<ArgumentException>()
            .WithMessage($"Minimun value ({min}) must be below or equal maximun value ({max}). (Parameter 'values')")
            .WithParameterName("values");
    }
}