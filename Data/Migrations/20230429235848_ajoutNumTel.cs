using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP1examuml.Data.Migrations
{
    public partial class ajoutNumTel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Telephone",
                table: "RendezVous",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "RendezVous");
        }
    }
}
