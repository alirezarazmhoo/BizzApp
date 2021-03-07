using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddCustomerMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerBusinessMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BizAppUserId = table.Column<string>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: false),
                    StatusEnum = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    LikeCount = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBusinessMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerBusinessMedia_AspNetUsers_BizAppUserId",
                        column: x => x.BizAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerBusinessMedia_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersInCustomerBusinessMediaLike",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BizAppUserId = table.Column<string>(nullable: true),
                    CustmoerBusinessMediaId = table.Column<Guid>(nullable: false),
                    CustomerBusinessMediaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInCustomerBusinessMediaLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersInCustomerBusinessMediaLike_AspNetUsers_BizAppUserId",
                        column: x => x.BizAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersInCustomerBusinessMediaLike_CustomerBusinessMedia_CustomerBusinessMediaId",
                        column: x => x.CustomerBusinessMediaId,
                        principalTable: "CustomerBusinessMedia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "052432ce-911f-4abc-a412-4a58f6acbd9b", new DateTime(2021, 3, 6, 10, 59, 23, 113, DateTimeKind.Local).AddTicks(9365), "AQAAAAEAACcQAAAAEJ1lbjOhvLnMGLHr2xZlcq12JNepPUp8NoINIRrCt/964A8RQT3FTCiQYpNIi3OPDA==", "fae38092-b76c-4188-abf5-b0353157b5b2" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBusinessMedia_BizAppUserId",
                table: "CustomerBusinessMedia",
                column: "BizAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBusinessMedia_BusinessId",
                table: "CustomerBusinessMedia",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInCustomerBusinessMediaLike_BizAppUserId",
                table: "UsersInCustomerBusinessMediaLike",
                column: "BizAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInCustomerBusinessMediaLike_CustomerBusinessMediaId",
                table: "UsersInCustomerBusinessMediaLike",
                column: "CustomerBusinessMediaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersInCustomerBusinessMediaLike");

            migrationBuilder.DropTable(
                name: "CustomerBusinessMedia");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aab95ea8-15de-4515-9d12-b0f4352fb1d5", new DateTime(2021, 3, 3, 13, 20, 35, 640, DateTimeKind.Local).AddTicks(1356), "AQAAAAEAACcQAAAAEAilMM5wCIAINzGBEeQN+kKQ9bXcdKez7CV2PUS7z2GcxKhQ7Labh2ZUw4IcW4++Fw==", "d2eae8c3-60f1-4267-b361-429acab0375c" });
        }
    }
}
