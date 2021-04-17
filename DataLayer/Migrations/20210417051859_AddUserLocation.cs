using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddUserLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 9, 48, 58, 698, DateTimeKind.Local).AddTicks(4126),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 14, 12, 13, 21, 212, DateTimeKind.Local).AddTicks(3751));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e24de2d-6bfa-4a40-9847-ee65b684ab8c", new DateTime(2021, 4, 17, 9, 48, 58, 703, DateTimeKind.Local).AddTicks(1180), "AQAAAAEAACcQAAAAEDwCFXFVaBdf4oa0syzaehpAokOyJ+uV75Dtl0ZTJWP3qKTGtoK8rk7YQ5DYRdZbQg==", "748e00d5-5693-4499-860b-8712476eea64" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 14, 12, 13, 21, 212, DateTimeKind.Local).AddTicks(3751),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 17, 9, 48, 58, 698, DateTimeKind.Local).AddTicks(4126));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fc83a85-e8de-4c92-8741-72a7656f46cf", new DateTime(2021, 4, 14, 12, 13, 21, 215, DateTimeKind.Local).AddTicks(9537), "AQAAAAEAACcQAAAAENOlpGkAjGCsOMH+Y9+w5rRthC1HLf/eTggv3FnmXwekZAqgJNuDESjIXI9I5G4FNQ==", "8c0b5630-1dac-4a30-8df0-1e12e2562992" });
        }
    }
}
