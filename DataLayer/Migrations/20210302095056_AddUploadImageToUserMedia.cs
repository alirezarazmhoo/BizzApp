using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddUploadImageToUserMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UploadedPhoto",
                table: "ApplicationUserMedias",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0cbf073f-326b-4f1e-8d06-e394afc8483d", new DateTime(2021, 3, 2, 13, 20, 56, 49, DateTimeKind.Local).AddTicks(1184), "AQAAAAEAACcQAAAAEMbirZZzd11CinIJg8ecAZT5u0Cu43tzw6ZgALyW3QeVl1VGBSAdeS76WUAADnvfHw==", "c76b3278-1820-4cd9-a618-fb68716e9e98" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedPhoto",
                table: "ApplicationUserMedias");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f24854ad-426a-44c8-b164-719fb33d7dea", new DateTime(2021, 3, 2, 10, 10, 18, 899, DateTimeKind.Local).AddTicks(1347), "AQAAAAEAACcQAAAAEB79sDbBEOiFMHIEvIzu1VERs4+UtaNk4EIiAVu5VUJVHCVrUiHYOblLH1AM4F3tWg==", "fbe591a3-9892-4416-8aac-876c675401e1" });
        }
    }
}
