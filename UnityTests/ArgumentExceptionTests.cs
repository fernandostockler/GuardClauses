namespace UnitTests;

public class ArgumentExceptionTests
{
    [TestCase(0.0f)]
    [TestCase(0.0d)]
    [TestCase(0)]
    [TestCase(0.00)]
    public void ZeroTestWithZero<T>(T value) where T : struct
    {
        _ = Assert.Throws<ArgumentException>(() => Guard.Against.Zero(value));
    }

    [TestCase(0.23f)]
    [TestCase(10.40d)]
    [TestCase(2)]
    [TestCase(0.10)]
    public void ZeroTestWithNonZero<T>(T value) where T : struct
    {
        Assert.DoesNotThrow(() => Guard.Against.Zero(value));
    }

    [TestCase(-10.30f)]
    [TestCase(-0.50d)]
    [TestCase(-20)]
    [TestCase(-50.33)]
    public void NegativeTestWithNegativeValues<T>(T value) where T : struct, IComparable
    {
        _ = Assert.Throws<ArgumentException>(() => Guard.Against.Negative(value));
    }

    [TestCase(10.30f)]
    [TestCase(0.50d)]
    [TestCase(20)]
    [TestCase(0)]
    public void NegativeTestWithNonNegativeValues<T>(T value) where T : struct, IComparable
    {
        Assert.DoesNotThrow(() => Guard.Against.Negative(value));
    }

    [TestCase(-10.30f)]
    [TestCase(-0.50d)]
    [TestCase(0.0f)]
    [TestCase(-20)]
    [TestCase(0)]
    [TestCase(-50.33)]
    public void NegativeOrZeroTestWithNegativeValues<T>(T value) where T : struct, IComparable
    {
        _ = Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(value));
    }

    [TestCase(10.30f)]
    [TestCase(0.50d)]
    [TestCase(20)]
    public void NegativeOrZeroTestWithNonNegativeValues<T>(T value) where T : struct, IComparable
    {
        Assert.DoesNotThrow(() => Guard.Against.NegativeOrZero(value));
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
        _ = Assert.Throws<ArgumentException>(
            () => Guard.Against.InvalidRegexFormat(value, pattern));
    }

    [TestCase("teste@domain.com")]
    [TestCase("teste.teste@domain.com.br")]
    [TestCase("b1@domain.com")]
    [TestCase("teste@domain.edu.fr")]
    [TestCase("teste@domain.com.br")]
    public void RegexEmailValidationWithValidEmail(string value)
    {
        var pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        Assert.DoesNotThrow(
            () => Guard.Against.InvalidRegexFormat(value, pattern));
    }

    [Test]
    public void InvalidInputTestWithNullValue()
    {
        string input = null;
        _ = Assert.Throws<ArgumentNullException>(
            () => Guard.Against.InvalidInput(input, x => x.StartsWith("test")));
    }

    [Test]
    public void InvalidInputTestWithInvalidValue()
    => _ = Assert.Throws<ArgumentException>(() => Guard.Against.InvalidInput(3, x => x % 2 == 0));

    [Test]
    public void InvalidInputTestWithValidValue()
    => Assert.DoesNotThrow(() => Guard.Against.InvalidInput(4, x => x % 2 == 0));
}