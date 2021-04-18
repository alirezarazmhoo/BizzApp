using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddUsersInCustomerMediaPicturesVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 12, 9, 52, 30, 42, DateTimeKind.Local).AddTicks(5321),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 11, 14, 56, 6, 786, DateTimeKind.Local).AddTicks(5578));

            migrationBuilder.CreateTable(
                name: "UsersInCustomerMediaPicturesVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerBusinessMediaPicturesId = table.Column<Guid>(nullable: false),
                    BizAppUserId = table.Column<string>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersInCustomerMediaPicturesVotes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 11, 14, 56, 6, 786, DateTimeKind.Local).AddTicks(5578),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 12, 9, 52, 30, 42, DateTimeKind.Local).AddTicks(5321));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7f9023b-b39f-4e9b-bf78-46c00a35b278", new DateTime(2021, 4, 11, 14, 56, 6, 790, DateTimeKind.Local).AddTicks(894), "AQAAAAEAACcQAAAAEF68cK2losfOKTh2rbrNTd5pzvbuPIljlCaD8+isbr6XArVKHCOEcFL7UkoRYtKBAg==", "966b2904-04fc-48ad-8fd8-d9a71c4622ae" });
        }
    }
}
