using Microsoft.EntityFrameworkCore.Migrations;

namespace MyModelAndDatabase.Migrations
{
    public partial class ChangePersonPart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Blocked",
                table: "People",
                newName: "IsBlock");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBlock",
                table: "People",
                newName: "Blocked");
        }
    }
}
