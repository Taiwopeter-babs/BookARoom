using BookARoom.Models;
using BookARoom.Redis;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BookARoom;

public class RedisConnectionFactory : IRedisConnectionFactory
{
    /// <summary>
    /// The redis connection object
    /// </summary>
    private readonly Lazy<ConnectionMultiplexer> _redisConnection;

    // private readonly IOptions<RedisConfigurationOptions> redis;

    public RedisConnectionFactory(IOptions<RedisConfigurationOptions> redis)
    {
        _redisConnection = new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect($"{redis.Value.Host}:{redis.Value.Port}"));
    }

    /// <summary>
    /// The lazily initialized redis connection. Returns the redis database
    /// </summary>
    public IDatabase Redis => _redisConnection.Value.GetDatabase();
}
