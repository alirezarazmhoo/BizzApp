using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddCategoryTermWithTrueName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTerm_Categories_CategoryId",
                table: "CategoryTerm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryTerm",
                table: "CategoryTerm");

            migrationBuilder.RenameTable(
                name: "CategoryTerm",
                newName: "CategoryTerms");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryTerm_CategoryId",
                table: "CategoryTerms",
                newName: "IX_CategoryTerms_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryTerms",
                table: "CategoryTerms",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f89f478a-1f71-4360-a8d5-5a955dc65c5b", new DateTime(2021, 2, 24, 10, 19, 57, 81, DateTimeKind.Local).AddTicks(3094), "AQAAAAEAACcQAAAAEDmWixj1f0pmG6EdTJvl1RDqd5Iq8+GSeF8IB796Bd6+jswzIu5R0WfhAAqzswsKqQ==", "b0ba5969-201b-4766-90ca-0ef9050a8879" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTerms_Categories_CategoryId",
                table: "CategoryTerms",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTerms_Categories_CategoryId",
                table: "CategoryTerms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryTerms",
                table: "CategoryTerms");

            migrationBuilder.RenameTable(
                name: "CategoryTerms",
                newName: "CategoryTerm");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryTerms_CategoryId",
                table: "CategoryTerm",
                newName: "IX_CategoryTerm_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryTerm",
                table: "CategoryTerm",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "450bc192-ced7-42e8-9b7f-9f232aab9a8d", new DateTime(2021, 2, 23, 14, 33, 0, 490, DateTimeKind.Local).AddTicks(2680), "AQAAAAEAACcQAAAAEP0CKMv3T33p67tgrKuna7P09vi4twNQrQ9YHnoj2HzdzyMgaXVyiJ1F/PhhhH0aoA==", "32db1469-9881-4214-9098-35934e425b8b" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTerm_Categories_CategoryId",
                table: "CategoryTerm",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
