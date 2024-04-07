using StackExchange.Redis;

namespace BookARoom;

public interface IRedisConnectionFactory
{
    IDatabase Redis { get; }
}
