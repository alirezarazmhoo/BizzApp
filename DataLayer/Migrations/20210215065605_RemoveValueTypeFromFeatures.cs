using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class RemoveValueTypeFromFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueType",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "BusinessFeatureType",
                table: "BusinessFeatures");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e00b350-3095-4328-bde5-016833c6be6b", new DateTime(2021, 2, 15, 10, 26, 4, 511, DateTimeKind.Local).AddTicks(5143), "AQAAAAEAACcQAAAAEPpfSUN/Vva68b08GwbAT6aJVOvCKtGVbyTkIwWjw/YIi9zW5MQb4AbOdpeJCTssrg==", "68b383ff-b509-4ebc-8779-9e7fd9796f61" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ValueType",
                table: "Features",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "bool");

            migrationBuilder.AddColumn<int>(
                name: "BusinessFeatureType",
                table: "BusinessFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66df90b4-a89d-4609-81c2-37461837aec8", new DateTime(2021, 2, 6, 11, 21, 10, 679, DateTimeKind.Local).AddTicks(1535), "AQAAAAEAACcQAAAAEFxvWSNBcNI7ty9gS+T60culp+Edn1z4JynIeZE7SJ06TcUNUQTCgN0urOdp1aDGmQ==", "f232ba28-8024-452c-bbc5-062e0967d7f2" });
        }
    }
}
