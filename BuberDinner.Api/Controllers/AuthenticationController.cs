using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    //[ErrorHandlingFilter] The error filter can be set as attribute in the controller
    public class AuthenticationController : ControllerBase
     {
        private readonly IAuthenticationService _IauthenticationService;

        public AuthenticationController(IAuthenticationService iauthenticationService)
        {
            _IauthenticationService = iauthenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _IauthenticationService.Register(
                request.FirstName,
                request.LastName,
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
        
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _IauthenticationService.Login( 
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