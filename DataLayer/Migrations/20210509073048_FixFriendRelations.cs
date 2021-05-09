using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FixFriendRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_ApplicatorId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_ReceiverId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_ApplicatorId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_ReceiverId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "ApplicatorId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Friends");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 9, 12, 0, 47, 875, DateTimeKind.Local).AddTicks(707),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 8, 10, 57, 18, 966, DateTimeKind.Local).AddTicks(2709));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f08300b3-760c-4b53-a58b-b1eb1663b2d2", new DateTime(2021, 5, 9, 12, 0, 47, 880, DateTimeKind.Local).AddTicks(3350), "AQAAAAEAACcQAAAAEGT6HOc9ErjkatSJqZb3ncWM6g0JiBSWHUZOs+Mx9yILfbp43r+BLGAKFpFAxCtEVA==", "9c616f7d-84c7-4b17-bdfe-0d799fd18252" });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_ApplicatorUserId",
                table: "Friends",
                column: "ApplicatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_ReceiverUserId",
                table: "Friends",
                column: "ReceiverUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_ApplicatorUserId",
                table: "Friends",
                column: "ApplicatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_ReceiverUserId",
                table: "Friends",
                column: "ReceiverUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_ApplicatorUserId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_ReceiverUserId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_ApplicatorUserId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_ReceiverUserId",
                table: "Friends");

            migrationBuilder.AddColumn<string>(
                name: "ApplicatorId",
                table: "Friends",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                table: "Friends",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 8, 10, 57, 18, 966, DateTimeKind.Local).AddTicks(2709),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 9, 12, 0, 47, 875, DateTimeKind.Local).AddTicks(707));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2c6d0a8-8a2c-420b-b6d7-7304464b45e1", new DateTime(2021, 5, 8, 10, 57, 18, 971, DateTimeKind.Local).AddTicks(708), "AQAAAAEAACcQAAAAEBwAOAnqbLskJb+VxvIvoceJV/rOnhYTaPdk2w9SHKIsfJTrbdAeOJeU3yfKKSnpmg==", "2b6f5f3a-48b0-4761-81ce-db79c9b6a220" });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_ApplicatorId",
                table: "Friends",
                column: "ApplicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_ReceiverId",
                table: "Friends",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_ApplicatorId",
                table: "Friends",
                column: "ApplicatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_ReceiverId",
                table: "Friends",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
