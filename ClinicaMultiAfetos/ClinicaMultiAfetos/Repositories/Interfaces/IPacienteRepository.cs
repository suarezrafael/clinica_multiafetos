using ClinicaMultiAfetos.Models;

namespace ClinicaMultiAfetos.Repositories.Interfaces
{
    public interface IPacienteRepository
    {
        IEnumerable<Paciente> Pacientes { get; }
        Paciente GetPacienteById(int pacienteId);
        Paciente GetPacienteByCpf(string cpf);
        Paciente GetPacienteByRg(string rg);

    }
}
