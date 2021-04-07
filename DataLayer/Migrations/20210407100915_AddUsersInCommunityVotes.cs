using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddUsersInCommunityVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 7, 14, 39, 15, 83, DateTimeKind.Local).AddTicks(7120),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 7, 12, 24, 12, 179, DateTimeKind.Local).AddTicks(4998));

            migrationBuilder.CreateTable(
                name: "UsersInCommunityVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BusinessFaqAnswerId = table.Column<Guid>(nullable: false),
                    VotesType = table.Column<int>(nullable: false),
                    BizAppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInCommunityVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersInCommunityVotes_AspNetUsers_BizAppUserId",
                        column: x => x.BizAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersInCommunityVotes_BusinessFaqAnswers_BusinessFaqAnswerId",
                        column: x => x.BusinessFaqAnswerId,
                        principalTable: "BusinessFaqAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b5c0946-24eb-42e2-8425-2e4b9e83cbc7", new DateTime(2021, 4, 7, 14, 39, 15, 87, DateTimeKind.Local).AddTicks(7914), "AQAAAAEAACcQAAAAEBtQJqCI25X2BsBpPmX6F6pfSlf97ur5KSQrhPDFxeknVGtHnznxkn304Ibs9PfdEQ==", "8ef68ec2-d3cd-4b0d-baec-f5b1c9e559c2" });

            migrationBuilder.CreateIndex(
                name: "IX_UsersInCommunityVotes_BizAppUserId",
                table: "UsersInCommunityVotes",
                column: "BizAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInCommunityVotes_BusinessFaqAnswerId",
                table: "UsersInCommunityVotes",
                column: "BusinessFaqAnswerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersInCommunityVotes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 7, 12, 24, 12, 179, DateTimeKind.Local).AddTicks(4998),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 7, 14, 39, 15, 83, DateTimeKind.Local).AddTicks(7120));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "793b028a-591c-4024-80a3-c43c2c252574", new DateTime(2021, 4, 7, 12, 24, 12, 184, DateTimeKind.Local).AddTicks(9899), "AQAAAAEAACcQAAAAEGFQhERk1DU8u2hjCwTNT9f50tWRzcJWua6A8bQ38eBtYQ5wU3mZJ+KzLlxSIlnZ0Q==", "8f597810-1502-4ed9-88f5-5ae49333239f" });
        }
    }
}
