using DotNetCore.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace DotNetCore.AspNetCore
{
    public static class IServiceCollectionExtensions
    {
        public static void AddAuthenticationDefault(this IServiceCollection services)
        {
            var jsonWebToken = services.BuildServiceProvider().GetRequiredService<IJsonWebToken>();

            void JwtBearer(JwtBearerOptions jwtBearer)
            {
                jwtBearer.TokenValidationParameters = jsonWebToken.TokenValidationParameters;
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearer);
        }

        public static void AddMvcDefault(this IServiceCollection services)
        {
            void Mvc(MvcOptions mvc)
            {
                mvc.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
            }

            void Json(MvcJsonOptions json)
            {
                json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            }

            services.AddMvc(Mvc).AddJsonOptions(Json);
        }

        public static void AddResponseCompressionDefault(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
            });
        }

        public static void AddSwaggerDefault(this IServiceCollection services, string name)
        {
            services.AddSwaggerGen(cfg => cfg.SwaggerDoc(name, new Info()));
        }
    }
}
