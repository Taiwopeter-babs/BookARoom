namespace BookARoom.Exceptions;

public abstract class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}

public sealed class AmenityAlreadyExistsException : BadRequestException
{
    public AmenityAlreadyExistsException(string amenityName) :
        base($"The amenity with the name: {amenityName}, already exists")
    { }
}

public sealed class BookingNotAvailableForGuestException : BadRequestException
{
    public BookingNotAvailableForGuestException(int guestId, int bookingId) :
        base($"The booking with the id: {bookingId}, is not available for the guest with Id: {guestId}")
    { }
}

public sealed class UnsuccefulBookingGuestException : BadRequestException
{
    public UnsuccefulBookingGuestException(int guestId) :
        base($"The booking was not succesful for the guest with Id: {guestId}")
    { }
}

public sealed class RoomMaximumOccupancyExceededException : BadRequestException
{
    public RoomMaximumOccupancyExceededException(int roomId, string? roomName, int validBooking) :
        base($"Guests exceeded the amount of rooms for room '{roomName}' with Id {roomId}. Valid rooms: {validBooking}")
    { }
}
