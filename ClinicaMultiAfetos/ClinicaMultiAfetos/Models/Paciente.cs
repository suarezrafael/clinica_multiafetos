using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMultiAfetos.Models
{
    [Table("Pacientes")]
    public class Paciente
    {
        [Key]
        public int PacienteId { get; set; }
        [StringLength(100,ErrorMessage = "O tamanho máximo é 100 caracteres.")]
        [Required(ErrorMessage = "Campo Nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Campo Data de nascimento é obrigatório.")]
        public DateTime DataNascimento { get; set; }

        [StringLength(10, ErrorMessage = "O tamanho máximo é 10 caracteres.")]
        [Display(Name = "CID")]
        public string Cid { get; set; }

        [StringLength(30, ErrorMessage = "O tamanho máximo é 30 caracteres.")]
        public string Plano { get; set; }

        [Required(ErrorMessage = "Campo Queixa Inicial é obrigatório.")]
        [Display(Name = "Queixa Inicial")]
        public string QueixaInicial { get; set; }

        [Required(ErrorMessage = "Campo Responsável é obrigatório.")]
        [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres.")]
        [Display(Name = "Responsável")]
        public string Responsavel { get; set; }

        [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres.")]
        [Display(Name = "Pai / Mãe")]
        public string PaiMae { get; set; }

        [Required(ErrorMessage = "Campo CPF é obrigatório.")]
        [StringLength(11, ErrorMessage = "O tamanho máximo é 11 caracteres.")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [StringLength(10, ErrorMessage = "O tamanho máximo é 10 caracteres.")]
        [Display(Name = "RG")]
        public string Rg { get; set; }

        [Required(ErrorMessage = "Campo Telefone de Contato é obrigatório.")]
        [StringLength(11, ErrorMessage = "O tamanho máximo são 11 caracteres.")]
        [Display(Name = "Telefone de Contato")]
        public string TelefoneContato { get; set; }

        [StringLength(150, ErrorMessage = "O tamanho máximo são 150 caracteres.")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [StringLength(15, ErrorMessage = "O tamanho máximo são 15 caracteres.")]
        [Display(Name = "Número")]
        public string EnderecoNumero { get; set; }

        [StringLength(50, ErrorMessage = "O tamanho máximo são 50 caracteres.")]
        [Display(Name = "Cidade")]
        public string EnderecoCidade { get; set; }

        [StringLength(8, ErrorMessage = "O tamanho máximo são 8 caracteres.")]
        [Display(Name = "CEP")]
        public string EnderecoCep { get; set; }

        [StringLength(2, ErrorMessage = "O tamanho máximo é 2 caracteres.")]
        [Display(Name = "UF")]
        public string EnderecoUf { get; set; }

        public List<ReciboPaciente> RecibosPaciente { get; set; }
        public List<PlanoPaciente> PlanosPaciente { get; set; }
        public List<DocumentoPaciente> DocumentosPaciente { get; set; }
    }
}
