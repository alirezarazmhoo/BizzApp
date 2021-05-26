using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FixBusinessRecentlyViewed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessRecentlyVieweds_Businesses_BusinessId1",
                table: "BusinessRecentlyVieweds");

            migrationBuilder.DropIndex(
                name: "IX_BusinessRecentlyVieweds_BusinessId1",
                table: "BusinessRecentlyVieweds");

            migrationBuilder.DropColumn(
                name: "BusinessId1",
                table: "BusinessRecentlyVieweds");

            migrationBuilder.AlterColumn<Guid>(
                name: "BusinessId",
                table: "BusinessRecentlyVieweds",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 24, 13, 55, 8, 996, DateTimeKind.Local).AddTicks(5299),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 24, 13, 4, 47, 728, DateTimeKind.Local).AddTicks(7966));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f6b7961-3914-44f4-a02e-34083b9d87d4", new DateTime(2021, 5, 24, 13, 55, 9, 2, DateTimeKind.Local).AddTicks(7615), "AQAAAAEAACcQAAAAEEz7TupQ+4v1+1WHFguj6vZpHFJXye5XtX5iWRHNPD/UE81m+ioN11ijxMqUXGopzg==", "f465a44e-c731-402b-a6de-89d3517b0736" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessRecentlyVieweds_BusinessId",
                table: "BusinessRecentlyVieweds",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessRecentlyVieweds_Businesses_BusinessId",
                table: "BusinessRecentlyVieweds",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessRecentlyVieweds_Businesses_BusinessId",
                table: "BusinessRecentlyVieweds");

            migrationBuilder.DropIndex(
                name: "IX_BusinessRecentlyVieweds_BusinessId",
                table: "BusinessRecentlyVieweds");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessId",
                table: "BusinessRecentlyVieweds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId1",
                table: "BusinessRecentlyVieweds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 24, 13, 4, 47, 728, DateTimeKind.Local).AddTicks(7966),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 24, 13, 55, 8, 996, DateTimeKind.Local).AddTicks(5299));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "163862b5-d1c4-4e02-9f35-4186659cbb51", new DateTime(2021, 5, 24, 13, 4, 47, 733, DateTimeKind.Local).AddTicks(5588), "AQAAAAEAACcQAAAAEELNKllmEQC/bEWAMMPcrYW3VnG8P5PWdfq7hyCp+TVPasqaqlXZ/WcZuiu7bTG/xA==", "2ca1e4fe-3875-4013-bf56-27d4128e2271" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessRecentlyVieweds_BusinessId1",
                table: "BusinessRecentlyVieweds",
                column: "BusinessId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessRecentlyVieweds_Businesses_BusinessId1",
                table: "BusinessRecentlyVieweds",
                column: "BusinessId1",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
