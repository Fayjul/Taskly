namespace Taskly.Application.DTOs
{
    // User registration request
    public record RegisterRequest(string Name, string Email, string Password);

    // Response after registration
    public record RegisterResponse(Guid Id, string Name, string Email, string Role);

    // Login request from client
    public record LoginRequest(string Email, string Password);

    // Login response containing JWT and session info
    public record LoginResponse(
        string Token,
        DateTime ExpiresAt,
        Guid UserId,
        string Name,
        string Role
    );

}
