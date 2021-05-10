using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddCreatedAtToFriendAndNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Friends",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 5, 11, 4, 29, 9, DateTimeKind.Local).AddTicks(8142),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 5, 9, 47, 25, 2, DateTimeKind.Local).AddTicks(1390));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa68f318-e301-4ea2-8994-c9a7f096d974", new DateTime(2021, 5, 5, 11, 4, 29, 13, DateTimeKind.Local).AddTicks(6657), "AQAAAAEAACcQAAAAEN1W0xqz9QGIk5Lt6vdhiG9j1GdBi9KbcvOvoUPU4XTPR4unur75c9d8ebhH7tBm8w==", "86fc553e-7f89-4b8d-a36c-e7c059c1aee9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Friends");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 5, 9, 47, 25, 2, DateTimeKind.Local).AddTicks(1390),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 5, 11, 4, 29, 9, DateTimeKind.Local).AddTicks(8142));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "655c570a-59d4-4465-9d6b-085527df379b", new DateTime(2021, 5, 5, 9, 47, 25, 6, DateTimeKind.Local).AddTicks(6464), "AQAAAAEAACcQAAAAEEL4yBTkXPUpWywflMACWL1dU48VgQ0m5WN4QUIOoZy3xxE21yCqx8A0qIb8BH0MHQ==", "cd499074-1d2c-415d-805d-2a55366a6354" });
        }
    }
}
