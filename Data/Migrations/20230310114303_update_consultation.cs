using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP1examuml.Data.Migrations
{
    public partial class update_consultation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultation_Analyse_AnalyseId",
                table: "Consultation");

            migrationBuilder.DropForeignKey(
                name: "FK_Laboratoire_Laboratoire_LaboratoireId",
                table: "Laboratoire");

            migrationBuilder.DropIndex(
                name: "IX_Laboratoire_LaboratoireId",
                table: "Laboratoire");

            //migrationBuilder.DropIndex(
            //    name: "IX_Consultation_AnalyseId",
            //    table: "Consultation");

            migrationBuilder.DropColumn(
                name: "LaboratoireId",
                table: "Laboratoire");

            migrationBuilder.DropColumn(
                name: "AnalyseId",
                table: "Consultation");

            migrationBuilder.AlterColumn<string>(
                name: "Resultat",
                table: "Examen",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Analyse_LaboratoireId",
                table: "Analyse",
                column: "LaboratoireId");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyse_Laboratoire_LaboratoireId",
                table: "Analyse",
                column: "LaboratoireId",
                principalTable: "Laboratoire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyse_Laboratoire_LaboratoireId",
                table: "Analyse");

            migrationBuilder.DropIndex(
                name: "IX_Analyse_LaboratoireId",
                table: "Analyse");

            migrationBuilder.AddColumn<int>(
                name: "LaboratoireId",
                table: "Laboratoire",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Resultat",
                table: "Examen",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnalyseId",
                table: "Consultation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Laboratoire_LaboratoireId",
                table: "Laboratoire",
                column: "LaboratoireId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_AnalyseId",
                table: "Consultation",
                column: "AnalyseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_Analyse_AnalyseId",
                table: "Consultation",
                column: "AnalyseId",
                principalTable: "Analyse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Laboratoire_Laboratoire_LaboratoireId",
                table: "Laboratoire",
                column: "LaboratoireId",
                principalTable: "Laboratoire",
                principalColumn: "Id");
        }
    }
}
