namespace UnitTests;

public class ArgumentExceptionTests
{
    [Test]
    public void Int32ZeroTestWithZero()
    => _ = Assert.Throws<ArgumentException>(() => Guard.Against.Zero(0));

    [Test]
    public void Int32ZeroTestWithNonZero()
    => Assert.DoesNotThrow(() => Guard.Against.Zero(2));

    [Test]
    public void DoubleNegativeTestWithNegative()
    => _ = Assert.Throws<ArgumentException>(() => Guard.Against.Negative(-99.0));

    [Test]
    public void DoubleNegativeTestWithPositive()
    => Assert.DoesNotThrow(() => Guard.Against.Negative(5.0));

    [Test]
    public void DoubleNegativeTestWithZero()
    => Assert.DoesNotThrow(() => Guard.Against.Negative(0.0));

    [Test]
    public void DecimalNegativeOrZeroTestWithNegative()
    => _ = Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(-42.0m));

    [Test]
    public void DecimalNegativeOrZeroTestWithZero()
    => _ = Assert.Throws<ArgumentException>(() => Guard.Against.NegativeOrZero(0.0m));

    [Test]
    public void DecimalNegativeOrZeroTestWithPositive()
    => Assert.DoesNotThrow(() => Guard.Against.NegativeOrZero(8.0m));

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