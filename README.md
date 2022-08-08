# Guard Clauses

Designed to fail early when encountering arguments outside the correct specification. 

> The bugs I hate are the ones that show up only after hours of successful operation, under unusual circumstances, or whose stack traces lead to dead ends.
> 
> -**_Martin Fowler_**.

**_Fail fast_** is a technique that exposes potential bugs early in development, making them immediately visible.

> Assertions are the key to failing fast. An assertion is a tiny piece of code that checks a condition and then fails if the condition isn’t met. So, when something starts to go wrong, an assertion detects the problem and makes it visible.
> 
> -**_Martin Fowler_**.

## Usage

```C#

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

        this.name = Guard.Against.NullOrWhiteSpace(name)
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
}

```

## Extensions methods

| Method | Exception |
| -------|-----------|
| `Guard.Against.Zero`                | Throw a `ArgumentException` if the input number is zero.|
| `Guard.Against.Negative`            | Throw a `ArgumentException` if the input number is negative.|
| `Guard.Against.NegativeOrZero`      | Throw a `ArgumentException` if the input number is negative or zero.|
| `Guard.Against.InvalidRegexFormat`  | Throw a `ArgumentException` if the input number does not match with a Regex pattern.|
| `Guard.Against.InvalidInput`        | Throw a `ArgumentException` if the input does not pass a predicate function.|
| `Guard.Against.OutOfRange`          | Throw a `ArgumentOutOfRangeException` if the input is out of range.|
| `Guard.Against.EnumOutOfRange`      | Throw a `InvalidEnumArgumentException` if the input is a not defined enum.|
| `Guard.Against.Null`                | Throw a `ArgumentNullException` if the input is null.|
| `Guard.Against.NullOrEmpty`         | Throw a `ArgumentNullException` if the input is null or empty.|
| `Guard.Against.NullOrWhiteSpace`    | Throw a `ArgumentNullException` if the input is null, empty or white spaces.|
| `And`                               | Throw a `ArgumentException` if the input does not satisfy a condition.|

## References

- Addressed by Nick Chapsas on YouTube: [How to write clean validation clauses in .NET](https://youtu.be/Tvx6DNarqDM).
- Inspired by the GitHub project: [ardalis/GuardClauses](https://github.com/ardalis/GuardClauses).
