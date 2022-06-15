using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saharacomnew.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    refence = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    designation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    stockfinal = table.Column<int>(type: "int", nullable: false),
                    stockinitial = table.Column<int>(type: "int", nullable: false),
                    qtevendue = table.Column<int>(type: "int", nullable: false),
                    qteacheté = table.Column<int>(type: "int", nullable: false),
                    prixachatHt = table.Column<double>(type: "double", nullable: false),
                    prixachatttc = table.Column<double>(type: "double", nullable: false),
                    prixventeHt = table.Column<double>(type: "double", nullable: false),
                    prixventettc = table.Column<double>(type: "double", nullable: false),
                    info = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nom = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numphone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    adresse = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tvas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    designation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    taux = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tvas", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Livraisons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    num = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    info = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Montantht = table.Column<double>(type: "double", nullable: false),
                    tva = table.Column<int>(type: "int", nullable: false),
                    montantttc = table.Column<double>(type: "double", nullable: false),
                    clientid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livraisons", x => x.id);
                    table.ForeignKey(
                        name: "FK_Livraisons_Clients_clientid",
                        column: x => x.clientid,
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DetailLivraisons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    num = table.Column<int>(type: "int", nullable: false),
                    qteup = table.Column<int>(type: "int", nullable: false),
                    prixht = table.Column<double>(type: "double", nullable: false),
                    Montantht = table.Column<double>(type: "double", nullable: false),
                    prixttc = table.Column<double>(type: "double", nullable: false),
                    montantttc = table.Column<double>(type: "double", nullable: false),
                    Articleid = table.Column<int>(type: "int", nullable: false),
                    Livraisonid = table.Column<int>(type: "int", nullable: false),
                    Tvaid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailLivraisons", x => x.id);
                    table.ForeignKey(
                        name: "FK_DetailLivraisons_Articles_Articleid",
                        column: x => x.Articleid,
                        principalTable: "Articles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailLivraisons_Livraisons_Livraisonid",
                        column: x => x.Livraisonid,
                        principalTable: "Livraisons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailLivraisons_Tvas_Tvaid",
                        column: x => x.Tvaid,
                        principalTable: "Tvas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DetailLivraisons_Articleid",
                table: "DetailLivraisons",
                column: "Articleid");

            migrationBuilder.CreateIndex(
                name: "IX_DetailLivraisons_Livraisonid",
                table: "DetailLivraisons",
                column: "Livraisonid");

            migrationBuilder.CreateIndex(
                name: "IX_DetailLivraisons_Tvaid",
                table: "DetailLivraisons",
                column: "Tvaid");

            migrationBuilder.CreateIndex(
                name: "IX_Livraisons_clientid",
                table: "Livraisons",
                column: "clientid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailLivraisons");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Livraisons");

            migrationBuilder.DropTable(
                name: "Tvas");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
