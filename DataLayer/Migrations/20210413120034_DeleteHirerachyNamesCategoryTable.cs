using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class DeleteHirerachyNamesCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryHierarchyNames");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 13, 16, 30, 33, 251, DateTimeKind.Local).AddTicks(3173),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 13, 15, 45, 53, 136, DateTimeKind.Local).AddTicks(9263));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d09f6bb-50c8-48ff-98b6-e9c1a702c063", new DateTime(2021, 4, 13, 16, 30, 33, 256, DateTimeKind.Local).AddTicks(7891), "AQAAAAEAACcQAAAAEIVJUl3tyeO5ypqCT6aSluj2NNjsnm05V+5oa+Ye+J5wxfYWhm2ZdW4tTnFHKXpnxw==", "34acedeb-91b9-47e3-a99b-6b4ae1a5be0c" });

            var createSpQuery =
                @"ALTER PROCEDURE [dbo].[sp_GetCategoryWithParentsById] (
					 @Id INT
				)
				AS
				BEGIN
					WITH CTE AS (
						-- This is end of the recursion: Select items with no parent
						SELECT id, ParentCategoryId, CONVERT(NVARCHAR(MAX),Name) AS ListName
						FROM Categories
						WHERE ParentCategoryId IS NULL

						UNION ALL

						-- This is the recursive part: It joins to CTE
						SELECT t.id, t.ParentCategoryId, c.ListName + ' - ' + CONVERT(NVARCHAR(MAX),t.Name) AS ListName
						FROM Categories t
								INNER JOIN CTE c ON t.ParentCategoryId = c.id
					)

					SELECT Id, ListName AS [Name]
					FROM CTE 
					WHERE CTE.Id NOT IN
					(
						SELECT c.ParentCategoryId 
						FROM	Categories AS c
						WHERE	c.ParentCategoryId IS NOT NULL
					)
					AND CTE.Id = @Id;
				END;";
            migrationBuilder.Sql(createSpQuery);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 13, 15, 45, 53, 136, DateTimeKind.Local).AddTicks(9263),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 13, 16, 30, 33, 251, DateTimeKind.Local).AddTicks(3173));

            migrationBuilder.CreateTable(
                name: "CategoryHierarchyNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                values: new object[] { "b3e47686-ceb7-4ab3-a7c1-34a1453bfb65", new DateTime(2021, 4, 13, 15, 45, 53, 141, DateTimeKind.Local).AddTicks(1389), "AQAAAAEAACcQAAAAEHO4ieYxNMEwvxEdckC82i5xgCdNDJCLSSluCiQSBUhQl9pCA5XErP0sVh9a3zDV/A==", "8a3cd73b-651c-42c7-97d6-dc4a4f2b2440" });
        }
    }
}
