using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookARoom.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultDateAndBookingDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAvailable",
                table: "rooms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "bookingDate",
                table: "bookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAvailable",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "bookingDate",
                table: "bookings");
        }
    }
}
