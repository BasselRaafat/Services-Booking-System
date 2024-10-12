using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingService.DAL.Data.Configurations
{
    /// <inheritdoc />
    public partial class AddPathProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Category");
        }
    }
}
