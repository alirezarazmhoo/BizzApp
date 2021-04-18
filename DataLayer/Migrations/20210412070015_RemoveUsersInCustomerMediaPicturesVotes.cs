using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class RemoveUsersInCustomerMediaPicturesVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersInCustomerMediaPicturesVotes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 12, 11, 30, 14, 575, DateTimeKind.Local).AddTicks(5481),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 12, 9, 52, 30, 42, DateTimeKind.Local).AddTicks(5321));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cbd5dc21-8a47-4bd4-9669-32f797c7b8ee", new DateTime(2021, 4, 12, 11, 30, 14, 579, DateTimeKind.Local).AddTicks(2408), "AQAAAAEAACcQAAAAENHQvdGPrkA6fXxRTsp7Xub1Sav6mEYaukKvl51lPMDUCsACK7JbAsKpa7fqvURJ3A==", "1bc56af3-40c3-4295-921e-c11fc3a9b35c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 12, 9, 52, 30, 42, DateTimeKind.Local).AddTicks(5321),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 12, 11, 30, 14, 575, DateTimeKind.Local).AddTicks(5481));

            migrationBuilder.CreateTable(
                name: "UsersInCustomerMediaPicturesVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BizAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CustomerBusinessMediaPicturesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInCustomerMediaPicturesVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersInCustomerMediaPicturesVotes_AspNetUsers_BizAppUserId",
                        column: x => x.BizAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersInCustomerMediaPicturesVotes_CustomerBusinessMediaPictures_CustomerBusinessMediaPicturesId",
                        column: x => x.CustomerBusinessMediaPicturesId,
                        principalTable: "CustomerBusinessMediaPictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22714bd6-bbe1-4d75-9f0c-ffadbcf9c88d", new DateTime(2021, 4, 12, 9, 52, 30, 46, DateTimeKind.Local).AddTicks(1794), "AQAAAAEAACcQAAAAELG+spCHis2rJFhoEXbnQ/DCrwNU4y5oRL1UgPRvN7V2EQ/e2nv7QX3MUYO84e6D4g==", "16528aa4-17d8-4695-90b9-c7e3437556a8" });

            migrationBuilder.CreateIndex(
                name: "IX_UsersInCustomerMediaPicturesVotes_BizAppUserId",
                table: "UsersInCustomerMediaPicturesVotes",
                column: "BizAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInCustomerMediaPicturesVotes_CustomerBusinessMediaPicturesId",
                table: "UsersInCustomerMediaPicturesVotes",
                column: "CustomerBusinessMediaPicturesId");
        }
    }
}
