using APICatalogo.Models;

namespace APICatalogo.Repositories.Interfaces;

public interface ICategoriaRepository
{
    IEnumerable<Categoria> GetCategorias();
    Categoria GetCategoria(int id);
    Categoria Create(Categoria categoria);
    Categoria Update(int id, Categoria categoria);
    Categoria Delete(int id);
}
