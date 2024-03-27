namespace BookARoom.Utilities;

public class RoomParameters : RequestParameters
{
    public decimal MinPrice { get; set; } = 0.00M;
    public decimal MaxPrice { get; set; } = 40000.00M;
    public int MinNumberAvailable { get; set; } = 0;
    public int MaxNumberAvailable { get; set; } = 20;
}
