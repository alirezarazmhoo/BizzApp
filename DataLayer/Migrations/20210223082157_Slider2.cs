using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Slider2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Image = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90bf4904-7274-4554-8f15-cd8113da6f01", new DateTime(2021, 2, 23, 11, 51, 56, 185, DateTimeKind.Local).AddTicks(234), "AQAAAAEAACcQAAAAEHPe7NUv5a/xgFty1NWsQ0ghyByTEL3HIEDu4n0Ul6+Nh7AJUJ2UWnzpb9ofy1FIOQ==", "cda06fcd-1e59-427c-8966-1b1584c73f3a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5018acb-0856-437b-a265-9efe853449a3", new DateTime(2021, 2, 23, 10, 37, 23, 780, DateTimeKind.Local).AddTicks(4820), "AQAAAAEAACcQAAAAEJpikmjGcxss9HkAjahwDB6hbDp2NI6HEvmpgr788dJSni3XNUqYE16GQUxwrYWXpQ==", "72a4239b-cc80-47d4-9571-2f5e9195188a" });
        }
    }
}
