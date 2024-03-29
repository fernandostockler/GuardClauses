﻿using GuardClauses;
using GuardClauses.Extensions;
using System.Runtime.CompilerServices;

Console.WriteLine("GuardClausesDemo" + Environment.NewLine);

IEnumerable<string> nullTest = null;
IEnumerable<int> test = new List<int>() { 1, 3, 5, 7, 9 };
string testValue = null;
Order? order = new(1, 5, "notes", TestEnum.One, new Test(), new DateOnly(1968, 12, 22));
try
{
    var order1 = Guard.Against.Null(order);
    Console.WriteLine($"Order: Id={order1.Id}, Stars={order1.Stars}, Notes={order1.Notes}, TestEnum={order1.MyEnum}, Test={order1.Test}, Birthday={order1.Birthday}");
    //Guard.Against.Null(testValue, (string)null);
    //Guard.Against.NullOrEmpty(nullTest, "testParam");
    //Guard.Against.InvalidInput<int>(42, x => x < 40);
    //Guard.Against.OutOfRange(test, 3, 9, "paramTest");
    //Guard.Against.EnumOutOfRange<TestEnum>((TestEnum)5);
    //Guard.Against.Zero<int>(0);
    //Guard.Against.Negative<double>(-5.6);

    //string longName = "012345678901234567890123456789012345678901234567890123456789012";

    var person = new Person(10, "Joe", new DateOnly(1980, 5, 15), Category.Vip, 5);

    Console.WriteLine(person);
    Console.WriteLine($"Length: {person.Name.Length}");
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
    public DateOnly Birthday { get; set; }


    public Order(int id, int stars, string notes, TestEnum myEnum, Test test, DateOnly birthday)
    {
        Id = Guard.Against.NegativeOrZero(id);
        Stars = Guard.Against.OutOfRange(stars, 0, 5);
        Notes = Guard.Against.InvalidInput(notes, x => !string.IsNullOrWhiteSpace(x) && x.Length < 30);
        MyEnum = Guard.Against.EnumOutOfRange(myEnum);
        Test = Guard.Against.Null(test);
        Birthday = Guard.Against.OutOfRange(birthday,
            minimunValue: new DateOnly(1968, 11, 22),
            maximunValue: new DateOnly(1969, 11, 22));
    }
}

public enum Category { Client, Vip }

public class Person
{
    int id;
    string name;
    DateOnly birthday;
    Category category;
    double stars;

    public Person(int id, string name, DateOnly birthday, Category category, double stars)
    {
        this.id = Guard.Against.NegativeOrZero(id);

        this.name = Guard.Against
            .NullOrWhiteSpace(name)
            .And(x => x.Length <= 50, nameof(name))
            .And(x => x.Length >= 3, nameof(name));

        this.birthday = Guard.Against.OutOfRange(birthday,
            minimunValue: new DateOnly(1900, 1, 1),
            maximunValue: new DateOnly(2022, 1, 1));

        this.category = Guard.Against.EnumOutOfRange(category);

        this.stars = Guard.Against.OutOfRange(stars,
            minimunValue: 0,
            maximunValue: 5);
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public DateOnly Birthday { get => birthday; set => birthday = value; }
    public Category Category { get => category; set => category = value; }
    public double Stars { get => stars; set => stars = value; }

    public override string ToString() => $"id: {id}, name: {name}, birthday: {birthday}, category: {category}, stars: {stars}";
}