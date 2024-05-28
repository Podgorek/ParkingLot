using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLot.Data.Migrations
{
    /// <inheritdoc />
    public partial class thirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfFloors",
                table: "Parkings");

            migrationBuilder.DropColumn(
                name: "FreeSpots",
                table: "Floors");

            migrationBuilder.RenameColumn(
                name: "SpotsCount",
                table: "Floors",
                newName: "OccupiedSpotsCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OccupiedSpotsCount",
                table: "Floors",
                newName: "SpotsCount");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfFloors",
                table: "Parkings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FreeSpots",
                table: "Floors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
