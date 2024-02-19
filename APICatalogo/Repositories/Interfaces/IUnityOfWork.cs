using APICatalogo.Models;

namespace APICatalogo.Repositories.Interfaces;

public interface IUnityOfWork
{
    IProdutoRepository ProdutoRepository { get; }
    ICategoriaRepository CategoriaRepository { get; }
    void Commit();
    void Dispose();
}
