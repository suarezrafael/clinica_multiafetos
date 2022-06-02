using ClinicaMultiAfetos.Models;

namespace ClinicaMultiAfetos.Repositories.Interfaces
{
    public interface IDocumentoClinicaRepository
    {
        IEnumerable<DocumentoClinica> DocumentosClinica { get; }
        IEnumerable<DocumentoClinica> DocumentosClinicaFavoritos { get; }
        DocumentoClinica GetDocumentoClinicaById(int documentoClinicaId);
    }
}
