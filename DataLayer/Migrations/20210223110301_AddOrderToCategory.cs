using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddOrderToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "CategoryTerm",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "450bc192-ced7-42e8-9b7f-9f232aab9a8d", new DateTime(2021, 2, 23, 14, 33, 0, 490, DateTimeKind.Local).AddTicks(2680), "AQAAAAEAACcQAAAAEP0CKMv3T33p67tgrKuna7P09vi4twNQrQ9YHnoj2HzdzyMgaXVyiJ1F/PhhhH0aoA==", "32db1469-9881-4214-9098-35934e425b8b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "CategoryTerm",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5018acb-0856-437b-a265-9efe853449a3", new DateTime(2021, 2, 23, 10, 37, 23, 780, DateTimeKind.Local).AddTicks(4820), "AQAAAAEAACcQAAAAEJpikmjGcxss9HkAjahwDB6hbDp2NI6HEvmpgr788dJSni3XNUqYE16GQUxwrYWXpQ==", "72a4239b-cc80-47d4-9571-2f5e9195188a" });
        }
    }
}
