using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddUserActivityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 21, 11, 54, 0, 280, DateTimeKind.Local).AddTicks(8843),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 19, 11, 46, 24, 532, DateTimeKind.Local).AddTicks(2930));

            migrationBuilder.CreateTable(
                name: "UserActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    TableName = table.Column<int>(nullable: false),
                    TableKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActivities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5540f547-2cb9-4ef7-b627-0634eb41c763", new DateTime(2021, 4, 21, 11, 54, 0, 300, DateTimeKind.Local).AddTicks(2167), "AQAAAAEAACcQAAAAEDu6xk9t7ui1MiNnZ2X+4LnU29TeUC31CBl0TasSzqmhrsxbZ5oJMmjsQpdj6I8JeA==", "04c418af-5be4-4db7-9ccc-a8b788c179ba" });

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_UserId",
                table: "UserActivities",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserActivities");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 19, 11, 46, 24, 532, DateTimeKind.Local).AddTicks(2930),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 21, 11, 54, 0, 280, DateTimeKind.Local).AddTicks(8843));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1570c1a7-879c-4c7f-9185-ceda4e29a91f", new DateTime(2021, 4, 19, 11, 46, 24, 537, DateTimeKind.Local).AddTicks(2626), "AQAAAAEAACcQAAAAENCjhOG4peQggkGAyvRaQGsfX7J2zL54V4D5jkTvtgcbu1j6EtxZ9tFwPopJbWC8Ew==", "6e429b5d-4870-441d-b753-4d91258851f6" });
        }
    }
}
