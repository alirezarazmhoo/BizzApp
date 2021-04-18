using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ChangeSpCammelCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 13, 16, 56, 30, 314, DateTimeKind.Local).AddTicks(4747),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 13, 16, 30, 33, 251, DateTimeKind.Local).AddTicks(3173));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "721d9e80-80c0-42ef-8373-4a51aa727058", new DateTime(2021, 4, 13, 16, 56, 30, 319, DateTimeKind.Local).AddTicks(7190), "AQAAAAEAACcQAAAAEOO/G1MU2wfnuNw1/x4yQjnCo/RsZ2O9nLzdllbi+Ow+emtWjz3TYqywvM3I7wLZJg==", "12b7e774-96f3-4b82-8fbe-459d897aff7c" });

            var createSpQuery =
                @"ALTER PROCEDURE [dbo].[sp_GetAllCategoryWithParentsById] 
                (
	                @id INT
                )
                AS
                BEGIN
	                ;WITH name_tree AS 
                    (
                       SELECT Id, ParentCategoryId, Name
                       FROM Categories
                       WHERE Id = @id -- this is the starting point you want in your recursion

                       UNION ALL

                       SELECT C.Id, C.ParentCategoryId, c.Name
                       FROM Categories c
	                       JOIN name_tree p on C.Id = P.ParentCategoryId  -- this is the recursion
	                       -- Since your parent id is not NULL the recursion will happen continously.
	                       -- For that we apply the condition C.id<>C.ParentCategoryId 
		                    AND C.Id <> C.ParentCategoryId 
                    ) 
                    -- Here you can insert directly to a temp table without CREATE TABLE synthax
                    SELECT *
                    FROM name_tree
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
                defaultValue: new DateTime(2021, 4, 13, 16, 30, 33, 251, DateTimeKind.Local).AddTicks(3173),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 13, 16, 56, 30, 314, DateTimeKind.Local).AddTicks(4747));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d09f6bb-50c8-48ff-98b6-e9c1a702c063", new DateTime(2021, 4, 13, 16, 30, 33, 256, DateTimeKind.Local).AddTicks(7891), "AQAAAAEAACcQAAAAEIVJUl3tyeO5ypqCT6aSluj2NNjsnm05V+5oa+Ye+J5wxfYWhm2ZdW4tTnFHKXpnxw==", "34acedeb-91b9-47e3-a99b-6b4ae1a5be0c" });
        }
    }
}
