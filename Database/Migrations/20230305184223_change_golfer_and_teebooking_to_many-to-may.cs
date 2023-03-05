using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class change_golfer_and_teebooking_to_manytomay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Golfers_TeeBookings_TeeBookingId",
                table: "Golfers");

            migrationBuilder.DropIndex(
                name: "IX_Golfers_TeeBookingId",
                table: "Golfers");

            migrationBuilder.DropColumn(
                name: "TeeBookingId",
                table: "Golfers");

            migrationBuilder.CreateTable(
                name: "GolferTeeBooking",
                columns: table => new
                {
                    GolfersId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeeBookingsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GolferTeeBooking", x => new { x.GolfersId, x.TeeBookingsId });
                    table.ForeignKey(
                        name: "FK_GolferTeeBooking_Golfers_GolfersId",
                        column: x => x.GolfersId,
                        principalTable: "Golfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GolferTeeBooking_TeeBookings_TeeBookingsId",
                        column: x => x.TeeBookingsId,
                        principalTable: "TeeBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GolferTeeBooking_TeeBookingsId",
                table: "GolferTeeBooking",
                column: "TeeBookingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GolferTeeBooking");

            migrationBuilder.AddColumn<int>(
                name: "TeeBookingId",
                table: "Golfers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Golfers_TeeBookingId",
                table: "Golfers",
                column: "TeeBookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Golfers_TeeBookings_TeeBookingId",
                table: "Golfers",
                column: "TeeBookingId",
                principalTable: "TeeBookings",
                principalColumn: "Id");
        }
    }
}
