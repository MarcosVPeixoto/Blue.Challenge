using Microsoft.Azure.NotificationHubs.Messaging;
using Newtonsoft.Json;
using System.Net;

namespace Blue.Challenge.App.Middlewares
{
    public class ExceptionHandlerMiddleware : IMiddleware
        {
        public ExceptionHandlerMiddleware()
        {
            
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
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

            private Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                var code = HttpStatusCode.InternalServerError;

                if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;
                else if (exception is BadHttpRequestException) code = HttpStatusCode.BadRequest;

                var result = JsonConvert.SerializeObject(new
                {
                    error = exception.Message                    
                });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;                
                return context.Response.WriteAsync(result);
            }
        }
    }


