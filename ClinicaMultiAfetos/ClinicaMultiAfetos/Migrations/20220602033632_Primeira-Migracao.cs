using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaMultiAfetos.Migrations
{
    public partial class PrimeiraMigracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentosClinica",
                columns: table => new
                {
                    DocumentoClinicaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDocumentoFavorito = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosClinica", x => x.DocumentoClinicaId);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cid = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Plano = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    QueixaInicial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Responsavel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaiMae = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Rg = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TelefoneContato = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    EnderecoNumero = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    EnderecoCidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EnderecoCep = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    EnderecoUf = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.PacienteId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosPaciente",
                columns: table => new
                {
                    DocumentoPacienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentoUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NomeArquivo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosPaciente", x => x.DocumentoPacienteId);
                    table.ForeignKey(
                        name: "FK_DocumentosPaciente_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanosPaciente",
                columns: table => new
                {
                    PlanoPacienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomePlano = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsPlanoFavorito = table.Column<bool>(type: "bit", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosPaciente", x => x.PlanoPacienteId);
                    table.ForeignKey(
                        name: "FK_PlanosPaciente_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecibosPaciente",
                columns: table => new
                {
                    ReciboPacienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentoUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NomeArquivo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsReciboFavorito = table.Column<bool>(type: "bit", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecibosPaciente", x => x.ReciboPacienteId);
                    table.ForeignKey(
                        name: "FK_RecibosPaciente_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosPaciente_PacienteId",
                table: "DocumentosPaciente",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosPaciente_PacienteId",
                table: "PlanosPaciente",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_RecibosPaciente_PacienteId",
                table: "RecibosPaciente",
                column: "PacienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentosClinica");

            migrationBuilder.DropTable(
                name: "DocumentosPaciente");

            migrationBuilder.DropTable(
                name: "PlanosPaciente");

            migrationBuilder.DropTable(
                name: "RecibosPaciente");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
