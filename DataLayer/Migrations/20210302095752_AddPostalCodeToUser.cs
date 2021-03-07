using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddPostalCodeToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afe47b54-c443-418e-9645-6daa08472a62", new DateTime(2021, 3, 2, 13, 27, 51, 853, DateTimeKind.Local).AddTicks(8351), "AQAAAAEAACcQAAAAEOPLoee/srFMiQkduWpbNEja5p8svvuZfbMj2jIlneZOY+rW6ftJuUJYKQYuDcsyxg==", "3a6f0d8f-8ac7-4207-b107-0bde5ec14f75" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0cbf073f-326b-4f1e-8d06-e394afc8483d", new DateTime(2021, 3, 2, 13, 20, 56, 49, DateTimeKind.Local).AddTicks(1184), "AQAAAAEAACcQAAAAEMbirZZzd11CinIJg8ecAZT5u0Cu43tzw6ZgALyW3QeVl1VGBSAdeS76WUAADnvfHw==", "c76b3278-1820-4cd9-a618-fb68716e9e98" });
        }
    }
}
