using ClinicaMultiAfetos.Context;
using ClinicaMultiAfetos.Models;
using ClinicaMultiAfetos.Repositories.Interfaces;

namespace ClinicaMultiAfetos.Repositories
{
    public class DocumentoClinicaRepository : IDocumentoClinicaRepository
    {
        private readonly AppDbContext _context;
        public DocumentoClinicaRepository(AppDbContext contexto)
        {
            _context = contexto;
        }
        public IEnumerable<DocumentoClinica> DocumentosClinica => _context.DocumentosClinica;

        public IEnumerable<DocumentoClinica> DocumentosClinicaFavoritos => _context.DocumentosClinica.
                                   Where(l => l.IsDocumentoFavorito);

        public DocumentoClinica GetDocumentoClinicaById(int documentoClinicaId) => _context.DocumentosClinica
                    .FirstOrDefault(l => l.DocumentoClinicaId == documentoClinicaId);
    }
}
