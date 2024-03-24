using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookARoom.Migrations
{
    /// <inheritdoc />
    public partial class RenameRoomField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "maxOccupancy",
                table: "rooms",
                newName: "maximumOccupancy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "maximumOccupancy",
                table: "rooms",
                newName: "maxOccupancy");
        }
    }
}
