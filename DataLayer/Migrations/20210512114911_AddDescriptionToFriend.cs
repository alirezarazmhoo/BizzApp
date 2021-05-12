using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddDescriptionToFriend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Friends",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 16, 19, 10, 396, DateTimeKind.Local).AddTicks(4464),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 40, 8, 908, DateTimeKind.Local).AddTicks(2301));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa6b8749-47c4-4dc0-82bf-80a4481ba884", new DateTime(2021, 5, 12, 16, 19, 10, 405, DateTimeKind.Local).AddTicks(8001), "AQAAAAEAACcQAAAAEOHU2kHTjxd8e/jIgOVO7M3qjHI6lNQTsiyjg3wLBXOceu6toRMTQOrhOPAG7Z0oZQ==", "0c655d2e-26f4-42a9-b939-d92ea31959d4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Friends");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 40, 8, 908, DateTimeKind.Local).AddTicks(2301),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 16, 19, 10, 396, DateTimeKind.Local).AddTicks(4464));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43a9d42f-7399-400c-bec6-6bcc95915af6", new DateTime(2021, 5, 12, 15, 40, 8, 912, DateTimeKind.Local).AddTicks(3284), "AQAAAAEAACcQAAAAELYqKiUKIMu6ivKPBkN0M49FQJHYvwpZBAOJL3lmNOmu1FO10P7igPnCURl2G/o4ng==", "bfd830f6-090e-4c08-9232-b85204b96137" });
        }
    }
}
