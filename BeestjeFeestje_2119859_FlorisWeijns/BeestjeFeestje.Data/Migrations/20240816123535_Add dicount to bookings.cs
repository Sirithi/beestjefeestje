using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeestjeFeestje.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adddicounttobookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "Bookings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Bookings");
        }
    }
}
