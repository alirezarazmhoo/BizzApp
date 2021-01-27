using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ChangeMainAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "9dc36be5-0037-400c-9045-219fc8f63e30", new DateTime(2021, 1, 27, 8, 51, 21, 134, DateTimeKind.Local).AddTicks(6540), "mainnadmin@email.com", "mainnadmin@email.com", "mainnadmin", "AQAAAAEAACcQAAAAEIAAydL83RRMOMU6AC46emiEGKf0C7PhJ5PHmYYYqea5USioCoBGTTxWiHGJhCak7g==", "16100e91-9a2b-4416-b1b2-45cf43c970c0", "mainnadmin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "1c40342a-0af3-4268-a4a5-87af1611077a", new DateTime(2021, 1, 26, 15, 20, 55, 491, DateTimeKind.Local).AddTicks(5299), "mainadmin@email.com", "mainadmin@email.com", "mainadmin", "AQAAAAEAACcQAAAAEP9Qh6kgt38RRioQ0/pDmwQF4HhRFJ3qf8MBnZW4jDqpeGTk2B3g9VvO6ImEb4PVxQ==", "0d503c40-f437-4835-a0e8-1fe32d7e7295", "mianadmin" });
        }
    }
}
