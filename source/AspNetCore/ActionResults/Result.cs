using DotNetCore.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetCore.AspNetCore
{
    public class Result : IActionResult
    {
        private readonly IResult<object> _result;

        public Result(IResult<object> result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var value = _result.Success ? _result.Data : _result.Message;
            var statusCode = _result.Success ? StatusCodes.Status200OK : StatusCodes.Status422UnprocessableEntity;
            var objectResult = new ObjectResult(value) { StatusCode = statusCode };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
