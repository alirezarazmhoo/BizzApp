using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class CustomerBusinessMediaFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBusinessMedia_AspNetUsers_BizAppUserId",
                table: "CustomerBusinessMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBusinessMedia_Businesses_BusinessId",
                table: "CustomerBusinessMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLike_AspNetUsers_BizAppUserId",
                table: "UsersInCustomerBusinessMediaLike");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLike_CustomerBusinessMedia_CustomerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersInCustomerBusinessMediaLike",
                table: "UsersInCustomerBusinessMediaLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerBusinessMedia",
                table: "CustomerBusinessMedia");

            migrationBuilder.RenameTable(
                name: "UsersInCustomerBusinessMediaLike",
                newName: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.RenameTable(
                name: "CustomerBusinessMedia",
                newName: "CustomerBusinessMedias");

            migrationBuilder.RenameIndex(
                name: "IX_UsersInCustomerBusinessMediaLike_CustomerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLikes",
                newName: "IX_UsersInCustomerBusinessMediaLikes_CustomerBusinessMediaId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersInCustomerBusinessMediaLike_BizAppUserId",
                table: "UsersInCustomerBusinessMediaLikes",
                newName: "IX_UsersInCustomerBusinessMediaLikes_BizAppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerBusinessMedia_BusinessId",
                table: "CustomerBusinessMedias",
                newName: "IX_CustomerBusinessMedias_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerBusinessMedia_BizAppUserId",
                table: "CustomerBusinessMedias",
                newName: "IX_CustomerBusinessMedias_BizAppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersInCustomerBusinessMediaLikes",
                table: "UsersInCustomerBusinessMediaLikes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerBusinessMedias",
                table: "CustomerBusinessMedias",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d22289d6-4d6e-44bd-b2c8-61cf06f05261", new DateTime(2021, 3, 6, 12, 6, 43, 522, DateTimeKind.Local).AddTicks(5537), "AQAAAAEAACcQAAAAEPhRIe56vYqHvEOnnbgoKKxAOwPkCOcsGIGrW7e5uX0qixrCGjxQj+a1F+V6LWv80g==", "ae7ed8e9-a137-4511-8b11-89af95736c31" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBusinessMedias_AspNetUsers_BizAppUserId",
                table: "CustomerBusinessMedias",
                column: "BizAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBusinessMedias_Businesses_BusinessId",
                table: "CustomerBusinessMedias",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLikes_AspNetUsers_BizAppUserId",
                table: "UsersInCustomerBusinessMediaLikes",
                column: "BizAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLikes_CustomerBusinessMedias_CustomerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLikes",
                column: "CustomerBusinessMediaId",
                principalTable: "CustomerBusinessMedias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBusinessMedias_AspNetUsers_BizAppUserId",
                table: "CustomerBusinessMedias");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBusinessMedias_Businesses_BusinessId",
                table: "CustomerBusinessMedias");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLikes_AspNetUsers_BizAppUserId",
                table: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLikes_CustomerBusinessMedias_CustomerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersInCustomerBusinessMediaLikes",
                table: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerBusinessMedias",
                table: "CustomerBusinessMedias");

            migrationBuilder.RenameTable(
                name: "UsersInCustomerBusinessMediaLikes",
                newName: "UsersInCustomerBusinessMediaLike");

            migrationBuilder.RenameTable(
                name: "CustomerBusinessMedias",
                newName: "CustomerBusinessMedia");

            migrationBuilder.RenameIndex(
                name: "IX_UsersInCustomerBusinessMediaLikes_CustomerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLike",
                newName: "IX_UsersInCustomerBusinessMediaLike_CustomerBusinessMediaId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersInCustomerBusinessMediaLikes_BizAppUserId",
                table: "UsersInCustomerBusinessMediaLike",
                newName: "IX_UsersInCustomerBusinessMediaLike_BizAppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerBusinessMedias_BusinessId",
                table: "CustomerBusinessMedia",
                newName: "IX_CustomerBusinessMedia_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerBusinessMedias_BizAppUserId",
                table: "CustomerBusinessMedia",
                newName: "IX_CustomerBusinessMedia_BizAppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersInCustomerBusinessMediaLike",
                table: "UsersInCustomerBusinessMediaLike",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerBusinessMedia",
                table: "CustomerBusinessMedia",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "052432ce-911f-4abc-a412-4a58f6acbd9b", new DateTime(2021, 3, 6, 10, 59, 23, 113, DateTimeKind.Local).AddTicks(9365), "AQAAAAEAACcQAAAAEJ1lbjOhvLnMGLHr2xZlcq12JNepPUp8NoINIRrCt/964A8RQT3FTCiQYpNIi3OPDA==", "fae38092-b76c-4188-abf5-b0353157b5b2" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBusinessMedia_AspNetUsers_BizAppUserId",
                table: "CustomerBusinessMedia",
                column: "BizAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBusinessMedia_Businesses_BusinessId",
                table: "CustomerBusinessMedia",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLike_AspNetUsers_BizAppUserId",
                table: "UsersInCustomerBusinessMediaLike",
                column: "BizAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLike_CustomerBusinessMedia_CustomerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLike",
                column: "CustomerBusinessMediaId",
                principalTable: "CustomerBusinessMedia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
