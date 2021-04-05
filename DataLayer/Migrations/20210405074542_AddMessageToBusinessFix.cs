using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddMessageToBusinessFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "MessageToBusinesses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 12, 15, 40, 944, DateTimeKind.Local).AddTicks(8149),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 5, 12, 11, 52, 213, DateTimeKind.Local).AddTicks(4739));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0aa3c0aa-09ff-42f1-81dd-2ea4e6333c6e", new DateTime(2021, 4, 5, 12, 15, 40, 951, DateTimeKind.Local).AddTicks(8335), "AQAAAAEAACcQAAAAEInWkCjwIB3M53e28aGpEJ8oMt3Oih+gVtod5ET00HovcSR+yQF0NUJDe6boVheSRw==", "5c51b5dd-211a-4452-b30e-cb21ebacece9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "MessageToBusinesses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 12, 11, 52, 213, DateTimeKind.Local).AddTicks(4739),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 5, 12, 15, 40, 944, DateTimeKind.Local).AddTicks(8149));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e706a261-6cd5-4526-acf2-2e1b3e485adc", new DateTime(2021, 4, 5, 12, 11, 52, 218, DateTimeKind.Local).AddTicks(6532), "AQAAAAEAACcQAAAAEHWVBrBSJT1pU2ZrroEpSClR/ExoNCC7ObjwIffdSCpyZ74CUjjA89BKVh3mpRqieg==", "fdd136f0-7482-4192-ac9d-dc92fcf5ecc1" });
        }
    }
}
