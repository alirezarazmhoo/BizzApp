using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddCreatedDateFieldForBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Businesses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7e0e27e-2c43-4ede-9a7b-754f9e4da1f6", new DateTime(2021, 1, 27, 11, 2, 12, 877, DateTimeKind.Local).AddTicks(9415), "AQAAAAEAACcQAAAAEJ1C4nLkJ7gJ0vOMtQM7lzzKH1uVHJVjak4uId3kiNJQ/9S+Sm+FWLf3b0Bd5MrxeQ==", "6b1321a1-9527-41eb-aa94-2c8fd00536bf" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Businesses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e09a6d3c-7b7e-48b4-9cca-4db2e1509d5a", new DateTime(2021, 1, 27, 9, 19, 47, 141, DateTimeKind.Local).AddTicks(6982), "AQAAAAEAACcQAAAAEIneKef5ULoLi2tBMBW7XDc3sht5+hmDkZbccxe6W0nr+PL2iUayO91K7c8Ph3el1A==", "5692ad92-e9dd-470f-9320-ba4fdcc80256" });
        }
    }
}
