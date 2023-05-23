using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VodafoneCashApi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Numbers",
                columns: table => new
                {
                    Number = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Numbers", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "TEXT", nullable: false, defaultValue: new Guid("78c14eda-efa0-41d9-a2ff-8c814d74aeb8")),
                    NumberId = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    CashBefore = table.Column<decimal>(type: "TEXT", nullable: false),
                    CashAfter = table.Column<decimal>(type: "TEXT", nullable: false),
                    TransactionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2023, 5, 10, 3, 56, 54, 136, DateTimeKind.Local).AddTicks(2235))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Numbers_NumberId",
                        column: x => x.NumberId,
                        principalTable: "Numbers",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_NumberId",
                table: "Transactions",
                column: "NumberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Numbers");
        }
    }
}
