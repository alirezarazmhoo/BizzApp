using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddUserCustomFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FindMeIn",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "HomeTown",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhotoChanged",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadedPhoto",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Webstie",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71e35f91-5b56-47dc-bb59-96cb711d8313", new DateTime(2021, 3, 1, 14, 24, 41, 346, DateTimeKind.Local).AddTicks(4541), "AQAAAAEAACcQAAAAEHAlcGHXzAPRuYDCvfh3ZLUiFhvn8cihIDCTwL2Hmjb05AQGC5f1kDHKVnzrK/pcWA==", "33885823-8a8c-4747-9d56-c8e1557856b9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FindMeIn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HomeTown",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhotoChanged",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UploadedPhoto",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Webstie",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01470d8c-0243-45f2-bd70-2488ffa0c60c", new DateTime(2021, 2, 28, 12, 11, 41, 324, DateTimeKind.Local).AddTicks(748), "AQAAAAEAACcQAAAAEF3rvp807ljptoOw9Zlh/zoI86QA4eqGoESBBy3wJFgLQxvgp9IoCNp2HYUUEVUJ4w==", "8a789ca6-4e55-49cb-b425-7d300354053a" });
        }
    }
}
