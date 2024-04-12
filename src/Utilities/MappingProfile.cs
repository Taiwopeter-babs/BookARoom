using AutoMapper;
using BookARoom.Dto;
using BookARoom.Extensions;
using BookARoom.Models;

namespace BookARoom.Utilities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Amenity Mapping
        CreateMap<Amenity, AmenityDto>();
        CreateMap<AmenityDto, Amenity>();

        CreateMap<AmenityCreationDto, Amenity>();

        CreateMap<AmenityUpdateDto, Amenity>();

        // Booking Mapping
        CreateMap<Booking, BookingDto>();

        CreateMap<BookingDto, Booking>();

        CreateMap<BookingCreationDto, Booking>()
            .ForMember(dest => dest.Rooms, opt => opt.Ignore())
            .ForMember(destBooking => destBooking.CheckinDate, opt => opt.MapFrom(src => src.CheckinDate!.GetUtcDate()))
            .ForMember(destBooking => destBooking.CheckoutDate, opt => opt.MapFrom(src => src.CheckoutDate!.GetUtcDate()));

        // Guest mapping
        CreateMap<Guest, GuestDto>()
            .ForMember(destGuest => destGuest.FullName, opt => opt.MapFrom(
                srcGuest => string.Join(' ', srcGuest.FirstName, srcGuest.LastName)
            ))
            .ForMember(destGuest => destGuest.Location, opt => opt.MapFrom(
                srcGuest => string.Join(", ", srcGuest.State, srcGuest.City, srcGuest.Country)
            ));

        CreateMap<GuestCreationDto, Guest>();

        CreateMap<GuestUpdateDto, Guest>();


        // Room Mapping
        CreateMap<Room, RoomDto>();
        CreateMap<RoomDto, Room>();

        CreateMap<RoomCreationDto, Room>();
        CreateMap<RoomUpdateDto, Room>()
            .ForMember(destRoom => destRoom.Amenities, opt => opt.Ignore());

    }


}