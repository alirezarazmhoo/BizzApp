using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ReviewEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CustomerBusinessMedias",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CustomerBusinessMedias",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LikeCount",
                table: "CustomerBusinessMedias",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07045c52-0394-456e-adfc-1afb7e57f6b0", new DateTime(2021, 3, 10, 10, 27, 55, 806, DateTimeKind.Local).AddTicks(3808), "AQAAAAEAACcQAAAAEJKOK8ygaII2N7+Yd0+OaVIFP7hDNARbF5dEb2IPXRk6OWZ17h6bmsngWHwbSpjJoQ==", "fec5bd2e-f085-4ea0-a44b-88efe0e1b498" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CustomerBusinessMedias");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "CustomerBusinessMedias");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "CustomerBusinessMedias");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ab5412f-fd8c-4cfd-8a39-a24847bae3dd", new DateTime(2021, 3, 9, 10, 38, 47, 554, DateTimeKind.Local).AddTicks(3052), "AQAAAAEAACcQAAAAEI2bvxUDbjddEs9pqjsVjY6dHAjgpyOCuixkorEztzpQrZgBKUItD40EQISs+qToBA==", "c43a5ba5-ac97-4a58-991c-e55f4a6e0d04" });
        }
    }
}
