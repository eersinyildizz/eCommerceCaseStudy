using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using HepsiBuradaCaseStudy.Domain;
using HepsiBuradaCaseStudy.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HepsiBuradaCaseStudy.Api.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware>
             logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            ErrorResponse response = new ErrorResponse();

            if (exception is BusinessException)
            {
                var businessException = exception as BusinessException;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.ErrorCode = businessException.ErrorCode;
                response.ErrorMessage = businessException.Message;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ErrorCode = Utils.UnknownExceptionErrorCode;
                response.ErrorMessage = Utils.UnknownExceptionMessage;
            }

            logger.LogCritical(exception, exception.Message);
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
