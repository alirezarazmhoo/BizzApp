using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddFriendAndNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 1, 11, 28, 10, 341, DateTimeKind.Local).AddTicks(3799),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 25, 11, 35, 52, 128, DateTimeKind.Local).AddTicks(8350));

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicatorUserId = table.Column<string>(maxLength: 450, nullable: false),
                    ReceiverUserId = table.Column<string>(maxLength: 450, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ApplicatorId = table.Column<string>(nullable: true),
                    ReceiverId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friends_AspNetUsers_ApplicatorId",
                        column: x => x.ApplicatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friends_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Model = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
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
                values: new object[] { "092f6237-dfa2-4be5-8981-dcb6589de1fe", new DateTime(2021, 5, 1, 11, 28, 10, 347, DateTimeKind.Local).AddTicks(1587), "AQAAAAEAACcQAAAAEHrqeMT+qEhPBQMXfL8z3s0TxE++/sFUlVAIj342qpCZ22vECr5UAjR8O64zJWhgsg==", "7d919749-235c-4460-8c37-ad7f9527b7da" });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_ApplicatorId",
                table: "Friends",
                column: "ApplicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_ReceiverId",
                table: "Friends",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 25, 11, 35, 52, 128, DateTimeKind.Local).AddTicks(8350),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 1, 11, 28, 10, 341, DateTimeKind.Local).AddTicks(3799));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d3a7867-3198-47b5-9533-4f7790a49593", new DateTime(2021, 4, 25, 11, 35, 52, 134, DateTimeKind.Local).AddTicks(7065), "AQAAAAEAACcQAAAAEELYvDI8DC36UBolLGWDT8ixlbNO6llzA/04IvrkQESCRLt+Zz/8y6HYrqUoWKpW5w==", "38c8d230-12b0-4ec9-911b-389b7c94689d" });
        }
    }
}
