using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddOwnerBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Businesses",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6fd24e66-f68b-4a1d-b9ef-42a36b31829a", new DateTime(2021, 2, 16, 10, 51, 55, 922, DateTimeKind.Local).AddTicks(9629), "AQAAAAEAACcQAAAAEHhwVgIOSSRv/KqBHqz6r4fygAAWJYIsjpNsa27faet35VAGaj8OaoHRk3Xp4y6cEw==", "143c7863-65a4-4ae3-ab8c-dfaada1f4dcc" });

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_OwnerId",
                table: "Businesses",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_AspNetUsers_OwnerId",
                table: "Businesses",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_AspNetUsers_OwnerId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_OwnerId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Businesses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18a00df3-44eb-42f3-830f-be2fcb4fa36b", new DateTime(2021, 2, 15, 12, 21, 39, 182, DateTimeKind.Local).AddTicks(875), "AQAAAAEAACcQAAAAEOE1mEXu0ThskdD+I6CQ3OloJM4zZ1RRKilf505xc2Xsxe9LxT6JBpMC3zMd1TmKtw==", "2d01d592-1002-49cc-a379-ceac28f2bf27" });
        }
    }
}
