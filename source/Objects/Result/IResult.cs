namespace DotNetCore.Objects
{
    public interface IResult<out T>
    {
        T Data { get; }

        string Message { get; }

        bool Success { get; }
    }
}
