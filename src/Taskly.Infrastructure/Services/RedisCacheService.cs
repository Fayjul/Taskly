using System.Text.Json;
using Taskly.Application.Interfaces;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Taskly.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _db;
        public RedisCacheService(IConnectionMultiplexer mux) => _db = mux.GetDatabase();

        public Task SetAsync<T>(string key, T value, TimeSpan? ttl = null) =>
            _db.StringSetAsync(key, JsonSerializer.Serialize(value), ttl);

        public async Task<T?> GetAsync<T>(string key)
        {
            var val = await _db.StringGetAsync(key);
            return val.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(val!);
        }

        public Task RemoveAsync(string key) => _db.KeyDeleteAsync(key);
    }
}
