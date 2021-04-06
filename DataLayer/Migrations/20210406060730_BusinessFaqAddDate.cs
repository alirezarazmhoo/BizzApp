using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class BusinessFaqAddDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "BusinessFaqs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 6, 10, 37, 30, 21, DateTimeKind.Local).AddTicks(1850),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 5, 14, 18, 17, 622, DateTimeKind.Local).AddTicks(3541));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f094efd-4d6c-4513-9a31-062a81494817", new DateTime(2021, 4, 6, 10, 37, 30, 26, DateTimeKind.Local).AddTicks(7095), "AQAAAAEAACcQAAAAEGAgo8P38vMeCV0CiUXKXu3kFXptPJfi0iqsGw9c0rJ6bHX8KQfxbAhl8meD/KUkhw==", "5a869504-169d-4a68-9e0c-e600f2807bad" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "BusinessFaqs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 14, 18, 17, 622, DateTimeKind.Local).AddTicks(3541),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 6, 10, 37, 30, 21, DateTimeKind.Local).AddTicks(1850));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f42bc47a-4992-4512-a9a4-13d7fae8ac5c", new DateTime(2021, 4, 5, 14, 18, 17, 625, DateTimeKind.Local).AddTicks(8251), "AQAAAAEAACcQAAAAEOuZ3VrIRMDNdQrzK7NI1H0nN0nY+geDSBQl1LNtD3mcU2JxVtgpkE8mZejyyHqe/w==", "263e7d48-887e-45b1-8f6a-9afc607fb741" });
        }
    }
}
