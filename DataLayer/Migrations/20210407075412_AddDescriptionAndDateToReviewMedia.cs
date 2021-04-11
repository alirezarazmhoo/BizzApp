using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddDescriptionAndDateToReviewMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ReviewMedias",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ReviewMedias",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 7, 12, 24, 12, 179, DateTimeKind.Local).AddTicks(4998),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 6, 10, 37, 30, 21, DateTimeKind.Local).AddTicks(1850));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "793b028a-591c-4024-80a3-c43c2c252574", new DateTime(2021, 4, 7, 12, 24, 12, 184, DateTimeKind.Local).AddTicks(9899), "AQAAAAEAACcQAAAAEGFQhERk1DU8u2hjCwTNT9f50tWRzcJWua6A8bQ38eBtYQ5wU3mZJ+KzLlxSIlnZ0Q==", "8f597810-1502-4ed9-88f5-5ae49333239f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ReviewMedias");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ReviewMedias");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 6, 10, 37, 30, 21, DateTimeKind.Local).AddTicks(1850),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 7, 12, 24, 12, 179, DateTimeKind.Local).AddTicks(4998));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f094efd-4d6c-4513-9a31-062a81494817", new DateTime(2021, 4, 6, 10, 37, 30, 26, DateTimeKind.Local).AddTicks(7095), "AQAAAAEAACcQAAAAEGAgo8P38vMeCV0CiUXKXu3kFXptPJfi0iqsGw9c0rJ6bHX8KQfxbAhl8meD/KUkhw==", "5a869504-169d-4a68-9e0c-e600f2807bad" });
        }
    }
}
