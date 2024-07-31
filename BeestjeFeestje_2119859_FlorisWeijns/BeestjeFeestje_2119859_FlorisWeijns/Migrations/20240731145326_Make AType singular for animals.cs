using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeestjeFeestje_2119859_FlorisWeijns.Migrations
{
    /// <inheritdoc />
    public partial class MakeATypesingularforanimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_Animals_AnimalId",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_AnimalId",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Types");

            migrationBuilder.AddColumn<string>(
                name: "AnimalTypeId",
                table: "Animals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_AnimalTypeId",
                table: "Animals",
                column: "AnimalTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Types_AnimalTypeId",
                table: "Animals",
                column: "AnimalTypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Types_AnimalTypeId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_AnimalTypeId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "AnimalTypeId",
                table: "Animals");

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Types",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Types_AnimalId",
                table: "Types",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Animals_AnimalId",
                table: "Types",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id");
        }
    }
}
