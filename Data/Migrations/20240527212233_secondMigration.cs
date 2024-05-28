using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLot.Data.Migrations
{
    /// <inheritdoc />
    public partial class secondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Parkings",
                newName: "ParkingId");

            migrationBuilder.RenameColumn(
                name: "Spots",
                table: "Floors",
                newName: "SpotsCount");

            migrationBuilder.AddColumn<int>(
                name: "FloorId",
                table: "Spots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParkingId",
                table: "Floors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Spots_FloorId",
                table: "Spots",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_ParkingId",
                table: "Floors",
                column: "ParkingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Floors_Parkings_ParkingId",
                table: "Floors",
                column: "ParkingId",
                principalTable: "Parkings",
                principalColumn: "ParkingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spots_Floors_FloorId",
                table: "Spots",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "FloorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Floors_Parkings_ParkingId",
                table: "Floors");

            migrationBuilder.DropForeignKey(
                name: "FK_Spots_Floors_FloorId",
                table: "Spots");

            migrationBuilder.DropIndex(
                name: "IX_Spots_FloorId",
                table: "Spots");

            migrationBuilder.DropIndex(
                name: "IX_Floors_ParkingId",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "FloorId",
                table: "Spots");

            migrationBuilder.DropColumn(
                name: "ParkingId",
                table: "Floors");

            migrationBuilder.RenameColumn(
                name: "ParkingId",
                table: "Parkings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SpotsCount",
                table: "Floors",
                newName: "Spots");
        }
    }
}
