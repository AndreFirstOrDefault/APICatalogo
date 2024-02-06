namespace APICatalogo.Models;

public class Categoria
{
    public int CategoriaId { get; set; }
    public string? Nome { get; set; }
    public string? ImagemUrl { get; set; }

    // Uma categoria possui uma coleção de produtos
    public ICollection<Produto> Produtos { get; set; }
}
