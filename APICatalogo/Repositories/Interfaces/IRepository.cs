using System.Linq.Expressions;

namespace APICatalogo.Repositories.Interfaces;

public interface IRepository<T>
{
    // Cuidados para não violar o principio ISP
    // Ex: Valor de estoque => categoria não tem estoque

    IEnumerable<T> GetAll();

    // Aceita uma função lambda e retorna um booleano
    T? Get(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);

}
