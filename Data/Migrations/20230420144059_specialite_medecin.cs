using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP1examuml.Data.Migrations
{
    public partial class specialite_medecin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Specialite",
                table: "Medecin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialite",
                table: "Medecin");
        }
    }
}
