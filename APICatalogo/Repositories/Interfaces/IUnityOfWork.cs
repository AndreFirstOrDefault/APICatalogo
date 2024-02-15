namespace APICatalogo.Repositories.Interfaces;

public interface IUnityOfWork
{
    //IRepository<Produto> ProdutoRepository { get; }
    //IRepository<Categoria> CategoriaRepository { get; }

    IProdutoRepository ProdutoRepository { get; }
    ICategoriaRepository CategoriaRepository { get; }
    void Commit();

}
