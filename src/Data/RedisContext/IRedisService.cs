namespace BookARoom.Redis;

public interface IRedisService<T>
{
    Task<T> Get(string key);

    Task Save(string key, T obj);

    Task Delete(string key);
}