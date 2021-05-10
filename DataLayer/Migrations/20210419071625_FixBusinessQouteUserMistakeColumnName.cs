using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FixBusinessQouteUserMistakeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NVARCHAR(1000)",
                table: "BusinessQouteUsers",
                newName: "AnswerQoute");

            migrationBuilder.AlterColumn<string>(
                name: "BizAppUserId",
                table: "BusinessQouteUsers",
                type: "NVARCHAR(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AnswerQoute",
                table: "BusinessQouteUsers",
                type: "NVARCHAR(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 19, 11, 46, 24, 532, DateTimeKind.Local).AddTicks(2930),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 18, 12, 28, 19, 337, DateTimeKind.Local).AddTicks(8483));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1570c1a7-879c-4c7f-9185-ceda4e29a91f", new DateTime(2021, 4, 19, 11, 46, 24, 537, DateTimeKind.Local).AddTicks(2626), "AQAAAAEAACcQAAAAENCjhOG4peQggkGAyvRaQGsfX7J2zL54V4D5jkTvtgcbu1j6EtxZ9tFwPopJbWC8Ew==", "6e429b5d-4870-441d-b753-4d91258851f6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnswerQoute",
                table: "BusinessQouteUsers",
                newName: "NVARCHAR(1000)");

            migrationBuilder.AlterColumn<string>(
                name: "BizAppUserId",
                table: "BusinessQouteUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(450)");

            migrationBuilder.AlterColumn<string>(
                name: "NVARCHAR(1000)",
                table: "BusinessQouteUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(1000)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 18, 12, 28, 19, 337, DateTimeKind.Local).AddTicks(8483),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 19, 11, 46, 24, 532, DateTimeKind.Local).AddTicks(2930));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "278531ed-2552-438d-9102-2a469b1d3059", new DateTime(2021, 4, 18, 12, 28, 19, 343, DateTimeKind.Local).AddTicks(4838), "AQAAAAEAACcQAAAAELX8qldO0H8AJKMkyylUiBpFUSOjmkumghcIOLoEZZ0tmBOgXhC5g7ZbYps3uFac8Q==", "f2600c01-0019-45d4-8dc0-dcff979ce008" });
        }
    }
}
