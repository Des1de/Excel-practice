using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mvc.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClosingBalances",
                columns: table => new
                {
                    ClosingBalanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<decimal>(type: "numeric", nullable: false),
                    Passive = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosingBalances", x => x.ClosingBalanceId);
                });

            migrationBuilder.CreateTable(
                name: "OpeningBalances",
                columns: table => new
                {
                    OpeningBalanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<decimal>(type: "numeric", nullable: false),
                    Passive = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningBalances", x => x.OpeningBalanceId);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    TableId = table.Column<Guid>(type: "uuid", nullable: false),
                    BankName = table.Column<string>(type: "text", nullable: false),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    Period = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableId);
                });

            migrationBuilder.CreateTable(
                name: "Turnovers",
                columns: table => new
                {
                    TurnoverId = table.Column<Guid>(type: "uuid", nullable: false),
                    Debit = table.Column<decimal>(type: "numeric", nullable: false),
                    Credit = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnovers", x => x.TurnoverId);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    TableId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassName = table.Column<string>(type: "text", nullable: false),
                    ClassNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_Classes_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountNumber = table.Column<int>(type: "integer", nullable: false),
                    OpeningBalanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    TurnoverId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClosingBalanceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_ClosingBalances_ClosingBalanceId",
                        column: x => x.ClosingBalanceId,
                        principalTable: "ClosingBalances",
                        principalColumn: "ClosingBalanceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_OpeningBalances_OpeningBalanceId",
                        column: x => x.OpeningBalanceId,
                        principalTable: "OpeningBalances",
                        principalColumn: "OpeningBalanceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Turnovers_TurnoverId",
                        column: x => x.TurnoverId,
                        principalTable: "Turnovers",
                        principalColumn: "TurnoverId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ClassId",
                table: "Accounts",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ClosingBalanceId",
                table: "Accounts",
                column: "ClosingBalanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OpeningBalanceId",
                table: "Accounts",
                column: "OpeningBalanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_TurnoverId",
                table: "Accounts",
                column: "TurnoverId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TableId",
                table: "Classes",
                column: "TableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "ClosingBalances");

            migrationBuilder.DropTable(
                name: "OpeningBalances");

            migrationBuilder.DropTable(
                name: "Turnovers");

            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
