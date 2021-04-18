using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddTitleToMessageToBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MessageToBusinesses",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 18, 12, 28, 19, 337, DateTimeKind.Local).AddTicks(8483),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 17, 9, 48, 58, 698, DateTimeKind.Local).AddTicks(4126));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "278531ed-2552-438d-9102-2a469b1d3059", new DateTime(2021, 4, 18, 12, 28, 19, 343, DateTimeKind.Local).AddTicks(4838), "AQAAAAEAACcQAAAAELX8qldO0H8AJKMkyylUiBpFUSOjmkumghcIOLoEZZ0tmBOgXhC5g7ZbYps3uFac8Q==", "f2600c01-0019-45d4-8dc0-dcff979ce008" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "MessageToBusinesses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 9, 48, 58, 698, DateTimeKind.Local).AddTicks(4126),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 18, 12, 28, 19, 337, DateTimeKind.Local).AddTicks(8483));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e24de2d-6bfa-4a40-9847-ee65b684ab8c", new DateTime(2021, 4, 17, 9, 48, 58, 703, DateTimeKind.Local).AddTicks(1180), "AQAAAAEAACcQAAAAEDwCFXFVaBdf4oa0syzaehpAokOyJ+uV75Dtl0ZTJWP3qKTGtoK8rk7YQ5DYRdZbQg==", "748e00d5-5693-4499-860b-8712476eea64" });
        }
    }
}
