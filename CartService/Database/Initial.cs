using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;


namespace CartService.Database;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "CartProduct",
            columns: table => new
            {
                Identifier = table.Column<int>(nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                UserId = table.Column<string>(nullable: false),
                ProductId = table.Column<string>(nullable: false),
                ProductName = table.Column<string>(nullable: false),
                ProductDescription = table.Column<string>(nullable: false),
                Quantity = table.Column<int>(nullable: false)
            });
    }
}