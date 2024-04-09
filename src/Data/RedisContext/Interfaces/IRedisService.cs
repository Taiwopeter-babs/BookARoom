namespace BookARoom.Redis;

public interface IRedisService
{
    /// <summary>
    /// Gets the value of a key from the redis database
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<T?> GetValueAsync<T>(string key);

    /// <summary>
    /// Saves the object associated with the key in redis
    /// </summary>
    /// <param name="key"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    Task SaveObjectAsync<T>(string key, T obj);

    /// <summary>
    /// Deletes a key from the redis database
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task DeleteAsync<T>(string key);
}