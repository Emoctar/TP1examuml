using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP1examuml.Data.Migrations
{
    public partial class analyseconsul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Consultation_Analyse_AnalyseId",
            //    table: "Consultation");

            migrationBuilder.AddColumn<int>(
                name: "AnalyseId",
                table: "Consultation",
                type: "int",
                nullable: false
                );

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_Analyse_AnalyseId",
                table: "Consultation",
                column: "AnalyseId",
                principalTable: "Analyse",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Consultation_Analyse_AnalyseId",
            //    table: "Consultation");

            migrationBuilder.AlterColumn<int>(
                name: "AnalyseId",
                table: "Consultation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_Analyse_AnalyseId",
                table: "Consultation",
                column: "AnalyseId",
                principalTable: "Analyse",
                principalColumn: "Id");
        }
    }
}
