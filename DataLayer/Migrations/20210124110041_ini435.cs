using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ini435 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Businesses",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Businesses",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Businesses",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Businesses",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "Businesses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_CityId",
                table: "Businesses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_ProvinceId",
                table: "Businesses",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Cities_CityId",
                table: "Businesses",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Provinces_ProvinceId",
                table: "Businesses",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Cities_CityId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Provinces_ProvinceId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_CityId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_ProvinceId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Businesses");
        }
    }
}
