
using BookARoom.Data;
using BookARoom.Interfaces;
using BookARoom.Models;
using BookARoom.Utilities;
using BookARoom.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookARoom.Repository;

public class GuestRepository : RepositoryBase<Guest>, IGuestRepository
{
    public GuestRepository(BookARoomContext context) : base(context)
    {
    }


    public void AddGuest(Guest guest) => Create(guest);

    /// <summary>
    /// Gets a Guest.
    /// </summary>
    /// <param name="guest"></param>
    /// <param name="includeBookings">Set to false by default to exclude relationship entities</param>
    /// <param name="trackChanges"></param>
    /// <returns></returns>
    public async Task<Guest?> GetGuestAsync(int guestId, bool includeBookings = true,
        bool trackChanges = false)
    {
        return await FindByCondition(guest => guest.Id.Equals(guestId), trackChanges)
            .IncludeBookings(includeBookings)
            .SingleOrDefaultAsync();
    }

    /// <summary>
    /// Gets a Guest by email.
    /// </summary>
    /// <param name="guest"></param>
    /// <param name="includeBookings">Set to false by default to exclude relationship entities</param>
    /// <param name="trackChanges"></param>
    /// <returns></returns>
    public async Task<Guest?> GetGuestByEmailAsync(string guestEmail,
        bool trackChanges = false)
    {
        return await FindByCondition(guest => guest.Email.Equals(guestEmail), trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<PagedList<Guest>> GetGuestsAsync(GuestParameters guestParams,
        bool trackChanges = false)
    {
        var minDate = guestParams.MinCreationDate.GetUtcDate();
        var maxDate = guestParams.MaxCreationDate.GetUtcDate();
        var lastBookingDate = guestParams.LastBookingDate.GetUtcDate();

        var guests = await FindAll(trackChanges)
            .FilterByCountry(guestParams.Country)
            .FilterByState(guestParams.State)
            .FilterByNumberOfBookings(guestParams.MinBookings, guestParams.MaxBookings)
            .FilterByGuestsCreationDate(minDate, maxDate)
            // .FilterByLastBookingDate(lastBookingDate)
            .ToListAsync();

        var guestsCount = await FindAll(trackChanges).CountAsync();

        return PagedList<Guest>.ToPagedList(guests, guestsCount,
            guestParams.PageNumber, guestParams.PageSize);
    }

    public void RemoveGuest(Guest guest) => Delete(guest);


    /// <summary>
    /// Update the updatedAt field of the modified guest
    /// </summary>
    /// <param name="entity"></param>
    public void UpdateModifiedTime(Guest guest) => UpdateTime(guest);
}