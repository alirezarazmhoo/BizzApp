using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class UpdateBizinessBiographyLenght : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Businesses",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Biography",
                table: "Businesses",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 21, 12, 31, 21, 273, DateTimeKind.Local).AddTicks(8075),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 30, 10, 33, 26, 979, DateTimeKind.Local).AddTicks(7373));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "942de341-b7a9-4fef-81ae-c21cbd2acd77", new DateTime(2021, 6, 21, 12, 31, 21, 278, DateTimeKind.Local).AddTicks(800), "AQAAAAEAACcQAAAAEAMeCATfgMAabkiEjVXju83XWM1t5TTk1vYiauwEZZovKOvR3kys7GMrFPvEs46NBQ==", "a187142f-9166-4343-821a-57a2085b7b9d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Businesses",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Biography",
                table: "Businesses",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 30, 10, 33, 26, 979, DateTimeKind.Local).AddTicks(7373),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 6, 21, 12, 31, 21, 273, DateTimeKind.Local).AddTicks(8075));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a69b5797-f1ee-4b56-8b26-6467109f05a5", new DateTime(2021, 5, 30, 10, 33, 26, 983, DateTimeKind.Local).AddTicks(3737), "AQAAAAEAACcQAAAAEB9lsstyiY3tQ++IyJZxQfIupOsNn66Py3lLOsY6XjAPrfwxcaXH8C3OGJ5SySWWtw==", "e6b2fdf1-9ee5-4ef2-8f7b-b7225c1fa5dc" });
        }
    }
}
