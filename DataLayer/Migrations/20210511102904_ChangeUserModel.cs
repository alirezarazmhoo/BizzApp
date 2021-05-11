using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ChangeUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MyFavoritMovie",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhenImNotYelping",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhyYouShouldReadMyReviews",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 11, 14, 59, 3, 636, DateTimeKind.Local).AddTicks(9438),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 9, 14, 25, 43, 497, DateTimeKind.Local).AddTicks(6748));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "940ce01c-74b1-4407-b713-76ad02b0e0ed", new DateTime(2021, 5, 11, 14, 59, 3, 640, DateTimeKind.Local).AddTicks(5293), "AQAAAAEAACcQAAAAEK7xebmSgCkaV1YXU/XOal6emIP/CEFygPxpfAIBIdOa9mJ8ntlzIpYpOm5zOQ4eKQ==", "f35f9b0b-7189-44b2-bd5f-8b324b30a705" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyFavoritMovie",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WhenImNotYelping",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WhyYouShouldReadMyReviews",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 9, 14, 25, 43, 497, DateTimeKind.Local).AddTicks(6748),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 11, 14, 59, 3, 636, DateTimeKind.Local).AddTicks(9438));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1459812-19c3-4c72-88a3-dbc17d516741", new DateTime(2021, 5, 9, 14, 25, 43, 503, DateTimeKind.Local).AddTicks(1862), "AQAAAAEAACcQAAAAEMHllSsW3o+FymdZP9yn6bOHAzLYR599ylmAWSGo9NmdLLelQlC/wMoUjC2bkkZGsw==", "64982f77-7996-4370-b43f-49bab39d6230" });
        }
    }
}
