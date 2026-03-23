using AspMarketplace.Web.Models;
using System.Linq.Expressions;

namespace AspMarketplace.Web.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    IQueryable<T> Query();
    IQueryable<T> QueryWithDeleted();
    Task AddAsync(T entity);
    void Update(T entity);
    void SoftDelete(T entity, string deletedBy);
    void HardDelete(T entity);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
}
