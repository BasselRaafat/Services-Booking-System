using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingService.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class aboellilprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartingPrice",
                table: "TechnicianService");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Technician");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "TechnicianService",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "TechnicianService");

            migrationBuilder.AddColumn<int>(
                name: "StartingPrice",
                table: "TechnicianService",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Technician",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
