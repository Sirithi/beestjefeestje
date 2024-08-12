using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeestjeFeestje.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeAnimalBookingrefidsinsteadofobjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalBookings_Animals_AnimalId",
                table: "AnimalBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalBookings_Bookings_BookingId",
                table: "AnimalBookings");

            migrationBuilder.DropIndex(
                name: "IX_AnimalBookings_AnimalId",
                table: "AnimalBookings");

            migrationBuilder.DropIndex(
                name: "IX_AnimalBookings_BookingId",
                table: "AnimalBookings");

            migrationBuilder.AlterColumn<string>(
                name: "BookingId",
                table: "AnimalBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AnimalId",
                table: "AnimalBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BookingId",
                table: "AnimalBookings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AnimalId",
                table: "AnimalBookings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalBookings_AnimalId",
                table: "AnimalBookings",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalBookings_BookingId",
                table: "AnimalBookings",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalBookings_Animals_AnimalId",
                table: "AnimalBookings",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalBookings_Bookings_BookingId",
                table: "AnimalBookings",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }
    }
}
