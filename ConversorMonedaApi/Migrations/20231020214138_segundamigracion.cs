using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConversorMonedaApi.Migrations
{
    /// <inheritdoc />
    public partial class segundamigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeUser",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "Currencies",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeUser",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "Currencies",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
