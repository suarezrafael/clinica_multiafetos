namespace ClinicaMultiAfetos.Models
{
    public class Documento
    {
        public int DocumentoId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string TipoDocumento { get; set; }
        public string Extensao { get; set; }
    }
}
