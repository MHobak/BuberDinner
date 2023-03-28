using BuberDinner.Application.Errors;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

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
            Result<AuthenticationResult> registerResult = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            if (registerResult.IsSuccess)
            {
                return Ok(MapAuthResult(registerResult.Value));
            }

            var firstError = registerResult.Errors[0];

            if (firstError is DuplicateEmailError)
            {
                return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exist.");
            }

            return Problem();
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