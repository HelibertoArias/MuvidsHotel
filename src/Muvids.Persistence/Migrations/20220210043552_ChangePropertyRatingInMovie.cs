using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muvids.Persistence.Migrations;

public partial class ChangePropertyRatingInMovie : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Rating",
            table: "Movies",
            newName: "Language");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Language",
            table: "Movies",
            newName: "Rating");
    }
}
