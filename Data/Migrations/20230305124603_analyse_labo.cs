using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP1examuml.Data.Migrations
{
    public partial class analyse_labo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Analyse");

            migrationBuilder.AddColumn<int>(
                name: "LaboratoireId",
                table: "Analyse",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LaboratoireId",
                table: "Analyse");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Analyse",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
