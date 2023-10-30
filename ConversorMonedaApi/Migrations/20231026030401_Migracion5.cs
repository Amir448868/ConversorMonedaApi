using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConversorMonedaApi.Migrations
{
    /// <inheritdoc />
    public partial class Migracion5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RemainingRequests",
                columns: new[] { "RequestId", "TypeUser", "Value" },
                values: new object[,]
                {
                    { 1, "free", 10 },
                    { 2, "trial", 100 },
                    { 3, "premium", 1000000000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RemainingRequests",
                keyColumn: "RequestId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RemainingRequests",
                keyColumn: "RequestId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RemainingRequests",
                keyColumn: "RequestId",
                keyValue: 3);
        }
    }
}
