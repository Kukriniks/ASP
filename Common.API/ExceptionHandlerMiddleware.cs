using Common.BL.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
namespace Common.API
{
	public class ExceptionHandlerMiddleWare
	{
		private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleWare(RequestDelegate next)
		{
            _next = next;
        }

		public async Task InvokeAsync(HttpContext httpContext)
		{
			var statusCode = HttpStatusCode.InternalServerError;
			var result = string.Empty;
			try
			{
				await _next.Invoke(httpContext);
			}
			catch (Exception e)
			{
				switch (e)
				{
					case NotFoundException notFoundException:

						statusCode = HttpStatusCode.NotFound;
						result = notFoundException.Message;
						break;

					case BadRequestException badRequestException:

						statusCode = HttpStatusCode.BadRequest;
						result = badRequestException.Message;
						break;

						case ForbiddenException forbiddenException:
							statusCode = HttpStatusCode.Forbidden;
							result = forbiddenException.Message;
							break;

					default:
						result = e.Message;
						break;
						
				}

				httpContext.Response.StatusCode = (int)statusCode;
				httpContext.Response.ContentType = "application/json";
				await httpContext.Response.WriteAsync(result);	
			}
		}
    }
}
