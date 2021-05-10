using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class IsDefaultToDistric : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Districts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 21, 12, 36, 37, 993, DateTimeKind.Local).AddTicks(3165),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 21, 11, 54, 0, 280, DateTimeKind.Local).AddTicks(8843));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d621b16-ac1a-48d3-bcdc-40b06a3b02c4", new DateTime(2021, 4, 21, 12, 36, 38, 0, DateTimeKind.Local).AddTicks(8953), "AQAAAAEAACcQAAAAEBnPSmJvZK2SnLX10QhEhSls7y9LFuIOkcvstF2OWtYLCpXVSWMnnz304M3GgmKZMA==", "6860eea1-3dc3-43e9-b5a1-b4bcbbbea1b7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Districts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 21, 11, 54, 0, 280, DateTimeKind.Local).AddTicks(8843),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 21, 12, 36, 37, 993, DateTimeKind.Local).AddTicks(3165));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5540f547-2cb9-4ef7-b627-0634eb41c763", new DateTime(2021, 4, 21, 11, 54, 0, 300, DateTimeKind.Local).AddTicks(2167), "AQAAAAEAACcQAAAAEDu6xk9t7ui1MiNnZ2X+4LnU29TeUC31CBl0TasSzqmhrsxbZ5oJMmjsQpdj6I8JeA==", "04c418af-5be4-4db7-9ccc-a8b788c179ba" });
        }
    }
}
