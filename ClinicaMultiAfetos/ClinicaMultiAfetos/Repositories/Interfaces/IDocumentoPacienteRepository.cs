using ClinicaMultiAfetos.Models;

namespace ClinicaMultiAfetos.Repositories.Interfaces
{
    public interface IDocumentoPacienteRepository
    {
        IEnumerable<DocumentoPaciente> DocumentosPaciente { get; }
        DocumentoPaciente GetDocumentoPacienteById(int documentoId);
    }
}
