using BookARoom.Models;
using Microsoft.EntityFrameworkCore;

namespace BookARoom.Data;

public class BookARoomContext : DbContext
{

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<Guest> Guests { get; set; }

    public BookARoomContext(DbContextOptions<BookARoomContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);

        // Room - Amenities join configuration
        modelBuilder.Entity<Room>()
        .HasMany(room => room.Amenities)
        .WithMany(am => am.Rooms)
        .UsingEntity("rooms_amenities");

        // Room - Amenities join configuration
        modelBuilder.Entity<Room>()
        .HasMany(room => room.Bookings)
        .WithMany(b => b.Rooms)
        .UsingEntity("rooms_bookings");
    }
}