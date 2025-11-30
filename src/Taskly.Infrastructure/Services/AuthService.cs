using Taskly.Application.DTOs;
using Taskly.Application.Interfaces;
using Taskly.Domain.Entities;

namespace Taskly.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly IJwtGenerator _jwt;
        private readonly ICacheService _cache;

        public AuthService(IUserRepository users, IJwtGenerator jwt, ICacheService cache)
        {
            _users = users;
            _jwt = jwt;
            _cache = cache;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest req, CancellationToken ct = default)
        {
            var existing = await _users.GetByEmailAsync(req.Email, ct);
            if (existing is not null)
                throw new InvalidOperationException("Email already used.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Email = req.Email,
                PasswordHash = PasswordHasher.Hash(req.Password),
                Role = "Member",
                CreatedAt = DateTime.UtcNow
            };

            await _users.AddAsync(user, ct);
            await _users.SaveChangesAsync(ct);

            return new RegisterResponse(user.Id, user.Name, user.Email, user.Role);
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest req, CancellationToken ct = default)
        {
            var user = await _users.GetByEmailAsync(req.Email, ct);
            if (user is null || !PasswordHasher.Verify(user.PasswordHash, req.Password))
                throw new UnauthorizedAccessException("Invalid credentials.");

            var token = _jwt.GenerateToken(user, out var expiresAt);

            // Cache essential user info in Redis
            var key = $"user:session:{user.Id}";
            var session = new
            {
                user.Id,
                user.Name,
                user.Role,
                TokenExpiry = expiresAt
            };
            var ttl = expiresAt - DateTime.UtcNow;
            await _cache.SetAsync(key, session, ttl);

            return new LoginResponse(token, expiresAt, user.Id, user.Name, user.Role);
        }
    }
}
