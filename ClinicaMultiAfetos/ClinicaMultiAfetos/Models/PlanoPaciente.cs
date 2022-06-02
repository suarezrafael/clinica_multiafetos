using System.ComponentModel.DataAnnotations;

namespace ClinicaMultiAfetos.Models
{
    public class PlanoPaciente
    {
        [Key]
        public int PlanoPacienteId { get; set; }


        [Display(Name = "Nome do plano")]
        [StringLength(250, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string NomePlano { get; set; }

        [Display(Name = "Favorito?")]
        public bool IsPlanoFavorito { get; set; }

        // FK do paciente
        public int PacienteId { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
}
