namespace UnitTests;

public class ArgumentOutOfRangeExceptionTests
{
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(11)]
    [TestCase(0)]
    public void OutOfRangeTestWithInvalidInput(int number)
    {
        _ = Assert.Throws<ArgumentOutOfRangeException>(
            () => Guard.Against.OutOfRange(number, 5, 10, "testParm"));
    }

    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    [TestCase(9)]
    [TestCase(10)]
    public void OutOfRangeTestWithValidInput(int number)
    {
        Assert.DoesNotThrow(
            () => Guard.Against.OutOfRange(number, 5, 10, "testParm"));
    }

    [Test]
    public void OutOfRangeTestWithMinMoreThanMax()
    => _ = Assert.Throws<ArgumentException>(() =>
        Guard.Against.OutOfRange(15, 50, 10, "testParm"));

    [Test]
    public void OutOfRangeTestWithCollectionAndBadData()
    => _ = Assert.Throws<ArgumentOutOfRangeException>(() =>
        Guard.Against.OutOfRange(new List<int> { 1, 2, 3 }, 5, 10, "testParm"));

    [Test]
    public void OutOfRangeTestWithCollectionAndGoodData()
    => Assert.DoesNotThrow(() =>
        Guard.Against.OutOfRange(new List<int> { 6, 8, 10 }, 5, 10, "testParm"));

    [Test]
    public void OutOfRangeTestWithCollectionAndMinMoreThanMax()
    => _ = Assert.Throws<ArgumentException>(() =>
        Guard.Against.OutOfRange(new List<int> { 1, 40, 60 }, 50, 10, "testParm"));

    [Test]
    public void OutOfRangeTestWithCollectionAndMaxLessThanMin()
    => _ = Assert.Throws<ArgumentException>(() =>
        Guard.Against.OutOfRange(new List<int> { -5, -1, 0, 5 }, 0, -5, "testParm"));
}