using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seminarski_Rad.Data.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3680083-1601-4b8f-9f5c-cda599fc85d6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c1df5349-eccd-43e8-a8a7-78f3b256d763", "bea4ff75-0344-4755-87b4-c252ff260756", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1df5349-eccd-43e8-a8a7-78f3b256d763");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e3680083-1601-4b8f-9f5c-cda599fc85d6", "61293665-7442-45bb-af0d-b8317b7826f5", "Admin", "ADMIN" });
        }
    }
}
