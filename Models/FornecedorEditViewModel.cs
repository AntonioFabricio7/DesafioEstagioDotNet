using System.ComponentModel.DataAnnotations;

namespace GestaoFornecedores.Models
{
    public class FornecedorEditViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [StringLength(14, MinimumLength = 14)]
        public string CNPJ { get; set; } = null!;

        [Required(ErrorMessage = "O segmento é obrigatório.")]
        public string Segmento { get; set; } = null!;

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(8, MinimumLength = 8)]
        public string CEP { get; set; } = null!;

        [StringLength(255)]
        public string Endereco { get; set; } = null!;

        [Display(Name = "Nova Foto")]
        public IFormFile? FotoArquivo { get; set; }

        public string? FotoNomeArquivoExistente { get; set; }
    }
}