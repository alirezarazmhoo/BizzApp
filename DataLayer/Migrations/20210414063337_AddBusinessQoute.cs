using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddBusinessQoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 14, 11, 3, 35, 971, DateTimeKind.Local).AddTicks(4641),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 14, 10, 9, 7, 656, DateTimeKind.Local).AddTicks(8319));

            migrationBuilder.CreateTable(
                name: "BusinessQoutes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    Ask = table.Column<string>(type: "NVARCHAR(500)", nullable: false),
                    Answer = table.Column<string>(nullable: false),
                    IsSelectedAnswer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessQoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessQoutes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessQouteUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BusinessId = table.Column<Guid>(nullable: false),
                    BusinessQouteId = table.Column<int>(nullable: false),
                    NVARCHAR1000 = table.Column<string>(name: "NVARCHAR(1000)", nullable: false),
                    BizAppUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessQouteUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessQouteUsers_AspNetUsers_BizAppUserId",
                        column: x => x.BizAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessQouteUsers_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessQouteUsers_BusinessQoutes_BusinessQouteId",
                        column: x => x.BusinessQouteId,
                        principalTable: "BusinessQoutes",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52a8cba0-7ce4-4d72-9400-1447f9390281", new DateTime(2021, 4, 14, 11, 3, 35, 979, DateTimeKind.Local).AddTicks(7564), "AQAAAAEAACcQAAAAEOhmO8NvTa0//fOLZ46vt5+VJjGI/yMfOWyujAmDItyswrqKDgWZATqih5eDx0WC0A==", "8f00edf8-936b-4738-8550-4c67cccf2afa" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessQoutes_CategoryId",
                table: "BusinessQoutes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessQouteUsers_BizAppUserId",
                table: "BusinessQouteUsers",
                column: "BizAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessQouteUsers_BusinessId",
                table: "BusinessQouteUsers",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessQouteUsers_BusinessQouteId",
                table: "BusinessQouteUsers",
                column: "BusinessQouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessQouteUsers");

            migrationBuilder.DropTable(
                name: "BusinessQoutes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 14, 10, 9, 7, 656, DateTimeKind.Local).AddTicks(8319),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 14, 11, 3, 35, 971, DateTimeKind.Local).AddTicks(4641));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b33dbf3a-2e13-4648-97e7-5d691ba8c20d", new DateTime(2021, 4, 14, 10, 9, 7, 662, DateTimeKind.Local).AddTicks(4572), "AQAAAAEAACcQAAAAEIqdFxKL8E1qFzX4yKHoR/gIYN0Ba8a873RvXmZZM3jOkKCU5BhtpfSDzfXD3iOH4Q==", "a70a295f-8336-48ef-a41b-c85fa1949bea" });
        }
    }
}
