using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}   