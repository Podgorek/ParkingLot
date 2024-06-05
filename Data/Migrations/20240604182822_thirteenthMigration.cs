using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLot.Data.Migrations
{
    /// <inheritdoc />
    public partial class thirteenthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkingId",
                table: "Spots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingId",
                table: "Spots");
        }
    }
}
