using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amado.Migrations
{
    public partial class AddValueInColorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Colors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Colors");
        }
    }
}
