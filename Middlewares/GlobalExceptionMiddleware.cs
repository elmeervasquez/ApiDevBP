
using System.Net;
using Serilog;

namespace ApiDevBP.Middlewares
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                LogError(e);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        void LogError(Exception e)
        {
            Log.Error($"Error message: {e.Message}");
        }
    }
}