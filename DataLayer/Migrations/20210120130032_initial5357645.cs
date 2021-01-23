using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class initial5357645 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCreatorId",
                table: "Businesses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_UserCreatorId",
                table: "Businesses",
                column: "UserCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_AspNetUsers_UserCreatorId",
                table: "Businesses",
                column: "UserCreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_AspNetUsers_UserCreatorId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_UserCreatorId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "UserCreatorId",
                table: "Businesses");
        }
    }
}
