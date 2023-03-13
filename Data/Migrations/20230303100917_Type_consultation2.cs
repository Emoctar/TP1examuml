using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP1examuml.Data.Migrations
{
    public partial class Type_consultation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnalyseId",
                table: "Consultation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Analyse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prix = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Laboratoire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaboratoireId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratoire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laboratoire_Laboratoire_LaboratoireId",
                        column: x => x.LaboratoireId,
                        principalTable: "Laboratoire",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_AnalyseId",
                table: "Consultation",
                column: "AnalyseId");

            migrationBuilder.CreateIndex(
                name: "IX_Laboratoire_LaboratoireId",
                table: "Laboratoire",
                column: "LaboratoireId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_Analyse_AnalyseId",
                table: "Consultation",
                column: "AnalyseId",
                principalTable: "Analyse",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultation_Analyse_AnalyseId",
                table: "Consultation");

            migrationBuilder.DropTable(
                name: "Analyse");

            migrationBuilder.DropTable(
                name: "Laboratoire");

            migrationBuilder.DropIndex(
                name: "IX_Consultation_AnalyseId",
                table: "Consultation");

            migrationBuilder.DropColumn(
                name: "AnalyseId",
                table: "Consultation");
        }
    }
}
