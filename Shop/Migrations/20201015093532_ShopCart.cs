using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Migrations
{
    public partial class ShopCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopCarId",
                table: "ShopCartItem");

            migrationBuilder.AddColumn<string>(
                name: "ShopCartId",
                table: "ShopCartItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopCartId",
                table: "ShopCartItem");

            migrationBuilder.AddColumn<string>(
                name: "ShopCarId",
                table: "ShopCartItem",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
