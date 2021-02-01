using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddUserDisableFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0203e438-ef69-4af6-acae-fe1e9c0acb63", new DateTime(2021, 2, 1, 9, 50, 25, 906, DateTimeKind.Local).AddTicks(3022), "AQAAAAEAACcQAAAAEP+CM6axaXPHcR6i74qqFOS2HpZQ4KU1LZeo7cBPzl/pcHOiwp8MjDz7/AmMhq886A==", "a48b455b-8911-40c6-86c0-199864e05e3a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7e0e27e-2c43-4ede-9a7b-754f9e4da1f6", new DateTime(2021, 1, 27, 11, 2, 12, 877, DateTimeKind.Local).AddTicks(9415), "AQAAAAEAACcQAAAAEJ1C4nLkJ7gJ0vOMtQM7lzzKH1uVHJVjak4uId3kiNJQ/9S+Sm+FWLf3b0Bd5MrxeQ==", "6b1321a1-9527-41eb-aa94-2c8fd00536bf" });
        }
    }
}
