using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FixBusibessMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBusinessMedia_AspNetUsers_BizAppUserId",
                table: "CustomerBusinessMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBusinessMedia_Businesses_BusinessId",
                table: "CustomerBusinessMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLike_AspNetUsers_BizAppUserId",
                table: "UsersInCustomerBusinessMediaLike");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLike_CustomerBusinessMedia_CustomerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersInCustomerBusinessMediaLike",
                table: "UsersInCustomerBusinessMediaLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerBusinessMedia",
                table: "CustomerBusinessMedia");

            migrationBuilder.DropColumn(
                name: "CustmoerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLike");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CustomerBusinessMedia");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "CustomerBusinessMedia");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "CustomerBusinessMedia");

            migrationBuilder.RenameTable(
                name: "UsersInCustomerBusinessMediaLike",
                newName: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.RenameTable(
                name: "CustomerBusinessMedia",
                newName: "CustomerBusinessMedias");

            migrationBuilder.RenameIndex(
                name: "IX_UsersInCustomerBusinessMediaLike_CustomerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLikes",
                newName: "IX_UsersInCustomerBusinessMediaLikes_CustomerBusinessMediaId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersInCustomerBusinessMediaLike_BizAppUserId",
                table: "UsersInCustomerBusinessMediaLikes",
                newName: "IX_UsersInCustomerBusinessMediaLikes_BizAppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerBusinessMedia_BusinessId",
                table: "CustomerBusinessMedias",
                newName: "IX_CustomerBusinessMedias_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerBusinessMedia_BizAppUserId",
                table: "CustomerBusinessMedias",
                newName: "IX_CustomerBusinessMedias_BizAppUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerBusinessMediaPicturesId",
                table: "UsersInCustomerBusinessMediaLikes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersInCustomerBusinessMediaLikes",
                table: "UsersInCustomerBusinessMediaLikes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerBusinessMedias",
                table: "CustomerBusinessMedias",
                column: "Id");

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
                values: new object[] { "0ab5412f-fd8c-4cfd-8a39-a24847bae3dd", new DateTime(2021, 3, 9, 10, 38, 47, 554, DateTimeKind.Local).AddTicks(3052), "AQAAAAEAACcQAAAAEI2bvxUDbjddEs9pqjsVjY6dHAjgpyOCuixkorEztzpQrZgBKUItD40EQISs+qToBA==", "c43a5ba5-ac97-4a58-991c-e55f4a6e0d04" });

            migrationBuilder.CreateIndex(
                name: "IX_UsersInCustomerBusinessMediaLikes_CustomerBusinessMediaPicturesId",
                table: "UsersInCustomerBusinessMediaLikes",
                column: "CustomerBusinessMediaPicturesId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBusinessMediaPictures_CustomerBusinessMediaId",
                table: "CustomerBusinessMediaPictures",
                column: "CustomerBusinessMediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBusinessMedias_AspNetUsers_BizAppUserId",
                table: "CustomerBusinessMedias",
                column: "BizAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBusinessMedias_Businesses_BusinessId",
                table: "CustomerBusinessMedias",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLikes_AspNetUsers_BizAppUserId",
                table: "UsersInCustomerBusinessMediaLikes",
                column: "BizAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLikes_CustomerBusinessMedias_CustomerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLikes",
                column: "CustomerBusinessMediaId",
                principalTable: "CustomerBusinessMedias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_CustomerBusinessMedias_AspNetUsers_BizAppUserId",
                table: "CustomerBusinessMedias");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerBusinessMedias_Businesses_BusinessId",
                table: "CustomerBusinessMedias");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLikes_AspNetUsers_BizAppUserId",
                table: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLikes_CustomerBusinessMedias_CustomerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLikes_CustomerBusinessMediaPictures_CustomerBusinessMediaPicturesId",
                table: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.DropTable(
                name: "CustomerBusinessMediaPictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersInCustomerBusinessMediaLikes",
                table: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.DropIndex(
                name: "IX_UsersInCustomerBusinessMediaLikes_CustomerBusinessMediaPicturesId",
                table: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerBusinessMedias",
                table: "CustomerBusinessMedias");

            migrationBuilder.DropColumn(
                name: "CustomerBusinessMediaPicturesId",
                table: "UsersInCustomerBusinessMediaLikes");

            migrationBuilder.RenameTable(
                name: "UsersInCustomerBusinessMediaLikes",
                newName: "UsersInCustomerBusinessMediaLike");

            migrationBuilder.RenameTable(
                name: "CustomerBusinessMedias",
                newName: "CustomerBusinessMedia");

            migrationBuilder.RenameIndex(
                name: "IX_UsersInCustomerBusinessMediaLikes_CustomerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLike",
                newName: "IX_UsersInCustomerBusinessMediaLike_CustomerBusinessMediaId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersInCustomerBusinessMediaLikes_BizAppUserId",
                table: "UsersInCustomerBusinessMediaLike",
                newName: "IX_UsersInCustomerBusinessMediaLike_BizAppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerBusinessMedias_BusinessId",
                table: "CustomerBusinessMedia",
                newName: "IX_CustomerBusinessMedia_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerBusinessMedias_BizAppUserId",
                table: "CustomerBusinessMedia",
                newName: "IX_CustomerBusinessMedia_BizAppUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "CustmoerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLike",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CustomerBusinessMedia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CustomerBusinessMedia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LikeCount",
                table: "CustomerBusinessMedia",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersInCustomerBusinessMediaLike",
                table: "UsersInCustomerBusinessMediaLike",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerBusinessMedia",
                table: "CustomerBusinessMedia",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f36498b2-4a1c-4823-9c3c-650e0e681877", new DateTime(2021, 3, 9, 10, 26, 41, 628, DateTimeKind.Local).AddTicks(8617), "AQAAAAEAACcQAAAAEMcAGVuJi1xHSq2GhZ4lZ8a0559UXNAaTyg8HWnTmNKjPJ6bz5nV5w8KwL+KCGoTjQ==", "74880ce3-2e31-47d3-bdb5-bc10f36799ff" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBusinessMedia_AspNetUsers_BizAppUserId",
                table: "CustomerBusinessMedia",
                column: "BizAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerBusinessMedia_Businesses_BusinessId",
                table: "CustomerBusinessMedia",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLike_AspNetUsers_BizAppUserId",
                table: "UsersInCustomerBusinessMediaLike",
                column: "BizAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInCustomerBusinessMediaLike_CustomerBusinessMedia_CustomerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLike",
                column: "CustomerBusinessMediaId",
                principalTable: "CustomerBusinessMedia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
