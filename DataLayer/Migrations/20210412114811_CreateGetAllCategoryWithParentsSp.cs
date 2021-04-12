using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class CreateGetAllCategoryWithParentsSp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Categories_CategoryId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryFeatures_Categories_CategoryId",
                table: "CategoryFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTerms_Categories_CategoryId",
                table: "CategoryTerms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Category",
                newName: "IX_Category_ParentCategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "BusinessTimes",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 12, 16, 18, 10, 844, DateTimeKind.Local).AddTicks(9665),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 12, 14, 43, 0, 887, DateTimeKind.Local).AddTicks(2413));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e88516a-00e4-4968-ab26-a3bfef2c38b4", new DateTime(2021, 4, 12, 16, 18, 10, 849, DateTimeKind.Local).AddTicks(6789), "AQAAAAEAACcQAAAAEOZOrRY0OnljTnhKGnlhTDNmnh3m95bSzlXKdOH5gUojmu5zJtx7kx/zfA4DaWuCTA==", "98627cf5-babe-4b65-a1eb-3a66efe69ec3" });

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Category_CategoryId",
                table: "Businesses",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFeatures_Category_CategoryId",
                table: "CategoryFeatures",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTerms_Category_CategoryId",
                table: "CategoryTerms",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Category_CategoryId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryFeatures_Category_CategoryId",
                table: "CategoryFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTerms_Category_CategoryId",
                table: "CategoryTerms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "BusinessTimes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 12, 14, 43, 0, 887, DateTimeKind.Local).AddTicks(2413),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 12, 16, 18, 10, 844, DateTimeKind.Local).AddTicks(9665));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3bf35de-749d-4b9d-ae51-62242678b77e", new DateTime(2021, 4, 12, 14, 43, 0, 890, DateTimeKind.Local).AddTicks(8437), "AQAAAAEAACcQAAAAEIOkt8LZt2yfs7Aazh4+TNuY+VIWbeKU/VfxC4WqIrCaxIHdTunTkaXwQ8vV6QI/Bw==", "5d298df7-ae87-4521-8b9f-bcc402f3c864" });

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Categories_CategoryId",
                table: "Businesses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFeatures_Categories_CategoryId",
                table: "CategoryFeatures",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTerms_Categories_CategoryId",
                table: "CategoryTerms",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
