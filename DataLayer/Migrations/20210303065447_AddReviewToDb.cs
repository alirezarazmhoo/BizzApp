using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddReviewToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StatusEnum = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Rate = table.Column<int>(nullable: false),
                    UsefulCount = table.Column<int>(nullable: false),
                    FunnyCount = table.Column<int>(nullable: false),
                    CoolCount = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    BusinessId = table.Column<Guid>(nullable: false),
                    BizAppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_BizAppUserId",
                        column: x => x.BizAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewMedias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReviewId = table.Column<Guid>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    LikeCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewMedias_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersInReviewLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReviewId = table.Column<Guid>(nullable: false),
                    BizAppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInReviewLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersInReviewLikes_AspNetUsers_BizAppUserId",
                        column: x => x.BizAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersInReviewLikes_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1920a894-701e-4b4a-a839-cee482f8cf1e", new DateTime(2021, 3, 3, 10, 24, 45, 863, DateTimeKind.Local).AddTicks(7505), "AQAAAAEAACcQAAAAEFEBfZXlxMmqlhmF9rkHHluZldY3hK9zFY4Y1Jt4T0L8AFeBq3WsHY2nGKyPYhknYQ==", "31a00790-c4d1-4dd5-8418-cf7d53e0cc37" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewMedias_ReviewId",
                table: "ReviewMedias",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BizAppUserId",
                table: "Reviews",
                column: "BizAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BusinessId",
                table: "Reviews",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInReviewLikes_BizAppUserId",
                table: "UsersInReviewLikes",
                column: "BizAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInReviewLikes_ReviewId",
                table: "UsersInReviewLikes",
                column: "ReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewMedias");

            migrationBuilder.DropTable(
                name: "UsersInReviewLikes");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afe47b54-c443-418e-9645-6daa08472a62", new DateTime(2021, 3, 2, 13, 27, 51, 853, DateTimeKind.Local).AddTicks(8351), "AQAAAAEAACcQAAAAEOPLoee/srFMiQkduWpbNEja5p8svvuZfbMj2jIlneZOY+rW6ftJuUJYKQYuDcsyxg==", "3a6f0d8f-8ac7-4207-b107-0bde5ec14f75" });
        }
    }
}
