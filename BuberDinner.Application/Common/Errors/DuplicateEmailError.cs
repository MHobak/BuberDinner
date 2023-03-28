using System.Net;
using BuberDinner.Application.Common.Errors;

namespace BuberDinner.Application.Errors
{
    public record struct DuplicateEmailError : IError
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage => "Email already exist.";
    }
}