using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConversorMonedaApi.Migrations
{
    /// <inheritdoc />
    public partial class primermigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    MonedaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.MonedaId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    RemainingRequests = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Conversions",
                columns: table => new
                {
                    ConversionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurrencyFromId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrencyToId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    Result = table.Column<double>(type: "REAL", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversions", x => x.ConversionId);
                    table.ForeignKey(
                        name: "FK_Conversions_Currencies_CurrencyFromId",
                        column: x => x.CurrencyFromId,
                        principalTable: "Currencies",
                        principalColumn: "MonedaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversions_Currencies_CurrencyToId",
                        column: x => x.CurrencyToId,
                        principalTable: "Currencies",
                        principalColumn: "MonedaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestsLog",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestsLog", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_RequestsLog_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_CurrencyFromId",
                table: "Conversions",
                column: "CurrencyFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_CurrencyToId",
                table: "Conversions",
                column: "CurrencyToId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_UserId",
                table: "Conversions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestsLog_UserId",
                table: "RequestsLog",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conversions");

            migrationBuilder.DropTable(
                name: "RequestsLog");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
