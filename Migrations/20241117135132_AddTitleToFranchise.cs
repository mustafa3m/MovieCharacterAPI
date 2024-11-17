using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieCharacterAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleToFranchise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Franchises",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Franchises",
                newName: "Name");
        }
    }
}
