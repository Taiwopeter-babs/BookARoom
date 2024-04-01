﻿// <auto-generated />
using System;
using BookARoom.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookARoom.Migrations
{
    [DbContext(typeof(BookARoomContext))]
    [Migration("20240401165525_AddColumnForNumberOfRoomsBooked")]
    partial class AddColumnForNumberOfRoomsBooked
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:CollationDefinition:my_collation", "en-u-ks-primary,en-u-ks-primary,icu,False")
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookARoom.Models.Amenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createdAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name")
                        .UseCollation("my_collation");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updatedAt");

                    b.HasKey("Id");

                    b.ToTable("amenities");
                });

            modelBuilder.Entity("BookARoom.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("bookingDate");

                    b.Property<DateTime>("CheckinDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("checkinDate");

                    b.Property<DateTime>("CheckoutDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("checkoutDate");

                    b.Property<int>("GuestId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.ToTable("bookings");
                });

            modelBuilder.Entity("BookARoom.Models.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("country");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createdAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("firstName");

                    b.Property<DateTime?>("LastBookingDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lastBookingDate");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("lastName");

                    b.Property<int>("NumberOfBookings")
                        .HasColumnType("integer")
                        .HasColumnName("numberOfBookings");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("state");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updatedAt");

                    b.HasKey("Id");

                    b.ToTable("guests");
                });

            modelBuilder.Entity("BookARoom.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createdAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("description");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean")
                        .HasColumnName("isAvailable");

                    b.Property<int>("MaximumOccupancy")
                        .HasColumnType("integer")
                        .HasColumnName("maximumOccupancy");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.Property<int>("NumberAvailable")
                        .HasColumnType("integer")
                        .HasColumnName("numberAvailable");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updatedAt");

                    b.HasKey("Id");

                    b.ToTable("rooms");
                });

            modelBuilder.Entity("BookARoom.Models.RoomsAmenities", b =>
                {
                    b.Property<int>("AmenityId")
                        .HasColumnType("integer")
                        .HasColumnName("amenityId");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer")
                        .HasColumnName("roomId");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createdOn");

                    b.HasKey("AmenityId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomsAmenities");
                });

            modelBuilder.Entity("BookARoom.Models.RoomsBookings", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("integer")
                        .HasColumnName("bookingId");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer")
                        .HasColumnName("roomId");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createdOn");

                    b.Property<int>("NumberOfRooms")
                        .HasColumnType("integer")
                        .HasColumnName("numberOfRooms");

                    b.HasKey("BookingId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomsBookings");
                });

            modelBuilder.Entity("BookARoom.Models.Booking", b =>
                {
                    b.HasOne("BookARoom.Models.Guest", "Guest")
                        .WithMany("Bookings")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("BookARoom.Models.RoomsAmenities", b =>
                {
                    b.HasOne("BookARoom.Models.Amenity", "Amenity")
                        .WithMany("RoomsAmenities")
                        .HasForeignKey("AmenityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookARoom.Models.Room", "Room")
                        .WithMany("RoomsAmenities")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Amenity");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("BookARoom.Models.RoomsBookings", b =>
                {
                    b.HasOne("BookARoom.Models.Booking", "Booking")
                        .WithMany("RoomsBookings")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookARoom.Models.Room", "Room")
                        .WithMany("RoomsBookings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("BookARoom.Models.Amenity", b =>
                {
                    b.Navigation("RoomsAmenities");
                });

            modelBuilder.Entity("BookARoom.Models.Booking", b =>
                {
                    b.Navigation("RoomsBookings");
                });

            modelBuilder.Entity("BookARoom.Models.Guest", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("BookARoom.Models.Room", b =>
                {
                    b.Navigation("RoomsAmenities");

                    b.Navigation("RoomsBookings");
                });
#pragma warning restore 612, 618
        }
    }
}
