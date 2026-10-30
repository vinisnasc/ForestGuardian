using ForestGuardian.Domain.Entities;
using System.Linq.Expressions;

namespace ForestGuardian.Domain.Interfaces.Infra;

public interface IBaseRepository<T> : IDisposable where T : BaseEntity
{
    Task<List<T>> SelecionarTudo();
    Task<T> Buscar(Expression<Func<T, bool>> predicate);
    Task<List<T>> BuscarVarios(Expression<Func<T, bool>> predicate);
    Task<T> SelecionarPorId(Guid id);

    void Incluir(T entity);
    void Alterar(T entity);
    void Remover(T entity);

    Task<int> SaveChanges();
}