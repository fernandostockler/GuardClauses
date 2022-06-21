using GuardClauses;
using GuardClauses.Extensions;
using System.Reflection.Metadata;

Console.WriteLine("GuardClausesDemo" + Environment.NewLine);

IEnumerable<string> nullTest = null;
IEnumerable<int> test = new List<int>() { 1, 3, 5, 7, 9 };
string testValue = null;
try
{
    Guard.Against.Null(testValue, "testValue");
    //Guard.Against.NullOrEmpty(nullTest, "testParam");
    //Guard.Against.InvalidInput<int>(42, x => x < 40);
    //Guard.Against.OutOfRange(test, 3, 9, "paramTest");
    //Guard.Against.EnumOutOfRange<TestEnum>((TestEnum)5);
    //Guard.Against.Zero<int>(0);
    //Guard.Against.Negative<double>(-5.6);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine("-------------------------------------------------------------");
    Console.WriteLine();
    Console.WriteLine(ex.ToString());
}

public enum TestEnum
{
    One,
    Two,
    Three,
    Four
}