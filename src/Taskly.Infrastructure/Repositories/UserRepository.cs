using Microsoft.EntityFrameworkCore;
using Taskly.Application.Interfaces;
using Taskly.Domain.Entities;
namespace Taskly.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TasklyDbContext _db;
        public UserRepository(TasklyDbContext db) => _db = db;

        public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) =>
            _db.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

        public Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
            _db.Users.FindAsync(new object[] { id }, ct).AsTask();

        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            await _db.Users.AddAsync(user, ct);
        }

        public Task SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
    }
}
