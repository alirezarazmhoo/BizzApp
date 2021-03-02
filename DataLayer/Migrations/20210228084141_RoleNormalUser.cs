using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class RoleNormalUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20df3014-a5e0-4f94-bf15-11685f5f9a85",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Owner", "OWNER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "447ffd0e-d5f1-4301-b9c1-bf08f8d351d2", "447ffd0e-d5f1-4301-b9c1-bf08f8d351d2", "member", "member" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01470d8c-0243-45f2-bd70-2488ffa0c60c", new DateTime(2021, 2, 28, 12, 11, 41, 324, DateTimeKind.Local).AddTicks(748), "AQAAAAEAACcQAAAAEF3rvp807ljptoOw9Zlh/zoI86QA4eqGoESBBy3wJFgLQxvgp9IoCNp2HYUUEVUJ4w==", "8a789ca6-4e55-49cb-b425-7d300354053a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "447ffd0e-d5f1-4301-b9c1-bf08f8d351d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20df3014-a5e0-4f94-bf15-11685f5f9a85",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Business Name", "BUSINESS NAME" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00b214d4-81ba-4a0a-8638-e3c7b897d6d7", new DateTime(2021, 2, 28, 10, 18, 0, 496, DateTimeKind.Local).AddTicks(3309), "AQAAAAEAACcQAAAAEMsi1zKiF0cdb35bk+hgQ9ovLRuXuflam6J+5CUTKxP8osK7N42Ef48YsM1qa1n6dQ==", "b65f50d7-3000-4b07-9239-6b5d43b8f1f2" });

            migrationBuilder.InsertData(
                table: "Sliders",
                columns: new[] { "Id", "Image", "Status", "Text", "Title" },
                values: new object[,]
                {
                    { 1, "/Upload/Slider/Files/1.jpg", 1, "Text1", "Title1" },
                    { 2, "/Upload/Slider/Files/2.jpg", 1, "Text2", "Title2" },
                    { 3, "/Upload/Slider/Files/3.jpg", 1, "Text3", "Title3" }
                });
        }
    }
}
