using System.ComponentModel.DataAnnotations.Schema;

namespace BookARoom.Models;

public abstract class BaseModel
{
    [Column("createdAt")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updatedAt")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}