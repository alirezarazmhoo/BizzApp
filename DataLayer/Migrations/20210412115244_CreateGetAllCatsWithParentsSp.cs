using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class CreateGetAllCatsWithParentsSp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a990538-360f-4768-9bd1-a3b2cd26959c", new DateTime(2021, 4, 12, 16, 22, 43, 517, DateTimeKind.Local).AddTicks(9842), "AQAAAAEAACcQAAAAEFLRpWq8u96K2J/I74PPd9Vp0hMYwVOKfbDn8PtOmUsYNxGZKE6QwCkCkbfpa04QJg==", "ba6b9788-77b4-4710-b7c2-62a3f633a230" });

			var createSpQuery =
                @"CREATE PROCEDURE [dbo].[sp_GetAllCategoryWithParentsById] 
                (
	                @Id INT
                )
                AS
                BEGIN
	                ;WITH name_tree AS 
                (
                   SELECT id, ParentCategoryId, Name
                   FROM Categories
                   WHERE id = 1044 -- this is the starting point you want in your recursion

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

		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e88516a-00e4-4968-ab26-a3bfef2c38b4", new DateTime(2021, 4, 12, 16, 18, 10, 849, DateTimeKind.Local).AddTicks(6789), "AQAAAAEAACcQAAAAEOZOrRY0OnljTnhKGnlhTDNmnh3m95bSzlXKdOH5gUojmu5zJtx7kx/zfA4DaWuCTA==", "98627cf5-babe-4b65-a1eb-3a66efe69ec3" });

			migrationBuilder.Sql("DROP PROCEDURE dbo.sp_GetCategoryWithParentsById");
		}
	}
}
