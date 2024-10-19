using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingService.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class aboellilPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Technician",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
