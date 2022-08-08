namespace UnitTests;

public class ArgumentNullExceptionTests
{

    private class TestClass { public static TestClass Test = new(); }

    [Test]
    public void NullTestWithStringNullValue()
    {
        string? stringNull = null;

        _ = Invoking(() => Guard.Against.Null(stringNull, "paramName"))
            .Should()
            .Throw<ArgumentNullException>()
            .WithMessage("paramName cannot be null. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [TestCase(typeof(int?))]
    [TestCase(typeof(double?))]
    [TestCase(typeof(decimal?))]
    [TestCase(typeof(long?))]
    public void NullTestWithNullValue(Type type)
    {
        var instance = Activator.CreateInstance(type);

        _ = Invoking(() => Guard.Against.Null(instance, "paramName"))
            .Should()
            .Throw<ArgumentNullException>()
            .WithMessage("paramName cannot be null. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [TestCase(typeof(TestClass))]
    [TestCase(typeof(decimal))]
    [TestCase(typeof(int))]
    public void NullTestWithNonNullValue(Type type)
    {
        var instance = Activator.CreateInstance(type);

        _ = Invoking(() => Guard.Against.Null(instance, "paramName"))
            .Should()
            .NotThrow<ArgumentNullException>();
    }

    [Test]
    public void NullTestWithNullCustomClass()
    {
        TestClass? testClass = null;

        _ = Invoking(() => Guard.Against.Null(testClass, "paramName"))
            .Should()
            .Throw<ArgumentNullException>()
            .WithMessage("paramName cannot be null. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [Test]
    public void NullMessageTestWithCustomMessage()
    {
        TestClass? testClass = null;

        _ = Invoking(() => Guard.Against.Null(testClass, "paramName", "Test message."))
            .Should()
            .Throw<ArgumentNullException>()
            .WithMessage("Test message. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [Test]
    public void NullOrWhiteSpaceTestWithWiteSpaces()
    {
        _ = Invoking(() => _ = Guard.Against.NullOrWhiteSpace("    ", "paramName"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Parameter cannot be white spaces. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [Test]
    public void NullOrWhiteSpaceTestWithIEnumerableOfStringAndWiteSpaces()
    {
        _ = Invoking(() => _ = Guard.Against.NullOrWhiteSpace(new List<string>() { "One", "Two", "  " }, "paramName"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Parameter cannot contain white spaces. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [Test]
    public void NullOrWhiteSpaceTestWithIEnumerableOfStringAndEmptyList()
    {
        _ = Invoking(() => _ = Guard.Against.NullOrWhiteSpace(new List<string>(), "paramName"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Parameter cannot be empty. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [Test]
    public void NullOrWhiteSpaceTestWithIEnumerableOfStringAndSomeValue()
    {
        _ = Invoking(() => _ = Guard.Against.NullOrWhiteSpace(new List<string>() { "One", "Two", "Three" }, "paramName"))
            .Should()
            .NotThrow();
    }

    [Test]
    public void NullOrWhiteSpaceTestWithSomeValue()
    {
        _ = Invoking(() => _ = Guard.Against.NullOrWhiteSpace("some", "paramName"))
            .Should()
            .NotThrow();
    }

    [Test]
    public void NullOrEmptyTestWithStringEmpty()
    {
        _ = Invoking(() => _ = Guard.Against.NullOrEmpty(string.Empty, "paramName"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Parameter cannot be empty. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [Test]
    public void NullOrEmptyTestWithGuidEmpty()
    {
        _ = Invoking(() => _ = Guard.Against.NullOrEmpty(Guid.Empty, "paramName"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Parameter cannot be empty. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [Test]
    public void NullOrEmptyTestWithEmptyIEnumerableOfT()
    {
        _ = Invoking(() => _ = Guard.Against.NullOrEmpty(new List<string>().AsEnumerable(), "paramName"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Parameter cannot be empty. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [Test]
    public void NullOrEmptyTestWithIEmptyListOfT()
    {
        _ = Invoking(() => _ = Guard.Against.NullOrEmpty(new List<string>(), "paramName"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Parameter cannot be empty. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [Test]
    public void NullOrEmptyTestWithNullIEnumerableOfT()
    {
        IEnumerable<int>? test = null;

        _ = Invoking(() => Guard.Against.NullOrEmpty(test, "paramName"))
            .Should()
            .Throw<ArgumentNullException>()
            .WithMessage("paramName cannot be null. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [Test]
    public void NullOrEmptyTestWithNullListOfT()
    {
        List<int>? test = null;

        _ = Invoking(() => Guard.Against.NullOrEmpty(test, "paramName"))
            .Should()
            .Throw<ArgumentNullException>()
            .WithMessage("paramName cannot be null. (Parameter 'paramName')")
            .WithParameterName("paramName");
    }

    [Test]
    public void NullOrEmptyTestWithNotEmptyIEnumerableOfT()
    {
        _ = Invoking(() => Guard.Against.NullOrEmpty(new List<int>() { 1 }.AsEnumerable(), "paramName"))
            .Should()
            .NotThrow();
    }

    [Test]
    public void NullOrEmptyTestWithNotEmptyListOfT()
    {
        _ = Invoking(() => Guard.Against.NullOrEmpty(new List<int>() { 1 }, "paramName"))
            .Should()
            .NotThrow();
    }

    [Test]
    public void NullOrEmptyTestWithDefaultInt()
    {
        int test = default;

        _ = Invoking(() => Guard.Against.NullOrEmpty(test, "paramName"))
            .Should()
            .NotThrow();
    }
}