using BookARoom.Models;
using Microsoft.EntityFrameworkCore;

namespace BookARoom.Extensions;

public static class RoomRepositoryExtension
{
    public static IQueryable<Room> IncludeAmenityAndBookingsRelation(this IQueryable<Room> room)
    {
        return room
        .Include(room => room.Amenities)
        .Include(room => room.Bookings)
        .AsSplitQuery();
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