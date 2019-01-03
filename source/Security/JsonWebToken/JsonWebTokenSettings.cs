using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace DotNetCore.Security
{
    public class JsonWebTokenSettings : IJsonWebTokenSettings
    {
        public JsonWebTokenSettings(string key) : this(key, DateTime.UtcNow.AddDays(1), nameof(Audience), nameof(Issuer))
        {
        }

        public JsonWebTokenSettings(string key, DateTime expires, string audience, string issuer)
        {
            Expires = expires;
            Audience = audience;
            Issuer = issuer;
            SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha512);
        }

        public string Audience { get; private set; }

        public DateTime Expires { get; private set; }

        public string Issuer { get; private set; }

        public SecurityKey SecurityKey { get; private set; }

        public SigningCredentials SigningCredentials { get; private set; }
    }
}
