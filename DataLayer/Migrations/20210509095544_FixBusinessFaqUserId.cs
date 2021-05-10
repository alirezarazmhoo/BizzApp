using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FixBusinessFaqUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessFaqs_AspNetUsers_BizAppUserId1",
                table: "BusinessFaqs");

            migrationBuilder.DropIndex(
                name: "IX_BusinessFaqs_BizAppUserId1",
                table: "BusinessFaqs");

            migrationBuilder.DropColumn(
                name: "BizAppUserId1",
                table: "BusinessFaqs");

            migrationBuilder.AlterColumn<string>(
                name: "BizAppUserId",
                table: "BusinessFaqs",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 9, 14, 25, 43, 497, DateTimeKind.Local).AddTicks(6748),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 9, 12, 0, 47, 875, DateTimeKind.Local).AddTicks(707));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1459812-19c3-4c72-88a3-dbc17d516741", new DateTime(2021, 5, 9, 14, 25, 43, 503, DateTimeKind.Local).AddTicks(1862), "AQAAAAEAACcQAAAAEMHllSsW3o+FymdZP9yn6bOHAzLYR599ylmAWSGo9NmdLLelQlC/wMoUjC2bkkZGsw==", "64982f77-7996-4370-b43f-49bab39d6230" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFaqs_BizAppUserId",
                table: "BusinessFaqs",
                column: "BizAppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessFaqs_AspNetUsers_BizAppUserId",
                table: "BusinessFaqs",
                column: "BizAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessFaqs_AspNetUsers_BizAppUserId",
                table: "BusinessFaqs");

            migrationBuilder.DropIndex(
                name: "IX_BusinessFaqs_BizAppUserId",
                table: "BusinessFaqs");

            migrationBuilder.AlterColumn<Guid>(
                name: "BizAppUserId",
                table: "BusinessFaqs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BizAppUserId1",
                table: "BusinessFaqs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 9, 12, 0, 47, 875, DateTimeKind.Local).AddTicks(707),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 9, 14, 25, 43, 497, DateTimeKind.Local).AddTicks(6748));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f08300b3-760c-4b53-a58b-b1eb1663b2d2", new DateTime(2021, 5, 9, 12, 0, 47, 880, DateTimeKind.Local).AddTicks(3350), "AQAAAAEAACcQAAAAEGT6HOc9ErjkatSJqZb3ncWM6g0JiBSWHUZOs+Mx9yILfbp43r+BLGAKFpFAxCtEVA==", "9c616f7d-84c7-4b17-bdfe-0d799fd18252" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFaqs_BizAppUserId1",
                table: "BusinessFaqs",
                column: "BizAppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessFaqs_AspNetUsers_BizAppUserId1",
                table: "BusinessFaqs",
                column: "BizAppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
