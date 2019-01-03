namespace DotNetCore.Security
{
    public interface IHash
    {
        string Create(string value);
    }
}
