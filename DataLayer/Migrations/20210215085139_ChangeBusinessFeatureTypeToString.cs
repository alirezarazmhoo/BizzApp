using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ChangeBusinessFeatureTypeToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "BusinessFeatures",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18a00df3-44eb-42f3-830f-be2fcb4fa36b", new DateTime(2021, 2, 15, 12, 21, 39, 182, DateTimeKind.Local).AddTicks(875), "AQAAAAEAACcQAAAAEOE1mEXu0ThskdD+I6CQ3OloJM4zZ1RRKilf505xc2Xsxe9LxT6JBpMC3zMd1TmKtw==", "2d01d592-1002-49cc-a379-ceac28f2bf27" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "BusinessFeatures",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d31a5a4-20a5-4512-a5ab-a3ed165886fe", new DateTime(2021, 2, 15, 10, 33, 22, 978, DateTimeKind.Local).AddTicks(5080), "AQAAAAEAACcQAAAAEHPQcvdRcOIjMGCtVo0ciMadfxh1wdPnTeQYSJh4YBS4rpzFt4RjzDT2ZFzdRpijXw==", "2da9eda5-e8e2-4fa6-be44-8a3496a337f9" });
        }
    }
}
