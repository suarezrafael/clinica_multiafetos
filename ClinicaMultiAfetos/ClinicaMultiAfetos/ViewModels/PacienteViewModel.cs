using ClinicaMultiAfetos.Models;

namespace ClinicaMultiAfetos.ViewModels
{
    public class PacienteViewModel
    {
        public Paciente Paciente { get; set; }
        public IEnumerable<DocumentoPaciente> DocumentosPaciente { get; set; }
    }
}
