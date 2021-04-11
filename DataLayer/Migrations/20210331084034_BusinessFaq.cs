using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class BusinessFaq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BoldFeature",
                table: "Businesses",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 31, 13, 10, 33, 524, DateTimeKind.Local).AddTicks(3131),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 16, 14, 24, 18, 858, DateTimeKind.Local).AddTicks(4634));

            migrationBuilder.CreateTable(
                name: "BusinessFaqs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessFaqs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessFaqs_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14a0d37e-b30b-4bd0-91c6-a51e11237389", new DateTime(2021, 3, 31, 13, 10, 33, 529, DateTimeKind.Local).AddTicks(694), "AQAAAAEAACcQAAAAEIfWZma62Rv+INlIcztT0DCmh8krEAwTkBJO5UXI6icjSAGiqezXuu7+J91LpYQRPg==", "04082825-6618-4ecf-b806-d2278fd41384" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFaqs_BusinessId",
                table: "BusinessFaqs",
                column: "BusinessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessFaqs");

            migrationBuilder.DropColumn(
                name: "BoldFeature",
                table: "Businesses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 16, 14, 24, 18, 858, DateTimeKind.Local).AddTicks(4634),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 31, 13, 10, 33, 524, DateTimeKind.Local).AddTicks(3131));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f0bc4b1-c894-4c5b-aa0c-5f50135cf5d5", new DateTime(2021, 3, 16, 14, 24, 18, 862, DateTimeKind.Local).AddTicks(3543), "AQAAAAEAACcQAAAAELvi2YmCv3ozojjAmoJwrVT7J+/erzzbtJDGKHjzFINFxN6jmjRi3d+M7MFBfWBCYA==", "31feb179-3c71-4939-ac2e-ba365ad81185" });
        }
    }
}
