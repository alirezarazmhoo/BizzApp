using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class CustomerBusinessMediaFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustmoerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CustomerBusinessMedias");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "CustomerBusinessMedias");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "CustomerBusinessMedias");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerBusinessMediaPicturesId",
                table: "UsersInCustomerBusinessMediaLikes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CustomerBusinessMediaPictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LikeCount = table.Column<long>(nullable: false),
                    StatusEnum = table.Column<int>(nullable: false),
                    CustomerBusinessMediaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBusinessMediaPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerBusinessMediaPictures_CustomerBusinessMedias_CustomerBusinessMediaId",
                        column: x => x.CustomerBusinessMediaId,
                        principalTable: "CustomerBusinessMedias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bcf361e-2bbe-4788-a4ab-0403840823c7", new DateTime(2021, 3, 6, 14, 3, 42, 26, DateTimeKind.Local).AddTicks(3738), "AQAAAAEAACcQAAAAEJ8GuD53vedZU0d4yc36K0pgKrJS6E3eSnkEaLQI4+bvCOyljgtUU1AMeDrQOUxzog==", "a8d2a8ac-d6ee-4f77-bb4c-67814afd2725" });

            migrationBuilder.CreateIndex(
                name: "IX_UsersInCustomerBusinessMediaLikes_CustomerBusinessMediaPicturesId",
                table: "UsersInCustomerBusinessMediaLikes",
                column: "CustomerBusinessMediaPicturesId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBusinessMediaPictures_CustomerBusinessMediaId",
                table: "CustomerBusinessMediaPictures",
                column: "CustomerBusinessMediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLikes_CustomerBusinessMediaPictures_CustomerBusinessMediaPicturesId",
                table: "UsersInCustomerBusinessMediaLikes",
                column: "CustomerBusinessMediaPicturesId",
                principalTable: "CustomerBusinessMediaPictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLikes_CustomerBusinessMediaPictures_CustomerBusinessMediaPicturesId",
                table: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.DropTable(
                name: "CustomerBusinessMediaPictures");

            migrationBuilder.DropIndex(
                name: "IX_UsersInCustomerBusinessMediaLikes_CustomerBusinessMediaPicturesId",
                table: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.DropColumn(
                name: "CustomerBusinessMediaPicturesId",
                table: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.AddColumn<Guid>(
                name: "CustmoerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLikes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CustomerBusinessMedias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CustomerBusinessMedias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LikeCount",
                table: "CustomerBusinessMedias",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d22289d6-4d6e-44bd-b2c8-61cf06f05261", new DateTime(2021, 3, 6, 12, 6, 43, 522, DateTimeKind.Local).AddTicks(5537), "AQAAAAEAACcQAAAAEPhRIe56vYqHvEOnnbgoKKxAOwPkCOcsGIGrW7e5uX0qixrCGjxQj+a1F+V6LWv80g==", "ae7ed8e9-a137-4511-8b11-89af95736c31" });
        }
    }
}
