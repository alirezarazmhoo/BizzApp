using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddGetCategoriesForAuthocompleteSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "896d0a0e-7b89-44b9-8847-261674f0a91b", new DateTime(2021, 2, 2, 14, 28, 8, 369, DateTimeKind.Local).AddTicks(8438), "AQAAAAEAACcQAAAAEK3IzLRRGePm0SNORNarg07EELA33E19uu9FajLdSqn2zM4UEnEwX3fKlxDgHocunw==", "5308bc78-0cb8-413d-9b86-78b47d7567f3" });

            var createSpSql = 
				@"CREATE PROCEDURE dbo.sp_GetCategoriesForAutoComplete (
					 @SERACHKEY NVARCHAR(MAX) = NULL	
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

					SELECT Id, ListName FROM CTE 
					WHERE CTE.Id NOT IN
					(
						SELECT c.ParentCategoryId 
						FROM	Categories AS c
						WHERE	c.ParentCategoryId IS NOT NULL
					)
					AND (ListName LIKE N'%' + @SERACHKEY  + '%'
									OR (@SERACHKEY IS NULL AND 1 = 1)
						)
					ORDER BY Id;
				END";
			migrationBuilder.Sql(createSpSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ce4c779-565c-41b5-8df5-7000ef1f4d6d", new DateTime(2021, 2, 1, 14, 1, 42, 315, DateTimeKind.Local).AddTicks(1601), "AQAAAAEAACcQAAAAEEHZ23C3keU5hi35kjaJWaK/EHwcTotAhe07ns4MKZUONpPLsuQEUTQsTDio83Kb4w==", "bea8269e-780c-494a-96a7-655f6016f187" });

			migrationBuilder.Sql("DROP PROCEDURE dbo.sp_GetCategoriesForAutoComplete");

        }
    }
}
