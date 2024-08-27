
// using System.Net;
// using System.Text.Json;
// using System.Text.Json.Serialization;
// using Microsoft.AspNetCore.Http.Extensions;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Primitives;
// using planck.Infrastructure.Common;
// using Serilog;

// namespace planck.API.Middlewares
// {
//     public class GlobalExceptionMiddleware : IMiddleware
//     {
//         private static readonly int ERROR_STATUS_CODE = (int)HttpStatusCode.InternalServerError;

//         public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//         {
//             context.Request.Headers.TryGetValue(TraceMiddleware.TRACE_HEADER, out StringValues traceId);

//             try
//             {
//                 await next(context);
//             }
//             catch (CustomException e)
//             {
//                 LogCustomError(traceId, e);
//                 HandleException(context, ERROR_STATUS_CODE, e.Code, e.Message, e?.ErrorParams);
//             }
//             catch (Exception e)
//             {
//                 LogError(traceId, e);
//                 HandleException(context, ERROR_STATUS_CODE, "There was an error while processing this request", e.Message);
//             }
//         }

//         private void HandleException(HttpContext context, int statusCode, string code, string message, Dictionary<string, string> errorParams = null)
//         {
//             context.Response.StatusCode = statusCode;
//             var url = context.Request.GetEncodedUrl();

//             CustomProblemDetails problem = new CustomProblemDetails(statusCode, code, message, url, errorParams);

//             context.Response.ContentType = "application/json";
//             context.Response.WriteAsync(JsonSerializer.Serialize(problem));
//         }

//         private void LogError(StringValues traceId, Exception e)
//         {
//             Log.Error($"Error message from {traceId}: {e.Message}");
//             Log.Error($"Error stack trace from {traceId}: {e?.StackTrace}");
//         }

//         private void LogCustomError(StringValues traceId, CustomException e)
//         {
//             Log.Error($"Error message from {traceId}: {e.Message} '{e.Code}'");
//             Log.Error($"Error stack trace from {traceId}: {e?.StackTrace}");
//         }
//     }

//     // The error response follows RFC7807 - Problem Details for HTTP APIs (https://datatracker.ietf.org/doc/html/rfc7807).
//     public class CustomProblemDetails : ProblemDetails
//     {
//         [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
//         public Dictionary<string, string>? ErrorParams { get; }

//         public CustomProblemDetails(int status, string title, string detail, string? url = null, Dictionary<string, string>? errorParams = null)
//         {
//             Status = status;
//             Title = title;
//             Detail = detail;
//             ErrorParams = errorParams;
//             Instance = url;
//         }
//     }
// }
