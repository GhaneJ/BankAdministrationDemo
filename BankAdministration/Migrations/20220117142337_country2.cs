using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAdministration.Migrations
{
    public partial class country2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Customers",
                newName: "TheCountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CountryId",
                table: "Customers",
                newName: "IX_Customers_TheCountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TheCountryId",
                table: "Customers",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_TheCountryId",
                table: "Customers",
                newName: "IX_Customers_CountryId");
        }
    }
}
