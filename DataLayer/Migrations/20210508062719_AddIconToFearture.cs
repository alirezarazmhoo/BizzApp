using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddIconToFearture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Features",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 8, 10, 57, 18, 966, DateTimeKind.Local).AddTicks(2709),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 5, 11, 4, 29, 9, DateTimeKind.Local).AddTicks(8142));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2c6d0a8-8a2c-420b-b6d7-7304464b45e1", new DateTime(2021, 5, 8, 10, 57, 18, 971, DateTimeKind.Local).AddTicks(708), "AQAAAAEAACcQAAAAEBwAOAnqbLskJb+VxvIvoceJV/rOnhYTaPdk2w9SHKIsfJTrbdAeOJeU3yfKKSnpmg==", "2b6f5f3a-48b0-4761-81ce-db79c9b6a220" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Features");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 5, 11, 4, 29, 9, DateTimeKind.Local).AddTicks(8142),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 8, 10, 57, 18, 966, DateTimeKind.Local).AddTicks(2709));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa68f318-e301-4ea2-8994-c9a7f096d974", new DateTime(2021, 5, 5, 11, 4, 29, 13, DateTimeKind.Local).AddTicks(6657), "AQAAAAEAACcQAAAAEN1W0xqz9QGIk5Lt6vdhiG9j1GdBi9KbcvOvoUPU4XTPR4unur75c9d8ebhH7tBm8w==", "86fc553e-7f89-4b8d-a36c-e7c059c1aee9" });
        }
    }
}
