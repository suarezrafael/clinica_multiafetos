using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaMultiAfetos.Migrations
{
    public partial class NovaColunaPaciente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DadosAdicionais",
                table: "Pacientes",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DadosAdicionais",
                table: "Pacientes");
        }
    }
}
