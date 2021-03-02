using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5018acb-0856-437b-a265-9efe853449a3", new DateTime(2021, 2, 23, 10, 37, 23, 780, DateTimeKind.Local).AddTicks(4820), "AQAAAAEAACcQAAAAEJpikmjGcxss9HkAjahwDB6hbDp2NI6HEvmpgr788dJSni3XNUqYE16GQUxwrYWXpQ==", "72a4239b-cc80-47d4-9571-2f5e9195188a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0d6240d-1a7b-461e-84e3-b1ea9ad6d4ba", new DateTime(2021, 2, 22, 14, 16, 1, 264, DateTimeKind.Local).AddTicks(8716), "AQAAAAEAACcQAAAAENJwg+XYLWSRmnyeS+756/1jsHgq/JrqY7OebUTl1bSoD3ZWL1DSxv+I0n9aXPWGrQ==", "4cbdb240-9f74-4b5c-bda1-52ff6aec1b21" });
        }
    }
}
