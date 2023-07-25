using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seminarski_Rad.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Kategorija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorija", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Narudzba",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumKreiran = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ukupno = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    Popust = table.Column<int>(type: "int", nullable: true),
                    NarudzbaPrvoIme = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NarudzbaDrugoIme = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NarudzbaEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NarudzbaTelefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NarudzbaAdresa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NarudzbaGrad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NarudzbaCountry = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NarudzbaPostanskibroj = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Poruka = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzba", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Količina = table.Column<int>(type: "int", nullable: false),
                    Cijena = table.Column<decimal>(type: "decimal(9,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KategorijaProizvoda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategorijaId = table.Column<int>(type: "int", nullable: false),
                    ProizvodId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategorijaProizvoda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KategorijaProizvoda_Kategorija_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KategorijaProizvoda_Proizvod_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Proizvod",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NarudzbaItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NarudzbaId = table.Column<int>(type: "int", nullable: false),
                    ProizvodId = table.Column<int>(type: "int", nullable: false),
                    Količina = table.Column<int>(type: "int", nullable: false),
                    Ukupno = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NarudzbaItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NarudzbaItem_Narudzba_NarudzbaId",
                        column: x => x.NarudzbaId,
                        principalTable: "Narudzba",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NarudzbaItem_Proizvod_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Proizvod",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KategorijaProizvoda_KategorijaId",
                table: "KategorijaProizvoda",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_KategorijaProizvoda_ProductId",
                table: "KategorijaProizvoda",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_NarudzbaItem_NarudzbaId",
                table: "NarudzbaItem",
                column: "NarudzbaId");

            migrationBuilder.CreateIndex(
                name: "IX_NarudzbaItem_ProductId",
                table: "NarudzbaItem",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KategorijaProizvoda");

            migrationBuilder.DropTable(
                name: "NarudzbaItem");

            migrationBuilder.DropTable(
                name: "Kategorija");

            migrationBuilder.DropTable(
                name: "Narudzba");

            migrationBuilder.DropTable(
                name: "Proizvod");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
