using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class PeliculasActores_Personaje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Personaje",
                table: "PeliculasActores",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Personaje",
                table: "PeliculasActores");
        }
    }
}
