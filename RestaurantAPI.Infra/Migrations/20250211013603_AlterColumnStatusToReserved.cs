using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AlterColumnStatusToReserved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "tables");

            migrationBuilder.AddColumn<sbyte>(
                name: "reserved",
                table: "tables",
                type: "tinyint",
                nullable: false,
                defaultValue: (sbyte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reserved",
                table: "tables");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "tables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
