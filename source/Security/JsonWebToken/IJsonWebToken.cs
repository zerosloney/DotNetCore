using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace DotNetCore.Security
{
    public interface IJsonWebToken
    {
        TokenValidationParameters TokenValidationParameters { get; }

        Dictionary<string, object> Decode(string token);

        string Encode(string sub, string[] roles);
    }
}
