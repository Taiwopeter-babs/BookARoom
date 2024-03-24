using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookARoom.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStateField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "state",
                table: "rooms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "rooms",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
