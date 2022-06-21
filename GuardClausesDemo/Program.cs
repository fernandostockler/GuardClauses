using GuardClauses;
using GuardClauses.Extensions;
using System.Reflection.Metadata;

string expected = "Parameter can't be null. (Parameter 'testValue')";

Console.WriteLine("GuardClausesDemo" + Environment.NewLine);
//Console.WriteLine("Test value: null");
//Console.WriteLine($"Expected message: {expected}");

//try
//{
//	string testValue = null;
//    var t = Guard.Against.Null(testValue, "testValue");
//}
//catch (Exception ex)
//{
//    //string actual = "Parameter can't be null.";
//    Console.WriteLine($"  Actual message: {ex.Message}");
//    Console.WriteLine(String.Equals(ex.Message, expected));
//}


IEnumerable<int> test = new List<int>();

try
{
    _ = Guard.Against.NullOrEmpty(test, "testParam");
}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}