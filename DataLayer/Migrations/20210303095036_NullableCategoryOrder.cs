using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class NullableCategoryOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Categories",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aab95ea8-15de-4515-9d12-b0f4352fb1d5", new DateTime(2021, 3, 3, 13, 20, 35, 640, DateTimeKind.Local).AddTicks(1356), "AQAAAAEAACcQAAAAEAilMM5wCIAINzGBEeQN+kKQ9bXcdKez7CV2PUS7z2GcxKhQ7Labh2ZUw4IcW4++Fw==", "d2eae8c3-60f1-4267-b361-429acab0375c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Categories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1920a894-701e-4b4a-a839-cee482f8cf1e", new DateTime(2021, 3, 3, 10, 24, 45, 863, DateTimeKind.Local).AddTicks(7505), "AQAAAAEAACcQAAAAEFEBfZXlxMmqlhmF9rkHHluZldY3hK9zFY4Y1Jt4T0L8AFeBq3WsHY2nGKyPYhknYQ==", "31a00790-c4d1-4dd5-8418-cf7d53e0cc37" });
        }
    }
}
