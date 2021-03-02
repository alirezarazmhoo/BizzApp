using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ChangeUserDistrictToCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f24854ad-426a-44c8-b164-719fb33d7dea", new DateTime(2021, 3, 2, 10, 10, 18, 899, DateTimeKind.Local).AddTicks(1347), "AQAAAAEAACcQAAAAEB79sDbBEOiFMHIEvIzu1VERs4+UtaNk4EIiAVu5VUJVHCVrUiHYOblLH1AM4F3tWg==", "fbe591a3-9892-4416-8aac-876c675401e1" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71e35f91-5b56-47dc-bb59-96cb711d8313", new DateTime(2021, 3, 1, 14, 24, 41, 346, DateTimeKind.Local).AddTicks(4541), "AQAAAAEAACcQAAAAEHAlcGHXzAPRuYDCvfh3ZLUiFhvn8cihIDCTwL2Hmjb05AQGC5f1kDHKVnzrK/pcWA==", "33885823-8a8c-4747-9d56-c8e1557856b9" });
        }
    }
}
