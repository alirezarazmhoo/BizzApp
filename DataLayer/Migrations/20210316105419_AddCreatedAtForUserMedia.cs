using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddCreatedAtForUserMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ApplicationUserMedias",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ApplicationUserMedias",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Mobile = table.Column<long>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    NationalCode = table.Column<string>(nullable: true),
                    ApiToken = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 3, 16, 14, 24, 18, 858, DateTimeKind.Local).AddTicks(4634))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f0bc4b1-c894-4c5b-aa0c-5f50135cf5d5", new DateTime(2021, 3, 16, 14, 24, 18, 862, DateTimeKind.Local).AddTicks(3543), "AQAAAAEAACcQAAAAELvi2YmCv3ozojjAmoJwrVT7J+/erzzbtJDGKHjzFINFxN6jmjRi3d+M7MFBfWBCYA==", "31feb179-3c71-4939-ac2e-ba365ad81185" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserMedias_ApplicationUserId",
                table: "ApplicationUserMedias",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserMedias_ApplicationUser_ApplicationUserId",
                table: "ApplicationUserMedias",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserMedias_ApplicationUser_ApplicationUserId",
                table: "ApplicationUserMedias");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserMedias_ApplicationUserId",
                table: "ApplicationUserMedias");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ApplicationUserMedias");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ApplicationUserMedias");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5be4cf83-a4b6-4583-bc06-eed620f7cee0", new DateTime(2021, 3, 14, 11, 20, 40, 748, DateTimeKind.Local).AddTicks(5876), "AQAAAAEAACcQAAAAEFJHy8O+r25hX/wpu8vp8X73ESXeywPSz8Zgbil41xn+Qvt/0UFZKnFMk8laOWz7Mg==", "df48bdf1-24d6-4c96-85d8-23894bc13de2" });
        }
    }
}
