using System.Collections.ObjectModel;

namespace APICatalogo.Models;

public class Categoria
{
    public Categoria() 
    {
        Produtos = new Collection<Produto>();
    }

    public int CategoriaId { get; set; }
    public string? Nome { get; set; }
    public string? ImagemUrl { get; set; }

    // Uma categoria possui uma coleção de produtos
    public ICollection<Produto> Produtos { get; set; }
}
