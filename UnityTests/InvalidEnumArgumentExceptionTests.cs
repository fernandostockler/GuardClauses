namespace UnityTests;

using GuardClauses;
using GuardClauses.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InvalidEnumArgumentExceptionTests
{
    [Test]
    public void EnumOutOfRangeTestWithBadInput()
    {
        _ = Assert.Throws<InvalidEnumArgumentException>(
            () => Guard.Against.EnumOutOfRange<DateTimeKind>((DateTimeKind)10));
    }

    [Test]
    public void EnumOutOfRangeTestWithBadInputAndCustomMessage()
    {
        _ = Assert.Throws<InvalidEnumArgumentException>(
            () => Guard.Against.EnumOutOfRange<DateTimeKind>((DateTimeKind)8,"test","Custom message."));
    }

    [Test]
    public void EnumOutOfRangeTestWithGoodData()
    {
        Assert.DoesNotThrow(() => Guard.Against.EnumOutOfRange<DateTimeKind>(DateTimeKind.Utc));
    }
}