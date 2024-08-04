using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeestjeFeestje.Data.Migrations
{
    /// <inheritdoc />
    public partial class FarmUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Farms_FarmId",
                table: "Animals");

            migrationBuilder.AlterColumn<string>(
                name: "FarmId",
                table: "Animals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Farms_FarmId",
                table: "Animals",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Farms_FarmId",
                table: "Animals");

            migrationBuilder.AlterColumn<string>(
                name: "FarmId",
                table: "Animals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Farms_FarmId",
                table: "Animals",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id");
        }
    }
}
