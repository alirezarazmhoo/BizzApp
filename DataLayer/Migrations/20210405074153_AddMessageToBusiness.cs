using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddMessageToBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 12, 11, 52, 213, DateTimeKind.Local).AddTicks(4739),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 5, 10, 12, 58, 510, DateTimeKind.Local).AddTicks(7959));

            migrationBuilder.CreateTable(
                name: "MessageToBusinesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageToBusinesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageToBusinesses_Businesses_BusinessId",
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
                values: new object[] { "e706a261-6cd5-4526-acf2-2e1b3e485adc", new DateTime(2021, 4, 5, 12, 11, 52, 218, DateTimeKind.Local).AddTicks(6532), "AQAAAAEAACcQAAAAEHWVBrBSJT1pU2ZrroEpSClR/ExoNCC7ObjwIffdSCpyZ74CUjjA89BKVh3mpRqieg==", "fdd136f0-7482-4192-ac9d-dc92fcf5ecc1" });

            migrationBuilder.CreateIndex(
                name: "IX_MessageToBusinesses_BusinessId",
                table: "MessageToBusinesses",
                column: "BusinessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageToBusinesses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 10, 12, 58, 510, DateTimeKind.Local).AddTicks(7959),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 5, 12, 11, 52, 213, DateTimeKind.Local).AddTicks(4739));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7273d119-4488-45bb-8c9c-6587076ed0c0", new DateTime(2021, 4, 5, 10, 12, 58, 514, DateTimeKind.Local).AddTicks(8330), "AQAAAAEAACcQAAAAEEch9xh+y3ieWLZDFszAsxERNTsQxbm5NAz7QcmtAmmRMcqSuxgtdt0/ivlqZbLC0A==", "b0f0e96c-6e94-47f3-90bb-cc75a6cc86a6" });
        }
    }
}
