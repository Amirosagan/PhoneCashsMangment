using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VodafoneCashApi.Migrations
{
    /// <inheritdoc />
    public partial class Fix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 5, 21, 4, 31, 51, 934, DateTimeKind.Local).AddTicks(9964));

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValue: new Guid("c962c063-25e3-4c66-a0fe-de3762eb0f5b"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 21, 4, 31, 51, 934, DateTimeKind.Local).AddTicks(9964),
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("c962c063-25e3-4c66-a0fe-de3762eb0f5b"),
                oldClrType: typeof(Guid),
                oldType: "TEXT");
        }
    }
}
