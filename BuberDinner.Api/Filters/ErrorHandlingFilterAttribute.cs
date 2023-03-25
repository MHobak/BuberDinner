using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuberDinner.Api.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        //If an exception is thrown and is not handled, this method is invoked
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            context.Result = new ObjectResult(new { error = "An error occurred while processing your request"})
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }
}