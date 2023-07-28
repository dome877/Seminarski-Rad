using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seminarski_Rad.Data.Migrations
{
    public partial class MalePromjene : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KategorijaProizvoda_Proizvod_ProductId",
                table: "KategorijaProizvoda");

            migrationBuilder.DropForeignKey(
                name: "FK_NarudzbaItem_Proizvod_ProductId",
                table: "NarudzbaItem");

            migrationBuilder.DropIndex(
                name: "IX_NarudzbaItem_ProductId",
                table: "NarudzbaItem");

            migrationBuilder.DropIndex(
                name: "IX_KategorijaProizvoda_ProductId",
                table: "KategorijaProizvoda");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "NarudzbaItem");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "KategorijaProizvoda");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Narudzba",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NarudzbaItem_ProizvodId",
                table: "NarudzbaItem",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_UserId",
                table: "Narudzba",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KategorijaProizvoda_ProizvodId",
                table: "KategorijaProizvoda",
                column: "ProizvodId");

            migrationBuilder.AddForeignKey(
                name: "FK_KategorijaProizvoda_Proizvod_ProizvodId",
                table: "KategorijaProizvoda",
                column: "ProizvodId",
                principalTable: "Proizvod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_AspNetUsers_UserId",
                table: "Narudzba",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NarudzbaItem_Proizvod_ProizvodId",
                table: "NarudzbaItem",
                column: "ProizvodId",
                principalTable: "Proizvod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KategorijaProizvoda_Proizvod_ProizvodId",
                table: "KategorijaProizvoda");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_AspNetUsers_UserId",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_NarudzbaItem_Proizvod_ProizvodId",
                table: "NarudzbaItem");

            migrationBuilder.DropIndex(
                name: "IX_NarudzbaItem_ProizvodId",
                table: "NarudzbaItem");

            migrationBuilder.DropIndex(
                name: "IX_Narudzba_UserId",
                table: "Narudzba");

            migrationBuilder.DropIndex(
                name: "IX_KategorijaProizvoda_ProizvodId",
                table: "KategorijaProizvoda");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Narudzba");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "NarudzbaItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "KategorijaProizvoda",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NarudzbaItem_ProductId",
                table: "NarudzbaItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_KategorijaProizvoda_ProductId",
                table: "KategorijaProizvoda",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_KategorijaProizvoda_Proizvod_ProductId",
                table: "KategorijaProizvoda",
                column: "ProductId",
                principalTable: "Proizvod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NarudzbaItem_Proizvod_ProductId",
                table: "NarudzbaItem",
                column: "ProductId",
                principalTable: "Proizvod",
                principalColumn: "Id");
        }
    }
}
