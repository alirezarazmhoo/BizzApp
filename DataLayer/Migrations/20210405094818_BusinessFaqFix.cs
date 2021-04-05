using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class BusinessFaqFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessFaqs_AspNetUsers_BizAppUserId",
                table: "BusinessFaqs");

            migrationBuilder.DropIndex(
                name: "IX_BusinessFaqs_BizAppUserId",
                table: "BusinessFaqs");

            migrationBuilder.AlterColumn<Guid>(
                name: "BizAppUserId",
                table: "BusinessFaqs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BizAppUserId1",
                table: "BusinessFaqs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusEnum",
                table: "BusinessFaqs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 14, 18, 17, 622, DateTimeKind.Local).AddTicks(3541),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 5, 12, 15, 40, 944, DateTimeKind.Local).AddTicks(8149));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f42bc47a-4992-4512-a9a4-13d7fae8ac5c", new DateTime(2021, 4, 5, 14, 18, 17, 625, DateTimeKind.Local).AddTicks(8251), "AQAAAAEAACcQAAAAEOuZ3VrIRMDNdQrzK7NI1H0nN0nY+geDSBQl1LNtD3mcU2JxVtgpkE8mZejyyHqe/w==", "263e7d48-887e-45b1-8f6a-9afc607fb741" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFaqs_BizAppUserId1",
                table: "BusinessFaqs",
                column: "BizAppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessFaqs_AspNetUsers_BizAppUserId1",
                table: "BusinessFaqs",
                column: "BizAppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessFaqs_AspNetUsers_BizAppUserId1",
                table: "BusinessFaqs");

            migrationBuilder.DropIndex(
                name: "IX_BusinessFaqs_BizAppUserId1",
                table: "BusinessFaqs");

            migrationBuilder.DropColumn(
                name: "BizAppUserId1",
                table: "BusinessFaqs");

            migrationBuilder.DropColumn(
                name: "StatusEnum",
                table: "BusinessFaqs");

            migrationBuilder.AlterColumn<string>(
                name: "BizAppUserId",
                table: "BusinessFaqs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 12, 15, 40, 944, DateTimeKind.Local).AddTicks(8149),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 5, 14, 18, 17, 622, DateTimeKind.Local).AddTicks(3541));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0aa3c0aa-09ff-42f1-81dd-2ea4e6333c6e", new DateTime(2021, 4, 5, 12, 15, 40, 951, DateTimeKind.Local).AddTicks(8335), "AQAAAAEAACcQAAAAEInWkCjwIB3M53e28aGpEJ8oMt3Oih+gVtod5ET00HovcSR+yQF0NUJDe6boVheSRw==", "5c51b5dd-211a-4452-b30e-cb21ebacece9" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFaqs_BizAppUserId",
                table: "BusinessFaqs",
                column: "BizAppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessFaqs_AspNetUsers_BizAppUserId",
                table: "BusinessFaqs",
                column: "BizAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
