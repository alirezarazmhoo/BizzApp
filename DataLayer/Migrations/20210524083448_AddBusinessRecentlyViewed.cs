using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddBusinessRecentlyViewed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 24, 13, 4, 47, 728, DateTimeKind.Local).AddTicks(7966),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 16, 19, 10, 396, DateTimeKind.Local).AddTicks(4464));

            migrationBuilder.CreateTable(
                name: "BusinessRecentlyVieweds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BizAppUserId = table.Column<string>(nullable: true),
                    BusinessId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    BusinessId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessRecentlyVieweds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessRecentlyVieweds_AspNetUsers_BizAppUserId",
                        column: x => x.BizAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusinessRecentlyVieweds_Businesses_BusinessId1",
                        column: x => x.BusinessId1,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "163862b5-d1c4-4e02-9f35-4186659cbb51", new DateTime(2021, 5, 24, 13, 4, 47, 733, DateTimeKind.Local).AddTicks(5588), "AQAAAAEAACcQAAAAEELNKllmEQC/bEWAMMPcrYW3VnG8P5PWdfq7hyCp+TVPasqaqlXZ/WcZuiu7bTG/xA==", "2ca1e4fe-3875-4013-bf56-27d4128e2271" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessRecentlyVieweds_BizAppUserId",
                table: "BusinessRecentlyVieweds",
                column: "BizAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessRecentlyVieweds_BusinessId1",
                table: "BusinessRecentlyVieweds",
                column: "BusinessId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessRecentlyVieweds");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 16, 19, 10, 396, DateTimeKind.Local).AddTicks(4464),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 24, 13, 4, 47, 728, DateTimeKind.Local).AddTicks(7966));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa6b8749-47c4-4dc0-82bf-80a4481ba884", new DateTime(2021, 5, 12, 16, 19, 10, 405, DateTimeKind.Local).AddTicks(8001), "AQAAAAEAACcQAAAAEOHU2kHTjxd8e/jIgOVO7M3qjHI6lNQTsiyjg3wLBXOceu6toRMTQOrhOPAG7Z0oZQ==", "0c655d2e-26f4-42a9-b939-d92ea31959d4" });
        }
    }
}
