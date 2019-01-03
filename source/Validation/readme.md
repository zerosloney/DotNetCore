# DotNetCore.Validation

The package provides classes for **validation**.

## RegularExpressions

```cs
public static class RegularExpressions
{
    public static readonly string Date;

    public static readonly string Decimal;

    public static readonly string Email;

    public static readonly string Hex;

    public static readonly string Integer;

    public static readonly string Login;

    public static readonly string Pass;

    public static readonly string Tag;

    public static readonly string Time;

    public static readonly string Url;
}
```

## Validator\<T\>

It provides a method to validate and throw exception of type **ApplicationException**, with a generic error message, or a list of error messages from rules of **FluentValidation** package.

```cs
public abstract class Validator<T> : AbstractValidator<T>
{
    protected Validator() { }

    protected Validator(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    private string ErrorMessage { get; set; }

    public IResult<string> Valid(T instance) { }
}
```
