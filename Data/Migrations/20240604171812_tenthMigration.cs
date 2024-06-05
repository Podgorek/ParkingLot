using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLot.Data.Migrations
{
    /// <inheritdoc />
    public partial class tenthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpotNumber",
                table: "Spots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ParkingName",
                table: "Parkings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpotNumber",
                table: "Spots");

            migrationBuilder.DropColumn(
                name: "ParkingName",
                table: "Parkings");
        }
    }
}
