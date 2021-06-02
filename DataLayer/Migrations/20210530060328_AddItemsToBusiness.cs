using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddItemsToBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOpenNow",
                table: "Businesses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 30, 10, 33, 26, 979, DateTimeKind.Local).AddTicks(7373),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 24, 13, 55, 8, 996, DateTimeKind.Local).AddTicks(5299));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a69b5797-f1ee-4b56-8b26-6467109f05a5", new DateTime(2021, 5, 30, 10, 33, 26, 983, DateTimeKind.Local).AddTicks(3737), "AQAAAAEAACcQAAAAEB9lsstyiY3tQ++IyJZxQfIupOsNn66Py3lLOsY6XjAPrfwxcaXH8C3OGJ5SySWWtw==", "e6b2fdf1-9ee5-4ef2-8f7b-b7225c1fa5dc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpenNow",
                table: "Businesses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 24, 13, 55, 8, 996, DateTimeKind.Local).AddTicks(5299),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 30, 10, 33, 26, 979, DateTimeKind.Local).AddTicks(7373));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f6b7961-3914-44f4-a02e-34083b9d87d4", new DateTime(2021, 5, 24, 13, 55, 9, 2, DateTimeKind.Local).AddTicks(7615), "AQAAAAEAACcQAAAAEEz7TupQ+4v1+1WHFguj6vZpHFJXye5XtX5iWRHNPD/UE81m+ioN11ijxMqUXGopzg==", "f465a44e-c731-402b-a6de-89d3517b0736" });
        }
    }
}
