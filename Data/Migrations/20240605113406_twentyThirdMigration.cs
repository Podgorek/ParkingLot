using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLot.Data.Migrations
{
    /// <inheritdoc />
    public partial class twentyThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingId",
                table: "VehicleToCreate");

            migrationBuilder.AddColumn<string>(
                name: "ParkingName",
                table: "VehicleToCreate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingName",
                table: "VehicleToCreate");

            migrationBuilder.AddColumn<int>(
                name: "ParkingId",
                table: "VehicleToCreate",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
