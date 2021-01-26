using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FeatureType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5606f00-0d32-4dc6-bf88-03fb9c53f134");

            migrationBuilder.AddColumn<int>(
                name: "BusinessFeatureType",
                table: "Features",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6cf20c8c-fbce-4c80-8424-0cef41c422f8", "6cf20c8c-fbce-4c80-8424-0cef41c422f8", "operator", "operator" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63eb8e7c-45e9-4ffc-8736-0a6728773424", new DateTime(2021, 1, 26, 10, 19, 40, 929, DateTimeKind.Local).AddTicks(2933), "AQAAAAEAACcQAAAAEHYYGsKwTvdXQqjje9MzWKKUrMetmuvJXBu96nabBFmnMb+Xp8fMIKcDl2ZChvT3WA==", "702732d5-cccd-4c5e-bef4-09105f52a74b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cf20c8c-fbce-4c80-8424-0cef41c422f8");

            migrationBuilder.DropColumn(
                name: "BusinessFeatureType",
                table: "Features");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f5606f00-0d32-4dc6-bf88-03fb9c53f134", "f5606f00-0d32-4dc6-bf88-03fb9c53f134", "operator", "operator" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3b475bd-335f-4581-aca9-97ccd0879eab", new DateTime(2021, 1, 25, 13, 50, 31, 49, DateTimeKind.Local).AddTicks(8392), "AQAAAAEAACcQAAAAEKccDMhbmTEgTSz3hDlOJQ9hlU49de3zvLX5bdmVpcyX8n8iHO9Mlpsq68v6/QMQFw==", "5b90aa01-30fe-43ea-8f56-6b8b8db6503c" });
        }
    }
}
