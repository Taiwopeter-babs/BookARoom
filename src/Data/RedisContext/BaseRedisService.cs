namespace BookARoom.Redis;

public abstract class BaseRedisService<T>
{
    protected Type Type => typeof(T);
    protected string Name => Type.Name;

    protected string? GenerateRedisKey(string? key)
    {
        if (key == null)
            return null;

        return string.Concat(key.ToLower(), ":", Name.ToLower());
    }
}
