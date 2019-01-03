# DotNetCore.Objects

The package provides generic classes for **objects**.

## FileBinary

```cs
public class FileBinary
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public byte[] Bytes { get; set; }

    public string ContentType { get; set; }

    public long Length { get; set; }
}
```

## Order

```cs
public class Order
{
    public bool IsAscending { get; set; }

    public string Property { get; set; }
}
```

## Page

```cs
public class Page
{
    public int Index { get; set; }

    public short Size { get; set; }
}
```

## PagedListParameters

```cs
public class PagedListParameters
{
    public IList<Order> Orders { get; set; } = new List<Order>();

    public Page Page { get; set; } = new Page();
}
```

## PagedList

The **PagedList\<T\>** class performs the sort and pagination logic, and return the total count and the paged list.

```cs
public class PagedList<T>
{
    public PagedList(IQueryable<T> queryable, PagedListParameters parameters) { }

    public long Count { get; }

    public IEnumerable<T> List { get; }
}
```

## IResult

```cs
public interface IResult<out T>
{
    T Data { get; }

    string Message { get; }

    bool Success { get; }
}
```

## Result

```cs
public class Result<T> : IResult<T>
{
    public Result(T data, bool success, string message) { }

    public T Data { get; }

    public string Message { get; }

    public bool Success { get; }
}
```

## SuccessResult

```cs
public sealed class SuccessResult<T> : Result<T>
{
    public SuccessResult(T data) : base(data, true, string.Empty) { }
}
```

## ErrorResult

```cs
public sealed class ErrorResult<T> : Result<T>
{
    public ErrorResult(string message) : base(default, false, message) { }
}
```
