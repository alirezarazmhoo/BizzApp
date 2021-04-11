using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddUsersInReviewVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 11, 14, 56, 6, 786, DateTimeKind.Local).AddTicks(5578),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 7, 14, 39, 15, 83, DateTimeKind.Local).AddTicks(7120));

            migrationBuilder.CreateTable(
                name: "UsersInReviewVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReviewId = table.Column<Guid>(nullable: false),
                    VotesType = table.Column<int>(nullable: false),
                    BizAppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInReviewVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersInReviewVotes_AspNetUsers_BizAppUserId",
                        column: x => x.BizAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersInReviewVotes_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7f9023b-b39f-4e9b-bf78-46c00a35b278", new DateTime(2021, 4, 11, 14, 56, 6, 790, DateTimeKind.Local).AddTicks(894), "AQAAAAEAACcQAAAAEF68cK2losfOKTh2rbrNTd5pzvbuPIljlCaD8+isbr6XArVKHCOEcFL7UkoRYtKBAg==", "966b2904-04fc-48ad-8fd8-d9a71c4622ae" });

            migrationBuilder.CreateIndex(
                name: "IX_UsersInReviewVotes_BizAppUserId",
                table: "UsersInReviewVotes",
                column: "BizAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInReviewVotes_ReviewId",
                table: "UsersInReviewVotes",
                column: "ReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersInReviewVotes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 7, 14, 39, 15, 83, DateTimeKind.Local).AddTicks(7120),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 11, 14, 56, 6, 786, DateTimeKind.Local).AddTicks(5578));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b5c0946-24eb-42e2-8425-2e4b9e83cbc7", new DateTime(2021, 4, 7, 14, 39, 15, 87, DateTimeKind.Local).AddTicks(7914), "AQAAAAEAACcQAAAAEBtQJqCI25X2BsBpPmX6F6pfSlf97ur5KSQrhPDFxeknVGtHnznxkn304Ibs9PfdEQ==", "8ef68ec2-d3cd-4b0d-baec-f5b1c9e559c2" });
        }
    }
}
