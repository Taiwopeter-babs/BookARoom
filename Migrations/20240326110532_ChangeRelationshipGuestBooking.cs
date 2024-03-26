using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookARoom.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationshipGuestBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_bookings_GuestId",
                table: "bookings");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_GuestId",
                table: "bookings",
                column: "GuestId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_bookings_GuestId",
                table: "bookings");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_GuestId",
                table: "bookings",
                column: "GuestId");
        }
    }
}
