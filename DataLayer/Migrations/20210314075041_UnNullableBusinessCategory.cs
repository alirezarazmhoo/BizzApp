using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class UnNullableBusinessCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Categories_CategoryId",
                table: "Businesses");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Businesses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5be4cf83-a4b6-4583-bc06-eed620f7cee0", new DateTime(2021, 3, 14, 11, 20, 40, 748, DateTimeKind.Local).AddTicks(5876), "AQAAAAEAACcQAAAAEFJHy8O+r25hX/wpu8vp8X73ESXeywPSz8Zgbil41xn+Qvt/0UFZKnFMk8laOWz7Mg==", "df48bdf1-24d6-4c96-85d8-23894bc13de2" });

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Categories_CategoryId",
                table: "Businesses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Categories_CategoryId",
                table: "Businesses");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Businesses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07045c52-0394-456e-adfc-1afb7e57f6b0", new DateTime(2021, 3, 10, 10, 27, 55, 806, DateTimeKind.Local).AddTicks(3808), "AQAAAAEAACcQAAAAEJKOK8ygaII2N7+Yd0+OaVIFP7hDNARbF5dEb2IPXRk6OWZ17h6bmsngWHwbSpjJoQ==", "fec5bd2e-f085-4ea0-a44b-88efe0e1b498" });

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Categories_CategoryId",
                table: "Businesses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
