using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seminarski_Rad.Data.Migrations
{
    public partial class Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e3820d63-8387-474a-8e73-63ccb6918f05", "b38f0e71-ed45-4a63-825f-584b3fec915e", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3820d63-8387-474a-8e73-63ccb6918f05");
        }
    }
}
