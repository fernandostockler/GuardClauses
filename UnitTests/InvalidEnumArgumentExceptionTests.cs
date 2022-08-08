namespace UnitTests;

public class InvalidEnumArgumentExceptionTests
{
    [Test]
    public void EnumOutOfRangeTestWithBadInput()
    {
        _ = Invoking(() => Guard.Against.EnumOutOfRange((DateTimeKind)10, "paramName"))
            .Should()
            .Throw<InvalidEnumArgumentException>()
            .WithParameterName("paramName");
    }

    [Test]
    public void EnumOutOfRangeTestWithBadInputAndCustomMessage()
    {
        _ = Invoking(() => Guard.Against.EnumOutOfRange((DateTimeKind)8, "paramName", "This is a custom message."))
            .Should()
            .Throw<InvalidEnumArgumentException>()
            .WithMessage($"This is a custom message.")
            .WithParameterName(null);
    }

    [Test]
    public void EnumOutOfRangeTestWithGoodData()
    {
        _ = Invoking(() => Guard.Against.EnumOutOfRange(DateTimeKind.Utc))
            .Should()
            .NotThrow();
    }
}