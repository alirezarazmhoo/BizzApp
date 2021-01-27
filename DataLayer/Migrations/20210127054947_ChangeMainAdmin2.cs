using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ChangeMainAdmin2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "e09a6d3c-7b7e-48b4-9cca-4db2e1509d5a", new DateTime(2021, 1, 27, 9, 19, 47, 141, DateTimeKind.Local).AddTicks(6982), "mainadmin@email.com", "mainadmin@email.com", "mainadmin", "AQAAAAEAACcQAAAAEIneKef5ULoLi2tBMBW7XDc3sht5+hmDkZbccxe6W0nr+PL2iUayO91K7c8Ph3el1A==", "5692ad92-e9dd-470f-9320-ba4fdcc80256", "mainadmin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "9dc36be5-0037-400c-9045-219fc8f63e30", new DateTime(2021, 1, 27, 8, 51, 21, 134, DateTimeKind.Local).AddTicks(6540), "mainnadmin@email.com", "mainnadmin@email.com", "mainnadmin", "AQAAAAEAACcQAAAAEIAAydL83RRMOMU6AC46emiEGKf0C7PhJ5PHmYYYqea5USioCoBGTTxWiHGJhCak7g==", "16100e91-9a2b-4416-b1b2-45cf43c970c0", "mainnadmin" });
        }
    }
}
