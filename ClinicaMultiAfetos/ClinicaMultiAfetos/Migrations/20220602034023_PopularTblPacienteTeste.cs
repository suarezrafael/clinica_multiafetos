using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaMultiAfetos.Migrations
{
    public partial class PopularTblPacienteTeste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Pacientes(NomeCompleto, DataNascimento, QueixaInicial, Responsavel, Cpf, TelefoneContato) " +
                 "VALUES('Rafael Vieira Suarez',GETDATE(),'Queixa teste','Caroline Splett','01552764095','51999708050')");

            migrationBuilder.Sql("INSERT INTO Pacientes(NomeCompleto, DataNascimento, QueixaInicial, Responsavel, Cpf, TelefoneContato) " +
                 "VALUES('Natasha Splett Suarez',GETDATE(),'Queixa teste','Caroline Splett','01552764093','51999708050')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Pacientes");
        }
    }
}
