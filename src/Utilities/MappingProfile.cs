using AutoMapper;
using BookARoom.Dto;
using BookARoom.Models;

namespace BookARoom.Utilities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Amenity Mapping
        CreateMap<Amenity, AmenityDto>();
        CreateMap<AmenityCreationDto, Amenity>();
        CreateMap<AmenityUpdateDto, Amenity>();

        // Booking Mapping
        CreateMap<Booking, BookingDto>()
            .ForMember(destBooking => destBooking.CheckinDate, opt => opt.MapFrom(src => src.CheckinDate.Date))
            .ForMember(destBooking => destBooking.CheckinTime, opt => opt.MapFrom(
                src => string.Join(
                    ':', src.CheckinDate.Hour.ToString(), src.CheckinDate.Minute.ToString(),
                    src.CheckinDate.Minute.ToString()
                    )))
            .ForMember(destBooking => destBooking.CheckoutDate, opt => opt.MapFrom(src => src.CheckoutDate.Date))
            .ForMember(destBooking => destBooking.CheckoutTime, opt => opt.MapFrom(
                src => string.Join(
                    ':', src.CheckoutDate.Hour.ToString(), src.CheckoutDate.Minute.ToString(),
                    src.CheckoutDate.Minute.ToString()
                    )));

        CreateMap<Guest, GuestDto>()
            .ForMember(destGuest => destGuest.FullName, opt => opt.MapFrom(
                srcGuest => string.Join(' ', srcGuest.FirstName, srcGuest.LastName)
            ))
            .ForMember(destGuest => destGuest.Location, opt => opt.MapFrom(
                srcGuest => string.Join(' ', srcGuest.State, srcGuest.City, srcGuest.Country)
            ));

        CreateMap<BookingCreationDto, Booking>();

        // Room Mapping
        CreateMap<Room, RoomDto>();
        CreateMap<RoomCreationDto, Room>();
        CreateMap<RoomForUpdateDto, Room>();

    }


}