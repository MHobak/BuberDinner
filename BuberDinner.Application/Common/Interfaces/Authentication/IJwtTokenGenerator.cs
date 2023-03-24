namespace BuberDinner.Application.interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string firstName, string lastName);
}   