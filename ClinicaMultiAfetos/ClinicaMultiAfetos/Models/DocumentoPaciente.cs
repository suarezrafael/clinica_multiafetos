using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMultiAfetos.Models
{
    [Table("DocumentosPaciente")]
    public class DocumentoPaciente
    {
        [Key]
        public int DocumentoPacienteId { get; set; }

        [Display(Name = "Caminho documento")]
        [StringLength(250, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string DocumentoUrl { get; set; }

        [Display(Name = "Nome do arquivo")]
        [StringLength(250, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string NomeArquivo { get; set; }

        // FK do paciente
        public int PacienteId { get; set; }
        public virtual Paciente Paciente { get; set; }

    }
}
