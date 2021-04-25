using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ChangeMessageToBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BizAppUserId",
                table: "MessageToBusinesses",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 25, 11, 35, 52, 128, DateTimeKind.Local).AddTicks(8350),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 21, 12, 36, 37, 993, DateTimeKind.Local).AddTicks(3165));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d3a7867-3198-47b5-9533-4f7790a49593", new DateTime(2021, 4, 25, 11, 35, 52, 134, DateTimeKind.Local).AddTicks(7065), "AQAAAAEAACcQAAAAEELYvDI8DC36UBolLGWDT8ixlbNO6llzA/04IvrkQESCRLt+Zz/8y6HYrqUoWKpW5w==", "38c8d230-12b0-4ec9-911b-389b7c94689d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BizAppUserId",
                table: "MessageToBusinesses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 21, 12, 36, 37, 993, DateTimeKind.Local).AddTicks(3165),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 25, 11, 35, 52, 128, DateTimeKind.Local).AddTicks(8350));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d621b16-ac1a-48d3-bcdc-40b06a3b02c4", new DateTime(2021, 4, 21, 12, 36, 38, 0, DateTimeKind.Local).AddTicks(8953), "AQAAAAEAACcQAAAAEBnPSmJvZK2SnLX10QhEhSls7y9LFuIOkcvstF2OWtYLCpXVSWMnnz304M3GgmKZMA==", "6860eea1-3dc3-43e9-b5a1-b4bcbbbea1b7" });
        }
    }
}
