using System.Net;
using System.Text.Json;

namespace BuberDinner.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        #region Explanation
        //AI EXPLANATION
        // The HandleExceptionAsync method in the example I provided is declared as static because it doesn't depend on any instance-level state. It's a simple method that takes in a HttpContext and an Exception, generates an error response based on the exception, and writes the response to the HTTP response stream.

        // In general, it's a good practice to declare methods as static whenever possible, especially if they don't depend on any instance-level state. Static methods have a number of benefits:

        // They are easier to test, since you don't need to create an instance of the class to call them.
        // They can be called directly on the class, without the need to instantiate the class first.
        // They don't require any memory allocation for the this pointer, which can result in better performance and lower memory usage.
        // That being said, there's nothing inherently wrong with declaring a method as non-static if it depends on instance-level state. In the case of the ErrorHandlingMiddleware class, however, the HandleExceptionAsync method doesn't depend on any instance-level state, so declaring it as static is a good practice.
        #endregion
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; //500 if unexpected
            var result = JsonSerializer.Serialize(new { error = "An error occurred while processing your request." });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}