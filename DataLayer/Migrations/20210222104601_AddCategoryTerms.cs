using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddCategoryTerms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryTerm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    Key = table.Column<string>(type: "varchar(50)", nullable: false),
                    Value = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTerm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryTerm_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0d6240d-1a7b-461e-84e3-b1ea9ad6d4ba", new DateTime(2021, 2, 22, 14, 16, 1, 264, DateTimeKind.Local).AddTicks(8716), "AQAAAAEAACcQAAAAENJwg+XYLWSRmnyeS+756/1jsHgq/JrqY7OebUTl1bSoD3ZWL1DSxv+I0n9aXPWGrQ==", "4cbdb240-9f74-4b5c-bda1-52ff6aec1b21" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTerm_CategoryId",
                table: "CategoryTerm",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryTerm");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Categories",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8897cbfc-a6dc-4ab6-bad5-5cddaa9b893b", new DateTime(2021, 2, 22, 9, 52, 23, 467, DateTimeKind.Local).AddTicks(9917), "AQAAAAEAACcQAAAAEFca1l5u3l2WzmFuM6wiNDzvHyed8gzjskJxpvC6S86+jKJn9eAgqARDKpe3aw3PUQ==", "1cb71d62-a79e-4405-89eb-7aa592baf75a" });
        }
    }
}
