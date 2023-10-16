﻿using Serilog;
using System.Net;
using System.Text.Json;

namespace Static.Exceptions
{
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate RequestDelegate;

        public GlobalErrorHandler(RequestDelegate requestDelegate) =>
            RequestDelegate = requestDelegate;

        public async Task Invoke(HttpContext context)
        {
            try { await RequestDelegate(context); }
            catch (Exception _) { await HandleException(context); }
        }

        private static async Task HandleException(HttpContext context)
        {
            dynamic loggingErrorObject = new { };

            string traceId = context.TraceIdentifier.ToString();
            int statusCode = (int) HttpStatusCode.InternalServerError;

            string? exceptionMessage = "Internal Server Error";

            loggingErrorObject = new
            {
                StatusCode = statusCode,
                Cause = "Unhandled exception",
                TraceId = traceId
            };

            string serializedErrorObject = JsonSerializer.Serialize(loggingErrorObject);
            Log.Error(serializedErrorObject);
     
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                Message = exceptionMessage,
                TraceId = traceId
            }));
       
        }
    }
}
