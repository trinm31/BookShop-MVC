using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShopMVC.DataAccess.Migrations
{
    public partial class ChangeisAuthorizedCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isAuthorizedCompany",
                table: "Companies",
                newName: "IsAuthorizedCompany");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAuthorizedCompany",
                table: "Companies",
                newName: "isAuthorizedCompany");
        }
    }
}
