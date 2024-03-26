using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterakcjeMiedzyLekami.Migrations
{
    public partial class drugsChecker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveSubstances",
                columns: table => new
                {
                    IdActiveSubstance = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameActiveSubstance = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Active_Substances", x => x.IdActiveSubstance);
                });

            migrationBuilder.CreateTable(
                name: "OtherSubstances",
                columns: table => new
                {
                    IdSubstance = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSubstance = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Other_Substances", x => x.IdSubstance);
                });

            migrationBuilder.CreateTable(
                name: "PharmaceuticalsCategories",
                columns: table => new
                {
                    IdCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCategory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmaceuticals_Categories", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "ActiveSubstancesInteractions ",
                columns: table => new
                {
                    IdInteractionSubstance = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdActiveSubstance1 = table.Column<int>(type: "int", nullable: false),
                    IdActiveSubstance2 = table.Column<int>(type: "int", nullable: false),
                    DescriptionInteraction = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReaction = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Active_Substance_Interactions ", x => x.IdInteractionSubstance);
                    table.ForeignKey(
                        name: "FK_ActiveSubstancesInteractions_ActiveSubstances1",
                        column: x => x.IdActiveSubstance1,
                        principalTable: "ActiveSubstances",
                        principalColumn: "IdActiveSubstance");
                    table.ForeignKey(
                        name: "FK_ActiveSubstancesInteractions_ActiveSubstances2",
                        column: x => x.IdActiveSubstance2,
                        principalTable: "ActiveSubstances",
                        principalColumn: "IdActiveSubstance");
                });

            migrationBuilder.CreateTable(
                name: "Pharmaceuticals",
                columns: table => new
                {
                    IdPharmaceutical = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamePharmaceutical = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IdCategory = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmaceuticals", x => x.IdPharmaceutical);
                    table.ForeignKey(
                        name: "FK_Pharmaceuticals_Pharmaceuticals_Categories",
                        column: x => x.IdCategory,
                        principalTable: "PharmaceuticalsCategories",
                        principalColumn: "IdCategory");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Passwordhash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Users_Role",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole");
                });

            migrationBuilder.CreateTable(
                name: "PharmaceuticalsActiveSubstance ",
                columns: table => new
                {
                    IdPharmaceutical = table.Column<int>(type: "int", nullable: false),
                    IdActiveSubstance = table.Column<int>(type: "int", nullable: false),
                    DoseSubstance = table.Column<double>(type: "float", nullable: false, comment: "mg")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmaceuticals_Active_Substance _1", x => new { x.IdPharmaceutical, x.IdActiveSubstance });
                    table.ForeignKey(
                        name: "FK_Pharmaceuticals_Active_Substance_Active_Substances",
                        column: x => x.IdActiveSubstance,
                        principalTable: "ActiveSubstances",
                        principalColumn: "IdActiveSubstance");
                    table.ForeignKey(
                        name: "FK_Pharmaceuticals_Active_Substance_Pharmaceuticals",
                        column: x => x.IdPharmaceutical,
                        principalTable: "Pharmaceuticals",
                        principalColumn: "IdPharmaceutical");
                });

            migrationBuilder.CreateTable(
                name: "PharmaceuticalsReviews",
                columns: table => new
                {
                    IdReview = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPharmaceutical = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacy_Reviews", x => x.IdReview);
                    table.ForeignKey(
                        name: "FK_Pharmaceuticals_Reviews_Pharmaceuticals",
                        column: x => x.IdPharmaceutical,
                        principalTable: "Pharmaceuticals",
                        principalColumn: "IdPharmaceutical");
                });

            migrationBuilder.CreateTable(
                name: "PharmaceuticalsSubstanceInteractions",
                columns: table => new
                {
                    IdInteraction = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPharmaceutical = table.Column<int>(type: "int", nullable: false),
                    IdSubstance = table.Column<int>(type: "int", nullable: false),
                    DescriptionInteraction = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeReaction = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmaceuticals_Substance_Interactions", x => x.IdInteraction);
                    table.ForeignKey(
                        name: "FK_PharmaceuticalsSubstanceInteractions_OtherSubstances",
                        column: x => x.IdSubstance,
                        principalTable: "OtherSubstances",
                        principalColumn: "IdSubstance");
                    table.ForeignKey(
                        name: "FK_PharmaceuticalsSubstanceInteractions_Pharmaceuticals",
                        column: x => x.IdPharmaceutical,
                        principalTable: "Pharmaceuticals",
                        principalColumn: "IdPharmaceutical");
                });

            migrationBuilder.CreateTable(
                name: "PharmaceuticalsReviewsUsers",
                columns: table => new
                {
                    IdReview = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmaceuticals_Reviews_Users_1", x => new { x.IdReview, x.IdUser });
                    table.ForeignKey(
                        name: "FK_PharmaceuticalsReviews_PharmaceuticalsReviewsUsers",
                        column: x => x.IdReview,
                        principalTable: "PharmaceuticalsReviews",
                        principalColumn: "IdReview");
                    table.ForeignKey(
                        name: "FK_User_Pharmaceuticals_Rieviews_Users",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstancesInteractions _IdActiveSubstance1",
                table: "ActiveSubstancesInteractions ",
                column: "IdActiveSubstance1");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstancesInteractions _IdActiveSubstance2",
                table: "ActiveSubstancesInteractions ",
                column: "IdActiveSubstance2");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmaceuticals_IdCategory",
                table: "Pharmaceuticals",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_PharmaceuticalsActiveSubstance _IdActiveSubstance",
                table: "PharmaceuticalsActiveSubstance ",
                column: "IdActiveSubstance");

            migrationBuilder.CreateIndex(
                name: "IX_PharmaceuticalsReviews_IdPharmaceutical",
                table: "PharmaceuticalsReviews",
                column: "IdPharmaceutical");

            migrationBuilder.CreateIndex(
                name: "IX_PharmaceuticalsReviewsUsers_IdUser",
                table: "PharmaceuticalsReviewsUsers",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_PharmaceuticalsSubstanceInteractions_IdPharmaceutical",
                table: "PharmaceuticalsSubstanceInteractions",
                column: "IdPharmaceutical");

            migrationBuilder.CreateIndex(
                name: "IX_PharmaceuticalsSubstanceInteractions_IdSubstance",
                table: "PharmaceuticalsSubstanceInteractions",
                column: "IdSubstance");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdRole",
                table: "Users",
                column: "IdRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveSubstancesInteractions ");

            migrationBuilder.DropTable(
                name: "PharmaceuticalsActiveSubstance ");

            migrationBuilder.DropTable(
                name: "PharmaceuticalsReviewsUsers");

            migrationBuilder.DropTable(
                name: "PharmaceuticalsSubstanceInteractions");

            migrationBuilder.DropTable(
                name: "ActiveSubstances");

            migrationBuilder.DropTable(
                name: "PharmaceuticalsReviews");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "OtherSubstances");

            migrationBuilder.DropTable(
                name: "Pharmaceuticals");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "PharmaceuticalsCategories");
        }
    }
}
