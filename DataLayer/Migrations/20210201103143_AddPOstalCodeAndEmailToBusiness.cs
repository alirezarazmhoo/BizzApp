using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddPOstalCodeAndEmailToBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Businesses",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Businesses",
                type: "nvarchar(11)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ce4c779-565c-41b5-8df5-7000ef1f4d6d", new DateTime(2021, 2, 1, 14, 1, 42, 315, DateTimeKind.Local).AddTicks(1601), "AQAAAAEAACcQAAAAEEHZ23C3keU5hi35kjaJWaK/EHwcTotAhe07ns4MKZUONpPLsuQEUTQsTDio83Kb4w==", "bea8269e-780c-494a-96a7-655f6016f187" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Businesses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0203e438-ef69-4af6-acae-fe1e9c0acb63", new DateTime(2021, 2, 1, 9, 50, 25, 906, DateTimeKind.Local).AddTicks(3022), "AQAAAAEAACcQAAAAEP+CM6axaXPHcR6i74qqFOS2HpZQ4KU1LZeo7cBPzl/pcHOiwp8MjDz7/AmMhq886A==", "a48b455b-8911-40c6-86c0-199864e05e3a" });
        }
    }
}
