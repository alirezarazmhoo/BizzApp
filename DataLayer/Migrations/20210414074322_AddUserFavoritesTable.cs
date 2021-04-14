using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddUserFavoritesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 14, 12, 13, 21, 212, DateTimeKind.Local).AddTicks(3751),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 14, 11, 3, 35, 971, DateTimeKind.Local).AddTicks(4641));

            migrationBuilder.CreateTable(
                name: "UserFavorits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BizAppUserId = table.Column<string>(nullable: true),
                    BusinessId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    BusinessId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavorits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavorits_AspNetUsers_BizAppUserId",
                        column: x => x.BizAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFavorits_Businesses_BusinessId1",
                        column: x => x.BusinessId1,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fc83a85-e8de-4c92-8741-72a7656f46cf", new DateTime(2021, 4, 14, 12, 13, 21, 215, DateTimeKind.Local).AddTicks(9537), "AQAAAAEAACcQAAAAENOlpGkAjGCsOMH+Y9+w5rRthC1HLf/eTggv3FnmXwekZAqgJNuDESjIXI9I5G4FNQ==", "8c0b5630-1dac-4a30-8df0-1e12e2562992" });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorits_BizAppUserId",
                table: "UserFavorits",
                column: "BizAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorits_BusinessId1",
                table: "UserFavorits",
                column: "BusinessId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavorits");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 14, 11, 3, 35, 971, DateTimeKind.Local).AddTicks(4641),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 14, 12, 13, 21, 212, DateTimeKind.Local).AddTicks(3751));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52a8cba0-7ce4-4d72-9400-1447f9390281", new DateTime(2021, 4, 14, 11, 3, 35, 979, DateTimeKind.Local).AddTicks(7564), "AQAAAAEAACcQAAAAEOhmO8NvTa0//fOLZ46vt5+VJjGI/yMfOWyujAmDItyswrqKDgWZATqih5eDx0WC0A==", "8f00edf8-936b-4738-8550-4c67cccf2afa" });
        }
    }
}
