using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
	public partial class RemoveCityProvinceFromBusinessAndUpdateSp : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Businesses_Cities_CityId",
				table: "Businesses");

			migrationBuilder.DropForeignKey(
				name: "FK_Businesses_Provinces_ProvinceId",
				table: "Businesses");

			migrationBuilder.DropIndex(
				name: "IX_Businesses_CityId",
				table: "Businesses");

			migrationBuilder.DropIndex(
				name: "IX_Businesses_ProvinceId",
				table: "Businesses");

			migrationBuilder.DropColumn(
				name: "CityId",
				table: "Businesses");

			migrationBuilder.DropColumn(
				name: "ProvinceId",
				table: "Businesses");

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
				columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
				values: new object[] { "66df90b4-a89d-4609-81c2-37461837aec8", new DateTime(2021, 2, 6, 11, 21, 10, 679, DateTimeKind.Local).AddTicks(1535), "AQAAAAEAACcQAAAAEFxvWSNBcNI7ty9gS+T60culp+Edn1z4JynIeZE7SJ06TcUNUQTCgN0urOdp1aDGmQ==", "f232ba28-8024-452c-bbc5-062e0967d7f2" });

			var alterSp =
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

					SELECT TOP 100 Id, ListName FROM CTE 
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

			var createSpQuery2 =
				@"CREATE PROCEDURE [dbo].[sp_GetCategoryWithParentsById] (
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

					SELECT Id, ListName FROM CTE 
					WHERE CTE.Id NOT IN
					(
						SELECT c.ParentCategoryId 
						FROM	Categories AS c
						WHERE	c.ParentCategoryId IS NOT NULL
					)
					AND CTE.Id = @Id;
				END";

			migrationBuilder.Sql(alterSp);
			migrationBuilder.Sql(createSpQuery2);

		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "CityId",
				table: "Businesses",
				type: "int",
				nullable: true);

			migrationBuilder.AddColumn<int>(
				name: "ProvinceId",
				table: "Businesses",
				type: "int",
				nullable: true);

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
				columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
				values: new object[] { "302aea2f-5a31-4e8a-ab86-d11da099965d", new DateTime(2021, 2, 3, 13, 28, 51, 515, DateTimeKind.Local).AddTicks(6833), "AQAAAAEAACcQAAAAEJRicvqur3AvWK3D6tFCuG6VmvKLyIgSX4Cm9k2JUL/M+F29mjeSja+N+ETl8OCthA==", "27e004de-6804-48c9-8642-f3d2cae68221" });

			migrationBuilder.CreateIndex(
				name: "IX_Businesses_CityId",
				table: "Businesses",
				column: "CityId");

			migrationBuilder.CreateIndex(
				name: "IX_Businesses_ProvinceId",
				table: "Businesses",
				column: "ProvinceId");

			migrationBuilder.AddForeignKey(
				name: "FK_Businesses_Cities_CityId",
				table: "Businesses",
				column: "CityId",
				principalTable: "Cities",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Businesses_Provinces_ProvinceId",
				table: "Businesses",
				column: "ProvinceId",
				principalTable: "Provinces",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.Sql("DROP PROCEDURE dbo.sp_GetCategoryWithParentsById");
		}
	}
}
