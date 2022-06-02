using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMultiAfetos.Models
{
    [Table("DocumentosClinica")]
    public class DocumentoClinica
    {
        [Key]
        public int DocumentoClinicaId { get; set; }

        [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres.")]
        [Required(ErrorMessage = "Campo Descrição é obrigatório.")]
        [Display(Name = "Descricao")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo Data Criação é obrigatório.")]
        public DateTime DataCriacao { get; set; }

        [Display(Name = "Favorito?")]
        public bool IsDocumentoFavorito { get; set; }
    }
}
