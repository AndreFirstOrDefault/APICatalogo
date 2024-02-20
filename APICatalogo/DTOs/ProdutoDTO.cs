using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;

public class ProdutoDTO
{
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 20 caracteres")]
    //[PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "O nome deve ter no maximo {1} caracteres")]
    public string? Descricao { get; set; }
        
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300, MinimumLength = 10)]
    public string? ImagemUrl { get; set; }

    // Mapeia para a chave estrangeira
    public int CategoriaId { get; set; }

}
