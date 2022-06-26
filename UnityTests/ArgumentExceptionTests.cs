namespace UnitTests;

public class ArgumentExceptionTests
{
    [TestCase(0.0f)]
    [TestCase(0.0d)]
    [TestCase(0)]
    [TestCase(0.00)]
    public void ZeroTestWithZero<T>(T value) where T : struct
    {
        _ = Invoking(() => Guard.Against.Zero(value, "paramName"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"Input paramName cannot be zero. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [TestCase(0.23f)]
    [TestCase(10.40d)]
    [TestCase(2)]
    [TestCase(0.10)]
    public void ZeroTestWithNonZero<T>(T value) where T : struct
    {
        _ = Invoking(() => Guard.Against.Zero(value, "paramName"))
            .Should()
            .NotThrow();
    }

    [TestCase(-10.30f)]
    [TestCase(-0.50d)]
    [TestCase(-20)]
    [TestCase(-50.33)]
    public void NegativeTestWithNegativeValues<T>(T value) where T : struct, IComparable
    {
        _ = Invoking(() => Guard.Against.Negative(value, "paramName"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"Input paramName cannot be negative. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [TestCase(10.30f)]
    [TestCase(0.50d)]
    [TestCase(20)]
    [TestCase(0)]
    public void NegativeTestWithNonNegativeValues<T>(T value) where T : struct, IComparable
    {
        _ = Invoking(() => Guard.Against.Negative(value, "paramName"))
            .Should()
            .NotThrow();
    }

    [TestCase(-10.30f)]
    [TestCase(-0.50d)]
    [TestCase(0.0f)]
    [TestCase(-20)]
    [TestCase(0)]
    [TestCase(-50.33)]
    public void NegativeOrZeroTestWithNegativeValues<T>(T value) where T : struct, IComparable
    {
        _ = Invoking(() => Guard.Against.NegativeOrZero(value, "paramName"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"Input paramName cannot be zero or negative. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [TestCase(10.30f)]
    [TestCase(0.50d)]
    [TestCase(20)]
    public void NegativeOrZeroTestWithNonNegativeValues<T>(T value) where T : struct, IComparable
    {
        _ = Invoking(() => Guard.Against.NegativeOrZero(value, "paramName"))
            .Should()
            .NotThrow();
    }

    [TestCase("teste")]
    [TestCase("teste.")]
    [TestCase("teste.com")]
    [TestCase("teste#com.")]
    [TestCase("@com")]
    [TestCase("teste@")]
    [TestCase("teste.com.br")]
    public void RegexEmailValidationWithInvalidEmail(string value)
    {
        var pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        _ = Invoking(() => Guard.Against.InvalidRegexFormat(value, pattern, "paramName"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"Input paramName was not in required format. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [TestCase("teste@domain.com")]
    [TestCase("teste.teste@domain.com.br")]
    [TestCase("b1@domain.com")]
    [TestCase("teste@domain.edu.fr")]
    [TestCase("teste@domain.com.br")]
    public void RegexEmailValidationWithValidEmail(string value)
    {
        var pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        _ = Invoking(() => Guard.Against.InvalidRegexFormat(value, pattern, "paramName"))
            .Should()
            .NotThrow();
    }

    [Test]
    public void InvalidInputTestWithNullValue()
    {
        string input = null;

        _ = Invoking(() => Guard.Against.InvalidInput(input, x => x.StartsWith("test"), "paramName"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"Value cannot be null. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [TestCase(5)]
    [TestCase(9)]
    [TestCase(7)]
    [TestCase(11)]
    public void InvalidInputTestWithInvalidValue(int input)
    {
        _ = Invoking(() => Guard.Against.InvalidInput(input, x => x % 2 == 0, "paramName"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"Input paramName did not satisfy the conditions. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [TestCase(2)]
    [TestCase(4)]
    [TestCase(20)]
    [TestCase(8)]
    public void InvalidInputTestWithValidValue(int value)
    {
        _ = Invoking(() => Guard.Against.InvalidInput(value, x => x % 2 == 0, "paramName"))
            .Should()
            .NotThrow();
    }
}