using GuardClauses;
using GuardClauses.Extensions;
using System.Reflection.Metadata;

Console.WriteLine("GuardClausesDemo" + Environment.NewLine);

IEnumerable<string> nullTest = null;
IEnumerable<int> test = new List<int>() { 1, 3, 5, 7, 9 };
string testValue = null;
Order? order = new(1,5,"notes", TestEnum.One, new Test());
try
{
    var order1 = Guard.Against.Null(order);
    Console.WriteLine($"Order: Id={order1.Id}, Stars={order1.Stars}, Notes={order1.Notes}, TestEnum={order1.MyEnum}, Test={order1.Test}");
    //Guard.Against.Null(testValue, "testValue");
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

//var order1 = new Order(-5, 1, "notes");
//var order2 = new Order(1, -34, "notes");
//var order3 = new Order(2, 2, "1234567890123456789012345678901");

//Console.WriteLine(order1.Id);
//Console.WriteLine(order2.Stars);
//Console.WriteLine(order3.Notes);
public enum TestEnum
{
    One,
    Two,
    Three,
    Four
}

public class Test { }
public class Order
{
    public int Id { get; set; }
    public int Stars { get; set; }
    public string Notes { get; set; }
    public TestEnum MyEnum { get; set; }
    public Test Test { get; set; }

    public Order(int id, int stars, string notes, TestEnum myEnum, Test test)
    {
        Id = Guard.Against.NegativeOrZero(id);
        Stars = Guard.Against.OutOfRange(stars, 0, 5);
        Notes = Guard.Against.InvalidInput(notes, x => !string.IsNullOrWhiteSpace(x) && x.Length < 30);
        MyEnum = Guard.Against.EnumOutOfRange(myEnum);
        Test = Guard.Against.Null(test);
    }
}