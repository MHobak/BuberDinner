using BuberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            var (statusCode, message) = exception switch
            {
                //Status409Conflict is for security risk
                DuplicateEmailException => (StatusCodes.Status409Conflict, "Email already exist."),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error ocurred.")
            };
            
            return Problem(title: message, statusCode: statusCode);
        }
    }
}