using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingService.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class finalMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Technician",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Technician",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ActorId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActorType",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ActorType",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "User",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Technician",
                newName: "Name");
        }
    }
}
