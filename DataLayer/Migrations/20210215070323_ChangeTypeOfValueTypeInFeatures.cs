using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ChangeTypeOfValueTypeInFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessFeatureType",
                table: "Features");

            migrationBuilder.AddColumn<int>(
                name: "ValueType",
                table: "Features",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d31a5a4-20a5-4512-a5ab-a3ed165886fe", new DateTime(2021, 2, 15, 10, 33, 22, 978, DateTimeKind.Local).AddTicks(5080), "AQAAAAEAACcQAAAAEHPQcvdRcOIjMGCtVo0ciMadfxh1wdPnTeQYSJh4YBS4rpzFt4RjzDT2ZFzdRpijXw==", "2da9eda5-e8e2-4fa6-be44-8a3496a337f9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueType",
                table: "Features");

            migrationBuilder.AddColumn<int>(
                name: "BusinessFeatureType",
                table: "Features",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e00b350-3095-4328-bde5-016833c6be6b", new DateTime(2021, 2, 15, 10, 26, 4, 511, DateTimeKind.Local).AddTicks(5143), "AQAAAAEAACcQAAAAEPpfSUN/Vva68b08GwbAT6aJVOvCKtGVbyTkIwWjw/YIi9zW5MQb4AbOdpeJCTssrg==", "68b383ff-b509-4ebc-8779-9e7fd9796f61" });
        }
    }
}
