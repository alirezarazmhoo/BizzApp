using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 13, 10, 51, 2, 631, DateTimeKind.Local).AddTicks(9709),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 13, 10, 41, 33, 627, DateTimeKind.Local).AddTicks(4574));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "388ffc33-874f-4dbf-91ee-8b1de6c32723", new DateTime(2021, 4, 13, 10, 51, 2, 635, DateTimeKind.Local).AddTicks(5470), "AQAAAAEAACcQAAAAEPpSAK6XmOfKv8i/5atxh/yC2O9/zzF3Udxt1YmcgCY2uWYYyg5AyyIhAovOwe+yAA==", "c9ee5387-b4c6-4bec-99bd-553988b2b1ed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 13, 10, 41, 33, 627, DateTimeKind.Local).AddTicks(4574),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 13, 10, 51, 2, 631, DateTimeKind.Local).AddTicks(9709));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c075e0f-eb5e-43b5-beab-6dd0f3f55b6d", new DateTime(2021, 4, 13, 10, 41, 33, 633, DateTimeKind.Local).AddTicks(7698), "AQAAAAEAACcQAAAAECjfbH551lupwko63LOGRA32M6e4nuf9tO6PoTBfNpUIaQhxs92MyNyz/MlYAXWURw==", "135e0203-6ac3-49dc-89da-cd1ffcf38871" });
        }
    }
}
