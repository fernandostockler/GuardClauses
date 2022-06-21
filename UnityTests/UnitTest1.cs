namespace UnityTests;

using GuardClauses;
using GuardClauses.Extensions;
using NUnit.Framework.Constraints;

public class ArgumentNullExceptionTests
{

    public class TestClass
    {
    }

    public const TestClass? testClass = null;

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void TestNull()
    {
        string? stringNull = null;

        _ = Assert.Throws<ArgumentNullException>(
            () => _ = Guard.Against.Null(stringNull, "stringNull"));
    }

    [TestCase(testClass)]
    public void TestNullWithCustomClass(TestClass? value)
    {
        _ = Assert.Throws<ArgumentNullException>(() => Guard.Against.Null(value, "testParam"));
    }

    [Test]
    public void TestNullMessage()
    {
        string? stringNull = null;
        string paramName = "stringNull";
        string expected = "Parameter can't be null. (Parameter 'stringNull')";
        try
        {
            _ = Guard.Against.Null(stringNull, paramName);
            Assert.Fail();
        }
        catch (Exception ex)
        {
            Assert.That(ex.Message, Is.EqualTo(expected));
        }
    }

    [Test]
    public void TestNullOrWhiteSpace()
    {
        string test = "     ";
        _ = Assert.Throws<ArgumentException>(
            () => _ = Guard.Against.NullOrWhiteSpace(test, "paraName"));
    }

    [Test]
    public void TestNullOrWhiteSpaceWithSomeValue()
    {
        string test = "some";
        Assert.DoesNotThrow(
            () => _ = Guard.Against.NullOrWhiteSpace(test, "paraName"));
    }

    [Test]
    public void TestNullOrEmptyWithString()
    {
        string test = string.Empty;
        _ = Assert.Throws<ArgumentException>(
            () => _ = Guard.Against.NullOrEmpty(test, "testParam"));
    }

    [Test]
    public void TestNullOrEmptyWithGuid()
    {
        Guid test = Guid.Empty;
        _ = Assert.Throws<ArgumentException>(
            () => _ = Guard.Against.NullOrEmpty(test, "testParam"));
    }

    [Test]
    public void TestNullOrEmptyWithIEmptyEnumerableOfT()
    {
        IEnumerable<int> test = new List<int>();
        _ = Assert.Throws<ArgumentException>(
            () => _ = Guard.Against.NullOrEmpty(test, "testParam"));
    }

    [Test]
    public void TestNullOrEmptyWithNullIEnumerableOfT()
    {
        IEnumerable<int> test = null;
        _ = Assert.Throws<ArgumentNullException>(
            () => _ = Guard.Against.NullOrEmpty(test, "testParam"));
    }

    [Test]
    public void TestNullOrEmptyWithNotEmptyIEnumerableOfT()
    {
        IEnumerable<int> test = new List<int>() { 1 };
        Assert.DoesNotThrow(
            () => _ = Guard.Against.NullOrEmpty(test, "testParam"));
    }

    [Test]
    public void TestNullOrEmptyWithInt()
    {
        int test = default;
        Assert.DoesNotThrow(
            () => _ = Guard.Against.NullOrEmpty(test, "testParam"));
    }
}