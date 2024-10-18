using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingService.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class dontknow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Technician");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTo",
                table: "Technician",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Startfrom",
                table: "Technician",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "ServiceCategoryId",
                table: "Service",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTo",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "Startfrom",
                table: "Technician");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Technician",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "Technician",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServiceCategoryId",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
