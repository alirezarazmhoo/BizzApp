using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ApplicationUserMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserMedias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    BizAppUserId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IsMainImage = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserMedias_AspNetUsers_BizAppUserId",
                        column: x => x.BizAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00b214d4-81ba-4a0a-8638-e3c7b897d6d7", new DateTime(2021, 2, 28, 10, 18, 0, 496, DateTimeKind.Local).AddTicks(3309), "AQAAAAEAACcQAAAAEMsi1zKiF0cdb35bk+hgQ9ovLRuXuflam6J+5CUTKxP8osK7N42Ef48YsM1qa1n6dQ==", "b65f50d7-3000-4b07-9239-6b5d43b8f1f2" });

            migrationBuilder.InsertData(
                table: "Sliders",
                columns: new[] { "Id", "Image", "Status", "Text", "Title" },
                values: new object[,]
                {
                    { 1, "/Upload/Slider/Files/1.jpg", 1, "Text1", "Title1" },
                    { 2, "/Upload/Slider/Files/2.jpg", 1, "Text2", "Title2" },
                    { 3, "/Upload/Slider/Files/3.jpg", 1, "Text3", "Title3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserMedias_BizAppUserId",
                table: "ApplicationUserMedias",
                column: "BizAppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserMedias");

            migrationBuilder.DeleteData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90bf4904-7274-4554-8f15-cd8113da6f01", new DateTime(2021, 2, 23, 11, 51, 56, 185, DateTimeKind.Local).AddTicks(234), "AQAAAAEAACcQAAAAEHPe7NUv5a/xgFty1NWsQ0ghyByTEL3HIEDu4n0Ul6+Nh7AJUJ2UWnzpb9ofy1FIOQ==", "cda06fcd-1e59-427c-8966-1b1584c73f3a" });
        }
    }
}
