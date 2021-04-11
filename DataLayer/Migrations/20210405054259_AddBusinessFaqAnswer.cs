using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddBusinessFaqAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "BusinessFaqs");

            migrationBuilder.AddColumn<string>(
                name: "BizAppUserId",
                table: "BusinessFaqs",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 10, 12, 58, 510, DateTimeKind.Local).AddTicks(7959),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 3, 12, 55, 17, 195, DateTimeKind.Local).AddTicks(1782));

            migrationBuilder.CreateTable(
                name: "BusinessFaqAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    BusinessFaqId = table.Column<Guid>(nullable: false),
                    HelpFullCount = table.Column<int>(nullable: false),
                    NotHelpFullCount = table.Column<int>(nullable: false),
                    StatusEnum = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    BizAppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessFaqAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessFaqAnswers_AspNetUsers_BizAppUserId",
                        column: x => x.BizAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusinessFaqAnswers_BusinessFaqs_BusinessFaqId",
                        column: x => x.BusinessFaqId,
                        principalTable: "BusinessFaqs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7273d119-4488-45bb-8c9c-6587076ed0c0", new DateTime(2021, 4, 5, 10, 12, 58, 514, DateTimeKind.Local).AddTicks(8330), "AQAAAAEAACcQAAAAEEch9xh+y3ieWLZDFszAsxERNTsQxbm5NAz7QcmtAmmRMcqSuxgtdt0/ivlqZbLC0A==", "b0f0e96c-6e94-47f3-90bb-cc75a6cc86a6" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFaqs_BizAppUserId",
                table: "BusinessFaqs",
                column: "BizAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFaqAnswers_BizAppUserId",
                table: "BusinessFaqAnswers",
                column: "BizAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFaqAnswers_BusinessFaqId",
                table: "BusinessFaqAnswers",
                column: "BusinessFaqId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessFaqs_AspNetUsers_BizAppUserId",
                table: "BusinessFaqs",
                column: "BizAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessFaqs_AspNetUsers_BizAppUserId",
                table: "BusinessFaqs");

            migrationBuilder.DropTable(
                name: "BusinessFaqAnswers");

            migrationBuilder.DropIndex(
                name: "IX_BusinessFaqs_BizAppUserId",
                table: "BusinessFaqs");

            migrationBuilder.DropColumn(
                name: "BizAppUserId",
                table: "BusinessFaqs");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "BusinessFaqs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 3, 12, 55, 17, 195, DateTimeKind.Local).AddTicks(1782),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 5, 10, 12, 58, 510, DateTimeKind.Local).AddTicks(7959));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ad2a80c-1bcb-4ed3-8603-088b91c1c9b3", new DateTime(2021, 4, 3, 12, 55, 17, 200, DateTimeKind.Local).AddTicks(5626), "AQAAAAEAACcQAAAAEDWhlIdksuM9x30aIUyzo3XjN1xs4LuOnQFmjHy6GZf8ki5FPpRqPqK6vatHUACeUg==", "8b9d2312-2ba3-4b35-a9b1-140f73c780c7" });
        }
    }
}
