namespace ClinicaMultiAfetos.Models
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string NomeCompleto { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }

    }
}
