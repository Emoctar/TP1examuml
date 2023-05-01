using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP1examuml.Data.Migrations
{
    public partial class exam1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Examen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnalyseId = table.Column<int>(type: "int", nullable: false),
                    ConsultationId = table.Column<int>(type: "int", nullable: false),
                    Resultat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateExamen = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateResultat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examen_Analyse_AnalyseId",
                        column: x => x.AnalyseId,
                        principalTable: "Analyse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Examen_Consultation_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examen_AnalyseId",
                table: "Examen",
                column: "AnalyseId");

            migrationBuilder.CreateIndex(
                name: "IX_Examen_ConsultationId",
                table: "Examen",
                column: "ConsultationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examen");
        }
    }
}
