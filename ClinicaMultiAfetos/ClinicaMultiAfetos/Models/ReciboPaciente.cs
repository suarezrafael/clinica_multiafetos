using System.ComponentModel.DataAnnotations;

namespace ClinicaMultiAfetos.Models
{
    public class ReciboPaciente
    {
        [Key]
        public int ReciboPacienteId { get; set; }

        [Display(Name = "Caminho documento")]
        [StringLength(250, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string DocumentoUrl { get; set; }

        [Display(Name = "Nome do arquivo")]
        [StringLength(250, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string NomeArquivo { get; set; }

        [Display(Name = "Favorito?")]
        public bool IsReciboFavorito { get; set; }

        // FK do paciente
        public int PacienteId { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
}
