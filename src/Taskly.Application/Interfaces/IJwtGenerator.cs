using Taskly.Domain.Entities;

public interface IJwtGenerator
{
    string GenerateToken(User user, out DateTime expiresAt);
}