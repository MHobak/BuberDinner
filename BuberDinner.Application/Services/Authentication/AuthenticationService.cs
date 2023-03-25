using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.interfaces.Authentication;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            //1. Validate the user doesn't exist
            if(_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("User with given email already exist");
            }

            //2. Create user (generate unique id) & persist to DB
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password 
            };

            _userRepository.Add(user);

            //3. Create JST token

            var token = _jwtTokenGenerator.GenerateToken(user.Id, firstName, lastName);

            return new AuthenticationResult(
                user.Id,
                firstName,
                lastName,
                email,
                token
            );
        }

        public AuthenticationResult Login(string email, string password)
        {
            //1. Validate the user exist
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User with given email does not exist");
            }

            //2. Validate the password is correct
            if(user.Password != password)
            {
                throw new Exception("Invalid password");
            }

            //3. Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);

            return new AuthenticationResult(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                token
            );
        }
    }
}