using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seminarski_Rad.Data.Migrations
{
    public partial class KolicinaInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55c7982e-23e0-44b9-b6fa-04aa1097fe43");

            migrationBuilder.AlterColumn<int>(
                name: "Količina",
                table: "NarudzbaItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bada6c53-2d6f-4905-aa8f-4b95cd489aec", "e3504990-dfaa-4706-8138-8b95d0c79a4f", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bada6c53-2d6f-4905-aa8f-4b95cd489aec");

            migrationBuilder.AlterColumn<decimal>(
                name: "Količina",
                table: "NarudzbaItem",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "55c7982e-23e0-44b9-b6fa-04aa1097fe43", "a4bb2c23-06d6-4427-834c-06a643d95793", "Admin", "ADMIN" });
        }
    }
}
