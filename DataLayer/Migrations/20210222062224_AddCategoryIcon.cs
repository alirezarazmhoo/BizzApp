using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddCategoryIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Categories",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "20df3014-a5e0-4f94-bf15-11685f5f9a85", "20df3014-a5e0-4f94-bf15-11685f5f9a85", "Business Name", "BUSINESS NAME" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8897cbfc-a6dc-4ab6-bad5-5cddaa9b893b", new DateTime(2021, 2, 22, 9, 52, 23, 467, DateTimeKind.Local).AddTicks(9917), "AQAAAAEAACcQAAAAEFca1l5u3l2WzmFuM6wiNDzvHyed8gzjskJxpvC6S86+jKJn9eAgqARDKpe3aw3PUQ==", "1cb71d62-a79e-4405-89eb-7aa592baf75a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20df3014-a5e0-4f94-bf15-11685f5f9a85");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6fd24e66-f68b-4a1d-b9ef-42a36b31829a", new DateTime(2021, 2, 16, 10, 51, 55, 922, DateTimeKind.Local).AddTicks(9629), "AQAAAAEAACcQAAAAEHhwVgIOSSRv/KqBHqz6r4fygAAWJYIsjpNsa27faet35VAGaj8OaoHRk3Xp4y6cEw==", "143c7863-65a4-4ae3-ab8c-dfaada1f4dcc" });
        }
    }
}
