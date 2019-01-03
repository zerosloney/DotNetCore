namespace DotNetCore.Objects.Results
{
    public sealed class ErrorResult<T> : Result<T>
    {
        public ErrorResult(string message) : base(default, false, message)
        {
        }
    }
}
