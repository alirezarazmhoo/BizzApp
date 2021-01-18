using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddBusinessModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryFeature_Categories_CategoryId",
                table: "CategoryFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryFeature",
                table: "CategoryFeature");

            migrationBuilder.RenameTable(
                name: "CategoryFeature",
                newName: "CategoryFeatures");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryFeature_CategoryId",
                table: "CategoryFeatures",
                newName: "IX_CategoryFeatures_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "ValueType",
                table: "Features",
                nullable: false,
                defaultValue: "bool");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryFeatures",
                table: "CategoryFeatures",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DistrictId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    FeatureImage = table.Column<string>(type: "nvarchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Businesses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Businesses_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCallNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<Guid>(nullable: false),
                    Number = table.Column<string>(type: "varchar(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCallNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCallNumbers_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<Guid>(nullable: false),
                    FeatureId = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessFeatures_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessGalleries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<Guid>(nullable: false),
                    FileAddress = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessGalleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessGalleries_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessTimes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<Guid>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    FromTime = table.Column<TimeSpan>(type: "time(1)", nullable: false),
                    ToTime = table.Column<TimeSpan>(type: "time(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessTimes_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCallNumbers_BusinessId",
                table: "BusinessCallNumbers",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_CategoryId",
                table: "Businesses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_DistrictId",
                table: "Businesses",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFeatures_BusinessId",
                table: "BusinessFeatures",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFeatures_FeatureId",
                table: "BusinessFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessGalleries_BusinessId",
                table: "BusinessGalleries",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTimes_BusinessId",
                table: "BusinessTimes",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFeatures_Categories_CategoryId",
                table: "CategoryFeatures",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryFeatures_Categories_CategoryId",
                table: "CategoryFeatures");

            migrationBuilder.DropTable(
                name: "BusinessCallNumbers");

            migrationBuilder.DropTable(
                name: "BusinessFeatures");

            migrationBuilder.DropTable(
                name: "BusinessGalleries");

            migrationBuilder.DropTable(
                name: "BusinessTimes");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryFeatures",
                table: "CategoryFeatures");

            migrationBuilder.DropColumn(
                name: "ValueType",
                table: "Features");

            migrationBuilder.RenameTable(
                name: "CategoryFeatures",
                newName: "CategoryFeature");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryFeatures_CategoryId",
                table: "CategoryFeature",
                newName: "IX_CategoryFeature_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryFeature",
                table: "CategoryFeature",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFeature_Categories_CategoryId",
                table: "CategoryFeature",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
