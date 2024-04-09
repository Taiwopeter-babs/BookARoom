using System.ComponentModel.DataAnnotations;

namespace BookARoom.Redis;

public sealed class RedisConfigurationOptions
{
    [Required]
    public string Host { get; set; } = null!;
    public string? Port { get; set; }
    public string? Name { get; set; }
}
