using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookARoom.Migrations
{
    /// <inheritdoc />
    public partial class JoinTablesForManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rooms_amenities");

            migrationBuilder.DropTable(
                name: "rooms_bookings");

            migrationBuilder.CreateTable(
                name: "RoomsAmenities",
                columns: table => new
                {
                    roomId = table.Column<int>(type: "integer", nullable: false),
                    amenityId = table.Column<int>(type: "integer", nullable: false),
                    createdOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomsAmenities", x => new { x.amenityId, x.roomId });
                    table.ForeignKey(
                        name: "FK_RoomsAmenities_amenities_amenityId",
                        column: x => x.amenityId,
                        principalTable: "amenities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomsAmenities_rooms_roomId",
                        column: x => x.roomId,
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomsBookings",
                columns: table => new
                {
                    roomId = table.Column<int>(type: "integer", nullable: false),
                    bookingId = table.Column<int>(type: "integer", nullable: false),
                    createdOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomsBookings", x => new { x.bookingId, x.roomId });
                    table.ForeignKey(
                        name: "FK_RoomsBookings_bookings_bookingId",
                        column: x => x.bookingId,
                        principalTable: "bookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomsBookings_rooms_roomId",
                        column: x => x.roomId,
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomsAmenities_roomId",
                table: "RoomsAmenities",
                column: "roomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomsBookings_roomId",
                table: "RoomsBookings",
                column: "roomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomsAmenities");

            migrationBuilder.DropTable(
                name: "RoomsBookings");

            migrationBuilder.CreateTable(
                name: "rooms_amenities",
                columns: table => new
                {
                    AmenitiesId = table.Column<int>(type: "integer", nullable: false),
                    RoomsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms_amenities", x => new { x.AmenitiesId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_rooms_amenities_amenities_AmenitiesId",
                        column: x => x.AmenitiesId,
                        principalTable: "amenities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rooms_amenities_rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rooms_bookings",
                columns: table => new
                {
                    BookingsId = table.Column<int>(type: "integer", nullable: false),
                    RoomsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms_bookings", x => new { x.BookingsId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_rooms_bookings_bookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "bookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rooms_bookings_rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rooms_amenities_RoomsId",
                table: "rooms_amenities",
                column: "RoomsId");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_bookings_RoomsId",
                table: "rooms_bookings",
                column: "RoomsId");
        }
    }
}
