using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VodafoneCashApi.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 21, 4, 31, 51, 934, DateTimeKind.Local).AddTicks(9964),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 5, 10, 3, 56, 54, 136, DateTimeKind.Local).AddTicks(2235));

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("c962c063-25e3-4c66-a0fe-de3762eb0f5b"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValue: new Guid("78c14eda-efa0-41d9-a2ff-8c814d74aeb8"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 10, 3, 56, 54, 136, DateTimeKind.Local).AddTicks(2235),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 5, 21, 4, 31, 51, 934, DateTimeKind.Local).AddTicks(9964));

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("78c14eda-efa0-41d9-a2ff-8c814d74aeb8"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValue: new Guid("c962c063-25e3-4c66-a0fe-de3762eb0f5b"));
        }
    }
}
