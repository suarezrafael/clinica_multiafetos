using ClinicaMultiAfetos.Context;
using ClinicaMultiAfetos.Models;
using ClinicaMultiAfetos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaMultiAfetos.Repositories
{
    public class DocumentoPacienteRepository : IDocumentoPacienteRepository
    {
        private readonly AppDbContext _context;
        public DocumentoPacienteRepository(AppDbContext contexto)
        {
            _context = contexto;
        }
        public IEnumerable<DocumentoPaciente> DocumentosPaciente => _context.DocumentosPaciente
            .Include(c => c.Paciente);

        public DocumentoPaciente GetDocumentoPacienteById(int documentoId) => _context.DocumentosPaciente
            .FirstOrDefault(l => l.DocumentoPacienteId == documentoId);
    }
}
