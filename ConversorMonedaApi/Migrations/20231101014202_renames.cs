using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConversorMonedaApi.Migrations
{
    /// <inheritdoc />
    public partial class renames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemainingRequests");

            migrationBuilder.RenameColumn(
                name: "RemainingRequests",
                table: "Users",
                newName: "ConversionCounter");

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeUser = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.RequestId);
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "RequestId", "TypeUser", "Value" },
                values: new object[,]
                {
                    { 1, "free", 10 },
                    { 2, "trial", 100 },
                    { 3, "premium", -1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "ConversionCounter",
                table: "Users",
                newName: "RemainingRequests");

            migrationBuilder.CreateTable(
                name: "RemainingRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeUser = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemainingRequests", x => x.RequestId);
                });

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
    }
}
