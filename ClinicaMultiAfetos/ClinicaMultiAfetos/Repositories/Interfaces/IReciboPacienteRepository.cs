using ClinicaMultiAfetos.Models;

namespace ClinicaMultiAfetos.Repositories.Interfaces
{
    public interface IReciboPacienteRepository
    {
        IEnumerable<ReciboPaciente> Recibos { get; }
        ReciboPaciente GetReciboById(int reciboId);
    }
}
