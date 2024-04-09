
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;
using System.Text.Json;

namespace BookARoom.Redis;

public sealed class RedisService : IRedisService
{
    private readonly IDatabase _redisDb;

    public RedisService(IRedisConnectionFactory redisDb)
    {
        _redisDb = redisDb.Redis;
    }

    public async Task DeleteAsync<T>(string key)
    {
        if (string.IsNullOrEmpty(key) || key.Contains(':'))
            throw new ArgumentException("Invalid key");

        string redisKey = GenerateRedisKey<T>(key);

        await _redisDb.KeyDeleteAsync(redisKey);
    }

    /// <summary>
    /// Gets a value from redis
    /// </summary>
    /// <param name="key"></param>
    /// <param name="typeofObject"></param>
    /// <returns></returns>
    public async Task<T?> GetValueAsync<T>(string key)
    {
        string? redisKey = GenerateRedisKey<T>(key);
        var defaultValue = default(T);

        RedisValue? redisValue = await _redisDb.StringGetAsync(redisKey);

        if (string.IsNullOrEmpty(redisValue.ToString()))
            return defaultValue;

        return JsonSerializer.Deserialize<T>(redisValue!);

    }

    /// <summary>
    /// Saves an object of type T to redis
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task SaveObjectAsync<T>(string key, T obj)
    {
        TimeSpan expiry = new TimeSpan(1, 0, 0, 0); // set to one day

        if (string.IsNullOrEmpty(key) || key.Contains(':'))
            throw new ArgumentException("Invalid key");

        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        string redisKey = GenerateRedisKey<T>(key);
        RedisValue redisValue = JsonSerializer.Serialize(obj);

        await _redisDb.StringSetAsync(redisKey, redisValue, expiry);
    }


    /// <summary>
    /// Generates a dynamic redis key following Redis naming convention.
    /// The format here is mostly TypeName:Id, example: <example><code>Amenity:Id</code></example>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="typeOfObjectToSave">The type of the object to save</param>
    /// <returns></returns>
    private string GenerateRedisKey<T>(string key)
    {
        string typeName = typeof(T).Name;
        return string.Concat(typeName.ToLower(), ":", key.ToLower());
    }
}
