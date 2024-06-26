using BookARoom.Models;
using Microsoft.EntityFrameworkCore;

namespace BookARoom.Extensions;

public static class RoomRepositoryExtension
{

    /// <summary>
    /// Extension method to include Amenities in the query.
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    public static IQueryable<Room> IncludeRelation(this IQueryable<Room> room,
        bool includeAmenity)
    {
        return includeAmenity ?
            room.Include(room => room.Amenities).AsSplitQuery() :
            room;

    }

    /// <summary>
    /// Filter rooms by price
    /// </summary>
    /// <param name="rooms"></param>
    /// <param name="minPrice"></param>
    /// <param name="maxPrice"></param>
    /// <returns></returns>
    public static IQueryable<Room> FilterRoomsByPrice(this IQueryable<Room> rooms,
        decimal minPrice, decimal maxPrice)
    {
        return rooms.Where(room => room.Price >= minPrice && room.Price <= maxPrice);
    }

    /// <summary>
    /// Filter rooms by the number available
    /// </summary>
    /// <param name="rooms"></param>
    /// <param name="minNumber"></param>
    /// <param name="maxNumber"></param>
    /// <returns></returns>
    public static IQueryable<Room> FilterRoomsByNumberAvailable(this IQueryable<Room> rooms,
        int minNumber, int maxNumber)
    {
        return rooms
            .Where(room => room.NumberAvailable >= minNumber && room.NumberAvailable <= maxNumber);
    }
}