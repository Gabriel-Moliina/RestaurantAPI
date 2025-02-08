using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnTableFreeToStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "free",
                table: "tables");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "tables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "tables");

            migrationBuilder.AddColumn<bool>(
                name: "free",
                table: "tables",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
