using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddDataModifiedOnDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "users",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "tables",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "tables",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "restaurants",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "restaurants",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "reservations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "reservations",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "tables");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "tables");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "restaurants");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "restaurants");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "reservations");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "reservations");
        }
    }
}
