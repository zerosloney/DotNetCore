using DotNetCore.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace DotNetCore.AspNetCore
{
    public class ExceptionMiddleware
    {
        public ExceptionMiddleware(RequestDelegate request, IHostingEnvironment environment, ILogger logger)
        {
            Request = request;
            Environment = environment;
            Logger = logger;
        }

        private IHostingEnvironment Environment { get; }

        private ILogger Logger { get; }

        private RequestDelegate Request { get; }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Request(context).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                if (Environment.IsDevelopment())
                {
                    throw;
                }

                Logger.Error(exception);

                await ResponseAsync(context, HttpStatusCode.InternalServerError, string.Empty).ConfigureAwait(false);
            }
        }

        private static async Task ResponseAsync(HttpContext context, HttpStatusCode statusCode, string response)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsync(response).ConfigureAwait(false);
        }
    }
}
