using Microsoft.IdentityModel.Tokens;
using System;

namespace DotNetCore.Security
{
    public interface IJsonWebTokenSettings
    {
        string Audience { get; }

        DateTime Expires { get; }

        string Issuer { get; }

        SecurityKey SecurityKey { get; }

        SigningCredentials SigningCredentials { get; }
    }
}
