namespace DotNetCore.Objects
{
    public class Result<T> : IResult<T>
    {
        public Result(T data, bool success, string message)
        {
            Data = data;
            Success = success;
            Message = message;
        }

        public T Data { get; }

        public string Message { get; }

        public bool Success { get; }
    }
}
