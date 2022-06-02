using ClinicaMultiAfetos.Models;

namespace ClinicaMultiAfetos.Repositories.Interfaces
{
    public interface IPlanoPacienteRepository
    {
        IEnumerable<PlanoPaciente> Planos { get; }
        PlanoPaciente GetPlanoById(int planoId);
    }
}
