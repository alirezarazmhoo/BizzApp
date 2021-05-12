using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ChangeNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelId",
                table: "Notifications",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 14, 36, 6, 970, DateTimeKind.Local).AddTicks(1858),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 11, 14, 59, 3, 636, DateTimeKind.Local).AddTicks(9438));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cea966a6-c3c0-407a-8858-7b6a94ebd6df", new DateTime(2021, 5, 12, 14, 36, 6, 974, DateTimeKind.Local).AddTicks(883), "AQAAAAEAACcQAAAAEEk5FDpzu28QMvyoq/2Gzdv4im1xkKslMGlqx+QjwbnPuTU1wf80hEGs3Qsq7Wp6vA==", "a0abb570-ad38-420b-807e-db45c09d816f" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatorUserId",
                table: "Notifications",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_CreatorUserId",
                table: "Notifications",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_CreatorUserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_CreatorUserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Notifications",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 11, 14, 59, 3, 636, DateTimeKind.Local).AddTicks(9438),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 14, 36, 6, 970, DateTimeKind.Local).AddTicks(1858));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "940ce01c-74b1-4407-b713-76ad02b0e0ed", new DateTime(2021, 5, 11, 14, 59, 3, 640, DateTimeKind.Local).AddTicks(5293), "AQAAAAEAACcQAAAAEK7xebmSgCkaV1YXU/XOal6emIP/CEFygPxpfAIBIdOa9mJ8ntlzIpYpOm5zOQ4eKQ==", "f35f9b0b-7189-44b2-bd5f-8b324b30a705" });
        }
    }
}
