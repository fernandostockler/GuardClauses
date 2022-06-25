namespace UnitTests;

public class ArgumentNullExceptionTests
{

    public class TestClass { }
    public const TestClass? testClass = null;

    [Test]
    public void NullTest()
    {
        string? stringNull = null;

        _ = Assert.Throws<ArgumentNullException>(() =>
            _ = Guard.Against.Null(stringNull, "stringNull"));
    }

    [TestCase(testClass)]
    public void NullTestWithCustomClass(TestClass? value)
    {
        _ = Assert.Throws<ArgumentNullException>(() =>
            Guard.Against.Null(value, "testParam"));
    }

    [Test]
    public void NullMessageTest()
    {
        string? stringNull = null;
        string expected = "Parameter cannot be null. (Parameter 'stringNull')";
        try
        {
            _ = Guard.Against.Null(stringNull, "stringNull");
        }
        catch (Exception ex)
        {
            Assert.That(ex.Message, Is.EqualTo(expected));
        }
    }

    [Test]
    public void NullMessageTestWithCustomMessage()
    {
        string? stringNull = null;
        string expected = "Test message. (Parameter 'stringNull')";
        try
        {
            _ = Guard.Against.Null(stringNull, "stringNull", "Test message.");
        }
        catch (Exception ex)
        {
            Assert.That(ex.Message, Is.EqualTo(expected));
        }
    }

    [Test]
    public void NullOrWhiteSpaceTestWithWiteSpaces()
    {
        string test = "     ";
        _ = Assert.Throws<ArgumentException>(() =>
            _ = Guard.Against.NullOrWhiteSpace(test, "paraName"));
    }

    [Test]
    public void NullOrWhiteSpaceTestWithIEnumerableOfStringAndWiteSpaces()
    {
        string test = "     ";
        List<string>? list = new()
        {
            "ksldkl",
            test,
            "ksldkl",
            " "
        };

        _ = Assert.Throws<ArgumentException>(() =>
            _ = Guard.Against.NullOrWhiteSpace(list, "paraName"));
    }

    [Test]
    public void NullOrWhiteSpaceTestWithIEnumerableOfStringAndEmptyList()
    {
        List<string>? list = new();

        _ = Assert.Throws<ArgumentException>(() =>
            _ = Guard.Against.NullOrWhiteSpace(list, "paraName"));
    }

    [Test]
    public void NullOrWhiteSpaceTestWithIEnumerableOfStringAndSomeValue()
    {
        List<string>? list = new()
        {
            "ksldkl",
            "teste",
            "ksldkl"
        };

        Assert.DoesNotThrow(() =>
            _ = Guard.Against.NullOrWhiteSpace(list, "paraName"));
    }

    [Test]
    public void NullOrWhiteSpaceTestWithSomeValue()
    {
        string test = "some";
        Assert.DoesNotThrow(() =>
            _ = Guard.Against.NullOrWhiteSpace(test, "paraName"));
    }

    [Test]
    public void NullOrEmptyTestWithString()
    {
        string test = string.Empty;
        _ = Assert.Throws<ArgumentException>(() =>
            _ = Guard.Against.NullOrEmpty(test, "testParam"));
    }

    [Test]
    public void NullOrEmptyTestWithGuid()
    {
        Guid test = Guid.Empty;
        _ = Assert.Throws<ArgumentException>(() =>
            _ = Guard.Against.NullOrEmpty(test, "testParam"));
    }

    [Test]
    public void NullOrEmptyTestWithIEmptyEnumerableOfT()
    {
        IEnumerable<int> test = new List<int>();
        _ = Assert.Throws<ArgumentException>(() =>
            _ = Guard.Against.NullOrEmpty(test, "testParam"));
    }

    [Test]
    public void NullOrEmptyTestWithNullIEnumerableOfT()
    {
        IEnumerable<int> test = null;
        _ = Assert.Throws<ArgumentNullException>(() =>
            _ = Guard.Against.NullOrEmpty(test, "testParam"));
    }

    [Test]
    public void NullOrEmptyTestWithNotEmptyIEnumerableOfT()
    {
        IEnumerable<int> test = new List<int>() { 1 };
        Assert.DoesNotThrow(() =>
            _ = Guard.Against.NullOrEmpty(test, "testParam"));
    }

    [Test]
    public void NullOrEmptyTestWithInt()
    {
        int test = default;
        Assert.DoesNotThrow(() =>
            _ = Guard.Against.NullOrEmpty(test, "testParam"));
    }
}