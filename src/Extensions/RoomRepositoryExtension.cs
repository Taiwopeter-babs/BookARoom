using BookARoom.Models;
using Microsoft.EntityFrameworkCore;

namespace BookARoom.Extensions;

public static class RoomRepositoryExtension
{

    /// <summary>
    /// Extension method to include Bookings in the query. Amenities are included by default
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    public static IQueryable<Room> IncludeBookingsRelation(this IQueryable<Room> room, bool includeBooking)
    {
        return includeBooking ?

        room
        .Include(room => room.Amenities)
        .Include(room => room.Bookings) :

        room
            .Include(room => room.Amenities);
    }

    // public static IQueryable<Student> FilterStudentsByDepartment(this IQueryable<Student> students,
    //     string department)
    // {
    //     if (department is null)
    //         return students;

    //     return students
    //         .Where(st => st.Department.Equals(department, StringComparison.InvariantCultureIgnoreCase));
    // }
}