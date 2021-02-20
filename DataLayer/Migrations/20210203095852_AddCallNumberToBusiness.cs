using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddCallNumberToBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CallNumber",
                table: "Businesses",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "CategoryHierarchyNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryHierarchyNames", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "302aea2f-5a31-4e8a-ab86-d11da099965d", new DateTime(2021, 2, 3, 13, 28, 51, 515, DateTimeKind.Local).AddTicks(6833), "AQAAAAEAACcQAAAAEJRicvqur3AvWK3D6tFCuG6VmvKLyIgSX4Cm9k2JUL/M+F29mjeSja+N+ETl8OCthA==", "27e004de-6804-48c9-8642-f3d2cae68221" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryHierarchyNames");

            migrationBuilder.DropColumn(
                name: "CallNumber",
                table: "Businesses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "896d0a0e-7b89-44b9-8847-261674f0a91b", new DateTime(2021, 2, 2, 14, 28, 8, 369, DateTimeKind.Local).AddTicks(8438), "AQAAAAEAACcQAAAAEK3IzLRRGePm0SNORNarg07EELA33E19uu9FajLdSqn2zM4UEnEwX3fKlxDgHocunw==", "5308bc78-0cb8-413d-9b86-78b47d7567f3" });
        }
    }
}
