using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddBusinessTimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessTime_Businesses_BusinessId",
                table: "BusinessTime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessTime",
                table: "BusinessTime");

            migrationBuilder.RenameTable(
                name: "BusinessTime",
                newName: "BusinessTimes");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessTime_BusinessId",
                table: "BusinessTimes",
                newName: "IX_BusinessTimes_BusinessId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 12, 14, 43, 0, 887, DateTimeKind.Local).AddTicks(2413),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 12, 14, 41, 30, 921, DateTimeKind.Local).AddTicks(9499));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessTimes",
                table: "BusinessTimes",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3bf35de-749d-4b9d-ae51-62242678b77e", new DateTime(2021, 4, 12, 14, 43, 0, 890, DateTimeKind.Local).AddTicks(8437), "AQAAAAEAACcQAAAAEIOkt8LZt2yfs7Aazh4+TNuY+VIWbeKU/VfxC4WqIrCaxIHdTunTkaXwQ8vV6QI/Bw==", "5d298df7-ae87-4521-8b9f-bcc402f3c864" });

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessTimes_Businesses_BusinessId",
                table: "BusinessTimes",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessTimes_Businesses_BusinessId",
                table: "BusinessTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessTimes",
                table: "BusinessTimes");

            migrationBuilder.RenameTable(
                name: "BusinessTimes",
                newName: "BusinessTime");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessTimes_BusinessId",
                table: "BusinessTime",
                newName: "IX_BusinessTime_BusinessId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 12, 14, 41, 30, 921, DateTimeKind.Local).AddTicks(9499),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 12, 14, 43, 0, 887, DateTimeKind.Local).AddTicks(2413));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessTime",
                table: "BusinessTime",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d1ee4b8-a8f5-4ecb-a811-52f1b1ed43a6", new DateTime(2021, 4, 12, 14, 41, 30, 926, DateTimeKind.Local).AddTicks(4262), "AQAAAAEAACcQAAAAELyRFMf8/y9FeEPJYSgy/UTiYYLhUQufkdDEwXAMBRXkjWaUbh7KhJlnXCvtWp7cPg==", "97500300-fe0d-41df-b762-0747799c59f8" });

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessTime_Businesses_BusinessId",
                table: "BusinessTime",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
