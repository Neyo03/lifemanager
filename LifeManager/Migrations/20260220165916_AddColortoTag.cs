using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeManager.Migrations
{
    /// <inheritdoc />
    public partial class AddColortoTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorHex",
                table: "Tags",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorHex",
                table: "Tags");
        }
    }
}
