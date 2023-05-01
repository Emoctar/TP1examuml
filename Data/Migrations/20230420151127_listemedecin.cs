using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP1examuml.Data.Migrations
{
    public partial class listemedecin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Medecin",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medecin_PatientId",
                table: "Medecin",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medecin_Patient_PatientId",
                table: "Medecin",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medecin_Patient_PatientId",
                table: "Medecin");

            migrationBuilder.DropIndex(
                name: "IX_Medecin_PatientId",
                table: "Medecin");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Medecin");
        }
    }
}
