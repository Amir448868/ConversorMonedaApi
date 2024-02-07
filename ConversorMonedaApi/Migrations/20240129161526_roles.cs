using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConversorMonedaApi.Migrations
{
    /// <inheritdoc />
    public partial class roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingRequest",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemainingRequest",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
