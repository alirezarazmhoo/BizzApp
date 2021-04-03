using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddIsClaimedToBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClaimed",
                table: "Businesses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 3, 12, 55, 17, 195, DateTimeKind.Local).AddTicks(1782),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 31, 13, 10, 33, 524, DateTimeKind.Local).AddTicks(3131));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ad2a80c-1bcb-4ed3-8603-088b91c1c9b3", new DateTime(2021, 4, 3, 12, 55, 17, 200, DateTimeKind.Local).AddTicks(5626), "AQAAAAEAACcQAAAAEDWhlIdksuM9x30aIUyzo3XjN1xs4LuOnQFmjHy6GZf8ki5FPpRqPqK6vatHUACeUg==", "8b9d2312-2ba3-4b35-a9b1-140f73c780c7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClaimed",
                table: "Businesses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 31, 13, 10, 33, 524, DateTimeKind.Local).AddTicks(3131),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 3, 12, 55, 17, 195, DateTimeKind.Local).AddTicks(1782));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14a0d37e-b30b-4bd0-91c6-a51e11237389", new DateTime(2021, 3, 31, 13, 10, 33, 529, DateTimeKind.Local).AddTicks(694), "AQAAAAEAACcQAAAAEIfWZma62Rv+INlIcztT0DCmh8krEAwTkBJO5UXI6icjSAGiqezXuu7+J91LpYQRPg==", "04082825-6618-4ecf-b806-d2278fd41384" });
        }
    }
}
