using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RouteSearch.API.Midlewares
{
    public class RouteSearchExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public RouteSearchExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            dynamic errors = null;

            switch (exception)
            {
                case KeyNotFoundException notFoundException:
                    errors = BuildNotFoundErrorObject(notFoundException.Message);
                    context.Response.StatusCode = errors.Code;
                    break;
                case ArgumentNullException notFoundException:
                    errors = BuildBadRequestErrorObject(notFoundException.Message);
                    context.Response.StatusCode = errors.Code;
                    break;
                case ArgumentOutOfRangeException notFoundException:
                    errors = BuildNotFoundErrorObject(notFoundException.Message);
                    context.Response.StatusCode = errors.Code;
                    break;
                case Exception commonException:
                    errors = BuildInternalServerErrorObject(commonException.Message);
                    context.Response.StatusCode = errors.Code;
                    break;
            }

            context.Response.ContentType = "application/json";

            var result = JsonConvert.SerializeObject((object)errors);
            await context.Response.WriteAsync(result);
        }

        private static dynamic BuildInternalServerErrorObject(string message)
        {            
            if (string.IsNullOrWhiteSpace(message))
                message = "The server was unable to complete your request.";

            return BuildErrorObject(message, (int)HttpStatusCode.InternalServerError);
        }

        private static dynamic BuildBadRequestErrorObject(string message)
        {
            return BuildErrorObject(message, (int)HttpStatusCode.BadRequest);
        }

        private static dynamic BuildNotFoundErrorObject(string message)
        {
            return BuildErrorObject(message, (int)HttpStatusCode.NotFound);
        }

        private static dynamic BuildErrorObject(string message, int httpStatusCode)
        {
            return new
            {
                Code = httpStatusCode,
                Message = message
            };
        }
    }
}