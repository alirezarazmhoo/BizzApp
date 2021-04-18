using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ChangeGetAllCategoryWithParentsSp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 14, 10, 9, 7, 656, DateTimeKind.Local).AddTicks(8319),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 13, 16, 56, 30, 314, DateTimeKind.Local).AddTicks(4747));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b33dbf3a-2e13-4648-97e7-5d691ba8c20d", new DateTime(2021, 4, 14, 10, 9, 7, 662, DateTimeKind.Local).AddTicks(4572), "AQAAAAEAACcQAAAAEIqdFxKL8E1qFzX4yKHoR/gIYN0Ba8a873RvXmZZM3jOkKCU5BhtpfSDzfXD3iOH4Q==", "a70a295f-8336-48ef-a41b-c85fa1949bea" });

            var updateSpQuery =
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
                    SELECT Id, ParentCategoryId, [Name]
                    FROM name_tree
                END;";
            migrationBuilder.Sql(updateSpQuery);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 13, 16, 56, 30, 314, DateTimeKind.Local).AddTicks(4747),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 14, 10, 9, 7, 656, DateTimeKind.Local).AddTicks(8319));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "721d9e80-80c0-42ef-8373-4a51aa727058", new DateTime(2021, 4, 13, 16, 56, 30, 319, DateTimeKind.Local).AddTicks(7190), "AQAAAAEAACcQAAAAEOO/G1MU2wfnuNw1/x4yQjnCo/RsZ2O9nLzdllbi+Ow+emtWjz3TYqywvM3I7wLZJg==", "12b7e774-96f3-4b82-8fbe-459d897aff7c" });
        }
    }
}
