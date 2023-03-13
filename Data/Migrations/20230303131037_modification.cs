using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP1examuml.Data.Migrations
{
    public partial class modification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeConsultationId",
                table: "TypeConsultation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeConsultation_TypeConsultationId",
                table: "TypeConsultation",
                column: "TypeConsultationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeConsultation_TypeConsultation_TypeConsultationId",
                table: "TypeConsultation",
                column: "TypeConsultationId",
                principalTable: "TypeConsultation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeConsultation_TypeConsultation_TypeConsultationId",
                table: "TypeConsultation");

            migrationBuilder.DropIndex(
                name: "IX_TypeConsultation_TypeConsultationId",
                table: "TypeConsultation");

            migrationBuilder.DropColumn(
                name: "TypeConsultationId",
                table: "TypeConsultation");
        }
    }
}
