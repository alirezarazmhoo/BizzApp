using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddBusinessTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 13, 15, 45, 53, 136, DateTimeKind.Local).AddTicks(9263),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 12, 11, 30, 14, 575, DateTimeKind.Local).AddTicks(5481));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3e47686-ceb7-4ab3-a7c1-34a1453bfb65", new DateTime(2021, 4, 13, 15, 45, 53, 141, DateTimeKind.Local).AddTicks(1389), "AQAAAAEAACcQAAAAEHO4ieYxNMEwvxEdckC82i5xgCdNDJCLSSluCiQSBUhQl9pCA5XErP0sVh9a3zDV/A==", "8a3cd73b-651c-42c7-97d6-dc4a4f2b2440" });

            var createSpQuery =
                @"CREATE PROCEDURE [dbo].[sp_GetAllCategoryWithParentsById] 
                (
	                @id INT
                )
                AS
                BEGIN
	                ;WITH name_tree AS 
                    (
                       SELECT id, ParentCategoryId, Name
                       FROM Categories
                       WHERE id = @id -- this is the starting point you want in your recursion

                       UNION ALL

                       SELECT C.id, C.ParentCategoryId, c.Name
                       FROM Categories c
	                       JOIN name_tree p on C.id = P.ParentCategoryId  -- this is the recursion
	                       -- Since your parent id is not NULL the recursion will happen continously.
	                       -- For that we apply the condition C.id<>C.ParentCategoryId 
		                    AND C.id<>C.ParentCategoryId 
                    ) 
                    -- Here you can insert directly to a temp table without CREATE TABLE synthax
                    SELECT *
                    FROM name_tree
                END";
            migrationBuilder.Sql(createSpQuery);

            var createSpQuery2 =
                @"ALTER PROCEDURE [dbo].[sp_GetCategoriesForAutoComplete] (
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

	                SELECT TOP 100 Id, ListName AS [Name] FROM CTE 
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
            migrationBuilder.Sql(createSpQuery2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 12, 11, 30, 14, 575, DateTimeKind.Local).AddTicks(5481),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 13, 15, 45, 53, 136, DateTimeKind.Local).AddTicks(9263));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cbd5dc21-8a47-4bd4-9669-32f797c7b8ee", new DateTime(2021, 4, 12, 11, 30, 14, 579, DateTimeKind.Local).AddTicks(2408), "AQAAAAEAACcQAAAAENHQvdGPrkA6fXxRTsp7Xub1Sav6mEYaukKvl51lPMDUCsACK7JbAsKpa7fqvURJ3A==", "1bc56af3-40c3-4295-921e-c11fc3a9b35c" });
        }
    }
}
