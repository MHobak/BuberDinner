using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Errors;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace BuberDinner.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    //[ErrorHandlingFilter] The error filter can be set as attribute in the controller
    public class AuthenticationController : ControllerBase
     {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            OneOf<AuthenticationResult, IError> registerResult = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            return registerResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                error => Problem(statusCode: (int)error.StatusCode, title: error.ErrorMessage)
            );
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authResponse)
        {
            return new AuthenticationResponse(
                authResponse.user.Id,
                authResponse.user.FirstName,
                authResponse.user.LastName,
                authResponse.user.Email,
                authResponse.Token);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login( 
                request.Email,
                request.Password);

            var response = new AuthenticationResponse(
                authResult.user.Id,
                authResult.user.FirstName,
                authResult.user.LastName,
                authResult.user.Email,
                authResult.Token);

            return Ok(response);
        }
    }
}