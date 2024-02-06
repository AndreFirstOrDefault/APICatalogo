namespace APICatalogo.Models;

public class Produto
{
    public int ProdutoId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }

    // Mapeia para a chave estrangeira
    public int CategoriaId { get; set; }

    // Propriedade de navegação para indicar que um produto possui uma categoria
    public Categoria? Categoria { get; set; }
}
