using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoFornecedores.Models
{
    public class Fornecedor
    {
        [Key] 
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter 14 dígitos.")]
        public string CNPJ { get; set; } = null!;

        [Required(ErrorMessage = "O segmento é obrigatório.")]
        public string Segmento { get; set; } = null!;

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve ter 8 dígitos.")]
        public string CEP { get; set; } = null!;

        [StringLength(255, ErrorMessage = "O endereço deve ter no máximo 255 caracteres.")]
        public string Endereco { get; set; } = null!;

        public string? FotoNomeArquivo { get; set; } 

    }
}
