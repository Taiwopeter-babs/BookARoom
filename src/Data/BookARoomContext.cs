using BookARoom.Models;
using Microsoft.EntityFrameworkCore;

namespace BookARoom.Data;

public class BookARoomContext : DbContext
{

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<RoomsAmenities> RoomsAmenities { get; set; }
    public DbSet<RoomsBookings> RoomsBookings { get; set; }

    public BookARoomContext(DbContextOptions<BookARoomContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Room - Amenities join configuration
        modelBuilder.Entity<Room>()
        .HasMany(room => room.Amenities)
        .WithMany(amenity => amenity.Rooms)
        .UsingEntity<RoomsAmenities>();

        // Room - bookings join configuration
        modelBuilder.Entity<Room>()
        .HasMany(room => room.Bookings)
        .WithMany(booking => booking.Rooms)
        .UsingEntity<RoomsBookings>();

        // create a collation for case-insensitive matching for the amenities table
        modelBuilder.HasCollation("my_collation", locale: "en-u-ks-primary", provider: "icu", deterministic: false);

        modelBuilder.Entity<Amenity>().Property(am => am.Name)
            .UseCollation("my_collation");
    }
}