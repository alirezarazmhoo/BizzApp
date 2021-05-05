using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FixUserFavorits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorits_Businesses_BusinessId1",
                table: "UserFavorits");

            migrationBuilder.DropIndex(
                name: "IX_UserFavorits_BusinessId1",
                table: "UserFavorits");

            migrationBuilder.DropColumn(
                name: "BusinessId1",
                table: "UserFavorits");

            migrationBuilder.AlterColumn<Guid>(
                name: "BusinessId",
                table: "UserFavorits",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 5, 9, 47, 25, 2, DateTimeKind.Local).AddTicks(1390),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 1, 11, 28, 10, 341, DateTimeKind.Local).AddTicks(3799));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "655c570a-59d4-4465-9d6b-085527df379b", new DateTime(2021, 5, 5, 9, 47, 25, 6, DateTimeKind.Local).AddTicks(6464), "AQAAAAEAACcQAAAAEEL4yBTkXPUpWywflMACWL1dU48VgQ0m5WN4QUIOoZy3xxE21yCqx8A0qIb8BH0MHQ==", "cd499074-1d2c-415d-805d-2a55366a6354" });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorits_BusinessId",
                table: "UserFavorits",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorits_Businesses_BusinessId",
                table: "UserFavorits",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorits_Businesses_BusinessId",
                table: "UserFavorits");

            migrationBuilder.DropIndex(
                name: "IX_UserFavorits_BusinessId",
                table: "UserFavorits");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessId",
                table: "UserFavorits",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId1",
                table: "UserFavorits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 1, 11, 28, 10, 341, DateTimeKind.Local).AddTicks(3799),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 5, 9, 47, 25, 2, DateTimeKind.Local).AddTicks(1390));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "092f6237-dfa2-4be5-8981-dcb6589de1fe", new DateTime(2021, 5, 1, 11, 28, 10, 347, DateTimeKind.Local).AddTicks(1587), "AQAAAAEAACcQAAAAEHrqeMT+qEhPBQMXfL8z3s0TxE++/sFUlVAIj342qpCZ22vECr5UAjR8O64zJWhgsg==", "7d919749-235c-4460-8c37-ad7f9527b7da" });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorits_BusinessId1",
                table: "UserFavorits",
                column: "BusinessId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorits_Businesses_BusinessId1",
                table: "UserFavorits",
                column: "BusinessId1",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
