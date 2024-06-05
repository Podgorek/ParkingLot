using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLot.Data.Migrations
{
    /// <inheritdoc />
    public partial class forteenthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParkingName",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingName",
                table: "Vehicles");
        }
    }
}
