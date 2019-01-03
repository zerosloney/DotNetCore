namespace DotNetCore.Objects
{
    public sealed class SuccessResult<T> : Result<T>
    {
        public SuccessResult(T data) : base(data, true, string.Empty)
        {
        }
    }
}
